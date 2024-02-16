using Retard.Core.Models.ValueTypes;

namespace Retard.Core.Models.Components.Tiles
{
    /// <summary>
    /// La position d'une case sur la carte
    /// </summary>
    internal sealed class TilePositionCD
    {
        #region Variables d'instance

        /// <summary>
        /// La position de la case sur la carte
        /// </summary>
        internal int2 Value;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="value">La position de la case sur la carte</param>
        internal TilePositionCD(int2 value)
        {
            this.Value = value;
        }

        #endregion
    }
}
