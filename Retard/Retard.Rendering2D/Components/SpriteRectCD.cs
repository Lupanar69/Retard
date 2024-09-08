using Arch.AOT.SourceGenerator;
using Microsoft.Xna.Framework;

namespace Retard.Rendering2D.Components
{
    /// <summary>
    /// Les dimensions du sprite
    /// </summary>
    /// <remarks>
    /// Constructeur
    /// </remarks>
    /// <param name="rect">Les dimensions du sprite</param>
    [Component]
    public struct SpriteRectCD(Rectangle rect)
    {
        #region Variables d'instance

        /// <summary>
        /// Les dimensions du sprite
        /// </summary>
        public Rectangle Value = rect;

        #endregion
    }
}
