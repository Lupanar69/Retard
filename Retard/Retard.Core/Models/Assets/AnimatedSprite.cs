using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Retard.Core.Models.Assets
{
    /// <summary>
    /// Représente un atlas de sprites animé
    /// </summary>
    internal sealed class AnimatedSprite : Sprite
    {
        #region Variables d'instance

        /// <summary>
        /// Le nombre total de sprites dans l'atlas
        /// </summary>
        private int _totalFrames;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="frame">L'ID de départ de l'animation (0 par défaut.)</param>
        internal AnimatedSprite(SpriteAtlas atlas, int frame = 0)
            : base(atlas, frame)
        {
            this._totalFrames = atlas.Rows * atlas.Columns;
        }

        #endregion

        #region Fonctions internes

        /// <summary>
        /// Màj le sprite à afficher au fil du temps
        /// </summary>
        internal void Update()
        {
            this.Frame = (this.Frame + 1) % this._totalFrames;
        }

        /// <summary>
        /// Affiche le sprite à l'écran.
        /// Penser à appeler spriteBatch.Begin() et End() avant et après cette méthode.
        /// </summary>
        /// <param name="spriteBatch">Gère le rendu du sprite à l'écran</param>
        /// <param name="screenPos">La position en pixels</param>
        /// <param name="color">La couleur du sprite</param>
        internal sealed override void Draw(in SpriteBatch spriteBatch, Vector2 screenPos, Color color)
        {
            this._sourceRectangle = this.Atlas.GetSpriteRect(this.Frame);
            base.Draw(spriteBatch, screenPos, color);
        }

        #endregion
    }
}
