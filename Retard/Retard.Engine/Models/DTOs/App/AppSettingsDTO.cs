using Newtonsoft.Json;
using Retard.Engine.Models.App;

namespace Retard.Engine.Models.DTOs.App
{
    /// <summary>
    /// Représente les données des paramètres de l'application
    /// </summary>
    public sealed class AppSettingsDTO
    {
        #region Propriétés

        /// <summary>
        /// Les paramètres de la fenêtre
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public WindowSettings WindowSettings { get; private set; }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="windowSettings">Les paramètres de la fenêtre</param>
        public AppSettingsDTO(WindowSettings windowSettings)
        {
            WindowSettings = windowSettings;
        }

        #endregion
    }
}
