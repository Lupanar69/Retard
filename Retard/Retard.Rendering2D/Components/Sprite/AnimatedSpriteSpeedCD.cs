using System.ComponentModel.DataAnnotations;
using Arch.AOT.SourceGenerator;

namespace Retard.Rendering2D.Components.Sprite
{
    /// <summary>
    /// Le nombre de frames à attendre avant de màj la frame du sprite.
    /// Plus le nb de frames est grand, plus l'animation est lente.
    /// </summary>
    /// <remarks>
    /// Constructeur
    /// </remarks>
    /// <param name="totalFrames">Le nombre de frames à attendre avant de màj la frame du sprite.</param>
    [Component]
    public struct AnimatedSpriteSpeedCD([Range(1, int.MaxValue)] int totalFrames = 1)
    {
        #region Variables d'instance

        /// <summary>
        /// Le nombre de frames à attendre avant de màj la frame du sprite.
        /// Plus le nb de frames est grand, plus l'animation est lente.
        /// </summary>
        [Range(1, int.MaxValue)]
        public int TotalFrames = totalFrames;

        /// <summary>
        /// Le nombre de frames écoulées.
        /// </summary>
        public int ElapsedFrames = 0;

        #endregion
    }
}
