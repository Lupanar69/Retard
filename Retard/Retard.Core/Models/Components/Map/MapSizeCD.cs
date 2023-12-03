using Microsoft.Xna.Framework;

namespace Retard.Core.Models.Components.Map
{
    /// <summary>
    /// Les dimensions de la carte
    /// </summary>
    internal sealed class MapSizeCD
    {
        #region Variables d'instance

        /// <summary>
        /// Les dimensions de la carte
        /// </summary>
        internal Vector2 Value;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="value">Les dimensions de la carte</param>
        internal MapSizeCD(Vector2 value)
        {
            this.Value = value;
        }

        #endregion
    }
}
