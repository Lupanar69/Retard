using Arch.Core;
using Microsoft.Xna.Framework;
using Retard.Cameras.Components.Camera;

namespace Retard.Cameras.Entities
{
    /// <summary>
    /// Contient les méthodes de création
    /// des différentes entités
    /// </summary>
    public static class EntityFactory
    {
        #region Méthodes statiques publiques

        /// <summary>
        /// Crée une caméra orthographique
        /// </summary>
        /// <param name="w">Le monde contenant les entités</param>
        /// <param name="pos">La position de l'entité dans la scène</param>
        /// <param name="viewportRect">Le cadre d'affichage de la caméra à l'écran</param>
        /// <returns>L'entité représentant une caméra orthographique</returns>
        public static Entity CreateOrthographicCamera(World w, Vector2 pos, Rectangle viewportRect)
        {
            return w.Create
                (
                    new CameraMatrixIsDirtyTag(),
                    new Camera2DPositionCD(pos),
                    new Camera2DRotationCD(0f),
                    new CameraZoomCD(1f, 1f, float.MaxValue),
                    new CameraPitchCD(1f, 1f, float.MaxValue),
                    new Camera2DViewportRectCD(viewportRect),
                    new Camera2DOriginCD(new Vector2(viewportRect.Width / 2f, viewportRect.Height / 2f)),
                    new Camera2DMatrixCD()
                );
        }

        #endregion
    }
}
