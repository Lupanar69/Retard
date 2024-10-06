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

        #region Variables d'instance

        /// <summary>
        /// Permet d'accéder aux texture2Ds depuis les queries
        /// </summary>
        private Resources<Texture2D> _texture2DResources { get; init; }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        private SpriteManager()
        {
            this._texture2DResources = new Resources<Texture2D>(1);
        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Enregistre la texture2D et retourne un handle pour permettre 
        /// de l'associer à une entité
        /// </summary>
        /// <param name="texture">La texture à enregistrer</param>
        /// <returns>Une référence unsafe à la texture</returns>
        public Handle<Texture2D> RegisterTexture(in Texture2D texture)
        {
            return this._texture2DResources.Add(in texture);
        }

        /// <summary>
        /// Retire la texture2D de la liste de ressources
        /// </summary>
        /// <param name="handle">La référence de la texture à enregistrer</param>
        public void UnregisterTexture(in Handle<Texture2D> handle)
        {
            this._texture2DResources.Remove(in handle);
        }

        /// <summary>
        /// Obtient la texture2D depuis la liste de ressources
        /// </summary>
        /// <param name="handle">La référence de la texture à récupérer</param>
        public Texture2D GetTexture(in Handle<Texture2D> handle)
        {
            return this._texture2DResources.Get(in handle);
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
