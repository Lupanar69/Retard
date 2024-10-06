using Arch.AOT.SourceGenerator;
using Microsoft.Xna.Framework;

namespace Retard.Cameras.Components.Camera
{
    /// <summary>
    /// La matrice d'échelle de la caméra
    /// </summary>
    /// <param name="value">La matrice d'échelle de la caméra</param>
    [Component]
    public struct Camera2DScaleMatrixCD(Matrix value)
    {
        #region Variables d'instance

        /// <summary>
        /// La matrice de la vue de la caméra
        /// </summary>
        public Matrix Value = value;

        #endregion
    }
}
