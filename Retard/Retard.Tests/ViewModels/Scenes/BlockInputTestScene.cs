using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Retard.Core.Models.Assets.Scene;
using Retard.Core.ViewModels.Scenes;
using Retard.Engine.ViewModels.Input;

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
        /// Pour afficher les sprites à l'écran
        /// </summary>
        private readonly SpriteBatch _spriteBatch;

        /// <summary>
        /// Texture de test
        /// </summary>
        private readonly Texture2D _debugTex;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="spriteBatch">Pour afficher les sprites à l'écran</param>
        /// <param name="debugTex">La texture de debug</param>
        public BlockInputTestScene(SpriteBatch spriteBatch, Texture2D debugTex)
        {
            this._debugTex = debugTex;
            this._spriteBatch = spriteBatch;

            this.ConsumeInput = true;
            this.Controls = new InputControls();
            this.Controls.GetButtonEvent("Test/Enter").Started += this.RemoveActiveSceneCallback;
        }

        #endregion

        #region Méthodes publiques

        ///<inheritdoc/>
        public void OnDraw(GameTime gameTime)
        {
            this._spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, null);

            this._spriteBatch.Draw(this._debugTex, Vector2.Zero, Color.White);

            this._spriteBatch.End();
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
