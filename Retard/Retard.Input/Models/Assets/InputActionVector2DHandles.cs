using System;
using Arch.LowLevel;
using Microsoft.Xna.Framework;
using Retard.Input.ViewModels;

namespace Retard.Input.Models.Assets
{
    /// <summary>
    /// Contient l'event retournant la valeur de l'action,
    /// avec le n° du joueur
    /// </summary>
    public readonly struct InputActionVector2DHandles
    {
        #region Propriétés

        /// <summary>
        /// Appelé quand l'action est en cours
        /// </summary>
        public Action<int, Vector2> Performed
        {
            get => InputManager.Instance.ActionVector2DResources.Get(in _performed);
            set => InputManager.Instance.ActionVector2DResources.Get(in _performed) = value;
        }


        #endregion

        #region Evénements

        /// <summary>
        /// Appelé quand l'action est en cours
        /// </summary>
        private readonly Handle<Action<int, Vector2>> _performed;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="performed">Handle de l'action en cours de l'event</param>
        public InputActionVector2DHandles(Handle<Action<int, Vector2>> performed)
        {
            _performed = performed;
        }

        #endregion
    }
}
