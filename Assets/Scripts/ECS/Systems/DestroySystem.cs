using Assets.Scripts.ECS.Entities;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.ECS.Systems
{
    /// <summary>
    /// Chargé de détruire les entités
    /// </summary>
    [BurstCompile]
    public partial struct DestroySystem : ISystem
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
        /// Quand m�j
        /// </summary>
        /// <param name="state">Le syst�me</param>
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                EntityFactory.DestroyAllEntities(ref this._em);
            }
        }

        #endregion
    }
}