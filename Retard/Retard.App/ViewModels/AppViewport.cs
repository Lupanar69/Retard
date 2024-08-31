using System;
using Microsoft.Xna.Framework;
using Retard.App.Models;

namespace Retard.App.ViewModels
{
    /// <summary>
    /// Gère les paramètres de la fenêtre du jeu
    /// </summary>
    public struct AppViewport : IDisposable
    {
        #region Evénements

        /// <summary>
        /// Appelé quand la résolution de la fenêtre est changée manuellement
        /// </summary>
        public readonly EventHandler<Point> OnClientSizeChangedEvent = delegate { };

        /// <summary>
        /// Appelé quand la résolution de la fenêtre est changée par code
        /// </summary>
        public readonly EventHandler<Point> OnViewportResolutionSetEvent = delegate { };

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

        #region Variables d'instance

        /// <summary>
        /// Permet de modifier les paramètres du jeu
        /// </summary>
        private readonly Game _game;

        /// <summary>
        /// Permet de modifier les paramètres de la fenêtre
        /// </summary>
        private readonly GraphicsDeviceManager _graphicsDeviceManager;

        #endregion

        #region Constructeur

        /// <summary>
        /// Initialise le script et la fenêtre
        /// </summary>
        /// <param name="game">Le script de lancement du jeu</param>
        /// <param name="graphicsDeviceManager">Configurateur des paramètres de la fenêtre du jeu</param>
        /// <param name="ws">Les paramètres de la fenêtre</param>
        public AppViewport(Game game, GraphicsDeviceManager graphicsDeviceManager, WindowSettings ws)
        {
            _game = game;
            _graphicsDeviceManager = graphicsDeviceManager;
            WindowResolution = new Point(game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);

            SetViewportResolution(ws.WindowSize, ws.FullScreen);
            SetGameProperties(ws.MouseVisible, ws.AllowUserResizing);

            game.Window.ClientSizeChanged += OnClientSizeChangedCallback;
        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void Dispose()
        {
            _game.Window.ClientSizeChanged -= OnClientSizeChangedCallback;
        }

        /// <summary>
        /// Assigne une nouvelle résolution à la fenêtre
        /// </summary>
        /// <param name="windowResolution">La nouvelle résolution de la fenêtre</param>
        /// <param name="fullScreen"><see langword="true"/> pour passer la fenêtre en plein écran</param>
        public void SetViewportResolution(Point windowResolution, bool fullScreen)
        {
            _graphicsDeviceManager.PreferredBackBufferWidth = windowResolution.X;
            _graphicsDeviceManager.PreferredBackBufferHeight = windowResolution.Y;
            _graphicsDeviceManager.IsFullScreen = fullScreen;
            _graphicsDeviceManager.ApplyChanges();

            WindowResolution = windowResolution;
            OnViewportResolutionSetEvent?.Invoke(null, windowResolution);
        }

        /// <summary>
        /// Assigne les paramètres de la fenêtre
        /// </summary>
        /// <param name="mouseVisible"><see langword="true"/> si la souris doit rester visible</param>
        /// <param name="allowUserResizing"><see langword="true"/> si le joueur peut redimensionner la fenêtre manuellement</param>
        public void SetGameProperties(bool mouseVisible = true, bool allowUserResizing = true)
        {
            _game.IsMouseVisible = mouseVisible;
            _game.Window.AllowUserResizing = allowUserResizing;
        }

        #endregion

        #region Méthodes privées

        /// <summary>
        /// Appelé quand la résolution de la fenêtre est changée manuellement
        /// </summary>
        /// <param name="sender">l'app</param>
        /// <param name="e">vide</param>
        private void OnClientSizeChangedCallback(object sender, EventArgs e)
        {
            WindowResolution = new Point(_game.GraphicsDevice.Viewport.Width, _game.GraphicsDevice.Viewport.Height);
            OnClientSizeChangedEvent?.Invoke(null, WindowResolution);
        }

        #endregion
    }
}
