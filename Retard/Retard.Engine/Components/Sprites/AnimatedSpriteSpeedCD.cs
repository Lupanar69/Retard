using System.ComponentModel.DataAnnotations;
using Arch.AOT.SourceGenerator;

namespace Retard.Engine.Components.Sprites
{
    /// <summary>
    /// Le nombre de frames à attendre avant de màj la frame du sprite.
    /// Plus le nb de frames est grand, plus l'animation est lente.
    /// </summary>
    [Component]
    public struct AnimatedSpriteSpeedCD
    {
        #region Variables d'instance

        /// <summary>
        /// Le nombre de frames à attendre avant de màj la frame du sprite.
        /// Plus le nb de frames est grand, plus l'animation est lente.
        /// </summary>
        [Range(1, int.MaxValue)]
        public int TotalFrames;

        /// <summary>
        /// Le nombre de frames écoulées.
        /// </summary>
        public int ElapsedFrames;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="totalFrames">Le nombre de frames à attendre avant de màj la frame du sprite.</param>
        public AnimatedSpriteSpeedCD([Range(1, int.MaxValue)] int totalFrames = 1)
        {
            this.TotalFrames = totalFrames;
            this.ElapsedFrames = 0;
        }

        #endregion
    }
}
