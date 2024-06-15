using System.IO;
using Microsoft.Xna.Framework;
using Retard.Core.Models;
using Retard.Core.Models.Assets.Scene;
using Retard.Core.ViewModels.JSON;

namespace Retard.Core.ViewModels.Scenes
{
    /// <summary>
    /// Crée les fichiers de configuration par défaut (paramètres, input, etc.)
    /// avant de se désactiver
    /// </summary>
    public sealed class DefaultConfigFileCreationScene : IScene
    {
        #region Properties

        /// <summary>
        /// <see langword="true"/> si la scène doit bloquer les inputs 
        /// pour les scènes qui suivent
        /// (ex: une scène de pause superposée à la scène de jeu)
        /// </summary>
        public bool ConsumeInput { get; init; }

        /// <summary>
        /// <see langword="true"/> si la scène doit bloquer le rendu
        /// pour les scènes qui suivent
        /// (ex: une scène de pause superposée à la scène de jeu)
        /// </summary>
        public bool ConsumeDraw { get; init; }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        public DefaultConfigFileCreationScene()
        {

        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Chargement du contenu
        /// </summary>
        public void OnInitialize()
        {

        }

        /// <summary>
        /// Màj à chaque frame
        /// </summary>
        public void OnLoadContent()
        {

        }

        /// <summary>
        /// Appelée à chaque fois que la scène devient active
        /// </summary>
        public void OnSetActive()
        {
            // Crée les fichiers de config par défaut
            // et retire la scène

            DefaultConfigFileCreationScene.CreateDefaultConfigFiles();
            SceneManager.RemoveActiveScene(this);
        }

        /// <summary>
        /// Récupère les inputs nécessaires au fonctionnement des systèmes
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début de l'application</param>
        public void OnUpdateInput(GameTime gameTime)
        {

        }

        /// <summary>
        /// Màj à chaque frame
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début de l'application</param>
        public void OnUpdate(GameTime gameTime)
        {

        }

        /// <summary>
        /// Pour afficher des éléments à l'écran
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début de l'application</param>
        public void OnDraw(GameTime gameTime)
        {

        }

        #endregion

        #region Méthodes statiques privées

        /// <summary>
        /// Crée les fichiers de config par défaut
        /// </summary>
        private static void CreateDefaultConfigFiles()
        {
            // Crée les fichiers de config des entrées par défaut.
            // On les réécrit à chaque lancement du jeu au cas où le joueur
            // les aurait modifié.

            string defaultInputConfigPath = $"{Constants.GAME_DIR_PATH}/{Constants.DEFAULT_INPUT_CONFIG_PATH}";

            JsonUtilities.CreatPathIfNotExists(defaultInputConfigPath);

            string defaultInputConfigJson = JsonUtilities.SerializeObject(Constants.DEFAULT_INPUT_CONFIG);

            JsonUtilities.WriteToFile(defaultInputConfigJson, defaultInputConfigPath);

            // Crée les fichiers personnalisés uniquement s'il n'existent pas,
            // pour éviter d'effacer les préférences du joueur

            string customInputConfigPath = $"{Constants.GAME_DIR_PATH}/{Constants.CUSTOM_INPUT_CONFIG_PATH}";

            if (!File.Exists(customInputConfigPath))
            {
                JsonUtilities.CreatPathIfNotExists(customInputConfigPath);

                JsonUtilities.WriteToFile(defaultInputConfigJson, customInputConfigPath);

                //string json = JsonUtilities.ReadFile(customInputConfigPath);
                //var config = JsonUtilities.DeserializeObject<InputConfigDTO>(json);
            }
        }

        #endregion
    }
}
