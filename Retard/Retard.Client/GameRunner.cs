using System.Diagnostics;
using Arch.Core;
using Arch.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Retard.Core.Models;
using Retard.Core.Models.Assets.Camera;
using Retard.Core.Models.Assets.Sprites;
using Retard.Core.Models.ValueTypes;
using Retard.Core.Systems;
using Retard.Core.Systems.Tests;
using Retard.Core.ViewModels;

namespace Retard.Client
{
    /// <summary>
    /// Callbacks du jeu
    /// </summary>
    internal sealed class GameRunner : Game
    {
        #region Variables d'instance

        /// <summary>
        /// Permet de modifier les paramètres de la fenêtre
        /// </summary>
        private readonly GraphicsDeviceManager _graphicsDeviceManager;

        /// <summary>
        /// La caméra du jeu
        /// </summary>
        private Camera _camera;

        /// <summary>
        /// Le monde contenant les entités
        /// </summary>
        private World _world;

        /// <summary>
        /// Les systèmes du monde à màj dans Update()
        /// </summary>
        private Group<float> _updateSystems;

        /// <summary>
        /// Les systèmes du monde
        /// </summary>
        private Group<byte> _drawSystems;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public GameRunner()
        {
            this._graphicsDeviceManager = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";


            this.SetupGameWindow(800, 600, false);
        }

        /// <summary>
        /// Finaliseur
        /// </summary>
        ~GameRunner()
        {
            this._graphicsDeviceManager.Dispose();
            this._camera.Dispose();
            this._world.Dispose();
            this._drawSystems.Dispose();
        }

        #endregion

        #region Fonctions protégées

        /// <summary>
        /// Init
        /// </summary>
        protected override void Initialize()
        {
            this._camera = new Camera();
            this._world = World.Create();
            this._updateSystems = new Group<float>("Update Systems");
            this._drawSystems = new Group<byte>("Draw Systems");

            base.Initialize();
        }

        /// <summary>
        /// Chargement du contenu
        /// </summary>
        protected override void LoadContent()
        {
            Texture2D debugTex = Content.Load<Texture2D>($"{Constants.TEXTURES_DIR_PATH_DEBUG}tiles_test2");
            SpriteAtlas debugAtlas = new(debugTex, 4, 4);

            // Créé ici car on a besoin de récupérer les textures

            this._drawSystems.Add(new SpriteDrawSystem(this._world, this.GraphicsDevice, debugAtlas, this._camera));
            this._updateSystems.Add(new AnimatedSpriteUpdateSystem(this._world));

#if DEBUG
            this._updateSystems.Add(new SpriteCreateSystemTest(this._world, debugAtlas, new int2(60, 60)));
#endif

            this._updateSystems.Initialize();
            this._drawSystems.Initialize();
        }

        /// <summary>
        /// Màj à chaque frame
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début de l'application</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || KeyboardInput.IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            if (KeyboardInput.IsKeyDown(Keys.J))
            {
                Trace.WriteLine("j");
            }

            this._camera.Update();
            this._updateSystems.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            // Appelé en dernier pour ne pas écraser le précédent KeyboardState
            // avant les comparaisons

            KeyboardInput.RefreshKeyboardState();

            base.Update(gameTime);
        }

        /// <summary>
        /// Pour afficher des éléments à l'écran
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début de l'application</param>
        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.Black);

            this._drawSystems.Update(0);

            base.Draw(gameTime);
        }

        #endregion

        #region Fonctions privées

        /// <summary>
        /// Initialise la fenêtre de jeu
        /// </summary>
        /// <param name="resolutionX">La résolution en X de la fenêtre</param>
        /// <param name="resolutionY">La résolution en Y de la fenêtre</param>
        /// <param name="fullScreen"><see langword="true"/> pour passer la fenêtre en plein écran</param>
        /// <param name="mouseVisible"><see langword="true"/> si la souris doit rester visible</param>
        /// <param name="allowUserResizing"><see langword="true"/> si le joueur peut redimensionner la fenêtre</param>
        private void SetupGameWindow(int resolutionX, int resolutionY, bool fullScreen = true, bool mouseVisible = true, bool allowUserResizing = true)
        {
            this.IsMouseVisible = mouseVisible;
            this.Window.AllowUserResizing = allowUserResizing;

            this._graphicsDeviceManager.PreferredBackBufferWidth = resolutionX;
            this._graphicsDeviceManager.PreferredBackBufferHeight = resolutionY;
            this._graphicsDeviceManager.IsFullScreen = fullScreen;
            this._graphicsDeviceManager.ApplyChanges();
        }

        #endregion
    }
}