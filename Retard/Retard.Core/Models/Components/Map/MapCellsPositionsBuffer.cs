using Microsoft.Xna.Framework;

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
        internal Vector2[] Value;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="length">Le nombre de cellules de la carte</param>
        internal MapCellsPositionsBuffer(int length)
        {
            this.Value = new Vector2[length];
        }

        #endregion
    }
}
