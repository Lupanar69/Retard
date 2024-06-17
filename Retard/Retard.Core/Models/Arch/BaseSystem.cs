using System;
using Arch.Core;
using Arch.System;

namespace Retard.Core.Models.Arch
{
    /// <summary>
    ///     A basic implementation of a <see cref="ISystem{T}"/>.
    /// </summary>
    public abstract class BaseSystem : ISystem, IWorld
    {

        /// <summary>
        ///     Creates an instance. 
        /// </summary>
        /// <param name="world">The <see cref="World"/>.</param>
        protected BaseSystem(World world)
        {
            this.World = world;
        }

        /// <summary>
        ///     The world instance. 
        /// </summary>
        public World World { get; set; }

        /// <inheritdoc />
        public virtual void Initialize() { }

        /// <inheritdoc />
        public virtual void BeforeUpdate() { }

        /// <inheritdoc />
        public virtual void Update() { }

        /// <inheritdoc />
        public virtual void AfterUpdate() { }

        /// <inheritdoc />
        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

}
