using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using Retard.Core.Models;
using Retard.Core.ViewModels.App;
using Retard.Core.ViewModels.Controllers;
using Retard.Core.ViewModels.Scenes;
using Retard.Engine.ViewModels.Engine;
using Retard.Engine.ViewModels.Input;
using Retard.Tests.ViewModels.Scenes;

namespace Retard.Tests.ViewModels.Engine
{
    /// <summary>
    /// Le point d'entrée du jeu
    /// </summary>
    public static class GameEntryPoint
    {
        #region Variables statiques

        /// <summary>
        /// Pour s'abonner aux inputs
        /// </summary>
        private static InputControls _controls;

        /// <summary>
        /// Le contrôleur de la caméra du jeu
        /// </summary>
        private static OrthographicCameraController _cameraController;

        /// <summary>
        /// La caméra du jeu
        /// </summary>
        private static OrthographicCamera _camera;

        #endregion

        #region Méthodes statiques publiques

        /// <summary>
        /// Initialise la session de jeu
        /// </summary>
        public static void Initialize()
        {
            // Crée les fichiers de config spécifiques à cette application

            AppConfigFileCreation.CreateDefaultConfigFiles(
                Constants.DEFAULT_INPUT_CONFIG,
                Constants.DEFAULT_APP_SETTINGS,
                Constants.OVERRIDE_DEFAULT_AND_CUSTOM_FILES);
        }

        /// <summary>
        /// Démarre le jeu
        /// </summary>
        /// <param name="game">Le jeu</param>
        public static void Start(Game game)
        {
            _controls = new InputControls();
            _controls.GetButtonEvent("Exit").Started += (_) => { game.Exit(); };

            _camera = new OrthographicCamera(game.GraphicsDevice);
            _cameraController = new OrthographicCameraController(_camera, _controls);
            _controls.Enable();

            CreateScenes();
        }

        /// <summary>
        /// Màj à chaque frame
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début du jeu</param>
        public static void Update(GameTime gameTime)
        {

        }

        #endregion

        #region Méthodes statiques privées

        /// <summary>
        /// Crée les scènes du jeu
        /// </summary>
        private static void CreateScenes()
        {
#if TESTS
            SceneManager.AddSceneToPool(new SpriteDrawTestScene
                (
                BaseEngine.World,
                BaseEngine.SpriteBatch,
                _camera,
                BaseEngine.Content.Load<Texture2D>($"{Constants.TEXTURES_DIR_PATH_DEBUG}tiles_test2"),
                new Point(100),
                Constants.SPRITE_SIZE_PIXELS));

            SceneManager.SetSceneAsActive<SpriteDrawTestScene>();
#endif
        }

        #endregion
    }
}
