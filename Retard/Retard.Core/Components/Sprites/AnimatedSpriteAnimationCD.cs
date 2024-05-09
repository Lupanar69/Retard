namespace Retard.Core.Components.Sprites
{
    /// <summary>
    /// Les IDs des sprites de début et fin de l'animation
    /// </summary>
    public struct AnimatedSpriteAnimationCD
    {
        #region Propriétés

        /// <summary>
        /// Le nombre de sprites dans l'animation
        /// </summary>
        public int Length => this.EndFrame - this.StartFrame;

        #endregion

        #region Variables d'instance

        /// <summary>
        /// L'ID du sprite de début de l'animation
        /// </summary>
        public int StartFrame;

        /// <summary>
        /// L'ID du sprite de début de l'animation
        /// </summary>
        public int EndFrame;

        #endregion
    }
}
