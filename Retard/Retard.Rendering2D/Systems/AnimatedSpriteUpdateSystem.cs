using Arch.Core;
using Retard.Core.Models.Arch;
using Retard.Rendering2D.Entities;

namespace Retard.Rendering2D.Systems
{
    /// <summary>
    /// Màj les frames des des sprites animés
    /// </summary>
    /// <remarks>
    /// Constructeur
    /// </remarks>
    /// <param name="world">Le monde contenant les entités des sprites</param>
    public readonly struct AnimatedSpriteUpdateSystem(World world) : ISystemWorld
    {
        #region Propriétés

        /// <summary>
        /// Le monde contenant les entités
        /// </summary>
        public readonly World World { get; init; } = world;

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void Update()
        {
            Queries.UpdateAnimatedSpriteFrameQuery(this.World);
        }

        #endregion
    }
}
