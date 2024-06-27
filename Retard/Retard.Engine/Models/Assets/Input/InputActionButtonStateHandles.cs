using System;
using Arch.LowLevel;
using Retard.Core.ViewModels.Input;

namespace Retard.Engine.Models.Assets.Input
{
    /// <summary>
    /// Les événements pour les inputs de type State
    /// (bouton maintenu)
    /// </summary>
    public struct InputActionButtonStateHandles
    {
        #region Propriétés

        /// <summary>
        /// Appelé quand l'action démarre
        /// </summary>
        public Action Started
        {
            get => InputManager.ActionResources.Get(in _started);
            set => InputManager.ActionResources.Get(in _started) = value;
        }

        /// <summary>
        /// Appelé quand l'action est en cours
        /// </summary>
        public Action Performed
        {
            get => InputManager.ActionResources.Get(in _performed);
            set => InputManager.ActionResources.Get(in _performed) = value;
        }

        /// <summary>
        /// Appelé quand l'action est terminée
        /// </summary>
        public Action Finished
        {
            get => InputManager.ActionResources.Get(in _finished);
            set => InputManager.ActionResources.Get(in _finished) = value;
        }

        #endregion

        #region Evénements

        /// <summary>
        /// Appelé quand l'action démarre
        /// </summary>
        private Handle<Action> _started;

        /// <summary>
        /// Appelé quand l'action est en cours
        /// </summary>
        private Handle<Action> _performed;

        /// <summary>
        /// Appelé quand l'action est terminée
        /// </summary>
        private Handle<Action> _finished;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="started">Handle de l'action de début de l'event</param>
        /// <param name="performed">Handle de l'action en cours de l'event</param>
        /// <param name="finished">Handle de l'action de fin de l'event</param>
        public InputActionButtonStateHandles(Handle<Action> started, Handle<Action> performed, Handle<Action> finished)
        {
            _started = started;
            _performed = performed;
            _finished = finished;
        }

        #endregion
    }
}
