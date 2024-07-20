using Microsoft.Xna.Framework;
using Retard.Core.ViewModels.App;
using Retard.Core.ViewModels.Scenes;
using Retard.Tests.ViewModels.Scenes;
using E = Retard.Engine.ViewModels.Engine;

namespace Retard.Core
{
    /// <summary>
    /// Le point d'entrée du jeu
    /// </summary>
    public static class GameEntryPoint
    {
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
        /// Crée les scènes du jeu
        /// </summary>
        public static void CreateScenes()
        {
            SceneManager.AddSceneToPool(new OrthographicCameraScene(E.Camera));

#if TESTS
            SceneManager.AddSceneToPool(new SpriteDrawTestScene(E.Camera, new Point(100), Constants.SPRITE_SIZE_PIXELS));
            SceneManager.SetSceneAsActive<SpriteDrawTestScene>();
#endif
            SceneManager.SetSceneAsActive<OrthographicCameraScene>();
        }

        /// <summary>
        /// Màj à chaque frame
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début du jeu</param>
        public static void Update(GameTime gameTime)
        {

        }

        #endregion
    }
}
