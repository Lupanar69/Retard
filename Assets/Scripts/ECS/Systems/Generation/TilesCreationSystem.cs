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

                var ecb = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter();
                var mapGenRandRW = SystemAPI.GetSingletonRW<MapGenRandomCD>();
                Entity mapE = SystemAPI.GetSingletonEntity<MapTag>();
                var mapSizeRO = SystemAPI.GetComponentRO<MapSizeCD>(mapE);
                Entity mapSettingsE = SystemAPI.GetSingletonEntity<MapGenSettingsMinMaxSizeCD>();
                var mapAlgorithms = SystemAPI.GetBuffer<MapGenSettingsAlgorithmIDBE>(mapSettingsE);

                #region Génère les IDs des types de cases à créer

                int2 size = new(mapSizeRO.ValueRO.Value.x, mapSizeRO.ValueRO.Value.y);
                int length = size.x * size.y;
                int mapGenIndex = mapAlgorithms[mapGenRandRW.ValueRW.Value.NextInt(0, mapAlgorithms.Length)].Value;
                int4 mapSettings = new(mapGenIndex, length, size.x, size.y);

                // Liste de tous les IDs et leur positions
                // (x: posX, y: posY, z: ID)

                NativeArray<int3> tilesIDsResults = new(length, Allocator.TempJob);
                JobHandle dependency = state.Dependency;

                this.ScheduleMapGenerationAlgorithm(mapSettings, ref tilesIDsResults, ref state, out JobHandle mapGenHandle, in dependency);
                mapGenHandle.Complete();

                //int count = 0;
                //StringBuilder sb = new(length + size.y + 10);
                //sb.AppendLine($"({size.x},{size.y})");

                //for (int y = 0; y < size.y; y++)
                //{
                //    for (int x = 0; x < size.x; x++)
                //    {
                //        sb.Append(tilesIDsResults[count + x].z);
                //    }

                //    sb.AppendLine();
                //    count += size.x;
                //}

                //Debug.Log(sb.ToString());

                #endregion

                #region Crée les entités des cases puis de leurs piles

                // On crée d'abord les cases

                new CreateTilesJob
                {
                    Ecb = ecb,
                    TileArchetype = this._tileArchetype,
                    TilesIDsRO = tilesIDsResults.AsReadOnly(),
                }
                .ScheduleParallel(tilesIDsResults.Length, 64, state.Dependency).Complete();

                #endregion

                #region Nettoyage

                tilesIDsResults.Dispose();

                #endregion
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
            (int4 mapSettings, ref NativeArray<int3> tilesIDsResults, ref SystemState state,
            [NoAlias] out JobHandle handle, [NoAlias] in JobHandle dependsOn = default)
        {
            int mapGenIndex = mapSettings.x;
            int length = mapSettings.y;
            int sizeX = mapSettings.z;
            int sizeY = mapSettings.w;
            GenerationAlgorithms.GetKeyFromMapGenAlgList(mapGenIndex, out FixedString64Bytes key);

            handle = key switch
            {
                var value when value == "OneRoom" => new GenerateOneRoomMapJob
                {
                    Length = length,
                    SizeX = sizeX,
                    SizeY = sizeY,
                    TilesIDsWO = tilesIDsResults,
                }
                .Schedule(dependsOn),

                var value when value == "Null" => new GenerateNullMapJob
                {
                    Length = length,
                    TilesIDsWO = tilesIDsResults,
                }
                .Schedule(dependsOn),

                _ => new GenerateNullMapJob
                {
                    Length = length,
                    TilesIDsWO = tilesIDsResults,
                }
                .Schedule(dependsOn),
            };
        }

        #endregion
    }
}