using Arch.AOT.SourceGenerator;
using Microsoft.Xna.Framework;

namespace Retard.Core.Components.Sprites
{
    /// <summary>
    /// La couleur d'un sprite
    /// </summary>
    [Component]
    public struct SpriteColorCD
    {
        #region Variables d'instance

        /// <summary>
        /// La couleur du sprite
        /// </summary>
        public Color Value;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="color">La couleur du sprite</param>
        public SpriteColorCD(Color color)
        {
            this.Value = color;
        }

        #endregion
    }
}
