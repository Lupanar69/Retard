using System.IO;
using Retard.Core.Models;
using Retard.Engine.Models.DTOs.App;
using Retard.Engine.Models.DTOs.Input;
using Retard.Engine.ViewModels.Utilities;

namespace Retard.Core.ViewModels.App
{
    /// <summary>
    /// Chargé de créer les fichiers par défaut
    /// </summary>
    public static class AppConfigFileCreation
    {
        #region Méthodes statiques publiques

        /// <summary>
        /// Crée les fichiers de config par défaut
        /// </summary>
        /// <param name="defaultInputConfig">La config des inputs par défaut</param>
        /// <param name="defaultAppSettings">Les paramètres de l'appli par défaut</param>
        /// <param name="overrideCustomFiles">Si <see langword="true"/>, écrase les fichiers créés par l'utilisateur</param>
        public static void CreateDefaultConfigFiles(InputConfigDTO defaultInputConfig, AppSettingsDTO defaultAppSettings, bool overrideCustomFiles = false)
        {
            // Chemins d'accès des fichiers de configuration

            string defaultInputConfigPath = $"{Constants.GAME_DIR_PATH}/{Constants.DEFAULT_INPUT_CONFIG_PATH}";
            string customInputConfigPath = $"{Constants.GAME_DIR_PATH}/{Constants.CUSTOM_INPUT_CONFIG_PATH}";

            string defaultAppSettingsConfigPath = $"{Constants.GAME_DIR_PATH}/{Constants.DEFAULT_APP_SETTINGS_CONFIG_PATH}";
            string customAppSettingsConfigPath = $"{Constants.GAME_DIR_PATH}/{Constants.CUSTOM_APP_SETTINGS_CONFIG_PATH}";

            JsonUtilities.CreatPathIfNotExists(defaultInputConfigPath);
            JsonUtilities.CreatPathIfNotExists(defaultAppSettingsConfigPath);

            // Crée les fichiers de config des entrées par défaut.
            // On les réécrit à chaque lancement du jeu au cas où le joueur
            // les aurait modifié.

            string defaultInputConfigJson = JsonUtilities.SerializeObject(defaultInputConfig);
            string defaultAppSettingsConfigJson = JsonUtilities.SerializeObject(defaultAppSettings);

            JsonUtilities.WriteToFile(defaultInputConfigJson, defaultInputConfigPath);
            JsonUtilities.WriteToFile(defaultAppSettingsConfigJson, defaultAppSettingsConfigPath);

            // Crée les fichiers personnalisés uniquement s'il n'existent pas,
            // pour éviter d'effacer les préférences du joueur.
            // (On fait un if pour chaque si l'un d'entre eux est supprimé mais les autres existent toujours)

            if (!File.Exists(customInputConfigPath) || overrideCustomFiles)
            {
                JsonUtilities.CreatPathIfNotExists(customInputConfigPath);
                JsonUtilities.WriteToFile(defaultInputConfigJson, customInputConfigPath);
            }

            if (!File.Exists(customAppSettingsConfigPath) || overrideCustomFiles)
            {
                JsonUtilities.CreatPathIfNotExists(customAppSettingsConfigPath);
                JsonUtilities.WriteToFile(defaultAppSettingsConfigJson, customAppSettingsConfigPath);
            }
        }

        #endregion
    }
}
