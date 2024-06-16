using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using Retard.Core.Models;
using Retard.Core.ViewModels.Input;

namespace Retard.Core.ViewModels.Controllers
{
    /// <summary>
    /// Permet de déplacer la caméra dans la scène
    /// </summary>
    public sealed class OrthographicCameraController
    {
        #region Propriétés

        /// <summary>
        /// La caméra du jeu
        /// </summary>
        public OrthographicCamera Camera
        {
            get;
            private set;
        }

        #endregion

        #region Variables d'instance

        /// <summary>
        /// La touche pour replacer la caméra à sa position d'origine 
        /// </summary>
        private readonly Keys _resetKey = Keys.R;

        /// <summary>
        /// Le contrôleur pour clavier
        /// </summary>
        private readonly KeyboardInput _keyboardInput;

        /// <summary>
        /// Le contrôleur pour souris
        /// </summary>
        private readonly MouseInput _mouseInput;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="camera">La caméra du jeu</param>
        public OrthographicCameraController(OrthographicCamera camera)
        {
            this.Camera = camera;
            this._keyboardInput = InputManager.GetScheme<KeyboardInput>();
            this._mouseInput = InputManager.GetScheme<MouseInput>();
        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Màj les commandes de la caméra
        /// </summary>
        public void Update()
        {
            if (this._keyboardInput.IsKeyPressed(this._resetKey))
            {
                this.Camera.Position = Vector2.Zero;
            }

            if (GameState.GameIsActivated && this._mouseInput.IsCursorInsideWindow && this._mouseInput.LeftMouseHeld())
            {
                this.Camera.Move(-this._mouseInput.MousePosDelta);
            }
        }

        #endregion
    }
}
