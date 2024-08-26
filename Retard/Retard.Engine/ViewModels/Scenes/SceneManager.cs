using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Retard.Engine.Models.Assets.Scene;

namespace Retard.Engine.ViewModels.Scenes
{
    /// TAF : Une fois qu'on sera passé à la prochaine version de Monogame,
    /// remplacer les champs et méthodes statiques par des champs et méthodes d'instance

    /// <summary>
    /// Gère l'ajout, la màj et la suppression des scènes
    /// </summary>
    public sealed class SceneManager
    {
        #region Singleton

        /// <summary>
        /// Singleton
        /// </summary>
        public static SceneManager Instance => SceneManager._instance.Value;

        /// <summary>
        /// Singleton
        /// </summary>
        private static readonly Lazy<SceneManager> _instance = new(() => new SceneManager());

        #endregion

        #region Propriétés

        /// <summary>
        /// <see langword="true"/> s'il n'y a aucune scène active
        /// </summary>
        public bool IsEmpty => this._activeScenes.Count == 0;

        #endregion

        #region Variables d'instance

        /// <summary>
        /// Un ObjectPool pour recycler les scènes déjà crées
        /// </summary>
        private readonly Dictionary<Type, IScene> _inactiveScenes;

        /// <summary>
        /// Les scènes actives
        /// </summary>
        private readonly List<IScene> _activeScenes;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        private SceneManager()
        {
            this._activeScenes = new List<IScene>(1);
            this._inactiveScenes = new Dictionary<Type, IScene>(1);
        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Ajoute une nouvelle scène à l'ObjectPool des scènes
        /// </summary>
        /// <param name="scene">La nouvelle scène</param>
        public void AddSceneToPool(IScene scene)
        {
            Type t = scene.GetType();
            this._inactiveScenes.Add(t, scene);
        }

        /// <summary>
        /// Ajoute une nouvelle scène à l'ObjectPool scènes actives
        /// </summary>
        /// <param name="scenes">Les nouvelles scènes</param>
        public void AddScenesToPool(params IScene[] scenes)
        {
            this._inactiveScenes.EnsureCapacity(scenes.Length);

            foreach (IScene scene in scenes)
            {
                this.AddSceneToPool(scene);
            }
        }

        /// <summary>
        /// Prend une scène de l'objectPool et la place dans la liste active
        /// </summary>
        /// <typeparam name="T">Le type de la scène</typeparam>
        public void SetSceneAsActive<T>()
        {
            Type t = typeof(T);
            this._inactiveScenes.Remove(t, out IScene scene);
            this._activeScenes.Add(scene);
            scene.OnSetActive();
            this.SetScenesControlsActiveState();
        }

        /// <summary>
        /// Retire la scène en fin de la liste des scènes actives
        /// </summary>
        public void RemoveLastActiveScene()
        {
            IScene scene = this._activeScenes[^1];
            scene.DisableControls();
            Type t = scene.GetType();
            this._activeScenes.Remove(scene);
            this._inactiveScenes.Add(t, scene);
            this.SetScenesControlsActiveState();
        }

        /// <summary>
        /// Retire la scène de la liste des scènes actives
        /// </summary>
        /// <param name="scene">La scène à supprimer</param>
        public void RemoveActiveScene(IScene scene)
        {
            Type t = scene.GetType();
            scene.DisableControls();
            this._activeScenes.Remove(scene);
            this._inactiveScenes.Add(t, scene);
            this.SetScenesControlsActiveState();
        }

        /// <summary>
        /// Retire la scène ainsi que toutes celles superposées de la liste des scènes actives
        /// </summary>
        /// <param name="scene">La scène à supprimer</param>
        public void RemoveActiveAndOverlaidScenes(IScene scene)
        {
            int index = this._activeScenes.IndexOf(scene);

            for (int i = this._activeScenes.Count - 1; i >= index; --i)
            {
                IScene s = this._activeScenes[i];
                Type t = s.GetType();
                s.DisableControls();
                this._activeScenes.RemoveAt(i);
                this._inactiveScenes.Add(t, s);
            }

            this.SetScenesControlsActiveState();
        }

        #endregion

        #region Méthodes internes

        /// <summary>
        /// Màj les entrées lues par chaque scène
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début du jeu</param>
        internal void UpdateInput(GameTime gameTime)
        {
            for (int i = this._activeScenes.Count - 1; i >= 0; --i)
            {
                IScene scene = this._activeScenes[i];
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
        /// <param name="gameTime">Le temps écoulé depuis le début du jeu</param>
        internal void Update(GameTime gameTime)
        {
            for (int i = this._activeScenes.Count - 1; i >= 0; --i)
            {
                IScene scene = this._activeScenes[i];
                scene.OnUpdate(gameTime);

                if (scene.ConsumeUpdate)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Affiche le contenu des scènes à l'écran
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début du jeu</param>
        internal void Draw(GameTime gameTime)
        {
            int startDrawIndex = 0;

            for (int i = 0; i < this._activeScenes.Count; ++i)
            {
                if (this._activeScenes[i].ConsumeDraw)
                {
                    startDrawIndex = i;
                }
            }

            for (int i = startDrawIndex; i < this._activeScenes.Count; ++i)
            {
                this._activeScenes[i].OnDraw(gameTime);
            }
        }

        #endregion

        #region Méthodes privées

        /// <summary>
        /// Active ou désactive les InputControls des scènes
        /// si l'un d'entre elles a <see cref="IScene.ConsumeInput"/> à <see langword="true"/>
        /// </summary>
        private void SetScenesControlsActiveState()
        {
            int endDisableIndex = 0;

            for (int i = 0; i < this._activeScenes.Count; ++i)
            {
                if (this._activeScenes[i].ConsumeInput)
                {
                    endDisableIndex = i;
                }
            }

            for (int i = 0; i < endDisableIndex; ++i)
            {
                this._activeScenes[i].DisableControls();
            }

            for (int i = endDisableIndex; i < this._activeScenes.Count; ++i)
            {
                this._activeScenes[i].EnableControls();
            }
        }

        #endregion
    }
}
