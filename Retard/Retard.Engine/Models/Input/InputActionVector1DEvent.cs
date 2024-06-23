using System;

namespace Retard.Engine.Models.Input
{
    /// <summary>
    /// Contient l'event retournant la valeur
    /// de l'action
    /// </summary>
    public struct InputActionVector1DEvent
    {
        #region Evénements

        /// <summary>
        /// Appelé quand l'action est en cours
        /// </summary>
        public event Action<float> Performed;

        #endregion
    }
}
