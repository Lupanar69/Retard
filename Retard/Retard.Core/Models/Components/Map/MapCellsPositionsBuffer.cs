using Retard.Core.Models.ValueTypes;

namespace Retard.Core.Models.Components.Map
{
    /// <summary>
    /// La liste des positions des cellules de la carte
    /// </summary>
    internal sealed class MapCellsPositionsBuffer
    {
        #region Variables d'instance

        /// <summary>
        /// La liste des positions des cellules de la carte
        /// </summary>
        internal int2[] Value;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="length">Le nombre de cellules de la carte</param>
        internal MapCellsPositionsBuffer(int length)
        {
            this.Value = new int2[length];
        }

        #endregion
    }
}
