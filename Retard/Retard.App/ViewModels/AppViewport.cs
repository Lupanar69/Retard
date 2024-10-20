using System;
using Microsoft.Xna.Framework;
using Retard.App.Models;

namespace Retard.App.ViewModels
{
    /// <summary>
    /// Gère les paramètres de la fenêtre du jeu
    /// </summary>
    public sealed class AppViewport : IDisposable
    {
        #region Evénements

        /// <summary>
        /// Appelé quand la résolution de la fenêtre est changée manuellement
        /// </summary>
        public EventHandler<Point> OnWindowResolutionSetEvent = static delegate { };

        #endregion

        #region Propriétés

        /// <summary>
        /// Les dimensions de la fenêtre
        /// </summary>
        public Point WindowResolution
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
            this._game = game;
            this._graphicsDeviceManager = graphicsDeviceManager;
            this.WindowResolution = new Point(game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);

            this.SetViewportResolution(ws.WindowSize, ws.FullScreen);
            this.SetGameProperties(ws.MouseVisible, ws.AllowUserResizing);

            game.Window.ClientSizeChanged += this.OnWindowResolutionSetCallback;
        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void Dispose()
        {
            this._game.Window.ClientSizeChanged -= this.OnWindowResolutionSetCallback;
        }

        /// <summary>
        /// Assigne une nouvelle résolution à la fenêtre
        /// </summary>
        /// <param name="windowResolution">La nouvelle résolution de la fenêtre</param>
        /// <param name="fullScreen"><see langword="true"/> pour passer la fenêtre en plein écran</param>
        public void SetViewportResolution(Point windowResolution, bool fullScreen)
        {
            this._graphicsDeviceManager.PreferredBackBufferWidth = windowResolution.X;
            this._graphicsDeviceManager.PreferredBackBufferHeight = windowResolution.Y;
            this._graphicsDeviceManager.IsFullScreen = fullScreen;
            this._graphicsDeviceManager.ApplyChanges();

            this.WindowResolution = windowResolution;
            this.OnWindowResolutionSetEvent?.Invoke(null, windowResolution);
        }

        /// <summary>
        /// Assigne les paramètres de la fenêtre
        /// </summary>
        /// <param name="mouseVisible"><see langword="true"/> si la souris doit rester visible</param>
        /// <param name="allowUserResizing"><see langword="true"/> si le joueur peut redimensionner la fenêtre manuellement</param>
        public void SetGameProperties(bool mouseVisible = true, bool allowUserResizing = true)
        {
            this._game.IsMouseVisible = mouseVisible;
            this._game.Window.AllowUserResizing = allowUserResizing;
        }

        #endregion

        #region Méthodes privées

        /// <summary>
        /// Appelé quand la résolution de la fenêtre est changée manuellement
        /// </summary>
        /// <param name="sender">L'objet appelant</param>
        /// <param name="e">vide</param>
        private void OnWindowResolutionSetCallback(object sender, EventArgs e)
        {
            this.WindowResolution = new Point(this._game.GraphicsDevice.Viewport.Width, this._game.GraphicsDevice.Viewport.Height);
            this.OnWindowResolutionSetEvent?.Invoke(sender, this.WindowResolution);
        }

        #endregion
    }
}
