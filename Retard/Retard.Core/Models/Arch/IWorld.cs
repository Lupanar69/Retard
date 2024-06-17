using Arch.Core;

namespace Retard.Core.Models.Arch;

/// <summary>
///     An interface providing the world for a system. 
/// </summary>
public interface IWorld
{
    /// <summary>
    ///     The world instance. 
    /// </summary>
    World World { get; set; }
}
