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
    public sealed class TestScene2 : IScene
    {
        #region Properties

        /// <inheritdoc/>
        public bool ConsumeInput { get; init; }

        /// <inheritdoc/>
        public bool ConsumeUpdate { get; init; }

        /// <inheritdoc/>
        public bool ConsumeDraw { get; init; }

        /// <inheritdoc/>
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
        public TestScene2(SpriteBatch spriteBatch, Texture2D debugTex)
        {
            this._debugTex = debugTex;
            this._spriteBatch = spriteBatch;
            this.Controls = new InputControls();
            this.Controls.GetButtonEvent("Test/Numpad8").Started += this.RemoveActiveAndOverlaidScenesCallback;
            this.Controls.GetButtonEvent("Test/Numpad2").Started += this.SetSceneAsActiveCallback;
        }

        #endregion

        #region Méthodes publiques

        /// <inheritdoc/>
        public void OnDraw(GameTime gameTime)
        {
            this._spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, null);

            this._spriteBatch.Draw(this._debugTex, Vector2.Zero, Color.White);

            this._spriteBatch.End();
        }

        #endregion

        #region Méthodes privées

        /// <summary>
        /// Retire la scène ainsi que toutes celles superposées de la liste des scènes actives
        /// </summary>
        private void RemoveActiveAndOverlaidScenesCallback(int _)
        {
            SceneManager.RemoveActiveAndOverlaidScenes(this);
        }

        /// <summary>
        /// Prend une scèe de l'objectPool et la place dans la liste active
        /// </summary>
        private void SetSceneAsActiveCallback(int _)
        {
            SceneManager.SetSceneAsActive<TestScene3>();
        }

        #endregion
    }
}
