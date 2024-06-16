using Newtonsoft.Json;

namespace Retard.Core.Models.DTOs.App
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
            this.WindowSettings = windowSettings;
        }

        #endregion
    }
}
