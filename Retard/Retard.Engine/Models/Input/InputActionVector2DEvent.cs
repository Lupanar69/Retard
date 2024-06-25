using System;
using Arch.LowLevel;
using Microsoft.Xna.Framework;
using Retard.Core.ViewModels.Input;

namespace Retard.Engine.Models.Input
{
    /// <summary>
    /// Contient l'event retournant la valeur
    /// de l'action
    /// </summary>
    public struct InputActionVector2DEvent
    {
        #region Propriétés

        /// <summary>
        /// Appelé quand l'action est en cours
        /// </summary>
        public Action<Vector2> Performed
        {
            get => InputManager.ActionVector2DResources.Get(in this._performed);
            set => InputManager.ActionVector2DResources.Get(in this._performed) = value;
        }


        #endregion

        #region Evénements

        /// <summary>
        /// Appelé quand l'action est en cours
        /// </summary>
        private Handle<Action<Vector2>> _performed;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="performed">Handle de l'action en cours de l'event</param>
        public InputActionVector2DEvent(Handle<Action<Vector2>> performed)
        {
            this._performed = performed;
        }

        #endregion
    }
}
