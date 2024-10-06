using System.Runtime.CompilerServices;
using Arch.Core;
using Arch.System;
using Microsoft.Xna.Framework;
using Retard.Cameras.Components.Camera;

namespace Retard.Cameras.Entities
{
    /// <summary>
    /// Regroupe les queries Arch pouvant être parallélisées
    /// </summary>
    internal static partial class Queries
    {
        #region Méthodes statiques internes

        /// <summary>
        /// Calcule la matrice des caméras modifiées
        /// </summary>
        /// <param name="w">Le monde contenant les entités</param>
        /// <param name="camE">L'entité de la caméra</param>
        /// <param name="pos">La position de la caméra</param>
        /// <param name="rot">La rotation de la caméra</param>
        /// <param name="zoom">Le zoom de la caméra</param>
        /// <param name="pitch">Le pitch de la caméra</param>
        /// <param name="origin">L'origine de la caméra</param>
        /// <param name="scaleMatrix">La matrice d'échelle de la caméra</param>
        /// <param name="viewMatrix">La matrice de la vue la caméra</param>
        [All(typeof(CameraMatrixIsDirtyTag), typeof(OrthographicCameraTag))]
        [Query]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void ComputeViewMatrices(
            [Data] World w,
            in Entity camE,
            in Camera2DPositionCD pos,
            in Camera2DRotationCD rot,
            in CameraZoomCD zoom,
            in CameraPitchCD pitch,
            in Camera2DOriginCD origin,
            in Camera2DScaleMatrixCD scaleMatrix,
            ref Camera2DViewMatrixCD viewMatrix)
        {
            Vector2 parallaxFactor = Vector2.One;
            viewMatrix.Value = Queries.GetVirtualViewMatrix(parallaxFactor, pos.Value, origin.Value, rot.Value, zoom.Value, pitch.Value) * scaleMatrix.Value;
            w.Remove<CameraMatrixIsDirtyTag>(camE);
        }

        #endregion

        #region Méthodes statiques privées

        /// <summary>
        /// Calcule la matrice de la vue de la caméra
        /// </summary>
        /// <param name="parallaxFactor">Facteur de parallax</param>
        /// <param name="pos">La position de la caméra</param>
        /// <param name="origin">L'origine de la caméra</param>
        /// <param name="rot">La rotation de la caméra</param>
        /// <param name="zoom">Le zoom de la caméra</param>
        /// <param name="pitch">Le pitch de la caméra</param>
        /// <returns>La matrice de la vue de la caméra</returns
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Matrix GetVirtualViewMatrix(Vector2 parallaxFactor, Vector2 pos, Vector2 origin, float rot, float zoom, float pitch)
        {
            return Matrix.CreateTranslation(new Vector3(-pos * parallaxFactor, 0f)) * Matrix.CreateTranslation(new Vector3(-origin, 0f)) * Matrix.CreateRotationZ(rot) * Matrix.CreateScale(zoom, zoom * pitch, 1f) * Matrix.CreateTranslation(new Vector3(origin, 0f));
        }

        #endregion
    }
}
