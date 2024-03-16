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
    public partial struct MapCreationSystem : ISystem, ISystemStartStop
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
                EntityFactory.DestroyEntitiesOfType(ref _em, ref mapQ);

                var mapGenRandRW = SystemAPI.GetSingletonRW<MapGenRandomCD>();
                Entity mapSettingsE = SystemAPI.GetSingletonEntity<MapGenerationSettingsMinMaxSizeCD>();
                var minMaxMapSize = SystemAPI.GetComponent<MapGenerationSettingsMinMaxSizeCD>(mapSettingsE);
                var mapAlgorithms = SystemAPI.GetBuffer<MapGenerationSettingsAlgorithmIDBE>(mapSettingsE);

                if (mapAlgorithms.Length == 0)
                    return;

                // Paramètres de la carte

                int sizeX = mapGenRandRW.ValueRW.Value.NextInt(minMaxMapSize.Value.x, minMaxMapSize.Value.y);
                int sizeY = mapGenRandRW.ValueRW.Value.NextInt(minMaxMapSize.Value.x, minMaxMapSize.Value.y);
                int length = sizeX * sizeY;
                int mapGenIndex = mapAlgorithms[mapGenRandRW.ValueRW.Value.NextInt(0, mapAlgorithms.Length)].Value;

                // Génère les IDs des types de cases à créer

                NativeArray<int> tilesIDsResults = new(length, Allocator.TempJob);

                GenerationAlgorithms.ScheduleMapGenerationAlgorithm(mapGenIndex, length, sizeX, sizeY, ref tilesIDsResults, out JobHandle mapGeHandle);
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