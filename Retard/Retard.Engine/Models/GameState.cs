using System;

namespace Retard.Core.Models
{
    /// <summary>
    /// Contient des infos sur la session en cours
    /// (pause, chargement, etc...)
    /// </summary>
    public static class GameState
    {
        #region Evénements

        /// <summary>
        /// Appelé quand la fenêtre gagne le focus
        /// </summary>
        public static EventHandler<EventArgs> OnFocusEvent = delegate { };

        /// <summary>
        /// Appelé quand la fenêtre perd le focus
        /// </summary>
        public static EventHandler<EventArgs> OnFocusLostEvent = delegate { };

        #endregion
    }
}
