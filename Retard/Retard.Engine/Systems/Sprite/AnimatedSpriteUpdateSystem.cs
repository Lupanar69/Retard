using Arch.Core;
using Retard.Engine.Entities;
using Retard.Core.Models.Arch;

namespace Retard.Engine.Systems.Sprite
{
    /// <summary>
    /// Màj les frames des des sprites animés
    /// </summary>
    public readonly struct AnimatedSpriteUpdateSystem : ISystemWorld
    {
        #region Propriétés

        /// <summary>
        /// Le monde contenant les entités
        /// </summary>
        public readonly World World { get; init; }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="world">Le monde contenant les entités des sprites</param>
        public AnimatedSpriteUpdateSystem(World world)
        {
            this.World = world;
        }

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
