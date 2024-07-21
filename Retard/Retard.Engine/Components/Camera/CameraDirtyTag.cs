using Arch.AOT.SourceGenerator;

namespace Retard.Engine.Components.Camera
{
    /// <summary>
    /// Tag pour indiquer que les propriétés de la caméra ont été modifiées,
    /// et que le système doit recalculer les valeurs de ses components
    /// </summary>
    [Component]
    internal readonly struct CameraDirtyTag
    {
    }
}
