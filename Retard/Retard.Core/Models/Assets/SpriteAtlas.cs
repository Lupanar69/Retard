using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Retard.Core.Models.Assets
{
    /// <summary>
    /// Représente un sprite provenant d'un atlas
    /// </summary>
    public class SpriteAtlas
    {
        #region Propriétés

        /// <summary>
        /// La texture source du sprite
        /// </summary>
        public Texture2D Texture { get; private set; }

        /// <summary>
        /// Le nombre de lignes de sprite
        /// </summary>
        public int Rows { get; private set; }

        /// <summary>
        /// Le nombre de colonnes de sprite
        /// </summary>
        public int Columns { get; private set; }

        /// <summary>
        /// L'ID du sprite à afficher dans l'atlas
        /// </summary>
        public int Frame { get; protected set; }

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
        /// <param name="texture">La texture source du sprite</param>
        /// <param name="rows">Le nombre de lignes de sprite</param>
        /// <param name="columns">Le nombre de colonnes de sprite</param>
        /// <param name="frame">L'ID du sprite à afficher dans l'atlas</param>
        public SpriteAtlas(Texture2D texture, int rows, int columns, int frame)
        {
            this.Texture = texture;
            this.Rows = rows;
            this.Columns = columns;
            this.Frame = frame;

            this._sourceRectangle = SpriteAtlas.GetSpriteFromAtlas(texture, rows, columns, frame);
        }

        #endregion

        #region Fonctions statiques

        /// <summary>
        /// Calcule les dimensions du sprite
        /// </summary>
        /// <param name="texture">La texture à afficher</param>
        /// <param name="rows">Le nombre de lignes de sprites dans l'atlas</param>
        /// <param name="columns">Le nombre de colonnes de sprites dans l'atlas</param>
        /// <param name="frame">L'id du sprite dans l'atlas à afficher</param>
        /// <returns>Les dimensions du sprite</returns>
        protected static Rectangle GetSpriteFromAtlas(Texture2D texture, int rows, int columns, int frame)
        {
            int width = texture.Width / columns;
            int height = texture.Height / rows;
            int row = frame / columns;
            int column = frame % columns;

            return new Rectangle(width * column, height * row, width, height);
        }

        #endregion

        #region Fonctions publiques

        /// <summary>
        /// Affiche le sprite à l'écran.
        /// Penser à appeler spriteBatch.Begin() et End() avant et après cette méthode.
        /// </summary>
        /// <param name="spriteBatch">Gère le rendu du sprite à l'écran</param>
        /// <param name="screenPos">La position en pixels</param>
        public virtual void Draw(in SpriteBatch spriteBatch, Vector2 screenPos)
        {
            Rectangle destinationRectangle =
                new((int)screenPos.X, (int)screenPos.Y, this._sourceRectangle.Width, this._sourceRectangle.Height);

            spriteBatch.Draw(Texture, destinationRectangle, this._sourceRectangle, Color.White);
        }

        #endregion
    }
}
