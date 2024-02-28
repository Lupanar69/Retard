using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace Retard.ECS
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
                EntityFactory.DestroyEntitiesOfType(ref this._em, ref mapQ);

                RefRW<MapGenRandomCD> mapGenRandRW = SystemAPI.GetSingletonRW<MapGenRandomCD>();


            }
        }

        #endregion

        #region Fonctions priv�es

        #endregion
    }
}