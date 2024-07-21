using Arch.AOT.SourceGenerator;

namespace Retard.Engine.Components.Camera
{
    /// <summary>
    /// Le pitch d'une caméra
    /// </summary>
    [Component]
    internal struct Camera2DPitchCD
    {
        #region Variables d'instance

        /// <summary>
        /// Le pitch d'une caméra
        /// </summary>
        public float Pitch;

        /// <summary>
        /// Le pitch max d'une caméra
        /// </summary>
        public float MaximumPitch;

        /// <summary>
        /// Le pitch min d'une caméra
        /// </summary>
        public float MinimumPitch;

        #endregion
    }
}
