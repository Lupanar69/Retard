using System;
using Arch.LowLevel;
using Retard.Input.ViewModels;

namespace Retard.Input.Models.Assets
{
    /// <summary>
    /// Les événements pour les inputs de type ButtonState,
    /// avec le n° du joueur
    /// </summary>
    public readonly struct InputActionButtonStateHandles
    {
        #region Propriétés

        /// <summary>
        /// Appelé quand l'action démarre
        /// </summary>
        public Action<int> Started
        {
            get => InputManager.Instance.ActionButtonResources.Get(in _started);
            set => InputManager.Instance.ActionButtonResources.Get(in _started) = value;
        }

        /// <summary>
        /// Appelé quand l'action est en cours
        /// </summary>
        public Action<int> Performed
        {
            get => InputManager.Instance.ActionButtonResources.Get(in _performed);
            set => InputManager.Instance.ActionButtonResources.Get(in _performed) = value;
        }

        /// <summary>
        /// Appelé quand l'action est terminée
        /// </summary>
        public Action<int> Finished
        {
            get => InputManager.Instance.ActionButtonResources.Get(in _finished);
            set => InputManager.Instance.ActionButtonResources.Get(in _finished) = value;
        }

        #endregion

        #region Evénements

        /// <summary>
        /// Appelé quand l'action démarre
        /// </summary>
        private readonly Handle<Action<int>> _started;

        /// <summary>
        /// Appelé quand l'action est en cours
        /// </summary>
        private readonly Handle<Action<int>> _performed;

        /// <summary>
        /// Appelé quand l'action est terminée
        /// </summary>
        private readonly Handle<Action<int>> _finished;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="started">Handle de l'action de début de l'event</param>
        /// <param name="performed">Handle de l'action en cours de l'event</param>
        /// <param name="finished">Handle de l'action de fin de l'event</param>
        public InputActionButtonStateHandles(Handle<Action<int>> started, Handle<Action<int>> performed, Handle<Action<int>> finished)
        {
            _started = started;
            _performed = performed;
            _finished = finished;
        }

        #endregion
    }
}
