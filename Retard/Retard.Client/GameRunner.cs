using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Entities;
using Retard.Core.Models;
using Retard.Core.Models.Assets;
using Retard.Core.View.Systems;
using Retard.Core.ViewModels.Systems;

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

        /// <summary>
        /// Init
        /// </summary>
        protected override void Initialize()
        {
            // Pour l'instant on génère l'aléa ici,
            // mais on le déplacera au lancement d'une nouvelle partie
            int seed = 1234;
            GameSession.New(seed);

            base.Initialize();
        }

        /// <summary>
        /// Chargement du contenu
        /// </summary>
        protected override void LoadContent()
        {
            Texture2D debugTex = Content.Load<Texture2D>($"{Constants.TEXTURES_DIR_PATH_DEBUG}tiles_test2");
            SpriteAtlas debugAtlas = new(debugTex, 4, 4);

            this._world = new WorldBuilder()
                .AddSystem(new CreateMapSystem(debugAtlas))
                .AddSystem(new MapRenderSystem(this.GraphicsDevice))
                .Build();

            this.Components.Add(this._world);
        }

        /// <summary>
        /// Màj à chaque frame
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début de l'application</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            this._world.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// Pour afficher des éléments à l'écran
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début de l'application</param>
        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.Black);

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