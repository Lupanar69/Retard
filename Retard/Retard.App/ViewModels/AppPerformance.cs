using System;
using Microsoft.Xna.Framework;
using Retard.Core.Models;

namespace Retard.App.ViewModels
{
    /// <summary>
    /// Gère les performance du jeu
    /// </summary>
    public struct AppPerformance : IDisposable
    {
        #region Variables d'instance

        /// <summary>
        /// Permet de modifier les paramètres du jeu
        /// </summary>
        private readonly Game _game;

        /// <summary>
        /// Cached to restore the user defined framerate
        /// each time we focus back on the game window
        /// </summary>
        private double _userDefinedFocusedFrameRate = Constants.DEFAULT_FOCUSED_FRAMERATE;

        /// <summary>
        /// Cached to restore the user defined framerate
        /// each time the game window is set in the background
        /// </summary>
        private double _userDefinedUnfocusedFrameRate = Constants.DEFAULT_UNFOCUSED_FRAMERATE;

        /// <summary>
        /// <see langword="true"/> si la fenêtre a le focus
        /// </summary>
        private bool _windowHasFocus = true;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="game">Le script de lancement du jeu</param>
        public AppPerformance(Game game)
        {
            _game = game;
            game.Activated += OnActivatedCallback;
            game.Deactivated += OnDeactivatedCallback;
        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void Dispose()
        {
            _game.Activated -= OnActivatedCallback;
            _game.Deactivated -= OnDeactivatedCallback;
        }

        /// <summary>
        /// Sets a new framerate for when the game is focused
        /// </summary>
        /// <param name="framerate">The new framerate to reach</param>
        public void SetFocusedFramerate(int framerate)
        {
            if (_windowHasFocus)
            {
                _game.TargetElapsedTime = TimeSpan.FromSeconds(1d / framerate);
            }

            _userDefinedFocusedFrameRate = framerate;
        }

        /// <summary>
        /// Sets a new framerate for when the game runs in the background
        /// </summary>
        /// <param name="framerate">The new framerate to reach</param>
        public void SetUnfocusedFramerate(int framerate)
        {
            if (!_windowHasFocus)
            {
                _game.TargetElapsedTime = TimeSpan.FromSeconds(1d / framerate);
            }

            _userDefinedUnfocusedFrameRate = framerate;
        }

        /// <summary>
        /// Resets the framerates to their respective default values
        /// </summary>
        public void ResetUserDefinedFrameRates()
        {
            _userDefinedFocusedFrameRate = Constants.DEFAULT_FOCUSED_FRAMERATE;
            _userDefinedUnfocusedFrameRate = Constants.DEFAULT_UNFOCUSED_FRAMERATE;
        }

        #endregion

        #region Méthodes privées

        /// <summary>
        /// Appelée quand la fenêtre gagne le focus
        /// </summary>
        /// <param name="sender">l'app</param>
        /// <param name="e">vide</param>
        private void OnActivatedCallback(object sender, EventArgs e)
        {
            _windowHasFocus = true;
            _game.TargetElapsedTime = TimeSpan.FromSeconds(1d / _userDefinedFocusedFrameRate);
        }

        /// <summary>
        /// Appelée quand la fenêtre perd le focus
        /// </summary>
        /// <param name="sender">l'app</param>
        /// <param name="e">vide</param>
        private void OnDeactivatedCallback(object sender, EventArgs e)
        {
            _windowHasFocus = false;
            _game.TargetElapsedTime = TimeSpan.FromSeconds(1d / _userDefinedUnfocusedFrameRate);
        }

        #endregion
    }
}
