using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using Retard.Core.Models.Assets.Scene;
using Retard.Core.ViewModels.Controllers;
using Retard.Core.ViewModels.Input;

namespace Retard.Core.ViewModels.Scenes
{
    /// <summary>
    /// Contient la caméra orthographique permettant de se déplacer
    /// dans les autres scènes
    /// </summary>
    public sealed class OrthographicCameraScene : IScene, IDisposable
    {
        #region Properties

        /// <summary>
        /// <see langword="true"/> si la scène doit bloquer les inputs 
        /// pour les scènes qui suivent
        /// (ex: une scène de pause superposée à la scène de jeu)
        /// </summary>
        public bool ConsumeInput { get; init; }

        /// <summary>
        /// <see langword="true"/> si la scène doit bloquer le rendu
        /// pour les scènes qui suivent
        /// (ex: une scène de pause superposée à la scène de jeu)
        /// </summary>
        public bool ConsumeDraw { get; init; }

        #endregion

        #region Variables d'instance

        /// <summary>
        /// <see langword="true"/> si l'on a appelé Dispose()
        /// </summary>
        private bool _disposedValue;

        /// <summary>
        /// Le contrôleur de la caméra du jeu
        /// </summary>
        private readonly OrthographicCameraController _cameraController;

        /// <summary>
        /// La caméra du jeu
        /// </summary>
        private readonly OrthographicCamera _camera;

        /// <summary>
        /// Le contrôleur pour clavier
        /// </summary>
        private readonly KeyboardInput _keyboardInput;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="camera">La caméra du jeu</param>
        public OrthographicCameraScene(OrthographicCamera camera)
        {
            this._camera = camera;
            this._cameraController = new OrthographicCameraController(this._camera);
            this._keyboardInput = InputManager.GetScheme<KeyboardInput>();
        }

        /// <summary>
        /// Nettoie l'objet
        /// </summary>
        //// TODO: override finalizer only if 'Dispose(bool disposingManaged)' has code to free unmanaged resources
        //~SpriteDrawTestScene()
        //{
        //    // Do not change this code. Put cleanup code in 'Dispose(bool disposingManaged)' method
        //    Dispose(disposingManaged: false);
        //}

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Chargement du contenu
        /// </summary>
        public void Initialize()
        {

        }

        /// <summary>
        /// Màj à chaque frame
        /// </summary>
        public void LoadContent()
        {

        }

        /// <summary>
        /// Appelée à chaque fois que la scène devient active
        /// </summary>
        public void Start()
        {

        }

        /// <summary>
        /// Récupère les inputs nécessaires au fonctionnement des systèmes
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début de l'application</param>
        public void UpdateInput(GameTime gameTime)
        {
            this._cameraController.Update(gameTime);
        }

        /// <summary>
        /// Pour afficher des éléments à l'écran
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début de l'application</param>
        public void Update(GameTime gameTime)
        {

        }

        /// <summary>
        /// Pour afficher des éléments à l'écran
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début de l'application</param>
        public void Draw(GameTime gameTime)
        {

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
                    this._cameraController.Dispose();
                }

                this._disposedValue = true;
            }
        }

        #endregion
    }
}
