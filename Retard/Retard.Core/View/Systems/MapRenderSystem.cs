using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using Retard.Core.Models.Components.Tiles;

namespace Retard.Core.View.Systems
{
    /// <summary>
    /// Affiche les cases de la carte à l'écran
    /// </summary>
    public class MapRenderSystem : EntityDrawSystem
    {
        #region Variables d'instance

        /// <summary>
        /// Pour afficher les sprites
        /// </summary>
        private readonly SpriteBatch _spriteBatch;

        /// <summary>
        /// Les positions de chaque case
        /// </summary>
        private ComponentMapper<TilePositionCD> _tilePosMapper;

        /// <summary>
        /// Les sprites de chaque case
        /// </summary>
        private ComponentMapper<TileSpriteCD> _spriteMapper;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="graphicsDevice">Utilisé pour créer le SpriteBatch</param>
        public MapRenderSystem(GraphicsDevice graphicsDevice)
            : base(Aspect.All(typeof(TilePositionCD), typeof(TileSpriteCD)))
        {
            this._spriteBatch = new SpriteBatch(graphicsDevice);
        }

        #endregion

        #region Fonctions publiques

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="mapperService">Pour initialiser les ComponentMappers</param>
        public override void Initialize(IComponentMapperService mapperService)
        {
            this._tilePosMapper = mapperService.GetMapper<TilePositionCD>();
            this._spriteMapper = mapperService.GetMapper<TileSpriteCD>();
        }

        /// <summary>
        /// Màj à chaque frame
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le lancement de l'application</param>
        public override void Draw(GameTime gameTime)
        {
            this._spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            foreach (int entityID in this.ActiveEntities)
            {
                TilePositionCD pos = this._tilePosMapper.Get(entityID);
                TileSpriteCD sprite = this._spriteMapper.Get(entityID);

                sprite.Sprite.Draw(in this._spriteBatch, pos.Value, sprite.Color);
            }
            this._spriteBatch.End();
        }

        #endregion
    }
}
