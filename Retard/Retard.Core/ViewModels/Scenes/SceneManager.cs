using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Retard.Core.ViewModels.Scenes
{
    /// <summary>
    /// Gère l'ajout, la màj et la suppression des scènes
    /// </summary>
    public static class SceneManager
    {
        #region Variables d'instance

        /// <summary>
        /// Les scènes actives
        /// </summary>
        private static List<Scene> _activeScenes;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        static SceneManager()
        {
            SceneManager._activeScenes = new List<Scene>(1);
        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Initialise les scènes
        /// </summary>
        public static void InitializeScenes()
        {
            foreach (Scene scene in SceneManager._activeScenes)
            {
                scene.Initialize();
                scene.LoadContent();
            }
        }

        /// <summary>
        /// Ajoute une nouvelle scène à la liste
        /// </summary>
        /// <param name="scene">La nouvelle scène active</param>
        public static void AddScene(Scene scene)
        {
            SceneManager._activeScenes.Add(scene);
        }

        /// <summary>
        /// Ajoute une nouvelle scène à la liste
        /// </summary>
        /// <param name="scenes">Les nouvelles scènes actives</param>
        public static void AddScenes(params Scene[] scenes)
        {
            SceneManager._activeScenes.AddRange(scenes);
        }

        /// <summary>
        /// Retire la scène de la liste
        /// </summary>
        /// <param name="scene">La scène à supprimer</param>
        public static void RemoveScene(Scene scene)
        {
            SceneManager._activeScenes.Remove(scene);
        }

        /// <summary>
        /// Màj les entrées lues par chaque scène
        /// </summary>
        public static void UpdateSceneInputs()
        {
            for (int i = SceneManager._activeScenes.Count - 1; i >= 0; i--)
            {
                Scene scene = SceneManager._activeScenes[i];

                if (scene.ConsumeInput)
                {
                    break;
                }

                scene.UpdateInput();
            }
        }

        /// <summary>
        /// Màj la logique de chaque scène
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début de l'application</param>
        public static void UpdateScenes(GameTime gameTime)
        {
            foreach (Scene scene in SceneManager._activeScenes)
            {
                scene.Update(gameTime);
            }
        }

        /// <summary>
        /// Affiche le contenu des scènes à l'écran
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début de l'application</param>
        public static void DrawScenes(GameTime gameTime)
        {
            foreach (Scene scene in SceneManager._activeScenes)
            {
                scene.Draw(gameTime);
            }
        }

        #endregion
    }
}
