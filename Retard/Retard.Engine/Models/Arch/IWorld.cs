using Arch.Core;

namespace Retard.Engine.Models.Arch;

/// <summary>
///     An interface providing the world for a system. 
/// </summary>
public interface IWorld
{
    /// <summary>
    ///     The world instance. 
    /// </summary>
    World World { get; init; }
}
