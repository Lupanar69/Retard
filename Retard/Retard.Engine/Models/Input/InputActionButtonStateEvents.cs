using System;

namespace Retard.Engine.Models.Input
{
    /// <summary>
    /// Les événements pour les inputs de type State
    /// (bouton maintenu)
    /// </summary>
    public struct InputActionButtonStateEvents
    {
        #region Evénements

        /// <summary>
        /// Appelé quand l'action démarre
        /// </summary>
        public event Action Started;

        /// <summary>
        /// Appelé quand l'action est en cours
        /// </summary>
        public event Action Performed;

        /// <summary>
        /// Appelé quand l'action est terminée
        /// </summary>
        public event Action Finished;

        #endregion
    }
}
