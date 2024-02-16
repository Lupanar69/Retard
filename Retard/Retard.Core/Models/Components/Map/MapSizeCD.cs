using Retard.Core.Models.ValueTypes;

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
        internal int2 Size;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="sizeX">La taille de la carte sur l'axe X</param>
        /// <param name="sizeY">La taille de la carte sur l'axe Y</param>
        internal MapSizeCD(int sizeX, int sizeY)
        {
            this.Size.X = sizeX;
            this.Size.Y = sizeY;
        }

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="size">La taille de la carte</param>
        internal MapSizeCD(int2 size)
        {
            this.Size = size;
        }

        #endregion
    }
}
