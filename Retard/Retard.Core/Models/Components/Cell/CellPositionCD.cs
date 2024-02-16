using Retard.Core.Models.ValueTypes;

namespace Retard.Core.Models.Components.Cell
{
    /// <summary>
    /// La position d'une cellule sur la carte
    /// </summary>
    internal sealed class CellPositionCD
    {
        #region Variables d'instance

        /// <summary>
        /// La position de la cellule sur la carte
        /// </summary>
        internal int2 Value;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="value">La position de la cellule sur la carte</param>
        internal CellPositionCD(int2 value)
        {
            this.Value = value;
        }

        #endregion
    }
}
