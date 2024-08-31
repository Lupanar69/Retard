using Newtonsoft.Json;
using Retard.Core.Models.App;
using Retard.Core.Models.DTOs;

namespace Retard.Engine.Models.DTOs.App
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
            this.WindowSettings = windowSettings;
        }

        #endregion
    }
}
