using System.Runtime.CompilerServices;
using Arch.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using Retard.Core.Components.Sprites;
using Retard.Core.Models.Arch;
using Retard.Core.Models.Assets.Sprites;

namespace Retard.Core.Systems.Sprite
{
    /// <summary>
    /// Affiche les sprites à l'écran
    /// </summary>
    public struct SpriteDrawSystem : ISystemWorld
    {
        #region Propriétés

        /// <summary>
        /// Le monde contenant les entités
        /// </summary>
        public World World { get; set; }

        #endregion

        #region Variables d'instance

        /// <summary>
        /// L'atlas des sprites à afficher
        /// </summary>
        private readonly SpriteAtlas _spriteAtlas;

        /// <summary>
        /// Pour afficher les sprites à l'écran
        /// </summary>
        private readonly SpriteBatch _spriteBatch;

        /// <summary>
        /// La caméra du jeu
        /// </summary>
        private readonly OrthographicCamera _camera;

        /// <summary>
        /// Retrouve les components d'un sprite
        /// </summary>
        private QueryDescription _spriteDesc = new QueryDescription().WithAll<SpritePositionCD, SpriteRectCD, SpriteColorCD>();

        /// <summary>
        /// Retrouve les components d'un sprite animé
        /// </summary>
        private QueryDescription _animatedSpriteDesc = new QueryDescription().WithAll<SpriteFrameCD, SpriteRectCD>();

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="world">Le monde contenant les entités des sprites</param>
        /// <param name="spriteBatch">Pour afficher les sprites à l'écran</param>
        /// <param name="spriteAtlas">L'atlas des sprites à afficher</param>
        /// <param name="camera">La caméra du jeu</param>
        public SpriteDrawSystem(World world, SpriteBatch spriteBatch, SpriteAtlas spriteAtlas, OrthographicCamera camera)
        {
            this._spriteBatch = spriteBatch;
            this._spriteAtlas = spriteAtlas;
            this._camera = camera;
            this.World = world;
        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Màj à chaque frame
        /// </summary>
        public void Update()
        {
            var local = this;

            this.World.Query(in this._animatedSpriteDesc, (ref SpriteFrameCD frame, ref SpriteRectCD rect) =>
            {
                rect.Value = local._spriteAtlas.GetSpriteRect(frame.Value);
            });

            this._spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, this._camera.GetViewMatrix());

            this.World.Query(in this._spriteDesc, (ref SpritePositionCD pos, ref SpriteRectCD rect, ref SpriteColorCD color) =>
            {
                local.Draw(local._spriteAtlas, local._spriteBatch, rect.Value, pos.Value, color.Value);
            });

            _spriteBatch.End();
        }

        /// <summary>
        /// Libère les allocations
        /// </summary>
        public void Dispose()
        {

        }

        #endregion

        #region Méthodes privées

        /// <summary>
        /// Affiche le sprite à l'écran.
        /// </summary>
        /// <param name="atlas">Le sprite source</param>
        /// <param name="spriteBatch">Gère le rendu du sprite à l'écran</param>
        /// <param name="screenPos">La position en pixels</param>
        /// <param name="color">La couleur du sprite</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Draw(SpriteAtlas atlas, SpriteBatch spriteBatch, Rectangle rect, Vector2 screenPos, Color color)
        {
            Rectangle destinationRectangle =
                new((int)screenPos.X, (int)screenPos.Y, rect.Width, rect.Height);

            spriteBatch.Draw(atlas.Texture, destinationRectangle, rect, color);
        }

        #endregion
    }
}
