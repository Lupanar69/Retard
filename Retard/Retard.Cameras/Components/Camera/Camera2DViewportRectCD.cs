using Arch.AOT.SourceGenerator;
using Microsoft.Xna.Framework;

namespace Retard.Cameras.Components.Camera
{
    /// <summary>
    /// Les dimensions du viewport de la caméra
    /// et sa position à l'écran
    /// </summary>
    /// <param name="value">Les dimensions du viewport de la caméra
    /// et sa position à l'écran</param>
    [Component]
    public struct Camera2DViewportRectCD(Rectangle value)
    {
        #region Propriétés

        /// <summary>
        /// Les limites du Viewport
        /// </summary>
        public readonly Rectangle BoundingRectangle => new(0, 0, this.Value.Width, this.Value.Height);

        /// <summary>
        /// Le centre du Viewport
        /// </summary>
        public readonly Point Center => this.Value.Center;

        #endregion

        #region Variables d'instance

        /// <summary>
        /// Les dimensions du viewport de la caméra
        /// et sa position à l'écran
        /// </summary>
        public Rectangle Value = value;

        #endregion
    }
}
