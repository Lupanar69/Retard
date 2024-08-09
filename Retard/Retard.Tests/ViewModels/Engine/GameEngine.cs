using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using Retard.Core.Models;
using Retard.Core.Models.Assets.Input;
using Retard.Core.Models.ValueTypes;
using Retard.Core.ViewModels.App;
using Retard.Core.ViewModels.Controllers;
using Retard.Core.ViewModels.Input;
using Retard.Engine.Models;
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

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override void CreateDefaultConfigFiles()
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
        protected override IInputScheme[] GetInputSchemes()
        {
            return new IInputScheme[3]
            {
                new MouseInput(),
                new KeyboardInput(),
                new GamePadInput(GamePad.MaximumGamePadCount)
            };
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns>Le fichier de configuration des entrées</returns>
        protected override InputConfigDTO GetInputConfig()
        {
            string customInputConfigPath = $"{Constants.GAME_DIR_PATH}/{Constants.DEFAULT_INPUT_CONFIG.CustomFilePath}";
            string json = JsonUtilities.ReadFile(customInputConfigPath);
            return JsonUtilities.DeserializeObject<InputConfigDTO>(json);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override WindowSettings GetWindowSettings()
        {
            string customAppSettingsConfigPath = $"{Constants.GAME_DIR_PATH}/{Constants.DEFAULT_APP_SETTINGS.CustomFilePath}";
            string json = JsonUtilities.ReadFile(customAppSettingsConfigPath);
            var settings = JsonUtilities.DeserializeObject<AppSettingsDTO>(json);
            return settings.WindowSettings;
        }

        #endregion
    }
}
