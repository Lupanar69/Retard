using Arch.AOT.SourceGenerator;

namespace Retard.Engine.Components.Camera
{
    /// <summary>
    /// Indique que cette caméra peut être contrôlée par un joueur selon son ID
    /// </summary>
    [Component]
    internal struct CameraPlayerControllerIDCD
    {
        #region Variables d'instance

        /// <summary>
        /// L'ID du contrôleur pouvant manipuler cette caméra
        /// </summary>
        public int Value;

        #endregion
    }
}
