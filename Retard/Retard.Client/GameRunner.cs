using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Entities;

namespace Retard.Client
{
    /// <summary>
    /// Callbacks du jeu
    /// </summary>
    internal sealed class GameRunner : Game
    {
        #region Variables d'instance

        /// <summary>
        /// Contient les entités et systèmes
        /// </summary>
        private World _world;

        /// <summary>
        /// Permet de modifier les paramètres de la fenêtre
        /// </summary>
        private GraphicsDeviceManager _graphics;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public GameRunner()
        {
            this._graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";

            this.SetupGameWindow();
        }

        #endregion

        #region Fonctions protégées

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            this._world = new WorldBuilder().Build();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            this._world.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            this._world.Draw(gameTime);

            base.Draw(gameTime);
        }

        #endregion

        #region Fonctions privées

        /// <summary>
        /// Initialise la fenêtre de jeu
        /// </summary>
        private void SetupGameWindow()
        {
            this.IsMouseVisible = true;
            this.Window.AllowUserResizing = true;

            this._graphics.PreferredBackBufferWidth = 1920;
            this._graphics.PreferredBackBufferHeight = 1080;
            //this._graphics.IsFullScreen = true;
            this._graphics.ApplyChanges();
        }

        #endregion
    }
}