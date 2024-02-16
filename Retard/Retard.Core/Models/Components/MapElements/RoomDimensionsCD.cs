using Retard.Core.Models.ValueTypes;

namespace Retard.Core.Models.Components.MapElements
{
    /// <summary>
    /// Représente les dimensions d'une salle
    /// </summary>
    internal class RoomDimensionsCD
    {
        #region Variables d'instance

        /// <summary>
        /// La position du point pivot de la salle sur la grille
        /// (en bas à gauche)
        /// </summary>
        internal int2 Position;

        /// <summary>
        /// La taille de la salle sur la grille
        /// </summary>
        internal int2 Size;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="position">La position du point pivot de la salle sur la grille</param>
        /// <param name="size">La taille de la salle sur la grille</param>
        public RoomDimensionsCD(int2 position, int2 size)
        {
            this.Position = position;
            this.Size = size;
        }

        #endregion
    }
}
