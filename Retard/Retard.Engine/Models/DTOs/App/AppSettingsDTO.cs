using Newtonsoft.Json;
using Retard.Engine.Models.App;

namespace Retard.Engine.Models.DTOs.App
{
    /// <summary>
    /// Représente les données des paramètres du jeu
    /// </summary>
    public sealed class AppSettingsDTO
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
            this.WindowSettings = windowSettings;
        }

        #endregion
    }
}
