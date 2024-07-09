using System;
using Arch.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using Retard.Core.Models;
using Retard.Core.ViewModels.App;
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
            this.Content.RootDirectory = "Content";
            AppConfigFileCreation.Initialize();
            AppViewport.Initialize(this);
            AppPerformance.Initialize(this);
        }

        #endregion

        #region Fonctions protégées

        /// <summary>
        /// Init
        /// </summary>
        protected override void Initialize()
        {
            // Initialise les components

            this._spriteBatch = new SpriteBatch(this.GraphicsDevice);
            this._camera = new OrthographicCamera(this.GraphicsDevice);
            this._world = World.Create();

            // Initialise les inputs

            InputManager.InitializeSchemes(new KeyboardInput(), new MouseInput(), new GamePadInput(GamePad.MaximumGamePadCount));
            InputManager.InitializeSystems(this._world);
            this._keyboardInput = InputManager.GetScheme<KeyboardInput>();
            this._gamePadInput = InputManager.GetScheme<GamePadInput>();

            // Initialise les scènes

            this.InitializeSceneManager();

            //InputControls c = new();
            //c.GetButtonEvent("Camera/LeftMousePressed").Started += (playerID) => p("pressed");
            //c.GetButtonEvent("Camera/LeftMousePressed").Performed += (playerID) => p("held");
            //c.GetButtonEvent("Camera/LeftMousePressed").Finished += (playerID) => p("released");
            //c.Enable();

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
            GameState.GameHasFocus = true;

            base.OnActivated(sender, args);
        }

        /// <summary>
        /// Quand la fenêtre gagne le focus
        /// </summary>
        /// <param name="sender">l'app</param>
        /// <param name="args">vide</param>
        protected override void OnDeactivated(object sender, EventArgs args)
        {
            GameState.GameHasFocus = false;

            base.OnActivated(sender, args);
        }

        #endregion

        #region Fonctions privées

        /// <summary>
        /// Crée toutes les scènes utilisées par le jeu
        /// </summary>
        private void InitializeSceneManager()
        {
            SceneManager.Initialize(2, this.Content, this._world, this._spriteBatch);
            SceneManager.AddSceneToPool(new OrthographicCameraScene(this._camera));

#if TESTS
            SceneManager.AddSceneToPool(new SpriteDrawTestScene(this._camera, new Point(100)));
            SceneManager.SetSceneAsActive<SpriteDrawTestScene>();
#endif
            SceneManager.SetSceneAsActive<OrthographicCameraScene>();
        }

        #endregion
    }
}