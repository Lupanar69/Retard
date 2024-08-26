using System;
using Microsoft.Xna.Framework;
using Retard.Engine.Models;

namespace Retard.Engine.ViewModels.App
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
            this._game = game;
            game.Activated += this.OnActivatedCallback;
            game.Deactivated += this.OnDeactivatedCallback;
        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void Dispose()
        {
            this._game.Activated -= this.OnActivatedCallback;
            this._game.Deactivated -= this.OnDeactivatedCallback;
        }

        /// <summary>
        /// Sets a new framerate for when the game is focused
        /// </summary>
        /// <param name="framerate">The new framerate to reach</param>
        public void SetFocusedFramerate(int framerate)
        {
            if (this._windowHasFocus)
            {
                this._game.TargetElapsedTime = TimeSpan.FromSeconds(1d / framerate);
            }

            this._userDefinedFocusedFrameRate = framerate;
        }

        /// <summary>
        /// Sets a new framerate for when the game runs in the background
        /// </summary>
        /// <param name="framerate">The new framerate to reach</param>
        public void SetUnfocusedFramerate(int framerate)
        {
            if (!this._windowHasFocus)
            {
                this._game.TargetElapsedTime = TimeSpan.FromSeconds(1d / framerate);
            }

            this._userDefinedUnfocusedFrameRate = framerate;
        }

        /// <summary>
        /// Resets the framerates to their respective default values
        /// </summary>
        public void ResetUserDefinedFrameRates()
        {
            this._userDefinedFocusedFrameRate = Constants.DEFAULT_FOCUSED_FRAMERATE;
            this._userDefinedUnfocusedFrameRate = Constants.DEFAULT_UNFOCUSED_FRAMERATE;
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
            this._windowHasFocus = true;
            this._game.TargetElapsedTime = TimeSpan.FromSeconds(1d / this._userDefinedFocusedFrameRate);
        }

        /// <summary>
        /// Appelée quand la fenêtre perd le focus
        /// </summary>
        /// <param name="sender">l'app</param>
        /// <param name="e">vide</param>
        private void OnDeactivatedCallback(object sender, EventArgs e)
        {
            this._windowHasFocus = false;
            this._game.TargetElapsedTime = TimeSpan.FromSeconds(1d / this._userDefinedUnfocusedFrameRate);
        }

        #endregion
    }
}
