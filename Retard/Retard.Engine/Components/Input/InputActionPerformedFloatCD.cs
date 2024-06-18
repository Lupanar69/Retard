using System;
using Arch.AOT.SourceGenerator;
using Arch.LowLevel;

namespace Retard.Core.Components.Input
{
    /// <summary>
    /// L'action à réaliser pendant l'exécution de l'InputAction
    /// </summary>
    [Component]
    public struct InputActionPerformedFloatCD
    {
        #region Variables d'instance

        /// <summary>
        /// L'action à réaliser pendant l'exécution de l'InputAction
        /// </summary>
        public Handle<Action<float>> Value;

        #endregion
    }
}
