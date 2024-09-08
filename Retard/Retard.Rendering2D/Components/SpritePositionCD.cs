using Arch.AOT.SourceGenerator;
using Microsoft.Xna.Framework;

namespace Retard.Rendering2D.Components
{
    /// <summary>
    /// La position du sprite à l'écran en pixels
    /// </summary>
    /// <remarks>
    /// Constructeur
    /// </remarks>
    /// <param name="pos">La position du sprite à l'écran en pixels</param>
    [Component]
    public struct SpritePositionCD(Vector2 pos)
    {
        #region Variables d'instance

        /// <summary>
        /// La position du sprite à l'écran en pixels
        /// </summary>
        public Vector2 Value = pos;

        #endregion
    }
}
