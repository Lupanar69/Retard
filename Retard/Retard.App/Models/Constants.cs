namespace Retard.Core.Models
{
    /// <summary>
    /// Les constantes partagées dans tout le projet
    /// </summary>
    internal static class Constants
    {
        #region Constants

        /// <summary>
        /// Le framerate par défaut de l'appli si active
        /// </summary>
        internal const double DEFAULT_FOCUSED_FRAMERATE = 60d;

        /// <summary>
        /// Le framerate par défaut de l'appli si inactive
        /// (ne peut pas être plus bas que 2 FPS)
        /// </summary>
        internal const double DEFAULT_UNFOCUSED_FRAMERATE = 2d;

        #endregion
    }
}