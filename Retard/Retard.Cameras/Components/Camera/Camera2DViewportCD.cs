using Arch.AOT.SourceGenerator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Retard.Cameras.Components.Camera
{
    /// <summary>
    /// Les dimensions du viewport de la caméra
    /// et sa position à l'écran
    /// </summary>
    /// <param name="value">Les dimensions du viewport de la caméra
    /// et sa position à l'écran</param>
    [Component]
    public struct Camera2DViewportCD(Viewport value)
    {
        #region Propriétés

        /// <summary>
        /// Les limites du Viewport
        /// </summary>
        public readonly Rectangle BoundingRectangle => value.Bounds;

        /// <summary>
        /// Le centre du Viewport
        /// </summary>
        public readonly Point Center => value.Bounds.Center;

        /// <summary>
        /// Les dimensions du viewport de la caméra
        /// et sa position à l'écran
        /// </summary>
        public readonly Viewport Value => value;

        #endregion
    }
}
