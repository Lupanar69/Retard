using Microsoft.Xna.Framework.Graphics;
using Retard.Core.Models.Assets;

namespace Retard.Core.Components.Tiles
{
    /// <summary>
    /// Le sprite d'une tile
    /// </summary>
    internal class TileSpriteCD
    {
        #region Variables d'instance

        /// <summary>
        /// Le sprite d'une tile
        /// </summary>
        public SpriteAtlas Value;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="texture">La texture source du sprite</param>
        /// <param name="rows">Le nombre de lignes de sprite</param>
        /// <param name="columns">Le nombre de colonnes de sprite</param>
        /// <param name="frame">L'ID du sprite à afficher dans l'atlas</param>
        public TileSpriteCD(Texture2D texture, int rows, int columns, int frame)
        {
            this.Value = new SpriteAtlas(texture, rows, columns, frame);
        }

        #endregion
    }
}
