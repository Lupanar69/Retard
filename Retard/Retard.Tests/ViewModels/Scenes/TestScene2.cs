using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Retard.Core.Models;
using Retard.Core.Models.Assets.Scene;
using Retard.Core.ViewModels.Input;
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
        /// Texture de test
        /// </summary>
        private Texture2D _debugTex;

        /// <summary>
        /// Le contrôleur pour clavier
        /// </summary>
        private readonly KeyboardInput _keyboardInput;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        public TestScene2()
        {
            this._keyboardInput = InputManager.GetScheme<KeyboardInput>();
            this._debugTex = SceneManager.Content.Load<Texture2D>($"{Constants.TEXTURES_DIR_PATH_DEBUG}tiles_test2");
        }

        #endregion

        #region Méthodes publiques

        /// <inheritdoc/>
        public void OnUpdateInput(GameTime gameTime)
        {
            if (this._keyboardInput.IsKeyPressed(Keys.NumPad8))
            {
                SceneManager.RemoveActiveAndOverlaidScenes(this);
            }

            if (this._keyboardInput.IsKeyPressed(Keys.NumPad2))
            {
                SceneManager.SetSceneAsActive<TestScene3>();
            }
        }

        /// <inheritdoc/>
        public void OnDraw(GameTime gameTime)
        {
            SceneManager.SpriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, null);

            SceneManager.SpriteBatch.Draw(this._debugTex, new Vector2(this._debugTex.Width + 32, 0), Color.White);

            SceneManager.SpriteBatch.End();
        }

        #endregion
    }
}
