﻿using System;
using System.Collections.Generic;
using Arch.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Retard.Core.Models.Assets.Scene;

namespace Retard.Core.ViewModels.Scenes
{
    /// <summary>
    /// Gère l'ajout, la màj et la suppression des scènes
    /// </summary>
    public static class SceneManager
    {
        #region Propriétés

        /// <summary>
        /// <see langword="true"/> s'il n'y a aucune scène active
        /// </summary>
        public static bool IsEmpty => _activeScenes.Count == 0;

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
        /// Un ObjectPoo pour recycler les scènes déjà crées
        /// </summary>
        private static Dictionary<Type, IScene> _inactiveScenes;

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
            _activeScenes = new List<IScene>(1);
        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Initialise le SceneManager
        /// </summary>
        /// <param name="scenePoolCapacity">La taille de l'ObjectPool des scènes</param>
        /// <param name="content">Les assets du jeu</param>
        /// <param name="world">Le monde contenant les entités</param>
        /// <param name="spriteBatch">Pour afficher les sprites à l'écran</param>
        public static void Initialize(int scenePoolCapacity, ContentManager content, World world, SpriteBatch spriteBatch)
        {
            _inactiveScenes = new Dictionary<Type, IScene>(scenePoolCapacity);
            Content = content;
            World = world;
            SpriteBatch = spriteBatch;
        }

        /// <summary>
        /// Ajoute une nouvelle scène à l'ObjectPool des scènes
        /// </summary>
        /// <param name="scene">La nouvelle scène</param>
        public static void AddSceneToPool(IScene scene)
        {
            Type t = scene.GetType();
            _inactiveScenes.Add(t, scene);
            scene.OnInitialize();
            scene.OnLoadContent();
        }

        /// <summary>
        /// Ajoute une nouvelle scène à l'ObjectPool scènes actives
        /// </summary>
        /// <param name="scenes">Les nouvelles scènes</param>
        public static void AddScenesToPool(params IScene[] scenes)
        {
            foreach (IScene scene in scenes)
            {
                AddSceneToPool(scene);
            }
        }

        /// <summary>
        /// Prend une scène de l'objectPool et la place dans la liste active
        /// </summary>
        /// <typeparam name="T">Le type de la scène</typeparam>
        public static void SetSceneAsActive<T>()
        {
            Type t = typeof(T);
            _inactiveScenes.Remove(t, out IScene scene);
            _activeScenes.Add(scene);
            scene.OnSetActive();
        }

        /// <summary>
        /// Retire la scène en fin de la liste des scènes actives
        /// </summary>
        public static void RemoveLastActiveScene()
        {
            IScene scene = _activeScenes[^1];
            Type t = scene.GetType();
            _activeScenes.Remove(scene);
            _inactiveScenes.Add(t, scene);
        }

        /// <summary>
        /// Retire la scène de la liste des scènes actives
        /// </summary>
        /// <param name="scene">La scène à supprimer</param>
        public static void RemoveActiveScene(IScene scene)
        {
            Type t = scene.GetType();
            _activeScenes.Remove(scene);
            _inactiveScenes.Add(t, scene);
        }

        /// <summary>
        /// Retire la scène ainsi que toutes celles superposées de la liste des scènes actives
        /// </summary>
        /// <param name="scene">La scène à supprimer</param>
        public static void RemoveActiveAndOverlaidScenes(IScene scene)
        {
            int index = _activeScenes.IndexOf(scene);

            for (int i = _activeScenes.Count - 1; i >= index; i--)
            {
                IScene s = _activeScenes[i];
                Type t = s.GetType();
                _activeScenes.RemoveAt(i);
                _inactiveScenes.Add(t, s);
            }
        }

        /// <summary>
        /// Màj les entrées lues par chaque scène
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début de l'application</param>
        public static void UpdateInput(GameTime gameTime)
        {
            for (int i = _activeScenes.Count - 1; i >= 0; i--)
            {
                IScene scene = _activeScenes[i];
                scene.OnUpdateInput(gameTime);

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
        public static void Update(GameTime gameTime)
        {
            foreach (IScene scene in _activeScenes)
            {
                scene.OnUpdate(gameTime);
            }
        }

        /// <summary>
        /// Affiche le contenu des scènes à l'écran
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début de l'application</param>
        public static void Draw(GameTime gameTime)
        {
            int startDrawIndex = 0;

            for (int i = 0; i < _activeScenes.Count; i++)
            {
                if (_activeScenes[i].ConsumeDraw)
                {
                    startDrawIndex = i;
                }
            }

            for (int i = startDrawIndex; i < _activeScenes.Count; i++)
            {
                _activeScenes[i].OnDraw(gameTime);
            }
        }

        #endregion
    }
}
