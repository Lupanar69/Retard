using System;
using Microsoft.Xna.Framework;
using Retard.Core.Models;
using Retard.Core.ViewModels.JSON;
using Retard.Engine.Models.App;
using Retard.Engine.Models.DTOs.App;

namespace Retard.Core.ViewModels.App
{
    /// <summary>
    /// Gère les paramètres de la fenêtre de l'application
    /// </summary>
    public static class AppViewport
    {
        #region Propriétés

        /// <summary>
        /// Les dimensions de la fenêtre
        /// </summary>
        public static Point WindowResolution
        {
            get;
            private set;
        }

        #endregion

        #region Variables statiques

        /// <summary>
        /// Permet de modifier les paramètres du jeu
        /// </summary>
        private static Game _game;

        /// <summary>
        /// Permet de modifier les paramètres de la fenêtre
        /// </summary>
        private static GraphicsDeviceManager _graphicsDeviceManager;

        #endregion

        #region Evénements

        /// <summary>
        /// Appelé quand la résolution de la fenêtre est changée manuellement
        /// </summary>
        internal static EventHandler OnClientSizeChangedEvent = delegate { };

        /// <summary>
        /// Appelé quand la résolution de la fenêtre est changée par code
        /// </summary>
        internal static EventHandler<Point> OnViewportResolutionSetEvent = delegate { };

        #endregion

        #region Méthodes statiques publiques

        /// <summary>
        /// Initialise le script et la fenêtre
        /// </summary>
        /// <param name="game">Le script de lancement du jeu</param>
        public static void Initialize(Game game)
        {
            AppViewport._game = game;
            AppViewport._graphicsDeviceManager = new GraphicsDeviceManager(game);

            string customAppSettingsConfigPath = $"{Constants.GAME_DIR_PATH}/{Constants.CUSTOM_APP_SETTINGS_CONFIG_PATH}";
            string json = JsonUtilities.ReadFile(customAppSettingsConfigPath);
            var config = JsonUtilities.DeserializeObject<AppSettingsDTO>(json);
            WindowSettings ws = config.WindowSettings;

            AppViewport.SetViewportResolution(ws.WindowSize, ws.FullScreen);
            AppViewport.SetGameProperties(ws.MouseVisible, ws.AllowUserResizing);

            game.Window.ClientSizeChanged += AppViewport.OnClientSizeChangedCallback;
        }

        /// <summary>
        /// Assigne une nouvelle résolution à la fenêtre
        /// </summary>
        /// <param name="windowResolution">La nouvelle résolution de la fenêtre</param>
        /// <param name="fullScreen"><see langword="true"/> pour passer la fenêtre en plein écran</param>
        public static void SetViewportResolution(Point windowResolution, bool fullScreen)
        {
            AppViewport._graphicsDeviceManager.PreferredBackBufferWidth = windowResolution.X;
            AppViewport._graphicsDeviceManager.PreferredBackBufferHeight = windowResolution.Y;
            AppViewport._graphicsDeviceManager.IsFullScreen = fullScreen;
            AppViewport._graphicsDeviceManager.ApplyChanges();

            AppViewport.WindowResolution = windowResolution;
            AppViewport.OnViewportResolutionSetEvent?.Invoke(null, windowResolution);
        }

        /// <summary>
        /// Assigne les paramètres de la fenêtre
        /// </summary>
        /// <param name="mouseVisible"><see langword="true"/> si la souris doit rester visible</param>
        /// <param name="allowUserResizing"><see langword="true"/> si le joueur peut redimensionner la fenêtre manuellement</param>
        public static void SetGameProperties(bool mouseVisible = true, bool allowUserResizing = true)
        {
            AppViewport._game.IsMouseVisible = mouseVisible;
            AppViewport._game.Window.AllowUserResizing = allowUserResizing;
        }

        #endregion

        #region Méthodes statiques privées

        /// <summary>
        /// Appelé quand la résolution de la fenêtre est changée manuellement
        /// </summary>
        /// <param name="sender">l'app</param>
        /// <param name="e">vide</param>
        private static void OnClientSizeChangedCallback(object sender, EventArgs e)
        {
            AppViewport.WindowResolution = new Point(AppViewport._game.GraphicsDevice.Viewport.Width, AppViewport._game.GraphicsDevice.Viewport.Height);
        }

        #endregion
    }
}
