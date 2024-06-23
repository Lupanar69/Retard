using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace Retard.Engine.Models.App
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
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public Point WindowSize;

        /// <summary>
        /// La résolution de la fenêtre
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public bool FullScreen;

        /// <summary>
        /// La résolution de la fenêtre
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public bool MouseVisible;

        /// <summary>
        /// La résolution de la fenêtre
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
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
