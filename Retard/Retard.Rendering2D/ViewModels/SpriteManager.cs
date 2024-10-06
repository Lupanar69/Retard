using System;
using Arch.LowLevel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Retard.Rendering2D.ViewModels
{
    /// <summary>
    /// Gère la création et destruction des entités des sprites
    /// </summary>
    public sealed class SpriteManager
    {
        #region Singleton

        /// <summary>
        /// Singleton
        /// </summary>
        public static SpriteManager Instance => SpriteManager._instance.Value;

        /// <summary>
        /// Singleton
        /// </summary>
        private static readonly Lazy<SpriteManager> _instance = new(() => new SpriteManager());

        #endregion

        #region Propriétés

        /// <summary>
        /// Permet d'accéder aux texture2Ds depuis les queries
        /// </summary>
        public Resources<Texture2D> Texture2DResources { get; init; }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        private SpriteManager()
        {
            this.Texture2DResources = new Resources<Texture2D>(1);
        }

        #endregion

        #region Méthodes statiques publiques

        /// <summary>
        /// Calcule les dimensions du sprite
        /// </summary>
        /// <param name="texture">La source</param>
        /// <param name="rows">Le nombre de lignes de l'atlas</param>
        /// <param name="columns">Le nombre de colonnes de l'atlas</param>
        /// <param name="frame">L'id du sprite dans l'atlas à afficher</param>
        /// <returns>Les dimensions du sprite</returns>
        public static Rectangle GetSpriteRect(in Texture2D texture, int rows, int columns, int frame)
        {
            int width = texture.Width / columns;
            int height = texture.Height / rows;
            int row = frame / columns;
            int column = frame % columns;

            return new Rectangle(width * column, height * row, width, height);
        }

        #endregion
    }
}
