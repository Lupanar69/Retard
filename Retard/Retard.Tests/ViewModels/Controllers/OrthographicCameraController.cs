using System;
using Arch.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Retard.App.ViewModels;
using Retard.Cameras.Components.Camera;
using Retard.Cameras.ViewModels;
using Retard.Input.Models;
using Retard.Input.Models.Assets;
using Retard.Input.ViewModels;

namespace Retard.Tests.ViewModels.Controllers
{
    /// <summary>
    /// Permet de déplacer la caméra dans la scène
    /// </summary>
    public sealed class OrthographicCameraController : IDisposable
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

        /// <summary>
        /// Gère la fenêtre
        /// </summary>
        private readonly AppViewport _appViewport;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="w">Le monde contenant les entités</param>
        /// <param name="camE">L'entité de la caméra du jeu</param>
        /// <param name="controls">Permet de s'abonner à l'InputSystem</param>
        /// <param name="appViewport">Gère la fenêtre</param>
        public OrthographicCameraController(World w, Entity camE, InputControls controls, AppViewport appViewport)
        {
            this._world = w;
            this._camE = camE;
            this._appViewport = appViewport;

            appViewport.OnWindowResolutionSetEvent += this.OnWindowResolutionSetCallback;

            InputManager.Instance.TryGetScheme(out this._mouseInput);

            controls.AddAction("Camera/Move", this.MoveCamera);
            controls.AddAction("Camera/Reset", InputEventHandleType.Started, this.ResetCameraPos);

            if (this._mouseInput != null)
            {
                controls.AddAction("Camera/LeftMouseHeld", InputEventHandleType.Performed, this.MoveCamera);
            }
        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Nettoyage
        /// </summary>
        public void Dispose()
        {
            this._appViewport.OnWindowResolutionSetEvent -= this.OnWindowResolutionSetCallback;
        }

        #endregion

        #region Méthodes privées

        /// <summary>
        /// Appelée quand la résolution de la fenêtre change
        /// </summary>
        /// <param name="windowResolution">La nouvelle résolution</param>
        private void OnWindowResolutionSetCallback(object _, Point windowResolution)
        {
            Viewport viewport = new(0, 0, windowResolution.X, windowResolution.Y);
            CameraManager.SetCamera2DViewport(this._world, this._camE, viewport);
        }

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
            Camera2DViewportCD camViewport = this._world.Get<Camera2DViewportCD>(this._camE);
            Vector2 window = new(camViewport.Value.Width, camViewport.Value.Height);

            bool isCursorInsideViewport =
                this._mouseInput.MousePos.X > 0 && this._mouseInput.MousePos.X < window.X &&
                this._mouseInput.MousePos.Y > 0 && this._mouseInput.MousePos.Y < window.Y;

            if (isCursorInsideViewport)
            {
                CameraManager.MoveCamera2D(this._world, this._camE, -this._mouseInput.MousePosDelta);
            }
        }

        #endregion
    }
}
