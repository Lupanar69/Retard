using Arch.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Retard.Cameras.Components.Camera;
using Retard.Cameras.ViewModels;
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
    /// <param name="camE">La caméra du jeu</param>
    public readonly struct SpriteUILayerDrawSystem(Entity camE) : ISystem<RenderingComponents2D>
    {
        #region Méthodes publiques

        /// <inheritdoc/>
        public void Update(World w, RenderingComponents2D components)
        {
            Viewport viewport = w.Get<Camera2DViewportCD>(camE).Value;
            components.GraphicsDevice.Viewport = viewport;

            Matrix m = CameraManager.GetCamera2DViewMatrix(w, camE);

            components.SpriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, m);

            Queries.DrawWorldSpaceUILayerSpritesQuery(w, w, components.SpriteBatch);

            components.SpriteBatch.End();

            components.SpriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, null);

            Queries.DrawUILayerSpritesQuery(w, w, components.SpriteBatch);

            components.SpriteBatch.End();
        }

        #endregion
    }
}
