using Arch.AOT.SourceGenerator;

namespace Retard.Cameras.Components.Camera
{
    /// <summary>
    /// La rotation de la caméra 2D sur l'axe Z
    /// </summary>
    /// <param name="value">La rotation de la caméra 2D sur l'axe Z</param>
    [Component]
    public struct Camera2DRotationCD(float value)
    {
        #region Variables d'instance

        /// <summary>
        /// La rotation de la caméra 2D sur l'axe Z
        /// </summary>
        public float Value = value;

        #endregion
    }
}
