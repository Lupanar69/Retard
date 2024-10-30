using System;

namespace Retard.UI.ViewModels
{
    /// <summary>
    /// Gère la création et gestions des éléments d'UI
    /// </summary>
    public sealed class UIManager
    {
        #region Singleton

        /// <summary>
        /// Singleton
        /// </summary>
        public static UIManager Instance => UIManager._instance.Value;

        /// <summary>
        /// Singleton
        /// </summary>
        private static readonly Lazy<UIManager> _instance = new(() => new UIManager());

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        private UIManager()
        {

        }

        #endregion
    }
}
