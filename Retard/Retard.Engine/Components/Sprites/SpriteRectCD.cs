using Arch.AOT.SourceGenerator;
using Microsoft.Xna.Framework;

namespace Retard.Engine.Components.Sprites
{
    /// <summary>
    /// Les dimensions du sprite
    /// </summary>
    [Component]
    public struct SpriteRectCD
    {
        #region Variables d'instance

        /// <summary>
        /// Les dimensions du sprite
        /// </summary>
        public Rectangle Value;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="rect">Les dimensions du sprite</param>
        public SpriteRectCD(Rectangle rect)
        {
            this.Value = rect;
        }

        #endregion
    }
}
