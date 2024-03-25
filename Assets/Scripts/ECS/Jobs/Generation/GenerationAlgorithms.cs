using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;

namespace Assets.Scripts.ECS.Jobs.Generation
{
    /// <summary>
    /// Contient les algorithmes de génération du jeu
    /// </summary>
    [BurstCompile]
    public static class GenerationAlgorithms
    {
        #region Variables statiques

        /// <summary>
        /// Les IDs des algorithmes de génération de carte
        /// </summary>
        public static readonly Dictionary<int, FixedString64Bytes> _mapGenAlgorithms = new();

        #endregion

        #region Fonctions statiques publiques

        /// <summary>
        /// Récupère le nom de l'algorithme lié à cet ID
        /// </summary>
        /// <param name="mapGenIndex">L'ID de l'algorithme</param>
        /// <param name="key">Le nom de l'algorithme lié à cet ID</param>
        /// <returns>Le nom de l'algorithme lié à cet ID</returns>
        [BurstDiscard]
        public static void GetKeyFromMapGenAlgList(int mapGenIndex, out FixedString64Bytes key)
        {
            key = _mapGenAlgorithms[mapGenIndex];
        }

        #endregion
    }
}