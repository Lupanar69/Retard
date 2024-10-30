using Arch.AOT.SourceGenerator;
using Microsoft.Xna.Framework;

namespace Retard.Sprites.Components.Sprite
{
    /// <summary>
    /// Les dimensions du sprite
    /// </summary>
    /// <remarks>
    /// Constructeur
    /// </remarks>
    /// <param name="value">Les dimensions du sprite</param>
    [Component]
    public struct SpriteRectCD(Rectangle value)
    {
        #region Variables d'instance

        /// <summary>
        /// Les dimensions du sprite
        /// </summary>
        public Rectangle Value = value;

        #endregion
    }
}
