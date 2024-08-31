using System;
using Arch.LowLevel;
using Retard.Input.ViewModels;

namespace Retard.Input.Models.Assets
{
    /// <summary>
    /// Contient l'event retournant la valeur de l'action,
    /// avec le n° du joueur
    /// </summary>
    public readonly struct InputActionVector1DHandles
    {
        #region Propriétés

        /// <summary>
        /// Appelé quand l'action est en cours
        /// </summary>
        public Action<int, float> Performed
        {
            get => InputManager.Instance.ActionVector1DResources.Get(in _performed);
            set => InputManager.Instance.ActionVector1DResources.Get(in _performed) = value;
        }

        #endregion

        #region Evénements

        /// <summary>
        /// Appelé quand l'action est en cours
        /// </summary>
        private readonly Handle<Action<int, float>> _performed;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="performed">Handle de l'action en cours de l'event</param>
        public InputActionVector1DHandles(Handle<Action<int, float>> performed)
        {
            _performed = performed;
        }

        #endregion
    }
}
