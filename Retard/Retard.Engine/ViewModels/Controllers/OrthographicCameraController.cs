using Microsoft.Xna.Framework;
using MonoGame.Extended;
using Retard.Engine.Models;
using Retard.Input.Models;
using Retard.Input.Models.Assets;
using Retard.Input.ViewModels;

namespace Retard.Engine.ViewModels.Controllers
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
        /// La vitesse de déplacement de la caméra (hors glisser de souris)
        /// </summary>
        private readonly float _moveSpeed = 10f;

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
        /// <param name="controls">Permet de s'abonner à l'InputSystem</param>
        public OrthographicCameraController(OrthographicCamera camera, InputControls controls)
        {
            this.Camera = camera;
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
            this.Camera.Position = Vector2.Zero;
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
                this.Camera.Move(value * this._moveSpeed);

                // Arrondit la position pour éviter des artefacts lors du rendu

                Vector2 camPos = this.Camera.Position;
                camPos = new Vector2((int)camPos.X, (int)camPos.Y);
                this.Camera.Position = camPos;
            }
        }

        /// <summary>
        /// Déplace la caméra
        /// </summary>
        private void MoveCamera(int _)
        {
            if (this._mouseInput.IsCursorInsideWindow)
            {
                this.Camera.Move(-this._mouseInput.MousePosDelta);
            }
        }

        #endregion
    }
}
