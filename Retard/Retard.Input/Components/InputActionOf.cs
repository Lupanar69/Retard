﻿using Arch.AOT.SourceGenerator;

namespace Retard.Input.Components
{
    /// <summary>
    /// Tag relationnel entre une InputAction et ses InputBindings.
    /// Nous permet d'éviter les conflits entre InputBindings
    /// selon leurs actions.
    /// </summary>
    [Component]
    public struct InputActionOf
    {
    }
}
