using Microsoft.Xna.Framework;

namespace Retard.Core.Models.App
{
    /// <summary>
    /// Paramètres de la fenêtre
    /// </summary>
    public struct WindowSettings
    {
        #region Variables d'instance

        /// <summary>
        /// La résolution de la fenêtre
        /// </summary>
        public Point WindowSize;

        /// <summary>
        /// La résolution de la fenêtre
        /// </summary>
        public bool FullScreen;

        /// <summary>
        /// La résolution de la fenêtre
        /// </summary>
        public bool MouseVisible;

        /// <summary>
        /// La résolution de la fenêtre
        /// </summary>
        public bool AllowUserResizing;

        #endregion

        #region Constructeur

        public WindowSettings(Point windowSize, bool fullScreen, bool mouseVisible, bool allowUserResizing)
        {
            WindowSize = windowSize;
            FullScreen = fullScreen;
            MouseVisible = mouseVisible;
            AllowUserResizing = allowUserResizing;
        }

        #endregion
    }
}
