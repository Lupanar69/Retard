using Arch.AOT.SourceGenerator;

namespace Retard.Core.Components.Sprites
{
    /// <summary>
    /// L'ID du sprite actuel depuis le début de l'animation
    /// </summary>
    [Component]
    public struct AnimatedSpriteRelativeFrameCD
    {
        #region Variables d'instance

        /// <summary>
        /// L'ID du sprite actuel depuis le début de l'animation
        /// </summary>
        public int Value;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="relativeFrame">L'ID du sprite actuel depuis le début de l'animation</param>
        public AnimatedSpriteRelativeFrameCD(int relativeFrame = 0)
        {
            this.Value = relativeFrame;
        }

        #endregion
    }
}
