using System;
using Microsoft.Xna.Framework;
using Retard.Core.Models;

namespace Retard.Core.ViewModels.App
{
    /// <summary>
    /// Gère les performance du jeu
    /// </summary>
    public static class AppPerformance
    {
        #region Variables statiques

        /// <summary>
        /// Permet de modifier les paramètres du jeu
        /// </summary>
        private static Game _game;

        /// <summary>
        /// Cached to restore the user defined framerate
        /// each time we focus back on the game window
        /// </summary>
        private static int _userDefinedFocusedFrameRate = Constants.DEFAULT_FOCUSED_FRAMERATE;

        /// <summary>
        /// Cached to restore the user defined framerate
        /// each time the game window is set in the background
        /// </summary>
        private static int _userDefinedUnfocusedFrameRate = Constants.DEFAULT_UNFOCUSED_FRAMERATE;

        /// <summary>
        /// <see langword="true"/> si la fenêtre a le focus
        /// </summary>
        private static bool _windowHasFocus;

        #endregion

        #region Méthodes statiques publiques

        /// <summary>
        /// Initialise le script
        /// </summary>
        /// <param name="game">Le script de lancement du jeu</param>
        public static void Initialize(Game game)
        {
            AppPerformance._game = game;
            game.Activated += AppPerformance.OnActivatedCallback;
            game.Deactivated += AppPerformance.OnDeactivatedCallback;
        }

        /// <summary>
        /// Sets a new framerate for when the game is focused
        /// </summary>
        /// <param name="framerate">The new framerate to reach</param>
        public static void SetFocusedFramerate(int framerate)
        {
            if (AppPerformance._windowHasFocus)
            {
                AppPerformance._game.TargetElapsedTime = TimeSpan.FromSeconds(1d / (double)framerate);
            }

            AppPerformance._userDefinedFocusedFrameRate = framerate;
        }

        /// <summary>
        /// Sets a new framerate for when the game runs in the background
        /// </summary>
        /// <param name="framerate">The new framerate to reach</param>
        public static void SetUnfocusedFramerate(int framerate)
        {
            if (!AppPerformance._windowHasFocus)
            {
                AppPerformance._game.TargetElapsedTime = TimeSpan.FromSeconds(1d / (double)framerate);
            }

            AppPerformance._userDefinedUnfocusedFrameRate = framerate;
        }

        /// <summary>
        /// Resets the framerates to their respective default values
        /// </summary>
        public static void ResetUserDefinedFrameRates()
        {
            AppPerformance._userDefinedFocusedFrameRate = Constants.DEFAULT_FOCUSED_FRAMERATE;
            AppPerformance._userDefinedUnfocusedFrameRate = Constants.DEFAULT_UNFOCUSED_FRAMERATE;
        }

        #endregion

        #region Méthodes statiques privées

        /// <summary>
        /// Appelée quand la fenêtre gagne le focus
        /// </summary>
        /// <param name="sender">l'app</param>
        /// <param name="e">vide</param>
        private static void OnActivatedCallback(object sender, EventArgs e)
        {
            AppPerformance._windowHasFocus = true;
            AppPerformance._game.TargetElapsedTime = TimeSpan.FromSeconds(1d / (double)AppPerformance._userDefinedFocusedFrameRate);
        }

        /// <summary>
        /// Appelée quand la fenêtre perd le focus
        /// </summary>
        /// <param name="sender">l'app</param>
        /// <param name="e">vide</param>
        private static void OnDeactivatedCallback(object sender, EventArgs e)
        {
            AppPerformance._windowHasFocus = false;
            AppPerformance._game.TargetElapsedTime = TimeSpan.FromSeconds(1d / (double)AppPerformance._userDefinedUnfocusedFrameRate);
        }

        #endregion
    }
}
