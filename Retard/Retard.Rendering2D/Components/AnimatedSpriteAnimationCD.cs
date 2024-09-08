using Arch.AOT.SourceGenerator;

namespace Retard.Rendering2D.Components
{
    /// <summary>
    /// Les IDs des sprites de début et fin de l'animation
    /// </summary>
    /// <remarks>
    /// Constructeur
    /// </remarks>
    /// <param name="startFrame">L'ID du sprite de début de l'animation</param>
    /// <param name="length">Le nombre de sprites dans l'animation</param>
    [Component]
    public struct AnimatedSpriteAnimationCD(int startFrame, int length)
    {
        #region Variables d'instance

        /// <summary>
        /// L'ID du sprite de début de l'animation
        /// </summary>
        public int StartFrame = startFrame;

        /// <summary>
        /// Le nombre de sprites dans l'animation
        /// </summary>
        public int Length = length;

        #endregion
    }
}
