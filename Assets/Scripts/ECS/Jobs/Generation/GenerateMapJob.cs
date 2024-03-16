using Assets.Scripts.Core.ViewModels.Generation;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

namespace Assets.Scripts.ECS.Jobs.Generation
{

    /// <summary>
    /// Génère les IDs des cases par défaut de la carte
    /// </summary>
    [BurstCompile]
    public partial struct GenerateMapJob<T> : IJob where T : struct, IMapGenerationAlgorithm
    {
        #region Variables d'instance

        /// <summary>
        /// Le nombre total de cellules sur la carte
        /// </summary>
        public int Length;

        /// <summary>
        /// La taille de la carte sur l'axe X
        /// </summary>
        public int SizeX;

        /// <summary>
        /// La taille de la carte sur l'axe Y
        /// </summary>
        public int SizeY;

        /// <summary>
        /// Les IDs des cases à retourner
        /// </summary>
        [WriteOnly]
        public NativeArray<int> TilesIDsWO;

        #endregion

        #region Fonctions publiques

        /// <summary>
        /// Génère les IDs des cases par défaut de la carte
        /// </summary>
        [BurstCompile]
        public readonly void Execute()
        {
            T algorithm = new();
            NativeArray<int> tilesIDs = algorithm.Generate(Length, SizeX, SizeY);
            tilesIDs.CopyTo(TilesIDsWO);
        }

        #endregion
    }
}