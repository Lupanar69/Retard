using Assets.Scripts.ECS.Components;
using Assets.Scripts.ECS.Entities;
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
    /// Crée l'entité de la carte
    /// </summary>
    [BurstCompile]
    public partial struct MapCreationSystem : ISystem
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
            state.RequireForUpdate<MapGenSettingsMinMaxSizeCD>();
            this._em = state.EntityManager;

            NativeArray<ComponentType> types = new(2, Allocator.Temp);
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

                var ecb = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter();
                var mapGenRandRW = SystemAPI.GetSingletonRW<MapGenRandomCD>();
                Entity mapSettingsE = SystemAPI.GetSingletonEntity<MapGenSettingsMinMaxSizeCD>();
                var minMaxMapSize = SystemAPI.GetComponent<MapGenSettingsMinMaxSizeCD>(mapSettingsE);

                int2 size = new
                    (
                        mapGenRandRW.ValueRW.Value.NextInt(minMaxMapSize.Value.x, minMaxMapSize.Value.y),
                        mapGenRandRW.ValueRW.Value.NextInt(minMaxMapSize.Value.x, minMaxMapSize.Value.y)
                    );
                int length = size.x * size.y;

                // Génère l'entité de la carte

                EntityFactory.CreateMapEntity(ref this._em, in size, this._mapArchetype, out _);

                // Génère les entités des piles de cases

                new CreateTileStacksJob
                {
                    Ecb = ecb,
                    Size = size,
                    TileStackArchetype = this._tileStackArchetype,
                }
                .ScheduleParallel(length, 64, state.Dependency).Complete();
            }
        }

        #endregion
    }
}