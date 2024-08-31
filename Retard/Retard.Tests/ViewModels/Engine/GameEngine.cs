using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using Retard.Core.Models.App;
using Retard.Core.Models.ValueTypes;
using Retard.Core.ViewModels.Utilities;
using Retard.Engine.Models;
using Retard.Engine.Models.Assets;
using Retard.Engine.Models.Assets.Input;
using Retard.Engine.Models.DTOs.App;
using Retard.Engine.Models.DTOs.Input;
using Retard.Engine.ViewModels.App;
using Retard.Engine.ViewModels.Controllers;
using Retard.Engine.ViewModels.Engine;
using Retard.Engine.ViewModels.Input;
using Retard.Engine.ViewModels.Scenes;
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
        public GameEngine(Game game, GraphicsDeviceManager graphicsDeviceManager) : base(game, graphicsDeviceManager)
        {
            // Initialise les components

            this._controls = new InputControls();
            this._controls.AddAction("Exit", InputEventHandleType.Started, (_) => { game.Exit(); });
            this._controls.Enable();

            this._camera = new OrthographicCamera(game.GraphicsDevice);
            this._cameraController = new OrthographicCameraController(this._camera, this._controls);
        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public sealed override void LoadContent()
        {
            Dictionary<NativeString, Texture2D> textures2D = new()
            {
                { "tiles_test2", this._content.Load<Texture2D>($"{Constants.TEXTURES_DIR_PATH_DEBUG}/tiles_test2") }
            };

            // Initialise les scènes

            GameResources gameResources = new()
            {
                Textures2D = textures2D,
            };

            this.CreateScenes(in gameResources);
        }

        #endregion

        #region Méthodes privées

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="gameResources"><inheritdoc/></param>
        protected sealed override void CreateScenes(in GameResources gameResources)
        {
            SceneManager.Instance.AddSceneToPool(new SpriteDrawTestScene
                (
                this._world,
                this._spriteBatch,
                this._camera,
                gameResources.Textures2D["tiles_test2"],
                new Point(100),
                Constants.SPRITE_SIZE_PIXELS));

            SceneManager.Instance.SetSceneAsActive<SpriteDrawTestScene>();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected sealed override void CreateDefaultConfigFiles()
        {
            AppConfigFileCreation.CreateDefaultConfigFiles
                (
                    Constants.GAME_DIR_PATH,
                    Constants.OVERRIDE_DEFAULT_AND_CUSTOM_FILES,
                    Constants.DEFAULT_INPUT_CONFIG,
                    Constants.DEFAULT_APP_SETTINGS
                );
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns>La liste des contrôleurs acceptés par le jeu</returns>
        protected sealed override IInputScheme[] GetInputSchemes()
        {
            return
            [
                new MouseInput(),
                new KeyboardInput(),
                new GamePadInput(Microsoft.Xna.Framework.Input.GamePad.MaximumGamePadCount)
            ];
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns>Le fichier de configuration des entrées</returns>
        protected sealed override InputConfigDTO GetInputConfig()
        {
            string customInputConfigPath = $"{Constants.GAME_DIR_PATH}/{Constants.DEFAULT_INPUT_CONFIG.CustomFilePath}";
            string json = JsonUtilities.ReadFile(customInputConfigPath);
            return JsonUtilities.DeserializeObject<InputConfigDTO>(json);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected sealed override WindowSettings GetWindowSettings()
        {
            string customAppSettingsConfigPath = $"{Constants.GAME_DIR_PATH}/{Constants.DEFAULT_APP_SETTINGS.CustomFilePath}";
            string json = JsonUtilities.ReadFile(customAppSettingsConfigPath);
            var settings = JsonUtilities.DeserializeObject<AppSettingsDTO>(json);
            return settings.WindowSettings;
        }

        #endregion
    }
}
