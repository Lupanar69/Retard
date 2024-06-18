using Arch.Core;
using Retard.Core.Entities;
using Retard.Core.Models.Arch;

namespace Retard.Core.Systems.Sprite
{
    /// <summary>
    /// Màj les frames des des sprites animés
    /// </summary>
    public struct AnimatedSpriteUpdateSystem : ISystemWorld
    {
        #region Propriétés

        /// <summary>
        /// Le monde contenant les entités
        /// </summary>
        public World World { get; set; }

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
        /// Màj à chaque frame
        /// </summary>
        public void Update()
        {
            Queries.UpdateSpriteAnimationQuery(this.World);
        }

        /// <summary>
        /// Libère les allocations
        /// </summary>
        public void Dispose()
        {

        }

        #endregion
    }
}
