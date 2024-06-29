using Microsoft.Xna.Framework;
using MonoGame.Extended;
using Retard.Core.Models.Assets.Scene;
using Retard.Core.ViewModels.Controllers;
using Retard.Engine.ViewModels.Input;

namespace Retard.Core.ViewModels.Scenes
{
    /// <summary>
    /// Contient la caméra orthographique permettant de se déplacer
    /// dans les autres scènes
    /// </summary>
    public sealed class OrthographicCameraScene : IScene
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
        /// Le contrôleur de la caméra du jeu
        /// </summary>
        private readonly OrthographicCameraController _cameraController;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="camera">La caméra du jeu</param>
        public OrthographicCameraScene(OrthographicCamera camera)
        {
            this._cameraController = new OrthographicCameraController(camera);
        }

        #endregion

        #region Méthodes publiques

        /// <inheritdoc/>
        public void OnUpdateInput(GameTime gameTime)
        {
            this._cameraController.Update();
        }

        #endregion
    }
}
