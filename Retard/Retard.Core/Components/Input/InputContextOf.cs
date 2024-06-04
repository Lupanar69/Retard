using Arch.AOT.SourceGenerator;

namespace Retard.Core.Components.Input
{
    /// <summary>
    /// Tag relationnel entre un InputContext et ses InputActions.
    /// Nous permet d'éviter les conflits entre InputActions
    /// selon leurs contextes.
    /// </summary>
    [Component]
    public struct InputContextOf
    {
    }
}
