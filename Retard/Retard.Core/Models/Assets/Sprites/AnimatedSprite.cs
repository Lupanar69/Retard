using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Retard.Core.Models.Assets.Sprites
{
    /// <summary>
    /// Représente un atlas de sprites animé
    /// </summary>
    public sealed class AnimatedSprite : Sprite
    {
        #region Propriétés

        /// <summary>
        /// L'ID du sprite actuel depuis le début de l'animation
        /// </summary>
        public int RelativeFrame { get; private set; }

        /// <summary>
        /// Le nombre de sprites dans l'animation
        /// </summary>
        public int Length { get; init; }

        /// <summary>
        /// L'ID du sprite de début de l'animation
        /// </summary>
        public int StartFrame { get; init; }

        /// <summary>
        /// L'ID du sprite de fin de l'animation
        /// </summary>
        public int EndFrame { get; init; }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="atlas">Le sprite source</param>
        /// <param name="startFrame">L'ID de départ de l'animation</param>
        /// <param name="endFrame">L'ID de fin de l'animation</param>
        public AnimatedSprite(SpriteAtlas atlas, int startFrame, int endFrame)
            : base(atlas, startFrame)
        {
            this.RelativeFrame = 0;
            this.StartFrame = startFrame;
            this.EndFrame = endFrame;
            this.Length = endFrame - startFrame;
        }

        #endregion

        #region Méthodes internes

        /// <summary>
        /// Màj le sprite à afficher au fil du temps
        /// </summary>
        public void Update()
        {
            this.RelativeFrame = (this.RelativeFrame + 1) % this.Length;
            this.Frame = this.StartFrame + this.RelativeFrame;
        }

        /// <summary>
        /// Affiche le sprite à l'écran.
        /// Penser à appeler spriteBatch.Begin() et End() avant et après cette méthode.
        /// </summary>
        /// <param name="atlas">Le sprite source</param>
        /// <param name="spriteBatch">Gère le rendu du sprite à l'écran</param>
        /// <param name="screenPos">La position en pixels</param>
        /// <param name="color">La couleur du sprite</param>
        public sealed override void Draw(in SpriteAtlas atlas, in SpriteBatch spriteBatch, Vector2 screenPos, Color color)
        {
            this._sourceRectangle = atlas.GetSpriteRect(this.Frame);
            base.Draw(in atlas, in spriteBatch, screenPos, color);
        }

        #endregion
    }
}
