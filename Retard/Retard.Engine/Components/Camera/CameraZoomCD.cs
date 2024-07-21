using Arch.AOT.SourceGenerator;

namespace Retard.Engine.Components.Camera
{
    /// <summary>
    /// Le zoom d'une caméra
    /// </summary>
    [Component]
    internal struct CameraZoomCD
    {
        #region Variables d'instance

        /// <summary>
        /// Le zoom d'une caméra
        /// </summary>
        public float Zoom;

        /// <summary>
        /// Le zoom max d'une caméra
        /// </summary>
        public float MaximumZoom;

        /// <summary>
        /// Le zoom min d'une caméra
        /// </summary>
        public float MinimumZoom;

        #endregion
    }
}
