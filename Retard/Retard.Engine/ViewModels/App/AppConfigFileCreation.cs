using System.IO;
using Retard.Core.Models.DTOs;
using Retard.Core.Models.ValueTypes;
using Retard.Core.ViewModels.Utilities;

namespace Retard.Engine.ViewModels.App
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
        /// <param name="rootPath">Emplacement des dossiers</param>
        /// <param name="overrideCustomFiles">Si <see langword="true"/>, écrase les fichiers créés par l'utilisateur</param>
        /// <param name="defaultConfigFiles">Fichiers de config par défaut</param>
        public static void CreateDefaultConfigFiles(NativeString rootPath, bool overrideCustomFiles = false, params DTOFilePath[] defaultConfigFiles)
        {
            foreach (DTOFilePath item in defaultConfigFiles)
            {
                string defaultPath = $"{rootPath}/{item.DefaultFilePath}";
                string customPath = $"{rootPath}/{item.CustomFilePath}";

                JsonUtilities.CreatPathIfNotExists(defaultPath);

                // Crée les fichiers de config par défaut.
                // On les réécrit à chaque lancement du jeu au cas où le joueur
                // les aurait modifié.

                string defaultConfigJson = JsonUtilities.SerializeObject(item.DTO);
                JsonUtilities.WriteToFile(defaultConfigJson, defaultPath);

                // Crée les fichiers personnalisés uniquement s'il n'existent pas,
                // pour éviter d'effacer les préférences du joueur.
                // (On fait un if pour chaque si l'un d'entre eux est supprimé mais les autres existent toujours)

                if (!File.Exists(customPath) || overrideCustomFiles)
                {
                    JsonUtilities.CreatPathIfNotExists(customPath);
                    JsonUtilities.WriteToFile(defaultConfigJson, customPath);
                }
            }
        }

        #endregion
    }
}
