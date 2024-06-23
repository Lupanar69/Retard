using System.IO;
using Retard.Core.Models;
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
        /// Initialise les fichiers de config
        /// </summary>
        public static void Initialize()
        {
            AppConfigFileCreation.CreateDefaultConfigFiles();
        }

        #endregion

        #region Méthodes statiques privées

        /// <summary>
        /// Crée les fichiers de config par défaut
        /// </summary>
        private static void CreateDefaultConfigFiles()
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

            string defaultInputConfigJson = JsonUtilities.SerializeObject(Constants.DEFAULT_INPUT_CONFIG);
            string defaultAppSettingsConfigJson = JsonUtilities.SerializeObject(Constants.DEFAULT_APP_SETTINGS);

            JsonUtilities.WriteToFile(defaultInputConfigJson, defaultInputConfigPath);
            JsonUtilities.WriteToFile(defaultAppSettingsConfigJson, defaultAppSettingsConfigPath);

            // Crée les fichiers personnalisés uniquement s'il n'existent pas,
            // pour éviter d'effacer les préférences du joueur.
            // (On fait un if pour chaque si l'un d'entre eux est supprimé mais les autres existent toujours)

            if (!File.Exists(customInputConfigPath) || Constants.OVERRIDE_DEFAULT_AND_CUSTOM_FILES)
            {
                JsonUtilities.CreatPathIfNotExists(customInputConfigPath);
                JsonUtilities.WriteToFile(defaultInputConfigJson, customInputConfigPath);
            }

            if (!File.Exists(customAppSettingsConfigPath) || Constants.OVERRIDE_DEFAULT_AND_CUSTOM_FILES)
            {
                JsonUtilities.CreatPathIfNotExists(customAppSettingsConfigPath);
                JsonUtilities.WriteToFile(defaultAppSettingsConfigJson, customAppSettingsConfigPath);
            }
        }

        #endregion
    }
}
