using Arch.AOT.SourceGenerator;

namespace Retard.Cameras.Components.Camera
{
    /// <summary>
    /// Le niveau de pitch de la caméra
    /// </summary>
    /// <param name="pitch">Le niveau de pitch de la caméra</param>
    /// <param name="minPitch">Le niveau de pitch min de la caméra</param>
    /// <param name="maxPitch">Le niveau de pitch max de la caméra</param>
    [Component]
    public struct CameraPitchCD(float pitch, float minPitch, float maxPitch)
    {
        #region Variables d'instance

        /// <summary>
        /// Le niveau de pitch de la caméra
        /// </summary>
        public float Value = pitch;

        /// <summary>
        /// Le niveau de pitch min de la caméra
        /// </summary>
        public float MinPitch = minPitch;

        /// <summary>
        /// Le niveau de pitch max de la caméra
        /// </summary>
        public float MaxPitch = maxPitch;

        #endregion
    }
}
