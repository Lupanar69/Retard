using Unity.Collections;

namespace Assets.Scripts.Core.ViewModels.Generation
{
    /// <summary>
    /// Interface des algorithmes de génération de niveau
    /// </summary>
    public interface IMapGenerationAlgorithm
    {
        #region Fonctions publiques

        /// <summary>
        /// Génère les IDs des cases par défaut de la carte
        /// </summary>
        /// <param name="length">Le nombre total de cellules sur la carte</param>
        /// <param name="sizeX">La taille de la carte sur l'axe X</param>
        /// <param name="sizeY">La taille de la carte sur l'axe Y</param>
        /// <returns>Un NativeArray contenant les IDs des cases à créer(alloué avec Allocator.Temp)</returns>
        public NativeArray<int> Generate(int length, int sizeX, int sizeY);

        #endregion
    }
}