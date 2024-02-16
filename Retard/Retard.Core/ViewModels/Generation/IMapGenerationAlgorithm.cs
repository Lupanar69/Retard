using Retard.Core.Models.Generation;
using Retard.Core.Models.ValueTypes;

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
        /// <param name="size">La taille de la carte</param>
        /// <param name="mapGenerationData">Contient les infos sur la carte générée</param>
        internal void Execute(int2 size, out MapGenerationData mapGenerationData);

        #endregion
    }
}
