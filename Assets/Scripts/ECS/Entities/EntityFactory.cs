using System.Runtime.CompilerServices;
using Assets.Scripts.ECS.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.ECS.Entities
{

    /// <summary>
    /// Contient toutes les m�thodes de cr�ation d'entit�s
    /// </summary>
    [BurstCompile]
    public static class EntityFactory
    {
        #region Fonctions statiques publiques

        #region Destroy

        /// <summary>
        /// D�truit toutes les entit�s d'un certain type
        /// </summary>
        /// <param name="em">Un EntityManager</param>
        /// <param name="q">Les types des entit�s � d�truire</param>
        [BurstCompile]
        public static void DestroyEntitiesOfType(ref EntityManager em, ref EntityQuery q)
        {
            em.DestroyEntity(q);
        }

        /// <summary>
        /// D�truit toutes les entit�s cr��es
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
        /// Cr�e l'entit� de l'al�a pour la g�n�ration de la carte
        /// </summary>
        /// <param name="em">Un EntityManager</param>
        /// <param name="seed">Le point de d�part de l'al�a</param>
        /// <param name="archetype">L'arch�type de l'entit�</param>
        /// <param name="entity">L'entit� de l'al�a</param>
        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateMapGenRandomEntity(ref EntityManager em, uint seed, EntityArchetype archetype, out Entity entity)
        {
            entity = em.CreateEntity(archetype);
            em.SetName(entity, "Map Gen Random Entity");
            em.SetComponentData(entity, new MapGenRandomCD { Value = new Random(seed) });
        }

        /// <summary>
        /// Cr�e l'entit� de la carte
        /// </summary>
        /// <param name="em">Un EntityManager</param>
        /// <param name="size">La taille de la carte</param>
        /// <param name="archetype">L'arch�type de l'entit�</param>
        /// <param name="entity">L'entit� de l'al�a</param>
        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateMapEntity(ref EntityManager em, in int2 size, EntityArchetype archetype, out Entity entity)
        {
            entity = em.CreateEntity(archetype);
            em.SetName(entity, "Map Entity");
            em.SetComponentData(entity, new MapSizeCD { Value = size });
        }

        /// <summary>
        /// Cr�e les entit�s des piles de cases pour chaque cellule de la carte
        /// </summary>
        /// <param name="sortKey">Pour trier les entit�s</param>
        /// <param name="ecb">Un EntityCommandBuffer</param>
        /// <param name="size">La taille de la carte</param>
        /// <param name="archetype">L'arch�type de l'entit�</param>
        /// <param name="entity">L'entit� de l'al�a</param>
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