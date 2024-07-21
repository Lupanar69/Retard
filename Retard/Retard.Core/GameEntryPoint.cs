using Microsoft.Xna.Framework;
using MonoGame.Extended;
using Retard.Core.ViewModels.App;
using Retard.Core.ViewModels.Controllers;
using Retard.Core.ViewModels.Scenes;
using Retard.Engine.ViewModels.Input;
using Retard.Tests.ViewModels.Scenes;

namespace Retard.Core
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
            GameEntryPoint._controls = new InputControls();
            GameEntryPoint._controls.GetButtonEvent("Exit").Started += (_) => { game.Exit(); };

            GameEntryPoint._camera = new OrthographicCamera(game.GraphicsDevice);
            GameEntryPoint._cameraController = new OrthographicCameraController(GameEntryPoint._camera, GameEntryPoint._controls);
            GameEntryPoint._controls.Enable();

            GameEntryPoint.CreateScenes();
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
            SceneManager.AddSceneToPool(new SpriteDrawTestScene(GameEntryPoint._camera, new Point(100), Constants.SPRITE_SIZE_PIXELS));
            SceneManager.SetSceneAsActive<SpriteDrawTestScene>();
#endif
        }

        #endregion
    }
}
