using System;
using Microsoft.Xna.Framework;
using MonoGame.ImGuiNet;

namespace Retard.UI.ViewModels
{
    /// <summary>
    /// Gère la création et destruction des éléments d'ui
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

        #region Variables d'instance

        /// <summary>
        /// Affiche les éléments d'ui à l'écran
        /// </summary>
        private ImGuiRenderer _imGuiRenderer;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        private UIManager()
        {

        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Initialise le ImGUIRenderer
        /// </summary>
        /// <param name="game">Le jeu</param>
        public void CreateImGUIRenderer(Game game)
        {
            this._imGuiRenderer = new ImGuiRenderer(game);
        }

        /// <summary>
        /// A appeler dans Game.OnLoadContent pour initaliser le ImGUIRenderer
        /// </summary>
        public void RebuildFontAtlas()
        {
            this._imGuiRenderer.RebuildFontAtlas();
        }

        #endregion
    }
}
