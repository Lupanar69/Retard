using Arch.Core;
using Arch.Core.Utils;
using Arch.LowLevel;
using Arch.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Retard.Core.Components.Sprites;
using Retard.Core.Models;
using Retard.Core.Models.Assets.Sprites;
using Retard.Core.Models.ValueTypes;
using Retard.Core.ViewModels;

namespace Retard.Core.Systems.Tests
{
    /// <summary>
    /// Crée des sprites fixes et animés pour tester les performances d'Arch
    /// </summary>
    public sealed partial class SpriteCreateSystemTest : BaseSystem<World, float>
    {
        #region Variables d'instance

        /// <summary>
        /// La texture des sprites
        /// </summary>
        private readonly SpriteAtlas _spriteAtlas;

        /// <summary>
        /// Nb de sprites à créer
        /// </summary>
        private readonly int2 _size;

        /// <summary>
        /// L'archétype des sprites
        /// </summary>
        private ComponentType[] _spriteArchetype = new ComponentType[] { typeof(SpriteRectCD), typeof(SpritePositionCD), typeof(SpriteColorCD) };

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="world">Le monde contenant les entités</param>
        /// <param name="spriteAtlas">La texture des sprites/param>
        /// <param name="spriteAtlas">La taille de la carte/param>
        public SpriteCreateSystemTest(World world, SpriteAtlas spriteAtlas, int2 size) : base(world)
        {
            this._spriteAtlas = spriteAtlas;
            this._size = size;
            world.Reserve(this._spriteArchetype, size.X * size.Y);
        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Màj à chaque frame
        /// </summary>
        /// <param name="_"></param>
        public override void Update(in float _)
        {
            if (KeyboardInput.IsKeyDown(Keys.Space))
            {
                using UnsafeArray<Entity> es = new(this._size.X * this._size.Y);
                int count = 0;

                for (int y = 0; y < this._size.Y; y++)
                {
                    for (int x = 0; x < this._size.X; x++)
                    {
                        Entity e = es[x + count] = this.World.Create(this._spriteArchetype);
                        this.World.Set(e, new SpritePositionCD(new Vector2(x, y) * Constants.SPRITE_SIZE_PIXELS));
                        this.World.Set(e, new SpriteColorCD(Color.White));
                    }

                    count += this._size.X;
                }

                count = 0;

                for (int i = 0; i < this._size.X; i++)
                {
                    this.World.Set(es[i], new SpriteRectCD() { Value = this._spriteAtlas.GetSpriteRect(0) });
                }

                count += this._size.X;


                for (int y = 1; y < this._size.Y - 1; y++)
                {
                    this.World.Set(es[count], new SpriteRectCD() { Value = this._spriteAtlas.GetSpriteRect(0) });

                    for (int x = 1; x < this._size.X - 1; x++)
                    {
                        this.World.Set(es[x + count], new SpriteRectCD() { Value = this._spriteAtlas.GetSpriteRect(1) });
                    }

                    this.World.Set(es[count + this._size.X - 1], new SpriteRectCD() { Value = this._spriteAtlas.GetSpriteRect(0) });

                    count += this._size.X;
                }

                for (int i = count; i < count + this._size.X; i++)
                {
                    this.World.Set(es[i], new SpriteRectCD() { Value = this._spriteAtlas.GetSpriteRect(0) });
                }
            }
        }

        #endregion
    }
}
