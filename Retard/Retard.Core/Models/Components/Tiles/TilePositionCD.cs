using Microsoft.Xna.Framework;

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
        internal Vector2 Value;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="value">La position de la case sur la carte</param>
        internal TilePositionCD(Vector2 value)
        {
            this.Value = value;
        }

        #endregion
    }
}
