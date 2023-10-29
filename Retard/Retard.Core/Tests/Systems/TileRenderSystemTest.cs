using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using Retard.Core.Components.Tiles;

namespace Retard.Core.Tests.Systems
{
    /// <summary>
    /// Pour tester l'affichage des sprites à l'écran
    /// </summary>
    public class TileRenderSystemTest : EntityDrawSystem
    {
        private readonly GraphicsDevice _graphicsDevice;
        private readonly SpriteBatch _spriteBatch;

        private ComponentMapper<Transform2> _transformMapper;
        private ComponentMapper<TileSpriteCD> _spriteMapper;

        public TileRenderSystemTest(GraphicsDevice graphicsDevice)
            : base(Aspect.All(typeof(Transform2), typeof(TileSpriteCD)))
        {
            _graphicsDevice = graphicsDevice;
            _spriteBatch = new SpriteBatch(graphicsDevice);
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _transformMapper = mapperService.GetMapper<Transform2>();
            _spriteMapper = mapperService.GetMapper<TileSpriteCD>();
        }

        public override void Draw(GameTime gameTime)
        {
            _graphicsDevice.Clear(Color.DarkBlue * 0.2f);
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            foreach (var entity in ActiveEntities)
            {
                var transform = _transformMapper.Get(entity);
                var sprite = _spriteMapper.Get(entity);

                sprite.Value.Draw(in _spriteBatch, transform.Position);
            }
            _spriteBatch.End();
        }
    }
}
