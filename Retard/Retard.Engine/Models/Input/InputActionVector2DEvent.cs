using System;
using Microsoft.Xna.Framework;

namespace Retard.Engine.Models.Input
{
    /// <summary>
    /// Contient l'event retournant la valeur
    /// de l'action
    /// </summary>
    public struct InputActionVector2DEvent
    {
        #region Evénements

        /// <summary>
        /// Appelé quand l'action est en cours
        /// </summary>
        public event Action<Vector2> Performed;

        #endregion
    }
}
