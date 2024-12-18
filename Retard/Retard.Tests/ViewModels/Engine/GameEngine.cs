﻿using System.Collections.Generic;
using FixedStrings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Retard.App.Models;
using Retard.App.Models.DTOs;
using Retard.App.ViewModels;
using Retard.Core.ViewModels.Utilities;
using Retard.Engine.Models.Assets;
using Retard.Engine.ViewModels;
using Retard.Input.Models;
using Retard.Input.Models.Assets;
using Retard.Input.Models.DTOs;
using Retard.Input.ViewModels;
using Retard.SceneManagement.ViewModels;
using Retard.Sprites.Models;
using Retard.Tests.Models;
using Retard.Tests.ViewModels.Scenes;
using Retard.UI.ViewModels;

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

            // Initialise les managers

            UIManager.Instance.CreateImGUIRenderer(game);
        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public sealed override void LoadContent()
        {
            base.LoadContent();

            // Récupère les ressources

            Dictionary<FixedString16, Texture2D> textures2D = new()
            {
                { "tiles_test2", this._content.Load<Texture2D>($"{Constants.TEXTURES_DIR_PATH_DEBUG}/tiles_test2") }
            };

            // Initialise les scènes

            GameResources gameResources = new()
            {
                Textures2D = textures2D,
            };

            this.CreateScenes(in gameResources);

            // Initialise les managers

            UIManager.Instance.RebuildFontAtlas();
        }

        #endregion

        #region Méthodes privées

        /// <inheritdoc/>
        protected sealed override void CreateScenes(in GameResources gameResources)
        {
            SceneManager.Instance.AddSceneToPool(new MultiCamTestScene
                (
                    this._world,
                    new RenderingComponents2D(this._spriteBatch, this._game.GraphicsDevice),
                    gameResources.Textures2D["tiles_test2"],
                    new Point(100),
                    Constants.SPRITE_SIZE_PIXELS,
                    this._appViewport
                )
            );

            SceneManager.Instance.SetSceneAsActive<MultiCamTestScene>();
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
