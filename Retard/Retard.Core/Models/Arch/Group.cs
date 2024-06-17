using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Retard.Core.Models.ValueTypes;

namespace Retard.Core.Models.Arch;

/// <summary>
///     A group of <see cref="ISystem"/>'s to organize them.
///     They will run in order.
/// </summary>
public readonly struct Group : ISystem
{
    /// <summary>
    /// A unique name to identify this group
    /// </summary>
    public NativeString Name { get; }

    /// <summary>
    /// All <see cref="SystemEntry"/>'s in this group. 
    /// </summary>
    private readonly List<SystemEntry> _systems = new();

    /// <summary>
    ///     Creates an instance with an array of <see cref="ISystem"/>'s that will belong to this group.
    /// </summary>
    /// <param name="name">A unique name to identify this group</param>
    /// <param name="systems">An <see cref="ISystem"/> array.</param>
    public Group(string name, params ISystem[] systems)
        : this(name, (IEnumerable<ISystem>)systems)
    {
    }

    /// <summary>
    ///     Creates an instance with an <see cref="IEnumerable"/> of <see cref="ISystem"/>'s that will belong to this group.
    /// </summary>
    /// <param name="name">A unique name to identify this group</param>
    /// <param name="systems">An <see cref="IEnumerable"/> of <see cref="ISystem"/>.</param>
    public Group(string name, IEnumerable<ISystem> systems)
    {
        this.Name = name;

#if NET5_0_OR_GREATER
        // If possible expand the list before adding all the systems
        if (systems.TryGetNonEnumeratedCount(out var count))
            this._systems.Capacity = count;
#endif

        foreach (var system in systems)
            Add(system);
    }

    /// <summary>
    ///     Adds several new <see cref="ISystem"/>'s to this group.
    /// </summary>
    /// <param name="systems">An <see cref="ISystem"/> array.</param>
    /// <returns>The same <see cref="Group"/>.</returns>
    public Group Add(params ISystem[] systems)
    {
        this._systems.Capacity = Math.Max(this._systems.Capacity, this._systems.Count + systems.Length);

        foreach (var system in systems)
            Add(system);

        return this;
    }

    /// <summary>
    ///     Adds an single <see cref="ISystem"/> to this group by its generic.
    ///     Automatically initializes it properly. Must be contructorless.
    /// </summary>
    /// <typeparam name="U">Its generic type.</typeparam>
    /// <returns>The same <see cref="Group"/>.</returns>
    public Group Add<U>() where U : ISystem, new()
    {
        return Add(new U());
    }

    /// <summary>
    ///     Adds an single <see cref="ISystem"/> to this group.
    /// </summary>
    /// <param name="system"></param>
    /// <returns></returns>
    public Group Add(ISystem system)
    {
        this._systems.Add(new SystemEntry(system));

        return this;
    }

    /// <summary>
    ///     Return the first <see cref="G"/> which was found in the hierachy.
    /// </summary>
    /// <typeparam name="U">The Type.</typeparam>
    /// <returns></returns>
    public U Get<U>() where U : ISystem
    {
        foreach (var item in _systems)
        {
            if (item.System is U sys)
            {
                return sys;
            }

            if (item.System is not Group grp)
            {
                continue;
            }

            return grp.Get<U>();
        }

        return default;
    }

    /// <summary>
    ///     Finds all <see cref="ISystem"/>s which can be cast into the given type.
    /// </summary>
    /// <typeparam name="G">The Type.</typeparam>
    /// <returns></returns>
    public IEnumerable<G> Find<G>() where G : ISystem
    {
        foreach (var item in _systems)
        {
            if (item.System is G sys)
            {
                yield return sys;
            }

            if (item.System is not Group grp)
            {
                continue;
            }

            foreach (var nested in grp.Find<G>())
            {
                yield return nested;
            }
        }
    }

    /// <summary>
    ///     Initializes all <see cref="ISystem"/>'s in this <see cref="Group"/>.
    /// </summary>
    public void Initialize()
    {
        for (var index = 0; index < this._systems.Count; index++)
        {
            var entry = _systems[index];
            entry.System.Initialize();
        }
    }

    /// <summary>
    ///     Runs <see cref="ISystem.BeforeUpdate"/> on each <see cref="ISystem"/> of this <see cref="Group"/>.
    /// </summary>
    public void BeforeUpdate()
    {
        for (var index = 0; index < this._systems.Count; index++)
        {
            var entry = _systems[index];


            entry.System.BeforeUpdate();
        }
    }

    /// <summary>
    ///     Runs <see cref="ISystem.Update"/> on each <see cref="ISystem"/> of this <see cref="Group"/>.
    /// </summary>
    public void Update()
    {
        for (var index = 0; index < this._systems.Count; index++)
        {
            var entry = _systems[index];


            entry.System.Update();

        }
    }

    /// <summary>
    ///     Runs <see cref="ISystem.AfterUpdate"/> on each <see cref="ISystem"/> of this <see cref="Group"/>.
    /// </summary>
    public void AfterUpdate()
    {
        for (var index = 0; index < this._systems.Count; index++)
        {
            var entry = _systems[index];

            entry.System.AfterUpdate();

        }
    }

    /// <summary>
    ///     Disposes this <see cref="Group"/> and all <see cref="ISystem"/>'s within.
    /// </summary>
    public void Dispose()
    {
        foreach (var system in _systems)
        {
            system.Dispose();
        }
    }

    /// <summary>
    ///     Converts this <see cref="Group"/> to a human readable string.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        // List all system names
        StringBuilder sb = new();
        foreach (var systemEntry in _systems)
        {
            sb.Append($"{systemEntry.System.GetType().Name},");
        }

        // Cut last `,`
        if (this._systems.Count > 0)
        {
            sb.Length--;
        }

        return $"Group = {{ {nameof(this.Name)} = {this.Name}, Systems = {{ {sb} }} }} ";
    }

    /// <summary>
    ///     The struct <see cref="SystemEntry"/> represents the given <see cref="ISystem"/> in the <see cref="Group"/> with all its performance statistics.
    /// </summary>
    private readonly struct SystemEntry : IDisposable
    {
        public readonly ISystem System;


        public void Dispose()
        {
            System.Dispose();
        }

        public SystemEntry(ISystem system)
        {
            System = system;
        }
    }
}