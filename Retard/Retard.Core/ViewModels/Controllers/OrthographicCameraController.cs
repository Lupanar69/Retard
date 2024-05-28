using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using Retard.Core.ViewModels.Input;

namespace Retard.Core.ViewModels.Controllers
{
    /// <summary>
    /// Permet de déplacer la caméra dans la scène
    /// </summary>
    public sealed class OrthographicCameraController : IDisposable
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
        /// <see langword="true"/> si l'on a appelé Dispose()
        /// </summary>
        private bool _disposedValue;

        /// <summary>
        /// La touche pour replacer la caméra à sa position d'origine 
        /// </summary>
        private readonly Keys _resetKey = Keys.R;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="camera">La caméra du jeu</param>
        public OrthographicCameraController(OrthographicCamera camera)
        {
            this.Camera = camera;
        }

        /// <summary>
        /// Nettoie l'objet
        /// </summary>
        //// TODO: override finalizer only if 'Dispose(bool disposingManaged)' has code to free unmanaged resources
        //~OrthographicCameraController()
        //{
        //    // Do not change this code. Put cleanup code in 'Dispose(bool disposingManaged)' method
        //    Dispose(disposingManaged: false);
        //}

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Màj les commandes de la caméra
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début de l'application</param>
        public void Update(GameTime gameTime)
        {
            if (KeyboardInput.IsKeyPressed(this._resetKey))
            {
                this.Camera.Position = Vector2.Zero;
            }

            if (MouseInput.LeftMouseHeld())
            {
                this.Camera.Move(-MouseInput.MousePosDelta);
            }
        }

        /// <summary>
        /// Nettoie l'objet
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            this.Dispose(disposingManaged: true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Méthodes privées

        /// <summary>
        /// Nettoie l'objet
        /// </summary>
        /// <param name="disposingManaged"><see langword="true"/> si on doit nettoyer des objets</param>
        private void Dispose(bool disposingManaged)
        {
            if (!this._disposedValue)
            {
                if (disposingManaged)
                {

                }

                this._disposedValue = true;
            }
        }

        #endregion
    }
}
