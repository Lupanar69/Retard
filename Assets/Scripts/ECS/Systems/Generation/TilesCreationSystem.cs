using Assets.Scripts.Core.Models;
using Assets.Scripts.Core.Models.Generation;
using Assets.Scripts.ECS.Components;
using Assets.Scripts.ECS.Entities;
using Assets.Scripts.ECS.Jobs.Generation;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Retard.ECS
{
    /// <summary>
    /// Génère les entités des cases
    /// </summary>
    [BurstCompile]
    public partial struct TilesCreationSystem : ISystem
    {
        #region Variables d'instance

        /// <summary>
        /// L'EntityManager de ce système
        /// </summary>
        private EntityManager _em;

        /// <summary>
        /// L'archétype des entités des cases
        /// </summary>
        private EntityArchetype _tileArchetype;

        #endregion

        #region Fonctions Unity

        /// <summary>
        /// Quand cr��
        /// </summary>
        /// <param name="state">Le syst�me</param>
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<MapTag>();
            state.RequireForUpdate<MapGenAlgorithmTag>();
            this._em = state.EntityManager;

            NativeArray<ComponentType> types = new(3, Allocator.Temp);
            types[0] = ComponentType.ReadWrite<TileTag>();
            types[1] = ComponentType.ReadWrite<TilePositionCD>();
            types[2] = ComponentType.ReadWrite<TileIdCD>();

            this._tileArchetype = this._em.CreateArchetype(types);
        }

        /// <summary>
        /// Quand d�truit
        /// </summary>
        /// <param name="state">Le syst�me</param>
        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }

        /// <summary>
        /// Quand m�j
        /// </summary>
        /// <param name="state">Le syst�me</param>
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                // Nettoie la carte précédente

                EntityQuery tileQ = SystemAPI.QueryBuilder().WithAny<TileTag>().Build();
                EntityFactory.DestroyEntitiesOfType(ref this._em, ref tileQ);

                // Paramètres de la carte

                var ecb1 = new EntityCommandBuffer(Allocator.TempJob);
                var ecb2 = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged);
                var mapGenRandRW = SystemAPI.GetSingletonRW<MapGenRandomCD>();
                Entity mapE = SystemAPI.GetSingletonEntity<MapTag>();
                var mapSizeRO = SystemAPI.GetComponentRO<MapSizeCD>(mapE);
                Entity mapSettingsE = SystemAPI.GetSingletonEntity<MapGenSettingsMinMaxSizeCD>();
                var mapAlgorithms = SystemAPI.GetBuffer<MapGenSettingsAlgorithmIDBE>(mapSettingsE);

                int2 size = new(mapSizeRO.ValueRO.Value.x, mapSizeRO.ValueRO.Value.y);
                int length = size.x * size.y;
                int mapGenIndex = mapAlgorithms[mapGenRandRW.ValueRW.Value.NextInt(0, mapAlgorithms.Length)].Value;
                int4 mapSettings = new(mapGenIndex, length, size.x, size.y);
                NativeArray<TilePosAndID> tilesIDsResults = new(length, Allocator.TempJob); // Liste de tous les IDs et leur positions (x: posX, y: posY, z: ID)
                JobHandle dependency = state.Dependency;

                // Génère les IDs des types de cases à créer

                this.ScheduleMapGenerationAlgorithm(mapSettings, ref tilesIDsResults, ref state, out JobHandle mapGenHandle, in dependency);

                // Vide les piles de cases

                JobHandle clearHandle = new ClearTerrainCellsJob().ScheduleParallel(mapGenHandle);

                // Crée les entités des cases

                new CreateTilesJob
                {
                    Ecb = ecb1.AsParallelWriter(),
                    TileArchetype = this._tileArchetype,
                    TilesIDsRO = tilesIDsResults.AsReadOnly(),
                }
                .ScheduleParallel(tilesIDsResults.Length, 64, clearHandle).Complete();

                ecb1.Playback(this._em);
                tilesIDsResults.Dispose();

                // Ajoute les cases aux cellules

                NativeArray<Entity> tileStacksEs = SystemAPI.QueryBuilder().WithAll<TileEntityInCellCD>().Build().ToEntityArray(Allocator.TempJob);

                new AddTerrainTilesToCellsJob
                {
                    SizeX = size.x,
                    Ecb = ecb2.AsParallelWriter(),
                    TileStacksEs = tileStacksEs.AsReadOnly(),
                }
                .ScheduleParallel(state.Dependency).Complete();

                tileStacksEs.Dispose();
            }
        }

        #endregion

        #region Fonctions privées

        /// <summary>
        /// Contient les algorithmes de génération du jeu
        /// </summary>
        /// <param name="mapSettings">Les paramètres de la carte</param>
        /// <param name="tilesIDsResults">Les IDs des cases à retourner</param>
        /// <param name="state">Le système</param>
        /// <param name="dependsOn">Le job précédent duquel il dépend</param>
        /// <param name="handle">Un job utilisant l'algorithme renseigné, lancé en parallèle</param>
        [BurstCompile]
        private void ScheduleMapGenerationAlgorithm
            (int4 mapSettings, ref NativeArray<TilePosAndID> tilesIDsResults, ref SystemState state,
            [NoAlias] out JobHandle handle, [NoAlias] in JobHandle dependsOn = default)
        {
            int mapGenIndex = mapSettings.x;
            int length = mapSettings.y;
            int sizeX = mapSettings.z;
            int sizeY = mapSettings.w;
            GenerationAlgorithms.GetKeyFromMapGenAlgList(mapGenIndex, out FixedString64Bytes key);

            handle = key switch
            {
                var value when value == Constants.ONEROOM_ALG_KEY => new GenerateOneRoomMapJob
                {
                    Length = length,
                    SizeX = sizeX,
                    SizeY = sizeY,
                    TilesIDsWO = tilesIDsResults,
                }
                .Schedule(dependsOn),
                _ => throw new System.Exception($"Erreur : Algorithme de génération \"{key}\" non référencé dans le dictionnaire."),
            };
        }

        #endregion
    }
}