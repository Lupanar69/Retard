using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;

namespace Assets.Scripts.ECS.Jobs.Generation
{
    /// <summary>
    /// Contient les algorithmes de g�n�ration du jeu
    /// </summary>
    [BurstCompile]
    public static class GenerationAlgorithms
    {
        #region Variables statiques

        /// <summary>
        /// Les IDs des algorithmes de g�n�ration de carte
        /// </summary>
        public static readonly Dictionary<int, FixedString64Bytes> _mapGenAlgorithms = new();

        #endregion

        #region Fonctions statiques publiques

        /// <summary>
        /// R�cup�re le nom de l'algorithme li� � cet ID
        /// </summary>
        /// <param name="mapGenIndex">L'ID de l'algorithme</param>
        /// <param name="key">Le nom de l'algorithme li� � cet ID</param>
        [BurstDiscard]
        public static void GetKeyFromMapGenAlgList(int mapGenIndex, out FixedString64Bytes key)
        {
            key = GenerationAlgorithms._mapGenAlgorithms[mapGenIndex];
        }

        #endregion
    }
}