using System.Collections.Generic;
using Arch.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Retard.App.Models;
using Retard.App.Models.DTOs;
using Retard.App.ViewModels;
using Retard.Cameras.Entities;
using Retard.Cameras.Models;
using Retard.Core.Models.ValueTypes;
using Retard.Core.ViewModels.Utilities;
using Retard.Engine.Models.Assets;
using Retard.Engine.ViewModels;
using Retard.Input.Models;
using Retard.Input.Models.Assets;
using Retard.Input.Models.DTOs;
using Retard.Input.ViewModels;
using Retard.SceneManagement.ViewModels;
using Retard.Tests.Models;
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
        /// L'entité de la caméra du jeu
        /// </summary>
        private readonly Entity _camE;

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

            this._camE = EntityFactory.CreateOrthographicCamera(this._world, Vector2.Zero, game.GraphicsDevice.Viewport.Bounds, RenderingLayer.Default | RenderingLayer.UI);
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

        /// <inheritdoc/>
        protected sealed override void CreateScenes(in GameResources gameResources)
        {
            SceneManager.Instance.AddSceneToPool(new SpriteDrawTestScene
                (
                this._world,
                this._spriteBatch,
                this._camE,
                gameResources.Textures2D["tiles_test2"],
                new Point(100),
                Constants.SPRITE_SIZE_PIXELS));

            SceneManager.Instance.SetSceneAsActive<SpriteDrawTestScene>();
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        protected sealed override IInputScheme[] GetInputSchemes()
        {
            return
            [
                new MouseInput(),
                new KeyboardInput(),
                new GamePadInput(Microsoft.Xna.Framework.Input.GamePad.MaximumGamePadCount)
            ];
        }

        /// <inheritdoc/>
        protected sealed override InputConfigDTO GetInputConfig()
        {
            string customInputConfigPath = $"{Constants.GAME_DIR_PATH}/{Constants.DEFAULT_INPUT_CONFIG.CustomFilePath}";
            string json = JsonUtilities.ReadFile(customInputConfigPath);
            return JsonUtilities.DeserializeObject<InputConfigDTO>(json);
        }

        /// <inheritdoc/>
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
