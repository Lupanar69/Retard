using Arch.AOT.SourceGenerator;
using Microsoft.Xna.Framework;

namespace Retard.Cameras.Components.Camera
{
    /// <summary>
    /// La position de la caméra à l'écran, en pixels
    /// </summary>
    /// <param name="value">La position de la caméra à l'écran, en pixels</param>
    [Component]
    public struct Camera2DPositionCD(Vector2 value)
    {
        #region Variables d'instance

        /// <summary>
        /// La position de la caméra à l'écran, en pixels
        /// </summary>
        public Vector2 Value = value;

        #endregion
    }
}
