using Arch.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Retard.Cameras.Components.Camera;
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
    public readonly struct SpriteDefaultLayerDrawSystem(SpriteBatch spriteBatch, Entity camE) : ISystem
    {
        #region Méthodes publiques

        /// <inheritdoc/>
        public void Update(World w)
        {
            ref var viewMatrixCD = ref w.Get<Camera2DViewMatrixCD>(camE);
            Matrix m = viewMatrixCD.Value;

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, m);

            Queries.DrawDefaultLayerSpritesQuery(w, w, spriteBatch);

            spriteBatch.End();
        }

        #endregion
    }
}
