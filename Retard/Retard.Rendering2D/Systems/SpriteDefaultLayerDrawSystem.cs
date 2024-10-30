using Arch.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Retard.Cameras.Components.Camera;
using Retard.Cameras.ViewModels;
using Retard.Core.Models.Arch;
using Retard.Sprites.Entities;
using Retard.Sprites.Models;

namespace Retard.Sprites.Systems
{
    /// <summary>
    /// Affiche les sprites à l'écran
    /// </summary>
    /// <remarks>
    /// Constructeur
    /// </remarks>
    /// <param name="spriteBatch">Pour afficher les sprites à l'écran</param>
    /// <param name="camE">La caméra du jeu</param>
    public readonly struct SpriteDefaultLayerDrawSystem(Entity camE) : ISystem<RenderingComponents2D>
    {
        #region Méthodes publiques

        /// <inheritdoc/>
        public void Update(World w, RenderingComponents2D components)
        {
            Viewport viewport = w.Get<Camera2DViewportCD>(camE).Value;
            components.GraphicsDevice.Viewport = viewport;

            Matrix m = CameraManager.GetCamera2DViewMatrix(w, camE);

            components.SpriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, m);

            Queries.DrawDefaultLayerSpritesQuery(w, w, components.SpriteBatch);

            components.SpriteBatch.End();
        }

        #endregion
    }
}
