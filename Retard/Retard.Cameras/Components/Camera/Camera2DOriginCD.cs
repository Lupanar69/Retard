using Arch.AOT.SourceGenerator;
using Microsoft.Xna.Framework;

namespace Retard.Cameras.Components.Camera
{
    /// <summary>
    /// L'origine de la caméra à l'écran, en pixels
    /// </summary>
    /// <param name="value">L'origine de la caméra à l'écran, en pixels</param>
    [Component]
    public struct Camera2DOriginCD(Vector2 value)
    {
        #region Variables d'instance

        /// <summary>
        /// L'origine de la caméra à l'écran, en pixels
        /// </summary>
        public Vector2 Value = value;

        #endregion
    }
}
