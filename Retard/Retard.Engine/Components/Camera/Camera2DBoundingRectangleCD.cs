using Arch.AOT.SourceGenerator;
using MonoGame.Extended;

namespace Retard.Engine.Components.Camera
{
    /// <summary>
    /// An axis-aligned, four sided, two dimensional box defined by a top-left position
    /// (MonoGame.Extended.RectangleF.X and MonoGame.Extended.RectangleF.Y) and a size
    /// (MonoGame.Extended.RectangleF.Width and MonoGame.Extended.RectangleF.Height).
    /// </summary>
    [Component]
    internal struct Camera2DBoundingRectangleCD
    {
        #region Variables d'instance

        /// <summary>
        /// An axis-aligned, four sided, two dimensional box defined by a top-left position
        /// (MonoGame.Extended.RectangleF.X and MonoGame.Extended.RectangleF.Y) and a size
        /// (MonoGame.Extended.RectangleF.Width and MonoGame.Extended.RectangleF.Height).
        /// </summary>
        public RectangleF Value;

        #endregion
    }
}
