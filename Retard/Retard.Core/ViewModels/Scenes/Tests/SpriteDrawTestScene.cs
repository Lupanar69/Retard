using System;
using Arch.Core;
using Arch.LowLevel;
using Arch.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Retard.Core.Components.Sprites;
using Retard.Core.Models;
using Retard.Core.Models.Assets.Camera;
using Retard.Core.Models.Assets.Sprites;
using Retard.Core.Models.ValueTypes;
using Retard.Core.Systems;
using Retard.Core.ViewModels.Input;

namespace Retard.Core.ViewModels.Scenes.Tests
{
    /// <summary>
    /// Scène de tes
    /// </summary>
    public sealed class SpriteDrawTestScene : Scene, IDisposable
    {
        #region Variables d'instance

        /// <summary>
        /// La caméra du jeu
        /// </summary>
        private Camera _camera;

        /// <summary>
        /// <see langword="true"/> si l'on a appelé Dispose()
        /// </summary>
        private bool _disposedValue;

        /// <summary>
        /// Les systèmes du monde à màj dans Update()
        /// </summary>
        private Group<float> _updateSystems;

        /// <summary>
        /// Les systèmes du monde à màj dans Draw()
        /// </summary>
        private Group<byte> _drawSystems;

        /// <summary>
        /// La texture des sprites
        /// </summary>
        private SpriteAtlas _spriteAtlas;

        /// <summary>
        /// Nb de sprites à créer
        /// </summary>
        private readonly int2 _size;

        /// <summary>
        /// Retrouve les components d'un sprite
        /// </summary>
        private readonly QueryDescription _spriteDesc = new QueryDescription().WithAll<SpritePositionCD, SpriteRectCD, SpriteColorCD>();

        /// <summary>
        /// L'archétype des sprites
        /// </summary>
        private readonly Arch.Core.Utils.ComponentType[] _spriteArchetype = new Arch.Core.Utils.ComponentType[] { typeof(SpriteRectCD), typeof(SpritePositionCD), typeof(SpriteColorCD) };

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="content">Les assets du jeu</param>
        /// <param name="world">Le monde contenant les entités</param>
        /// <param name="spriteBatch">Pour afficher les sprites à l'écran</param>
        /// <param name="camera">La caméra du jeu</param>
        public SpriteDrawTestScene(ContentManager content, World world, SpriteBatch spriteBatch, Camera camera) : base(content, world, spriteBatch)
        {
            this._camera = camera;
            this._size = new int2(60);
            this._world.Reserve(_spriteArchetype, _size.X * _size.Y);
        }

        /// <summary>
        /// Nettoie l'objet
        /// </summary>
        //// TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        //~SpriteDrawTestScene()
        //{
        //    // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //    Dispose(disposing: false);
        //}

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Chargement du contenu
        /// </summary>
        public override void Initialize()
        {
            this._updateSystems = new Group<float>("Update Systems");
            this._drawSystems = new Group<byte>("Draw Systems");
        }

        /// <summary>
        /// Màj à chaque frame
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début de l'application</param>
        public override void LoadContent()
        {
            Texture2D debugTex = _content.Load<Texture2D>($"{Constants.TEXTURES_DIR_PATH_DEBUG}tiles_test2");
            this._spriteAtlas = new(debugTex, 4, 4);

            // Créé ici car on a besoin de récupérer les textures

            this._drawSystems.Add(new SpriteDrawSystem(_world, _spriteBatch, _spriteAtlas, _camera));
            this._updateSystems.Add(new AnimatedSpriteUpdateSystem(_world));

            this._updateSystems.Initialize();
            this._drawSystems.Initialize();
        }

        /// <summary>
        /// Récupère les inputs nécessaires au fonctionnement des systèmes
        /// </summary>
        public override void UpdateInput()
        {
            this._camera.UpdateInput();

            if (KeyboardInput.IsKeyDown(Keys.Space))
            {
                this.CreateSpriteEntities();
            }
        }

        /// <summary>
        /// Pour afficher des éléments à l'écran
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début de l'application</param>
        public override void Update(GameTime gameTime)
        {
            this._updateSystems.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        /// <summary>
        /// Pour afficher des éléments à l'écran
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début de l'application</param>
        public override void Draw(GameTime gameTime)
        {
            this._drawSystems.Update(0);
        }

        /// <summary>
        /// Nettoie l'objet
        /// </summary>
        /// <param name="disposingManaged"><see langword="true"/>si on doit nettoyer des objets</param>
        private void Dispose(bool disposingManaged)
        {
            if (!this._disposedValue)
            {
                if (disposingManaged)
                {
                    this._updateSystems.Dispose();
                    this._drawSystems.Dispose();
                }

                this._disposedValue = true;
            }
        }

        /// <summary>
        /// Nettoie l'objet
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            this.Dispose(disposingManaged: true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Méthodes privées

        /// <summary>
        /// Crée les entités des sprites
        /// pour tester les systèmes d'affihage
        /// </summary>
        private void CreateSpriteEntities()
        {
            using UnsafeArray<Entity> es = new(this._size.X * this._size.Y);
            int count = 0;

            for (int y = 0; y < this._size.Y; y++)
            {
                for (int x = 0; x < this._size.X; x++)
                {
                    Entity e = es[x + count] = this._world.Create(this._spriteArchetype);
                    this._world.Set(e, new SpritePositionCD(new Vector2(x, y) * Constants.SPRITE_SIZE_PIXELS));
                }

                count += this._size.X;
            }

            this._world.Set(in this._spriteDesc, new SpriteColorCD(Color.White));
            count = 0;

            for (int i = 0; i < this._size.X; i++)
            {
                this._world.Set(es[i], new SpriteRectCD() { Value = this._spriteAtlas.GetSpriteRect(0) });
            }

            count += _size.X;


            for (int y = 1; y < this._size.Y - 1; y++)
            {
                this._world.Set(es[count], new SpriteRectCD() { Value = this._spriteAtlas.GetSpriteRect(0) });

                for (int x = 1; x < this._size.X - 1; x++)
                {
                    this._world.Set(es[x + count], new SpriteRectCD() { Value = this._spriteAtlas.GetSpriteRect(1) });
                }

                this._world.Set(es[count + this._size.X - 1], new SpriteRectCD() { Value = this._spriteAtlas.GetSpriteRect(0) });

                count += _size.X;
            }

            for (int i = count; i < count + this._size.X; i++)
            {
                this._world.Set(es[i], new SpriteRectCD() { Value = this._spriteAtlas.GetSpriteRect(0) });
            }
        }

        #endregion
    }
}
