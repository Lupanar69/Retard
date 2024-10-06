using Arch.AOT.SourceGenerator;

namespace Retard.Cameras.Components.Camera
{
    /// <summary>
    /// Le niveau d'agrandissement de la caméra
    /// </summary>
    /// <param name="zoom">Le niveau d'agrandissement de la caméra</param>
    /// <param name="minZoom">Le niveau d'agrandissement min de la caméra</param>
    /// <param name="maxZoom">Le niveau d'agrandissement max de la caméra</param>
    [Component]
    public struct CameraZoomCD(float zoom, float minZoom, float maxZoom)
    {
        #region Variables d'instance

        /// <summary>
        /// Le niveau d'agrandissement de la caméra
        /// </summary>
        public float Value = zoom;

        /// <summary>
        /// Le niveau d'agrandissement min de la caméra
        /// </summary>
        public float MinZoom = minZoom;

        /// <summary>
        /// Le niveau d'agrandissement max de la caméra
        /// </summary>
        public float MaxZoom = maxZoom;

        #endregion
    }
}
