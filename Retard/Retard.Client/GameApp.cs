using Microsoft.Xna.Framework;
using Retard.Engine.ViewModels;
using Retard.Tests.ViewModels.Engine;

namespace Retard.Client
{
    /// <summary>
    /// Callbacks du jeu
    /// </summary>
    internal sealed class GameApp : Game
    {
        #region Variables d'instance

        /// <summary>
        /// Configurateur des paramètres de la fenêtre du jeu
        /// </summary>
        private readonly GraphicsDeviceManager _graphicsDeviceManager;

        /// <summary>
        /// Le moteur de jeu
        /// </summary>
        private BaseEngine _engine;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        public GameApp()
        {
            this._graphicsDeviceManager = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";
        }

        #endregion

        #region Fonctions protégées

        /// <summary>
        /// Initialise le jeu
        /// </summary>
        protected override void Initialize()
        {
            this._engine = new GameEngine(this, this._graphicsDeviceManager);

            base.Initialize();
        }

        /// <summary>
        /// Charge le contenu externe du jeu
        /// </summary>
        protected override void LoadContent()
        {
            this._engine.LoadContent();
            base.LoadContent();
        }

        /// <summary>
        /// Màj à chaque frame
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début du jeu</param>
        protected override void Update(GameTime gameTime)
        {
            this._engine.Update(this, gameTime);

            this._engine.AfterUpdate();

            base.Update(gameTime);
        }

        /// <summary>
        /// Pour afficher des éléments à l'écran
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début du jeu</param>
        protected override void Draw(GameTime gameTime)
        {
            this._engine.Draw(this.GraphicsDevice, gameTime);

            base.Draw(gameTime);
        }

        #endregion
    }
}