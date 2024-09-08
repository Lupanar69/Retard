using Arch.AOT.SourceGenerator;
using Microsoft.Xna.Framework;

namespace Retard.Rendering2D.Components.Sprite
{
    /// <summary>
    /// La couleur d'un sprite
    /// </summary>
    /// <remarks>
    /// Constructeur
    /// </remarks>
    /// <param name="color">La couleur du sprite</param>
    [Component]
    public struct SpriteColorCD(Color color)
    {
        #region Variables d'instance

        /// <summary>
        /// La couleur du sprite
        /// </summary>
        public Color Value = color;

        #endregion
    }
}
