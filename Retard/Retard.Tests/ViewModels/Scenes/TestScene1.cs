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
    public sealed class TestScene1 : IScene
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
        /// Texture de test
        /// </summary>
        private readonly Texture2D _debugTex;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        public TestScene1()
        {
            this._debugTex = E.Content.Load<Texture2D>($"{Constants.TEXTURES_DIR_PATH_DEBUG}tiles_test2");
            this.Controls = new InputControls();
            this.Controls.GetButtonEvent("Test/Numpad7").Started += this.RemoveActiveAndOverlaidScenesCallback;
            this.Controls.GetButtonEvent("Test/Numpad1").Started += this.SetSceneAsActiveCallback;
        }

        #endregion

        #region Méthodes publiques

        /// <inheritdoc/>
        public void OnDraw(GameTime gameTime)
        {
            SceneManager.SpriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, null);

            SceneManager.SpriteBatch.Draw(this._debugTex, Vector2.Zero, Color.White);

            SceneManager.SpriteBatch.End();
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
            SceneManager.SetSceneAsActive<TestScene2>();
        }

        #endregion
    }
}
