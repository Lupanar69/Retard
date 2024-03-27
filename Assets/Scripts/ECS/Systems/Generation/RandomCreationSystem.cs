using Assets.Scripts.ECS.Components;
using Assets.Scripts.ECS.Entities;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.ECS.Systems.Generation
{
    /// <summary>
    /// Crée les entités des randoms utilisés
    /// pour la génération procédurale
    /// </summary>
    [BurstCompile]
    public partial struct RandomCreationSystem : ISystem
    {
        #region Variables d'instance

        /// <summary>
        /// L'EntityManager de ce système
        /// </summary>
        private EntityManager _em;

        /// <summary>
        /// L'archétype du random de génération
        /// </summary>
        private EntityArchetype _mapGenRandomArchetype;

        #endregion

        #region Fonctions Unity

        /// <summary>
        /// Quand cr��
        /// </summary>
        /// <param name="state">Le syst�me</param>
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            this._em = state.EntityManager;

            NativeArray<ComponentType> types = new(1, Allocator.Temp);
            types[0] = ComponentType.ReadWrite<MapGenRandomCD>();

            this._mapGenRandomArchetype = this._em.CreateArchetype(types);

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
            if (Input.GetKeyDown(KeyCode.Z))
            {
                // Crée une nouvelle entité représentant
                // l'aléa de génération de la carte

                EntityQuery mapRandQ = SystemAPI.QueryBuilder().WithAll<MapGenRandomCD>().Build();
                EntityFactory.DestroyEntitiesOfType(ref this._em, ref mapRandQ);

                uint seed = 1;//(uint)math.ceil(SystemAPI.Time.ElapsedTime);
                EntityFactory.CreateMapGenRandomEntity(ref this._em, seed, this._mapGenRandomArchetype, out _);
            }
        }

        #endregion

        #region Fonctions priv�es

        #endregion
    }
}