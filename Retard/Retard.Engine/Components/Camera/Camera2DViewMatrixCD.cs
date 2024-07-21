using Arch.AOT.SourceGenerator;
using Microsoft.Xna.Framework;

namespace Retard.Engine.Components.Camera
{
    /// <summary>
    /// La matrice de vue de la caméra.
    /// Utilisée pour l'affiche des sprites à l'écran
    /// </summary>
    [Component]
    internal struct Camera2DViewMatrixCD
    {
        #region Variables d'instance

        /// <summary>
        /// La matrice de vue de la caméra.
        /// Utilisée pour l'affiche des sprites à l'écran
        /// </summary>
        public Matrix Value;

        #endregion
    }
}
