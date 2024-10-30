using Microsoft.Xna.Framework.Graphics;

namespace Retard.Sprites.Models
{
    /// <summary>
    /// Components utilisés pour le rendu des sprites
    /// </summary>
    /// <param name="spriteBatch">Pour afficher les sprites à l'écran</param>
    /// <param name="graphicsDevice">Pour assigner le viewport de chaque caméra</param>
    public sealed class RenderingComponents2D(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
    {
        #region Propriétés

        /// <summary>
        /// Pour afficher les sprites à l'écran
        /// </summary>
        public SpriteBatch SpriteBatch => spriteBatch;

        /// <summary>
        /// Pour assigner le viewport de chaque caméra
        /// </summary>
        public GraphicsDevice GraphicsDevice => graphicsDevice;

        #endregion
    }
}
