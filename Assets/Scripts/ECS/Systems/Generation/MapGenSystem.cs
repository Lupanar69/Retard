using Assets.Scripts.ECS.Components;
using Assets.Scripts.ECS.Entities;
using Assets.Scripts.ECS.Jobs.Generation;
using Unity.Burst;
using Unity.Burst.CompilerServices;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.ECS.Systems.Generation
{
    /// <summary>
    /// Génère l'entité de la carte
    /// </summary>
    [BurstCompile]
    public partial struct MapGenSystem : ISystem, ISystemStartStop
    {
        #region Variables d'instance

        /// <summary>
        /// L'EntityManager de ce système
        /// </summary>
        private EntityManager _em;

        /// <summary>
        /// L'archétype de la carte
        /// </summary>
        private EntityArchetype _mapArchetype;

        /// <summary>
        /// L'archétype des entités des cases
        /// </summary>
        private EntityArchetype _tileArchetype;

        /// <summary>
        /// L'archétype des entités des piles de cases
        /// </summary>
        private EntityArchetype _tileStackArchetype;

        #endregion

        #region Fonctions Unity

        /// <summary>
        /// Quand cr��
        /// </summary>
        /// <param name="state">Le syst�me</param>
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<MapGenRandomCD>();
            this._em = state.EntityManager;

            NativeArray<ComponentType> types = new(3, Allocator.Temp);
            types[0] = ComponentType.ReadWrite<TileTag>();
            types[1] = ComponentType.ReadWrite<TilePositionCD>();
            types[2] = ComponentType.ReadWrite<TileIdCD>();

            this._tileArchetype = this._em.CreateArchetype(types);

            types = new(2, Allocator.Temp);
            types[0] = ComponentType.ReadWrite<TileStackPositionCD>();
            types[1] = ComponentType.ReadWrite<TileEntityInStackBE>();

            this._tileStackArchetype = this._em.CreateArchetype(types);

            types[0] = ComponentType.ReadWrite<MapTag>();
            types[1] = ComponentType.ReadWrite<MapSizeCD>();

            this._mapArchetype = this._em.CreateArchetype(types);
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
        /// Quand r�activ�
        /// </summary>
        /// <param name="state">Le syst�me</param>
        [BurstCompile]
        public void OnStartRunning(ref SystemState state)
        {

        }

        /// <summary>
        /// Quand d�sactiv�
        /// </summary>
        /// <param name="state">Le syst�me</param>
        [BurstCompile]
        public void OnStopRunning(ref SystemState state)
        {

        }

        /// <summary>
        /// Quand m�j
        /// </summary>
        /// <param name="state">Le syst�me</param>
        [BurstCompile, SkipLocalsInit]
        public void OnUpdate(ref SystemState state)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Nettoie la carte précédente

                EntityQuery mapQ = SystemAPI.QueryBuilder().WithAny<MapTag, TileStackPositionCD>().Build();
                EntityFactory.DestroyEntitiesOfType(ref this._em, ref mapQ);

                // Paramètres de la carte

                var ecbTiles = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter();
                var mapGenRandRW = SystemAPI.GetSingletonRW<MapGenRandomCD>();
                Entity mapSettingsE = SystemAPI.GetSingletonEntity<MapGenSettingsMinMaxSizeCD>();
                var minMaxMapSize = SystemAPI.GetComponent<MapGenSettingsMinMaxSizeCD>(mapSettingsE);
                var mapAlgorithms = SystemAPI.GetBuffer<MapGenSettingsAlgorithmIDBE>(mapSettingsE);

                if (mapAlgorithms.Length == 0)
                    return;

                int2 size = new
                    (
                        mapGenRandRW.ValueRW.Value.NextInt(minMaxMapSize.Value.x, minMaxMapSize.Value.y),
                        mapGenRandRW.ValueRW.Value.NextInt(minMaxMapSize.Value.x, minMaxMapSize.Value.y)
                    );
                int length = size.x * size.y;
                int mapGenIndex = mapAlgorithms[mapGenRandRW.ValueRW.Value.NextInt(0, mapAlgorithms.Length)].Value;

                // Génère l'entité de la carte

                EntityFactory.CreateMapEntity(ref this._em, in size, this._mapArchetype, out _);

                // Génère les entités des piles de cases

                new CreateTileStacksJob
                {
                    Ecb = ecbTiles,
                    Size = size,
                    TileStackArchetype = this._tileStackArchetype,
                }
                .ScheduleParallel(length, 64, state.Dependency).Complete();

                #region Génère les IDs des types de cases à créer

                //// Liste de tous les IDs et leur positions
                //// (x: posX, y: posY, z: ID)

                //NativeArray<int3> tilesIDsResults = new(length, Allocator.TempJob);
                //JobHandle dependency = state.Dependency;

                //int4 mapSettings = new(mapGenIndex, length, sizeX, sizeY);
                //this.ScheduleMapGenerationAlgorithm(mapSettings, ref tilesIDsResults, ref state, out JobHandle mapGenHandle, in dependency);
                //mapGenHandle.Complete();

                ////int count = 0;
                ////StringBuilder sb = new(length + sizeY + 10);
                ////sb.AppendLine($"({sizeX},{sizeY})");

                ////for (int y = 0; y < sizeY; y++)
                ////{
                ////    for (int x = 0; x < sizeX; x++)
                ////    {
                ////        sb.Append(tilesIDsResults[count + x].z);
                ////    }

                ////    sb.AppendLine();
                ////    count += sizeX;
                ////}

                ////Debug.Log(sb.ToString());

                #endregion

                #region Crée les entités des cases puis de leurs piles

                //// On crée d'abord les cases

                //new CreateTilesJob
                //{
                //    Ecb = ecbTiles,
                //    TileArchetype = this._tileArchetype,
                //    TilesIDsRO = tilesIDsResults.AsReadOnly(),
                //}
                //.ScheduleParallel(tilesIDsResults.Length, 64, state.Dependency).Complete();

                #endregion

                #region Nettoyage

                //tilesIDsResults.Dispose();

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