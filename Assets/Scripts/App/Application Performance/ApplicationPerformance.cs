using UnityEngine;

namespace Assets.Scripts.App
{
    /// <summary>
    /// Allows to edit the application's
    /// performance settings (fps, memory, etc.)
    /// </summary>
    public static class ApplicationPerformance
    {
        #region Constants

        private static readonly int DEFAULT_FOCUSED_FRAMERATE = (int)Screen.currentResolution.refreshRateRatio.value;
        private const int DEFAULT_UNFOCUSED_FRAMERATE = 1;

        #endregion

        #region Static variables

        /// <summary>
        /// Cached to restore the user defined framerate
        /// each time we focus back on the game window
        /// </summary>
        private static int _userDefinedFocusedFrameRate = DEFAULT_FOCUSED_FRAMERATE;

        /// <summary>
        /// Cached to restore the user defined framerate
        /// each time the game window is set in the background
        /// </summary>
        private static int _userDefinedUnfocusedFrameRate = DEFAULT_UNFOCUSED_FRAMERATE;

        #endregion

        #region Public methods

        /// <summary>
        /// Sets a new framerate for when the game is focused
        /// </summary>
        /// <param name="framerate">The new framerate to reach</param>
        public static void SetFocusedFramerate(int framerate)
        {
            Application.targetFrameRate = framerate;
            _userDefinedFocusedFrameRate = framerate;
        }

        /// <summary>
        /// Sets a new framerate for when the game runs in the background
        /// </summary>
        /// <param name="framerate">The new framerate to reach</param>
        public static void SetUnfocusedFramerate(int framerate)
        {
            _userDefinedUnfocusedFrameRate = framerate;
        }

        /// <summary>
        /// Resets the framerates to their respective default values
        /// </summary>
        public static void ResetUserDefinedFrameRates()
        {
            _userDefinedFocusedFrameRate = DEFAULT_FOCUSED_FRAMERATE;
            _userDefinedUnfocusedFrameRate = DEFAULT_UNFOCUSED_FRAMERATE;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Called before the splash screen appears to initialize the settings
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        private static void OnBeforeSplashScreen()
        {
            SetFocusedFramerate(DEFAULT_FOCUSED_FRAMERATE);
            SetUnfocusedFramerate(DEFAULT_UNFOCUSED_FRAMERATE);
            Application.lowMemory += OnApplicationLowMemory;
            Application.focusChanged += OnApplicationFocusChanged;
        }

        /// <summary>
        /// Called when the application loses or regains focus
        /// </summary>
        /// <param name="focus">TRUE is the application is focus, FALSE if it runs in the background</param>
        private static void OnApplicationFocusChanged(bool focus)
        {
            Application.targetFrameRate = focus ? _userDefinedFocusedFrameRate : _userDefinedUnfocusedFrameRate;
        }

        /// <summary>
        /// Called when the applications runs out of memory
        /// </summary>
        private static void OnApplicationLowMemory()
        {
            // Remove all assets and scripts with no references.
            // Also calls GC.Collect() internally.

            Resources.UnloadUnusedAssets();
        }

        #endregion
    }
}