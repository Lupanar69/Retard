using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using Retard.Core.Components.Tiles;

namespace Retard.Core.Tests.Systems
{
    /// <summary>
    /// Pour tester la création des entités des tiles
    /// </summary>
    public class TileCreationSystemTest : EntitySystem
    {
        private readonly FastRandom _random = new FastRandom();
        private ComponentMapper<Transform2> _transformMapper;
        private ComponentMapper<TileSpriteCD> _spriteMapper;

        private Texture2D texture;

        public TileCreationSystemTest(Texture2D texture)
            : base(Aspect.All(typeof(Transform2), typeof(TileSpriteCD)))
        {
            this.texture = texture;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _transformMapper = mapperService.GetMapper<Transform2>();
            _spriteMapper = mapperService.GetMapper<TileSpriteCD>();

            for (int i = 0; i < 200; i++)
            {
                Vector2 pos = new(16 * i, 0);
                int frame = _random.Next(0, 16);
                CreateTile(texture, pos, frame);
            }
        }

        private int CreateTile(Texture2D tex, Vector2 position, int frame)
        {
            var entity = CreateEntity();
            entity.Attach(new Transform2(position));
            entity.Attach(new TileSpriteCD(tex, 4, 4, frame));
            return entity.Id;
        }
    }
}
