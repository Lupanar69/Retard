using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Retard.Core.Models.Assets
{
    /// <summary>
    /// Représente un sprite provenant d'un atlas
    /// </summary>
    internal class Sprite
    {
        #region Propriétés

        /// <summary>
        /// La texture source du sprite
        /// </summary>
        internal SpriteAtlas Atlas { get; private set; }

        /// <summary>
        /// L'ID du sprite à afficher dans l'atlas
        /// </summary>
        internal int Frame { get; private protected set; }

        #endregion

        #region Variables d'instance

        /// <summary>
        /// Les dimensions du sprite
        /// </summary>
        protected Rectangle _sourceRectangle;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="atlas">La texture source du sprite</param>
        /// <param name="frame">L'ID du sprite à afficher dans l'atlas</param>
        internal Sprite(SpriteAtlas atlas, int frame)
        {
            this.Atlas = atlas;
            this.Frame = frame;

            this._sourceRectangle = atlas.GetSpriteRect(frame);
        }

        #endregion

        #region Fonctions publiques

        /// <summary>
        /// Affiche le sprite à l'écran.
        /// Penser à appeler spriteBatch.Begin() et End() avant et après cette méthode.
        /// </summary>
        /// <param name="spriteBatch">Gère le rendu du sprite à l'écran</param>
        /// <param name="screenPos">La position en pixels</param>
        /// <param name="color">La couleur du sprite</param>
        internal virtual void Draw(in SpriteBatch spriteBatch, Vector2 screenPos, Color color)
        {
            Rectangle destinationRectangle =
                new((int)screenPos.X, (int)screenPos.Y, this._sourceRectangle.Width, this._sourceRectangle.Height);

            spriteBatch.Draw(this.Atlas.Texture, destinationRectangle, this._sourceRectangle, color);
        }

        #endregion
    }
}
