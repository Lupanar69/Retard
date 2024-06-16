namespace Retard.Core.Models
{
    /// <summary>
    /// Contient des infos sur la session en cours
    /// (pause, chargement, etc...)
    /// </summary>
    public static class GameState
    {
        #region Propriétés

        /// <summary>
        /// <see langword="true"/> si la fenêtre a le focus
        /// </summary>
        public static bool GameIsActivated { get; set; }

        #endregion
    }
}
