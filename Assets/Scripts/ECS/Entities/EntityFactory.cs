using System.Runtime.CompilerServices;
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
        /// Crée l'entité de l'aléa pour la génération de la carte
        /// </summary>
        /// <param name="em">Un EntityManager</param>
        /// <param name="seed">Le point de départ de l'aléa</param>
        /// <param name="archetype">L'archétype de l'entité</param>
        /// <param name="entity">L'entité de l'aléa</param>
        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateMapGenRandomEntity(ref EntityManager em, uint seed, EntityArchetype archetype, out Entity entity)
        {
            entity = em.CreateEntity(archetype);
            em.SetName(entity, "Map Gen Random Entity");
            em.SetComponentData(entity, new MapGenRandomCD { Value = new Random(seed) });
        }

        /// <summary>
        /// Crée l'entité de la carte
        /// </summary>
        /// <param name="em">Un EntityManager</param>
        /// <param name="size">La taille de la carte</param>
        /// <param name="archetype">L'archétype de l'entité</param>
        /// <param name="entity">L'entité de l'aléa</param>
        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateMapEntity(ref EntityManager em, in int2 size, EntityArchetype archetype, out Entity entity)
        {
            entity = em.CreateEntity(archetype);
            em.SetName(entity, "Map Entity");
            em.SetComponentData(entity, new MapSizeCD { Value = size });
        }

        /// <summary>
        /// Crée les entités des piles de cases pour chaque cellule de la carte
        /// </summary>
        /// <param name="sortKey">Pour trier les entités</param>
        /// <param name="ecb">Un EntityCommandBuffer</param>
        /// <param name="size">La taille de la carte</param>
        /// <param name="archetype">L'archétype de l'entité</param>
        /// <param name="entity">L'entité de l'aléa</param>
        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateTileStackEntity(int sortKey, ref EntityCommandBuffer.ParallelWriter ecb, in int2 size, EntityArchetype archetype, out Entity entity)
        {
            entity = ecb.CreateEntity(sortKey, archetype);
            ecb.SetName(sortKey, entity, $"Tile Stack Entity #{sortKey}");
            ecb.SetComponent(sortKey, entity, new CellPositionCD { Value = new int2(sortKey % size.x, sortKey / size.y) });
        }

        #endregion

        #endregion
    }
}