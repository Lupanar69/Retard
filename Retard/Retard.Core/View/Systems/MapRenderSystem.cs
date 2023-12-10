using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using Retard.Core.Models;
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

        /// <summary>
        /// La caméra du jeu
        /// </summary>
        private Camera _camera;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="graphicsDevice">Utilisé pour créer le SpriteBatch</param>
        public MapRenderSystem(GraphicsDevice graphicsDevice, Camera camera)
            : base(Aspect.All(typeof(TilePositionCD), typeof(TileSpriteCD)))
        {
            this._spriteBatch = new SpriteBatch(graphicsDevice);
            this._camera = camera;
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
            // TAF : Modifier le script de la caméra
            // pour améliorer le positionnement de la carte
            // et permettre le déplacement au clavier
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                this._camera.UpdateXYPos();
            }

            this._spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            foreach (int entityID in this.ActiveEntities)
            {
                TilePositionCD pos = this._tilePosMapper.Get(entityID);
                TileSpriteCD sprite = this._spriteMapper.Get(entityID);

                sprite.Sprite.Draw(in this._spriteBatch, pos.Value + this._camera.MouseXY, sprite.Color);
            }
            this._spriteBatch.End();
        }

        #endregion
    }
}
