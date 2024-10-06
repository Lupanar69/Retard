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
    /// <param name="w">Le monde contenant les entités des sprites</param>
    public readonly struct AnimatedSpriteUpdateSystem : ISystem
    {
        #region Méthodes publiques

        /// <inheritdoc/>
        public void Update(World w)
        {
            Queries.UpdateAnimatedSpriteFrameQuery(w);
            Queries.UpdateAnimatedSpriteRectQuery(w, w);
        }

        #endregion
    }
}
