using Microsoft.Xna.Framework;
using MonoGame.Extended;
using Retard.Core.Models.Assets.Scene;
using Retard.Core.ViewModels.Controllers;

namespace Retard.Core.ViewModels.Scenes
{
    /// <summary>
    /// Contient la caméra orthographique permettant de se déplacer
    /// dans les autres scènes
    /// </summary>
    public sealed class OrthographicCameraScene : IScene
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
        /// Le contrôleur de la caméra du jeu
        /// </summary>
        private readonly OrthographicCameraController _cameraController;

        /// <summary>
        /// La caméra du jeu
        /// </summary>
        private readonly OrthographicCamera _camera;

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
        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Chargement du contenu
        /// </summary>
        public void OnInitialize()
        {

        }

        /// <summary>
        /// Màj à chaque frame
        /// </summary>
        public void OnLoadContent()
        {

        }

        /// <summary>
        /// Appelée à chaque fois que la scène devient active
        /// </summary>
        public void OnSetActive()
        {

        }

        /// <summary>
        /// Récupère les inputs nécessaires au fonctionnement des systèmes
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début de l'application</param>
        public void OnUpdateInput(GameTime gameTime)
        {
            this._cameraController.Update(gameTime);
        }

        /// <summary>
        /// Màj à chaque frame
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début de l'application</param>
        public void OnUpdate(GameTime gameTime)
        {

        }

        /// <summary>
        /// Pour afficher des éléments à l'écran
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début de l'application</param>
        public void OnDraw(GameTime gameTime)
        {

        }

        #endregion
    }
}
