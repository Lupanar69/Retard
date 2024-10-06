using Arch.Core;
using Microsoft.Xna.Framework;
using Retard.Cameras.ViewModels;
using Retard.Input.Models;
using Retard.Input.Models.Assets;
using Retard.Input.ViewModels;
using Retard.Tests.ViewModels.Engine;

namespace Retard.Tests.ViewModels.Controllers
{
    /// <summary>
    /// Permet de déplacer la caméra dans la scène
    /// </summary>
    public readonly struct OrthographicCameraController
    {
        #region Variables d'instance

        /// <summary>
        /// L'entité de la caméra du jeu
        /// </summary>
        private readonly Entity _camE;

        /// <summary>
        /// La vitesse de déplacement de la caméra (hors glisser de souris)
        /// </summary>
        private readonly float _moveSpeed = 10f;

        /// <summary>
        /// Le contrôleur pour souris
        /// </summary>
        private readonly MouseInput _mouseInput;

        /// <summary>
        /// Le monde contenant les entités
        /// </summary>
        private readonly World _world;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="w">Le monde contenant les entités</param>
        /// <param name="camE">L'entité de la caméra du jeu</param>
        /// <param name="controls">Permet de s'abonner à l'InputSystem</param>
        public OrthographicCameraController(World w, Entity camE, InputControls controls)
        {
            this._world = w;
            this._camE = camE;
            this._mouseInput = InputManager.Instance.GetScheme<MouseInput>();

            controls.AddAction("Camera/Move", this.MoveCamera);
            controls.AddAction("Camera/Reset", InputEventHandleType.Started, this.ResetCameraPos);
            controls.AddAction("Camera/LeftMouseHeld", InputEventHandleType.Performed, this.MoveCamera);

            GameState.OnFocusEvent += (_, _) => controls.Enable();
            GameState.OnFocusLostEvent += (_, _) => controls.Disable();
        }

        #endregion

        #region Méthodes privées

        /// <summary>
        /// Remet la position de la caméra à son origine
        /// </summary>
        private void ResetCameraPos(int _)
        {
            CameraManager.SetCamera2DPosition(this._world, this._camE, Vector2.Zero);
        }

        /// <summary>
        /// Déplace la caméra
        /// </summary>
        /// <param name="playerIndex">L'ID du contrôleur</param>
        /// <param name="value">La valeur du contrôleur</param>
        private void MoveCamera(int playerIndex, Vector2 value)
        {
            if (playerIndex == 0)
            {
                value.Y *= -1;
                CameraManager.MoveCamera2D(this._world, this._camE, value * this._moveSpeed);
            }
        }

        /// <summary>
        /// Déplace la caméra
        /// </summary>
        private void MoveCamera(int _)
        {
            if (this._mouseInput.IsCursorInsideWindow)
            {
                CameraManager.MoveCamera2D(this._world, this._camE, -this._mouseInput.MousePosDelta);
            }
        }

        #endregion
    }
}
