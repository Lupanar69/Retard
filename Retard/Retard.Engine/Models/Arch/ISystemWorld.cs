namespace Retard.Engine.Models.Arch
{
    /// <summary>
    ///     A basic implementation of a <see cref="ISystem"/>.
    /// </summary>
    public interface ISystemWorld : ISystem, IWorld
    {

    }

    /// <summary>
    ///     A basic implementation of a <see cref="ISystem{T}"/>.
    /// </summary>
    public interface ISystemWorld<T> : ISystem<T>, IWorld
    {

    }
}