using Microsoft.Xna.Framework;
using Retard.Core.Models.Assets;

namespace Retard.Core.Models.Components.Tiles
{
    /// <summary>
    /// L'image d'une case
    /// </summary>
    internal sealed class TileSpriteCD
    {
        #region Variables d'instance

        /// <summary>
        /// Le sprite d'une tile
        /// </summary>
        public Sprite Sprite;

        /// <summary>
        /// La couleur du sprite
        /// </summary>
        public Color Color;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="atlas">La texture source du sprite</param>
        /// <param name="frame">La position du sprite dans l'atlas</param>
        /// <param name="color">La couleur du sprite</param>
        public TileSpriteCD(SpriteAtlas atlas, int frame, Color color)
        {
            this.Sprite = new Sprite(atlas, frame);
            this.Color = color;
        }

        #endregion
    }
}
