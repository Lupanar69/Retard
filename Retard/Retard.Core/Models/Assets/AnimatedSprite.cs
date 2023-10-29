using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Retard.Core.Models.Assets
{
    /// <summary>
    /// Représente un atlas de sprites animé
    /// </summary>
    public sealed class AnimatedSprite : SpriteAtlas
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
        /// <param name="texture">La texture source du sprite</param>
        /// <param name="rows">Le nombre de lignes de sprite</param>
        /// <param name="columns">Le nombre de colonnes de sprite</param>
        /// <param name="frame">L'ID de départ de l'animation (0 par défaut.)</param>
        public AnimatedSprite(Texture2D texture, int rows, int columns, int frame = 0)
            : base(texture, rows, columns, frame)
        {
            this._totalFrames = rows * columns;
        }

        #endregion

        #region Fonctions publiques

        /// <summary>
        /// Màj le sprite à afficher au fil du temps
        /// </summary>
        public void Update()
        {
            this.Frame++;
            if (this.Frame == this._totalFrames)
                this.Frame = 0;
        }

        /// <summary>
        /// Affiche le sprite à l'écran.
        /// Penser à appeler spriteBatch.Begin() et End() avant et après cette méthode.
        /// </summary>
        /// <param name="spriteBatch">Gère le rendu du sprite à l'écran</param>
        /// <param name="screenPos">La position en pixels</param>
        public sealed override void Draw(in SpriteBatch spriteBatch, Vector2 screenPos)
        {
            this._sourceRectangle = SpriteAtlas.GetSpriteFromAtlas(this.Texture, this.Rows, this.Columns, this.Frame);
            base.Draw(spriteBatch, screenPos);
        }

        #endregion
    }
}
