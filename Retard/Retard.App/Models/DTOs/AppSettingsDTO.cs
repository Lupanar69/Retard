using Newtonsoft.Json;
using Retard.Core.Models.DTOs;

namespace Retard.App.Models.DTOs
{
    /// <summary>
    /// Représente les données des paramètres du jeu
    /// </summary>
    public sealed class AppSettingsDTO : DataTransferObject
    {
        #region Propriétés

        /// <summary>
        /// Les paramètres de la fenêtre
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public readonly WindowSettings WindowSettings;

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
