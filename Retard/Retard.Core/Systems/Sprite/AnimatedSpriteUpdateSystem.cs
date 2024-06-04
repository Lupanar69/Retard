using Arch.Core;
using Arch.System;
using Retard.Core.Entities;

namespace Retard.Core.Systems.Sprite
{
    /// <summary>
    /// Màj les frames des des sprites animés
    /// </summary>
    public sealed partial class AnimatedSpriteUpdateSystem : BaseSystem<World, float>
    {
        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="world">Le monde contenant les entités des sprites</param>
        public AnimatedSpriteUpdateSystem(World world) : base(world) { }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Màj à chaque frame
        /// </summary>
        public override void Update(in float _)
        {
            Queries.UpdateSpriteAnimationQuery(World);
        }

        #endregion
    }
}
