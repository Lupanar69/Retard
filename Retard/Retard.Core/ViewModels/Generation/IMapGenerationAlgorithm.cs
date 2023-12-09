namespace Retard.Core.ViewModels.Generation
{
    /// <summary>
    /// Interface des algorithmes de génération de niveau
    /// </summary>
    internal interface IMapGenerationAlgorithm
    {
        #region Fonctions internes

        /// <summary>
        /// Génère un nouveau niveau selon l'algorithme implémenté
        /// </summary>
        /// <param name="length">Le nombre total de cellules sur la carte</param>
        /// <param name="sizeX">La taille de la carte sur l'axe X</param>
        /// <param name="sizeY">La taille de la carte sur l'axe Y</param>
        /// <returns>Les IDs de toutes les cases à instancier par cellule</returns>
        internal int[] Execute(int length, int sizeX, int sizeY);

        #endregion
    }
}
