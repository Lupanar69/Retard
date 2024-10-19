using Arch.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Retard.Cameras.ViewModels;
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
    /// <param name="spriteBatch">Pour afficher les sprites à l'écran</param>
    /// <param name="camE">La caméra du jeu</param>
    public readonly struct SpriteUILayerDrawSystem(SpriteBatch spriteBatch, Entity camE) : ISystem
    {
        #region Méthodes publiques

        /// <inheritdoc/>
        public void Update(World w)
        {
            Matrix m = CameraManager.GetCamera2DViewMatrix(w, camE);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, m);

            Queries.DrawWorldSpaceUILayerSpritesQuery(w, w, spriteBatch);

            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, null);

            Queries.DrawUILayerSpritesQuery(w, w, spriteBatch);

            spriteBatch.End();
        }

        #endregion
    }
}
