using System;
using Microsoft.Xna.Framework;
using Retard.Core.Models;
using Retard.Engine.Models.App;
using Retard.Engine.Models.DTOs.App;
using Retard.Engine.ViewModels.Utilities;

namespace Retard.Core.ViewModels.App
{
    /// <summary>
    /// Gère les paramètres de la fenêtre du jeu
    /// </summary>
    public static class AppViewport
    {
        #region Evénements

        /// <summary>
        /// Appelé quand la résolution de la fenêtre est changée manuellement
        /// </summary>
        public static readonly EventHandler<Point> OnClientSizeChangedEvent = delegate { };

        /// <summary>
        /// Appelé quand la résolution de la fenêtre est changée par code
        /// </summary>
        public static readonly EventHandler<Point> OnViewportResolutionSetEvent = delegate { };

        #endregion

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
            AppViewport.OnClientSizeChangedEvent?.Invoke(null, AppViewport.WindowResolution);
        }

        #endregion
    }
}
