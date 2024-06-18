using System;
using Arch.Core;

namespace Retard.Core.Models.Arch
{
    /// <summary>
    ///     A basic implementation of a <see cref="ISystem"/>.
    /// </summary>
    public abstract class BaseSystem : ISystemWorld
    {
        #region Propriétés

        /// <summary>
        ///     The world instance. 
        /// </summary>
        public World World { get; set; }

        #endregion

        #region Constructeur

        /// <summary>
        ///     Creates an instance. 
        /// </summary>
        /// <param name="world">The <see cref="World"/>.</param>
        protected BaseSystem(World world)
        {
            this.World = world;
        }

        #endregion

        #region Méthodes publiques

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

        #endregion
    }

    /// <summary>
    ///     A basic implementation of a <see cref="ISystem{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type passed to the <see cref="ISystem{T}"/> interface.</typeparam>
    public abstract class BaseSystem<T> : ISystem<T>
    {
        #region Propriétés

        /// <summary>
        ///     The world instance. 
        /// </summary>
        public World World { get; private set; }

        #endregion

        #region Constructeur

        /// <summary>
        ///     Creates an instance. 
        /// </summary>
        /// <param name="world">The <see cref="World"/>.</param>
        protected BaseSystem(World world)
        {
            this.World = world;
        }

        #endregion

        /// <inheritdoc />
        public virtual void Initialize() { }

        /// <inheritdoc />
        public virtual void BeforeUpdate(in T t) { }

        /// <inheritdoc />
        public virtual void Update(in T t) { }

        /// <inheritdoc />
        public virtual void AfterUpdate(in T t) { }

        /// <inheritdoc />
        public virtual void Dispose() { }
    }

}
