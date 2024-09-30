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
    /// <param name="w">Le monde contenant les entités des sprites</param>
    /// <param name="spriteBatch">Pour afficher les sprites à l'écran</param>
    /// <param name="camera">La caméra du jeu</param>
    public readonly struct SpriteDrawSystem(SpriteBatch spriteBatch, OrthographicCamera camera) : ISystem
    {
        #region Méthodes publiques

        /// <inheritdoc/>
        public void Update(World w)
        {
            Queries.UpdateAnimatedSpriteRectQuery(w, w);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.GetViewMatrix());

            Queries.DrawSpritesQuery(w, w, spriteBatch);

            spriteBatch.End();
        }

        #endregion
    }
}
