using System.Runtime.CompilerServices;
using Arch.Core;
using Microsoft.Xna.Framework;
using Retard.Cameras.Components.Camera;
using Retard.Cameras.Components.Layers;
using Retard.Cameras.Models;

namespace Retard.Cameras.Entities
{
    /// <summary>
    /// Contient les méthodes de création
    /// des différentes entités
    /// </summary>
    internal static class EntityFactory
    {
        #region Méthodes statiques publiques

        /// <summary>
        /// Crée une caméra orthographique
        /// </summary>
        /// <param name="w">Le monde contenant les entités</param>
        /// <param name="pos">La position de l'entité dans la scène</param>
        /// <param name="viewportRect">Le cadre d'affichage de la caméra à l'écran</param>
        /// <param name="layers">Les layers à appliquer à la caméra</param>
        /// <returns>L'entité représentant une caméra orthographique</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Entity CreateOrthographicCamera(World w, Vector2 pos, Rectangle viewportRect, RenderingLayer layers = RenderingLayer.Default)
        {
            Entity camE = w.Create
            (
                new OrthographicCameraTag(),
                new CameraMatrixIsDirtyTag(),
                new Camera2DPositionCD(pos),
                new Camera2DRotationCD(0f),
                new CameraZoomCD(1f, 1f, float.MaxValue),
                new CameraPitchCD(1f, 1f, float.MaxValue),
                new Camera2DViewportRectCD(viewportRect),
                new Camera2DOriginCD(new Vector2(viewportRect.Width / 2f, viewportRect.Height / 2f)),
                new Camera2DViewMatrixCD(Matrix.Identity),
                new Camera2DScaleMatrixCD(Matrix.Identity)
            );

            // Crée un LayerTag pour chaque layer renseigné

            int flagMask = 1 << 30; // start with high-order bit...
            while (flagMask != 0)   // loop terminates once all flags have been compared
            {
                // switch on only a single bit...

                switch (layers & (RenderingLayer)flagMask)
                {
                    case RenderingLayer.Default:
                        w.Add<DefaultLayerTag>(camE);
                        break;

                    case RenderingLayer.UI:
                        w.Add<UILayerTag>(camE);
                        break;
                }

                flagMask >>= 1;  // bit-shift the flag value one bit to the right
            }

            return camE;
        }

        #endregion
    }
}
