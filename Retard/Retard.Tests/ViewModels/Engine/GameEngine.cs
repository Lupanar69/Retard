using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using Retard.Core.Models;
using Retard.Core.Models.Assets.Input;
using Retard.Core.Models.ValueTypes;
using Retard.Core.ViewModels.App;
using Retard.Core.ViewModels.Controllers;
using Retard.Engine.Models.App;
using Retard.Engine.Models.DTOs.App;
using Retard.Engine.Models.DTOs.Input;
using Retard.Engine.ViewModels.Engine;
using Retard.Engine.ViewModels.Input;
using Retard.Engine.ViewModels.Utilities;
using Retard.Tests.ViewModels.Scenes;

namespace Retard.Tests.ViewModels.Engine
{
    /// <summary>
    /// Le point d'entrée du jeu
    /// </summary>
    public sealed class GameEngine : BaseEngine
    {
        #region Variables d'instance

        /// <summary>
        /// Pour s'abonner aux inputs
        /// </summary>
        private readonly InputControls _controls;

        /// <summary>
        /// Le contrôleur de la caméra du jeu
        /// </summary>
        private readonly OrthographicCameraController _cameraController;

        /// <summary>
        /// La caméra du jeu
        /// </summary>
        private readonly OrthographicCamera _camera;

        #endregion

        #region Constructeur

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public GameEngine(Game game, GraphicsDeviceManager graphicsDeviceManager, params IInputScheme[] inputSchemes) : base(game, graphicsDeviceManager, inputSchemes)
        {
            // Crée les fichiers de config spécifiques à cette application

            AppConfigFileCreation.CreateDefaultConfigFiles
                (
                    Constants.GAME_DIR_PATH,
                    Constants.OVERRIDE_DEFAULT_AND_CUSTOM_FILES,
                    Constants.DEFAULT_INPUT_CONFIG,
                    Constants.DEFAULT_APP_SETTINGS
                );

            // Initialise la fenêtre de jeu

            this.InitialiseAppViewport();

            // Initialise les managers

            this.InitializeInputManager();

            // Initialise les components

            this._controls = new InputControls();
            this._controls.GetButtonEvent("Exit").Started += (_) => { game.Exit(); };

            this._camera = new OrthographicCamera(game.GraphicsDevice);
            this._cameraController = new OrthographicCameraController(this._camera, this._controls);
            this._controls.Enable();
        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public sealed override void LoadContent()
        {
            Dictionary<NativeString, Texture2D> textures2D = new(1)
            {
                { "tiles_test2", this._content.Load<Texture2D>($"{Constants.TEXTURES_DIR_PATH_DEBUG}/tiles_test2") }
            };

            // Initialise les scènes

            this.CreateScenes(textures2D);
        }

        #endregion

        #region Méthodes privées

        /// <summary>
        /// Initialise les paramètres de la fenêtre de jeu
        /// </summary>
        private void InitialiseAppViewport()
        {
            // Initialise l'appViewport

            string customAppSettingsConfigPath = $"{Constants.GAME_DIR_PATH}/{Constants.DEFAULT_APP_SETTINGS.CustomFilePath}";
            string json = JsonUtilities.ReadFile(customAppSettingsConfigPath);
            var settings = JsonUtilities.DeserializeObject<AppSettingsDTO>(json);
            WindowSettings ws = settings.WindowSettings;

            this._appViewport.SetViewportResolution(ws.WindowSize, ws.FullScreen);
            this._appViewport.SetGameProperties(ws.MouseVisible, ws.AllowUserResizing);
        }

        /// <summary>
        /// Initialise les systèmes de l'InputManager
        /// </summary>
        private void InitializeInputManager()
        {
            string customInputConfigPath = $"{Constants.GAME_DIR_PATH}/{Constants.DEFAULT_INPUT_CONFIG.CustomFilePath}";
            string json = JsonUtilities.ReadFile(customInputConfigPath);
            var config = JsonUtilities.DeserializeObject<InputConfigDTO>(json);

            this._inputManager.InitializeSystems(this._world, config);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override void CreateScenes(Dictionary<NativeString, Texture2D> textures2D)
        {
            this._sceneManager.AddSceneToPool(new SpriteDrawTestScene
                (
                this._world,
                this._spriteBatch,
                this._camera,
                textures2D["tiles_test2"],
                new Point(100),
                Constants.SPRITE_SIZE_PIXELS));

            this._sceneManager.SetSceneAsActive<SpriteDrawTestScene>();
        }

        #endregion
    }
}
