using System;
using Arch.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using Retard.Core.Models;
using Retard.Core.ViewModels.Controllers;
using Retard.Core.ViewModels.Input;
using Retard.Core.ViewModels.Scenes;
using Retard.Tests.ViewModels.Scenes;

namespace Retard.Client
{
    /// <summary>
    /// Callbacks du jeu
    /// </summary>
    internal sealed class App : Game
    {
        #region Variables d'instance

        /// <summary>
        /// Permet de modifier les paramètres de la fenêtre
        /// </summary>
        private readonly GraphicsDeviceManager _graphicsDeviceManager;

        /// <summary>
        /// Pour afficher les sprites à l'écran
        /// </summary>
        private SpriteBatch _spriteBatch;

        /// <summary>
        /// La caméra du jeu
        /// </summary>
        private OrthographicCameraController _cameraController;

        /// <summary>
        /// Le monde contenant les entités
        /// </summary>
        private World _world;

        /// <summary>
        /// Le contrôleur pour clavier
        /// </summary>
        private KeyboardInput _keyboardInput;

        /// <summary>
        /// Le contrôleur pour manette
        /// </summary>
        private GamePadInput _gamePadInput;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public App()
        {
            this._graphicsDeviceManager = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";

            this.SetupGameWindow(800, 600, false);
        }

        #endregion

        #region Fonctions protégées

        /// <summary>
        /// Init
        /// </summary>
        protected override void Initialize()
        {
            // Initialise les inputs

            Vector2 windowSize = new(this._graphicsDeviceManager.PreferredBackBufferWidth, this._graphicsDeviceManager.PreferredBackBufferHeight);
            InputManager.Initialize(new KeyboardInput(), new MouseInput(windowSize), new GamePadInput());
            this._keyboardInput = InputManager.GetScheme<KeyboardInput>();
            this._gamePadInput = InputManager.GetScheme<GamePadInput>();

            // Initialise les components

            this._spriteBatch = new SpriteBatch(this.GraphicsDevice);
            this._cameraController = new OrthographicCameraController(new OrthographicCamera(this.GraphicsDevice));
            this._world = World.Create();

            // Initialise les scènes

            this.InitializeSceneManager();

            base.Initialize();
        }

        /// <summary>
        /// Chargement du contenu
        /// </summary>
        protected override void LoadContent()
        {

        }

        /// <summary>
        /// Màj à chaque frame
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début de l'application</param>
        protected override void Update(GameTime gameTime)
        {
            // Màj les inputs

            InputManager.Update();

            if (this._gamePadInput.IsButtonPressed(0, Buttons.Back) || this._keyboardInput.IsKeyPressed(Keys.Escape) || SceneManager.IsEmpty)
            {
                Exit();
            }

            // Màj les scènes

            SceneManager.UpdateInput(gameTime);
            SceneManager.Update(gameTime);

            // Appelé en dernier pour ne pas écraser le précédent KeyboardState
            // avant les comparaisons

            InputManager.AfterUpdate();

            base.Update(gameTime);
        }

        /// <summary>
        /// Pour afficher des éléments à l'écran
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début de l'application</param>
        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.Black);

            SceneManager.Draw(gameTime);

            base.Draw(gameTime);
        }

        /// <summary>
        /// Quand la fenêtre gagne le focus
        /// </summary>
        /// <param name="sender">l'app</param>
        /// <param name="args">vide</param>
        protected override void OnActivated(object sender, EventArgs args)
        {
            GameState.GameIsActivated = true;

            base.OnActivated(sender, args);
        }

        /// <summary>
        /// Quand la fenêtre gagne le focus
        /// </summary>
        /// <param name="sender">l'app</param>
        /// <param name="args">vide</param>
        protected override void OnDeactivated(object sender, EventArgs args)
        {
            GameState.GameIsActivated = false;

            base.OnActivated(sender, args);
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

        /// <summary>
        /// Crée toutes les scènes utilisées par le jeu
        /// </summary>
        private void InitializeSceneManager()
        {
            SceneManager.Initialize(3, this.Content, this._world, this._spriteBatch);
            SceneManager.AddSceneToPool(new DefaultConfigFileCreationScene());
            SceneManager.AddSceneToPool(new OrthographicCameraScene(this._cameraController));

#if TESTS
            //SceneManager.AddSceneToPool(new TestScene1());
            //SceneManager.AddSceneToPool(new TestScene2());
            //SceneManager.AddSceneToPool(new TestScene3());
            //SceneManager.SetSceneAsActive<TestScene1>();
            SceneManager.AddSceneToPool(new SpriteDrawTestScene(this._cameraController.Camera, new Point(100)));
            //SceneManager.AddSceneToPool(new BlockDrawTestScene());
            //SceneManager.AddSceneToPool(new BlockInputTestScene());
            SceneManager.SetSceneAsActive<SpriteDrawTestScene>();
            //SceneManager.SetSceneAsActive<BlockDrawTestScene>();
#endif

            SceneManager.SetSceneAsActive<OrthographicCameraScene>();
            SceneManager.SetSceneAsActive<DefaultConfigFileCreationScene>();
        }

        #endregion
    }
}