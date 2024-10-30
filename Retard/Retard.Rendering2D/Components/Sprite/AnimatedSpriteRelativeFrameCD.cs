using Arch.AOT.SourceGenerator;

namespace Retard.Sprites.Components.Sprite
{
    /// <summary>
    /// L'ID du sprite actuel depuis le début de l'animation
    /// </summary>
    /// <remarks>
    /// Constructeur
    /// </remarks>
    /// <param name="relativeFrame">L'ID du sprite actuel depuis le début de l'animation</param>
    [Component]
    public struct AnimatedSpriteRelativeFrameCD(int relativeFrame = 0)
    {
        #region Variables d'instance

        /// <summary>
        /// L'ID du sprite actuel depuis le début de l'animation
        /// </summary>
        public int Value = relativeFrame;

        #endregion
    }
}
