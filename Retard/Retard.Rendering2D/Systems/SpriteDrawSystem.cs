using Arch.Core;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using Retard.Core.Models.Arch;
using Retard.Rendering2D.Entities;

namespace Retard.Rendering2D.Systems
{
    /// <summary>
    /// Affiche les sprites à l'écran
    /// </summary>
    /// <remarks>
    /// Constructeur
    /// </remarks>
    /// <param name="world">Le monde contenant les entités des sprites</param>
    /// <param name="spriteBatch">Pour afficher les sprites à l'écran</param>
    /// <param name="camera">La caméra du jeu</param>
    public readonly struct SpriteDrawSystem(World world, SpriteBatch spriteBatch, OrthographicCamera camera) : ISystemWorld
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
            Queries.UpdateAnimatedSpriteRectQuery(this.World, this.World);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.GetViewMatrix());

            Queries.DrawSpritesQuery(this.World, this.World, spriteBatch);

            spriteBatch.End();
        }

        #endregion
    }
}
