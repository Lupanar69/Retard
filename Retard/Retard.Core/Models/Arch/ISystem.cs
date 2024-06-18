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
    void Initialize() { }

    /// <summary>
    ///     Runs before <see cref="Update"/>.
    /// </summary>
    void BeforeUpdate() { }

    /// <summary>
    ///     Updates the system.
    /// </summary>
    void Update() { }

    /// <summary>
    ///     Runs after <see cref="Update"/>.
    /// </summary>
    void AfterUpdate() { }
}

/// <summary>
///     An interface providing several methods for a system. 
/// </summary>
/// <typeparam name="T">The type passed to each method. For example a delta time or some other data.</typeparam>
public interface ISystem<T> : IDisposable
{
    /// <summary>
    ///     Initializes a system, before its first ever run.
    /// </summary>
    void Initialize() { }

    /// <summary>
    ///     Runs before <see cref="Update"/>.
    /// </summary>
    /// <param name="t">An instance passed to it.</param>
    void BeforeUpdate(in T t) { }

    /// <summary>
    ///     Updates the system.
    /// </summary>
    /// <param name="t">An instance passed to it.</param>
    void Update(in T t) { }

    /// <summary>
    ///     Runs after <see cref="Update"/>.
    /// </summary>
    /// <param name="t">An instance passed to it.</param>
    void AfterUpdate(in T t) { }
}
