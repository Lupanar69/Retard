using Arch.Core;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using Retard.Core.Models.Arch;
using Retard.Rendering2D.Entities;
using Retard.Rendering2D.Models;

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
    /// <param name="spriteAtlas">L'atlas des sprites à afficher</param>
    /// <param name="camera">La caméra du jeu</param>
    public readonly struct SpriteDrawSystem(World world, SpriteBatch spriteBatch, SpriteAtlas spriteAtlas, OrthographicCamera camera) : ISystemWorld
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
            Queries.UpdateAnimatedSpriteRectQuery(this.World, in spriteAtlas);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.GetViewMatrix());

            Queries.DrawSpritesQuery(this.World, in spriteAtlas, in spriteBatch);

            spriteBatch.End();
        }

        #endregion
    }
}
