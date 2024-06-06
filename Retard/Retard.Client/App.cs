using Arch.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using Retard.Core.Models;
using Retard.Core.ViewModels.Input;
using Retard.Core.ViewModels.JSON;
using Retard.Core.ViewModels.Scenes;
using Retard.Core.ViewModels.Scenes.Tests;

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
        private OrthographicCamera _camera;

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
            // Crée les fichiers de config par défaut

            this.CreateDefaultConfigFiles();

            // Initialise les components

            this._spriteBatch = new SpriteBatch(this.GraphicsDevice);
            this._camera = new OrthographicCamera(this.GraphicsDevice);
            this._world = World.Create();

            // Initialise les inputs

            InputManager.Initialize(new KeyboardInput(), new MouseInput(), new GamePadInput());
            this._keyboardInput = InputManager.GetScheme<KeyboardInput>();
            this._gamePadInput = InputManager.GetScheme<GamePadInput>();

            // Initialise les scènes

            SceneManager.Initialize(this.Content, this._world, this._spriteBatch);

#if TESTS
            SceneManager.AddScene(new SpriteDrawTestScene(this._camera));
            //SceneManager.AddScene(new BlockInputTestScene());
#endif
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

            if (this._gamePadInput.IsButtonPressed(0, Buttons.Back) || this._keyboardInput.IsKeyPressed(Keys.Escape))
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
        /// Crée les fichiers de config par défaut
        /// </summary>
        private void CreateDefaultConfigFiles()
        {
            // Crée le fichier de config des entrées par défaut

            string defaultInputConfigPath = $"{Constants.GAME_DIR_PATH}/{Constants.DEFAULT_INPUT_CONFIG_PATH}";

            string defaultInputConfigJson = JsonUtilities.SerializeObject(Constants.DEFAULT_INPUT_CONFIG);

            JsonUtilities.WriteToFile(defaultInputConfigJson, defaultInputConfigPath);
        }

        #endregion
    }
}