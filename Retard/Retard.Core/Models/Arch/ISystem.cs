using System;

namespace Retard.Core.Models.Arch;

/// <summary>
///     An interface providing several methods for a system. 
/// </summary>
public interface ISystem : IDisposable
{
    /// <summary>
    ///     Initializes a system, before its first ever run.
    /// </summary>
    void Initialize();

    /// <summary>
    ///     Runs before <see cref="Update"/>.
    /// </summary>
    void BeforeUpdate();

    /// <summary>
    ///     Updates the system.
    /// </summary>
    void Update();

    /// <summary>
    ///     Runs after <see cref="Update"/>.
    /// </summary>
    void AfterUpdate();
}
