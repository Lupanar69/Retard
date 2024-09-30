using Arch.Core;

namespace Retard.Core.Models.Arch;

/// <summary>
///     An interface providing several methods for a system. 
/// </summary>
public interface ISystem
{
    /// <summary>
    ///     Initializes a system, before its first ever run.
    /// </summary>
    void Initialize() { }

    /// <summary>
    ///     Runs before <see cref="Update"/>.
    /// </summary>
    /// <param name="w">Le monde contenant les entités</param>
    void BeforeUpdate(World w) { }

    /// <summary>
    ///     Updates the system.
    /// </summary>
    /// <param name="w">Le monde contenant les entités</param>
    void Update(World w) { }

    /// <summary>
    ///     Runs after <see cref="Update"/>.
    /// </summary>
    /// <param name="w">Le monde contenant les entités</param>
    void AfterUpdate(World w) { }
}

/// <summary>
///     An interface providing several methods for a system. 
/// </summary>
/// <typeparam name="T">The type passed to each method. For example a delta time or some other data.</typeparam>
public interface ISystem<T>
{
    /// <summary>
    ///     Initializes a system, before its first ever run.
    /// </summary>
    void Initialize() { }

    /// <summary>
    ///     Runs before <see cref="Update"/>.
    /// </summary>
    /// <param name="w">Le monde contenant les entités</param>
    /// <param name="t">An instance passed to it.</param>
    void BeforeUpdate(World w, in T t) { }

    /// <summary>
    ///     Updates the system.
    /// </summary>
    /// <param name="w">Le monde contenant les entités</param>
    /// <param name="t">An instance passed to it.</param>
    void Update(World w, in T t) { }

    /// <summary>
    ///     Runs after <see cref="Update"/>.
    /// </summary>
    /// <param name="w">Le monde contenant les entités</param>
    /// <param name="t">An instance passed to it.</param>
    void AfterUpdate(World w, in T t) { }
}
