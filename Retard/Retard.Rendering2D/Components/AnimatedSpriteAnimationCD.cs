using Arch.AOT.SourceGenerator;

namespace Retard.Rendering2D.Components
{
    /// <summary>
    /// Les IDs des sprites de début et fin de l'animation
    /// </summary>
    [Component]
    public struct AnimatedSpriteAnimationCD
    {
        #region Variables d'instance

        /// <summary>
        /// L'ID du sprite de début de l'animation
        /// </summary>
        public int StartFrame;

        /// <summary>
        /// Le nombre de sprites dans l'animation
        /// </summary>
        public int Length;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="startFrame">L'ID du sprite de début de l'animation</param>
        /// <param name="length">Le nombre de sprites dans l'animation</param>
        public AnimatedSpriteAnimationCD(int startFrame, int length)
        {
            StartFrame = startFrame;
            Length = length;
        }

        #endregion
    }
}
