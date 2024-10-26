using System;
using Arch.Core;
using Arch.LowLevel;
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
    /// Permet de déplacer plusieurs caméras dans la scène
    /// </summary>
    public sealed class MultiOrthographicCameraController : IDisposable
    {
        #region Propriétés

        /// <summary>
        /// Obtient le nombre de caméras enregistrées
        /// </summary>
        public int CamCount
        {
            get { return this._camEs.Count; }
        }

        #endregion

        #region Variables d'instance

        /// <summary>
        /// Les entités des caméras
        /// </summary>
        private UnsafeList<Entity> _camEs;

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
        /// <param name="camCapacity">Le nombre de caméras de départ</param>
        /// <param name="controls">Permet de s'abonner à l'InputSystem</param>
        /// <param name="appViewport">Gère la fenêtre</param>
        public MultiOrthographicCameraController(World w, int camCapacity, InputControls controls, AppViewport appViewport)
        {
            this._world = w;
            this._appViewport = appViewport;
            this._camEs = new UnsafeList<Entity>(camCapacity);

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
        /// Ajoute une caméra
        /// </summary>
        public void AddCamera(Entity camE)
        {
            this._camEs.Add(camE);
            this.OnWindowResolutionSetCallback(this, this._appViewport.WindowResolution);
        }

        /// <summary>
        /// Retire une caméra
        /// </summary>
        /// <param name="index">La position de l'entité dans la liste</param>
        public void RemoveCamera(int index)
        {
            Entity camE = this._camEs[index];
            this._camEs.Remove(camE);
            this._world.Destroy(camE);
            this.OnWindowResolutionSetCallback(this, this._appViewport.WindowResolution);
        }

        /// <summary>
        /// Nettoyage
        /// </summary>
        public void Dispose()
        {
            this._camEs.Dispose();
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
            for (int i = 0; i < this._camEs.Count; ++i)
            {
                Viewport viewport = new
                (
                    new Rectangle
                    (
                        windowResolution.X / this._camEs.Count * i,
                        windowResolution.Y / this._camEs.Count * i,
                        windowResolution.X / this._camEs.Count,
                        windowResolution.Y / this._camEs.Count
                    )
                );

                CameraManager.SetCamera2DViewport(this._world, this._camEs[i], viewport);
            }
        }

        /// <summary>
        /// Remet la position de la caméra à son origine
        /// </summary>
        private void ResetCameraPos(int _)
        {
            for (int i = 0; i < this._camEs.Count; ++i)
            {
                Entity camE = this._camEs[i];
                Viewport camViewport = this._world.Get<Camera2DViewportCD>(camE).Value;

                bool isCursorInsideViewport =
                    this._mouseInput.MousePos.X > camViewport.X && this._mouseInput.MousePos.X < camViewport.X + camViewport.Width &&
                    this._mouseInput.MousePos.Y > camViewport.Y && this._mouseInput.MousePos.Y < camViewport.Y + camViewport.Height;

                if (isCursorInsideViewport)
                {
                    CameraManager.SetCamera2DPosition(this._world, camE, Vector2.Zero);
                }
            }
        }

        /// <summary>
        /// Déplace la caméra
        /// </summary>
        /// <param name="playerIndex">L'ID du contrôleur</param>
        /// <param name="value">La valeur du contrôleur</param>
        private void MoveCamera(int playerIndex, Vector2 value)
        {
            value.Y *= -1;
            CameraManager.MoveCamera2D(this._world, this._camEs[playerIndex], value * this._moveSpeed);
        }

        /// <summary>
        /// Déplace la caméra
        /// </summary>
        private void MoveCamera(int _)
        {
            for (int i = 0; i < this._camEs.Count; ++i)
            {
                Entity camE = this._camEs[i];
                Viewport camViewport = this._world.Get<Camera2DViewportCD>(camE).Value;

                bool isCursorInsideViewport =
                    this._mouseInput.MousePos.X > camViewport.X && this._mouseInput.MousePos.X < camViewport.X + camViewport.Width &&
                    this._mouseInput.MousePos.Y > camViewport.Y && this._mouseInput.MousePos.Y < camViewport.Y + camViewport.Height;

                if (isCursorInsideViewport)
                {
                    CameraManager.MoveCamera2D(this._world, camE, -this._mouseInput.MousePosDelta);
                }
            }
        }

        #endregion
    }
}
