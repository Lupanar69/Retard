using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Retard.Rendering2D.Models
{
    /// <summary>
    /// Représente un sprite provenant d'un atlas
    /// </summary>
    public sealed class SpriteAtlas
    {
        #region Propriétés

        /// <summary>
        /// La texture source du sprite
        /// </summary>
        public readonly Texture2D Texture;

        /// <summary>
        /// Le nombre de lignes de sprite
        /// </summary>
        public readonly int Rows;

        /// <summary>
        /// Le nombre de colonnes de sprite
        /// </summary>
        public readonly int Columns;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="texture">La texture source du sprite</param>
        /// <param name="rows">Le nombre de lignes de sprite</param>
        /// <param name="columns">Le nombre de colonnes de sprite</param>
        public SpriteAtlas(Texture2D texture, int rows, int columns)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
        }

        #endregion

        #region Méthodes internes

        /// <summary>
        /// Calcule les dimensions du sprite
        /// </summary>
        /// <param name="frame">L'id du sprite dans l'atlas à afficher</param>
        /// <param name="spriteSize">La taille du sprite à récupérer (1x1, 2x2, etc.)</param>
        /// <returns>Les dimensions du sprite</returns>
        public Rectangle GetSpriteRect(int frame, int spriteSize = 1)
        {
            int width = Texture.Width / Columns * spriteSize;
            int height = Texture.Height / Rows * spriteSize;
            int row = frame / Columns;
            int column = frame % Columns;

            return new Rectangle(width * column, height * row, width, height);
        }

        #endregion
    }
}
