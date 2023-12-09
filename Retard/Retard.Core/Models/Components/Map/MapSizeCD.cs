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
        /// La taille de la carte sur l'axe X
        /// </summary>
        internal int SizeX;

        /// <summary>
        /// La taille de la carte sur l'axe Y
        /// </summary>
        internal int SizeY;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="sizeX">La taille de la carte sur l'axe X</param>
        /// <param name="sizeY">La taille de la carte sur l'axe Y</param>
        internal MapSizeCD(int sizeX, int sizeY)
        {
            this.SizeX = sizeX;
            this.SizeY = sizeY;
        }

        #endregion
    }
}
