using Assets.Scripts.ECS.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.ECS.Entities
{

    /// <summary>
    /// Contient toutes les méthodes de création d'entités
    /// </summary>
    [BurstCompile]
    public static class EntityFactory
    {
        #region Fonctions statiques publiques

        #region Destroy

        /// <summary>
        /// Détruit toutes les entités d'un certain type
        /// </summary>
        /// <param name="em">Un EntityManager</param>
        /// <param name="q">Les types des entités à détruire</param>
        [BurstCompile]
        public static void DestroyEntitiesOfType(ref EntityManager em, ref EntityQuery q)
        {
            em.DestroyEntity(q);
        }

        /// <summary>
        /// Détruit toutes les entités créées
        /// </summary>
        /// <param name="em">Un EntityManager</param>
        [BurstDiscard]
        public static void DestroyAllEntities(ref EntityManager em)
        {
            em.DestroyAndResetAllEntities();
        }

        #endregion

        #region Map

        /// <summary>
        /// Crée un aléa pour la génération de la carte
        /// </summary>
        /// <param name="em">Un EntityManager</param>
        /// <param name="seed">Le point de départ de l'aléa</param>
        /// <param name="entity">L'entité de l'aléa</param>
        [BurstCompile]
        public static void CreateMapGenRandomEntity(ref EntityManager em, uint seed, out Entity entity)
        {
            entity = em.CreateEntity();
            em.AddComponentData(entity, new MapGenRandomCD { Value = new Random(seed) });
        }

        #endregion

        #endregion
    }
}