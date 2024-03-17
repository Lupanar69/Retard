using Assets.Scripts.ECS.Components;
using Assets.Scripts.ECS.Entities;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.ECS.Systems.Generation
{
    /// <summary>
    /// Génère les entités des randoms utilisés
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
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                // Crée une nouvelle entité représentant
                // l'aléa de génération de la carte

                EntityQuery mapRandQ = SystemAPI.QueryBuilder().WithAll<MapGenRandomCD>().Build();
                EntityFactory.DestroyEntitiesOfType(ref this._em, ref mapRandQ);

                uint seed = (uint)math.ceil(SystemAPI.Time.ElapsedTime);
                EntityFactory.CreateMapGenRandomEntity(ref this._em, seed, out _);
            }
        }

        #endregion

        #region Fonctions priv�es

        #endregion
    }
}