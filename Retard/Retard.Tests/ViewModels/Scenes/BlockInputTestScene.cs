using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Retard.Core.Models;
using Retard.Core.Models.Assets.Scene;
using Retard.Core.ViewModels.Scenes;
using Retard.Engine.ViewModels.Input;
using E = Retard.Engine.ViewModels.GameEngine;

namespace Retard.Tests.ViewModels.Scenes
{
    /// <summary>
    /// Scène de test pour vérifier qu'elle bloque bien les entrées
    /// pour les scènes la suivant dans la liste
    /// </summary>
    public sealed class BlockInputTestScene : IScene
    {
        #region Properties

        ///<inheritdoc/>
        public bool ConsumeInput { get; init; }

        ///<inheritdoc/>
        public bool ConsumeUpdate { get; init; }

        ///<inheritdoc/>
        public bool ConsumeDraw { get; init; }

        ///<inheritdoc/>
        public InputControls Controls { get; init; }

        #endregion

        #region Variables d'instance

        /// <summary>
        /// Texture de test
        /// </summary>
        private readonly Texture2D _debugTex;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        public BlockInputTestScene()
        {
            this.ConsumeInput = true;
            this._debugTex = E.Content.Load<Texture2D>($"{Constants.TEXTURES_DIR_PATH_DEBUG}tiles_test2");

            this.Controls = new InputControls();
            this.Controls.GetButtonEvent("Test/Enter").Started += this.RemoveActiveSceneCallback;
        }

        #endregion

        #region Méthodes publiques

        ///<inheritdoc/>
        public void OnDraw(GameTime gameTime)
        {
            SceneManager.SpriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, null);

            SceneManager.SpriteBatch.Draw(this._debugTex, Vector2.Zero, Color.White);

            SceneManager.SpriteBatch.End();
        }

        #endregion

        #region Méthodes privées

        /// <summary>
        /// Retire la scène de la liste des scènes actives
        /// </summary>
        public void RemoveActiveSceneCallback(int _)
        {
            SceneManager.RemoveActiveScene(this);
        }

        #endregion
    }
}
