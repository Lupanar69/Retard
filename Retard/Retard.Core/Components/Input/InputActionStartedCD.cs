﻿using System;
using Arch.AOT.SourceGenerator;
using Arch.LowLevel;

namespace Retard.Core.Components.Input
{
    /// <summary>
    /// L'action à réaliser au début de l'InputAction
    /// </summary>
    [Component]
    public struct InputActionStartedCD
    {
        #region Variables d'instance

        /// <summary>
        /// L'action à réaliser au début de l'InputAction
        /// </summary>
        public Handle<Action> Value;

        #endregion
    }
}
