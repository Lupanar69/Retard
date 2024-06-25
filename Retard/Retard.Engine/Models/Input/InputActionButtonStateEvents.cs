using System;
using Arch.LowLevel;
using Retard.Core.ViewModels.Input;

namespace Retard.Engine.Models.Input
{
    /// <summary>
    /// Les événements pour les inputs de type State
    /// (bouton maintenu)
    /// </summary>
    public struct InputActionButtonStateEvents
    {
        #region Propriétés

        /// <summary>
        /// Appelé quand l'action démarre
        /// </summary>
        public Action Started
        {
            get => InputManager.ActionResources.Get(in this._started);
            set => InputManager.ActionResources.Get(in this._started) = value;
        }

        /// <summary>
        /// Appelé quand l'action est en cours
        /// </summary>
        public Action Performed
        {
            get => InputManager.ActionResources.Get(in this._performed);
            set => InputManager.ActionResources.Get(in this._performed) = value;
        }

        /// <summary>
        /// Appelé quand l'action est terminée
        /// </summary>
        public Action Finished
        {
            get => InputManager.ActionResources.Get(in this._finished);
            set => InputManager.ActionResources.Get(in this._finished) = value;
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
        public InputActionButtonStateEvents(Handle<Action> started, Handle<Action> performed, Handle<Action> finished)
        {
            this._started = started;
            this._performed = performed;
            this._finished = finished;
        }

        #endregion
    }
}
