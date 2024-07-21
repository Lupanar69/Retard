using Arch.AOT.SourceGenerator;
using Microsoft.Xna.Framework;

namespace Retard.Engine.Components.Camera
{
    /// <summary>
    /// Le centre de la caméra (Position + Origin)
    /// </summary>
    [Component]
    internal struct Camera2DCenterCD
    {
        #region Variables d'instance

        /// <summary>
        /// Le centre de la caméra (Position + Origin)
        /// </summary>
        public Vector2 Value;

        #endregion
    }
}
