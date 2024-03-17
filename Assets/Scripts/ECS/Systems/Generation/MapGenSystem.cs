using Assets.Scripts.ECS.Components;
using Assets.Scripts.ECS.Entities;
using Retard.ECS;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
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
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                EntityQuery mapQ = SystemAPI.QueryBuilder().WithAll<MapTag>().Build();
                EntityFactory.DestroyEntitiesOfType(ref this._em, ref mapQ);

                var mapGenRandRW = SystemAPI.GetSingletonRW<MapGenRandomCD>();
                Entity mapSettingsE = SystemAPI.GetSingletonEntity<MapGenSettingsMinMaxSizeCD>();
                var minMaxMapSize = SystemAPI.GetComponent<MapGenSettingsMinMaxSizeCD>(mapSettingsE);
                var mapAlgorithms = SystemAPI.GetBuffer<MapGenSettingsAlgorithmIDBE>(mapSettingsE);

                if (mapAlgorithms.Length == 0)
                    return;

                // Paramètres de la carte

                int sizeX = mapGenRandRW.ValueRW.Value.NextInt(minMaxMapSize.Value.x, minMaxMapSize.Value.y);
                int sizeY = mapGenRandRW.ValueRW.Value.NextInt(minMaxMapSize.Value.x, minMaxMapSize.Value.y);
                int length = sizeX * sizeY;
                int mapGenIndex = mapAlgorithms[mapGenRandRW.ValueRW.Value.NextInt(0, mapAlgorithms.Length)].Value;

                // Génère les IDs des types de cases à créer

                NativeArray<int> tilesIDsResults = new(length, Allocator.TempJob);
                JobHandle dependency = state.Dependency;

                GenerationAlgorithms.ScheduleMapGenerationAlgorithm(mapGenIndex, length, sizeX, sizeY, ref tilesIDsResults, out JobHandle mapGenHandle, in dependency);
                mapGenHandle.Complete();
                tilesIDsResults.Dispose();

                //Entity[] tiles = this.CreateMapTiles(tilesIDs, sizeX, sizeY, in this._atlas);
                //Entity[] cellEs = this.CreateCells(length, sizeX, sizeY, in tiles);
                //this.CreateMap(length, sizeX, sizeY, in cellEs);
            }
        }

        #endregion

        #region Fonctions priv�es

        #endregion
    }
}