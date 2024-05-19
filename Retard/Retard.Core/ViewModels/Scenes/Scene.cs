using Arch.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Retard.Core.ViewModels.Scenes
{
    /// <summary>
    /// Permet de compartimenter la logique parmi différents contextes
    /// pour éviter de tout rassembler dans la classe ppale
    /// </summary>
    public abstract class Scene
    {
        #region Propriétés

        /// <summary>
        /// <see langword="true"/> si la scène doit bloquer les inputs 
        /// pour les scènes qui suivent
        /// (ex: une scène de pause superposée à la scène de jeu)
        /// </summary>
        public bool ConsumeInput { get; protected init; }

        #endregion

        #region Variables d'instance

        /// <summary>
        /// Les assets du jeu
        /// </summary>
        protected ContentManager _content;

        /// <summary>
        /// Le monde contenant les entités
        /// </summary>
        protected World _world;

        /// <summary>
        /// Pour afficher les sprites à l'écran
        /// </summary>
        protected SpriteBatch _spriteBatch;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="content">Les assets du jeu</param>
        /// <param name="world">Le monde contenant les entités</param>
        /// <param name="spriteBatch">Pour afficher les sprites à l'écran</param>
        protected Scene(ContentManager content, World world, SpriteBatch spriteBatch)
        {
            this._content = content;
            this._world = world;
            this._spriteBatch = spriteBatch;
        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Init
        /// </summary
        public abstract void Initialize();

        /// <summary>
        /// Chargement du contenu
        /// </summary>
        public abstract void LoadContent();

        /// <summary>
        /// Màj à chaque frame
        /// </summary>
        public abstract void UpdateInput();

        /// <summary>
        /// Màj à chaque frame
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début de l'application</param>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// Pour afficher des éléments à l'écran
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début de l'application</param>
        public abstract void Draw(GameTime gameTime);

        #endregion
    }
}
