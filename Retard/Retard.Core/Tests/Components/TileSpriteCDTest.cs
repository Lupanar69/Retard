using Microsoft.Xna.Framework;
using Retard.Core.Models.Assets;

namespace Retard.Core.Tests.Components
{
    /// <summary>
    /// Le sprite d'une tile
    /// </summary>
    internal class TileSpriteCDTest
    {
        #region Variables d'instance

        /// <summary>
        /// Le sprite d'une tile
        /// </summary>
        public SpriteAtlas Sprite;

        /// <summary>
        /// La couleur du sprite
        /// </summary>
        public Color Color;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="sprite">L'image à afficher</param>
        public TileSpriteCDTest(SpriteAtlas sprite, Color color)
        {
            Sprite = sprite;
            Color = color;
        }

        #endregion
    }
}
