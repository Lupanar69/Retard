using System.Collections.Generic;
using Arch.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Retard.Core.ViewModels.Scenes
{
    /// <summary>
    /// Gère l'ajout, la màj et la suppression des scènes
    /// </summary>
    public static class SceneManager
    {
        #region Propriétés

        /// <summary>
        /// Pour afficher les sprites à l'écran
        /// </summary>
        public static SpriteBatch SpriteBatch { get; private set; }

        /// <summary>
        /// Le monde contenant les entités
        /// </summary>
        public static World World { get; private set; }

        /// <summary>
        /// Les assets du jeu
        /// </summary
        public static ContentManager Content { get; private set; }

        #endregion

        #region Variables d'instance

        /// <summary>
        /// Les scènes actives
        /// </summary>
        private static List<IScene> _activeScenes;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        static SceneManager()
        {
            SceneManager._activeScenes = new List<IScene>(1);
        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Initialise le SceneManager
        /// </summary>
        /// <param name="content">Les assets du jeu</param>
        /// <param name="world">Le monde contenant les entités</param>
        /// <param name="spriteBatch">Pour afficher les sprites à l'écran</param>
        public static void Initialize(ContentManager content, World world, SpriteBatch spriteBatch)
        {
            SceneManager.Content = content;
            SceneManager.World = world;
            SceneManager.SpriteBatch = spriteBatch;
        }

        /// <summary>
        /// Ajoute une nouvelle scène à la liste des scènes actives
        /// </summary>
        /// <param name="scene">La nouvelle scène active</param>
        public static void AddScene(IScene scene)
        {
            SceneManager._activeScenes.Add(scene);
            scene.Initialize();
            scene.LoadContent();
        }

        /// <summary>
        /// Ajoute une nouvelle scène à la liste des scènes actives
        /// </summary>
        /// <param name="scenes">Les nouvelles scènes actives</param>
        public static void AddScenes(params IScene[] scenes)
        {
            SceneManager._activeScenes.AddRange(scenes);

            foreach (IScene scene in scenes)
            {
                scene.Initialize();
                scene.LoadContent();
            }
        }

        /// <summary>
        /// Retire la scène en fin de la liste des scènes actives
        /// </summary>
        public static void RemoveLastScene()
        {
            SceneManager._activeScenes.RemoveAt(SceneManager._activeScenes.Count - 1);
        }

        /// <summary>
        /// Retire la scène de la liste des scènes actives
        /// </summary>
        /// <param name="scene">La scène à supprimer</param>
        /// <param name="removeOverlaidScenes"><see langword="true"/> si on doit aussi retirer les scènes suivantes</param>
        public static void RemoveScene(IScene scene, bool removeOverlaidScenes = true)
        {
            if (removeOverlaidScenes)
            {
                int index = SceneManager._activeScenes.IndexOf(scene);

                for (int i = SceneManager._activeScenes.Count - 1; i >= index; i--)
                {
                    SceneManager._activeScenes.RemoveAt(i);
                }
            }
            else
            {
                SceneManager._activeScenes.Remove(scene);
            }
        }

        /// <summary>
        /// Màj les entrées lues par chaque scène
        /// </summary>
        public static void UpdateSceneInputs()
        {
            for (int i = SceneManager._activeScenes.Count - 1; i >= 0; i--)
            {
                IScene scene = SceneManager._activeScenes[i];
                scene.UpdateInput();

                if (scene.ConsumeInput)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Màj la logique de chaque scène
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début de l'application</param>
        public static void UpdateScenes(GameTime gameTime)
        {
            foreach (IScene scene in SceneManager._activeScenes)
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
            foreach (IScene scene in SceneManager._activeScenes)
            {
                scene.Draw(gameTime);
            }
        }

        #endregion
    }
}
