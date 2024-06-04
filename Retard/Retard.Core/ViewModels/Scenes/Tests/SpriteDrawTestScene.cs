using System;
using Arch.Core;
using Arch.LowLevel;
using Arch.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using Retard.Core.Components.Sprites;
using Retard.Core.Models;
using Retard.Core.Models.Assets.Scene;
using Retard.Core.Models.Assets.Sprites;
using Retard.Core.Systems.Sprite;
using Retard.Core.ViewModels.Controllers;
using Retard.Core.ViewModels.Input;

namespace Retard.Core.ViewModels.Scenes.Tests
{
    /// <summary>
    /// Scène de test
    /// </summary>
    public sealed class SpriteDrawTestScene : IScene, IDisposable
    {
        #region Properties

        /// <summary>
        /// <see langword="true"/> si la scène doit bloquer les inputs 
        /// pour les scènes qui suivent
        /// (ex: une scène de pause superposée à la scène de jeu)
        /// </summary>
        public bool ConsumeInput { get; init; }

        #endregion

        #region Variables d'instance

        /// <summary>
        /// <see langword="true"/> si l'on a appelé Dispose()
        /// </summary>
        private bool _disposedValue;

        /// <summary>
        /// Le contrôleur de la caméra du jeu
        /// </summary>
        private readonly OrthographicCameraController _cameraController;

        /// <summary>
        /// La caméra du jeu
        /// </summary>
        private readonly OrthographicCamera _camera;

        /// <summary>
        /// Le contrôleur pour clavier
        /// </summary>
        private readonly KeyboardInput _keyboardInput;

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
        private readonly Point _size;

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
        /// <param name="camera">La caméra du jeu</param>
        public SpriteDrawTestScene(OrthographicCamera camera)
        {
            this._camera = camera;
            this._cameraController = new OrthographicCameraController(this._camera);
            this._keyboardInput = InputManager.GetScheme<KeyboardInput>();
            this._size = new Point(60);
            SceneManager.World.Reserve(_spriteArchetype, _size.X * _size.Y);
        }

        /// <summary>
        /// Nettoie l'objet
        /// </summary>
        //// TODO: override finalizer only if 'Dispose(bool disposingManaged)' has code to free unmanaged resources
        //~SpriteDrawTestScene()
        //{
        //    // Do not change this code. Put cleanup code in 'Dispose(bool disposingManaged)' method
        //    Dispose(disposingManaged: false);
        //}

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Chargement du contenu
        /// </summary>
        public void Initialize()
        {
            this._updateSystems = new Group<float>("Update Systems");
            this._drawSystems = new Group<byte>("Draw Systems");
        }

        /// <summary>
        /// Màj à chaque frame
        /// </summary>
        public void LoadContent()
        {
            Texture2D debugTex = SceneManager.Content.Load<Texture2D>($"{Constants.TEXTURES_DIR_PATH_DEBUG}tiles_test2");
            this._spriteAtlas = new(debugTex, 4, 4);

            // Créé ici car on a besoin de récupérer les textures

            this._drawSystems.Add(new SpriteDrawSystem(SceneManager.World, SceneManager.SpriteBatch, this._spriteAtlas, this._camera));
            this._updateSystems.Add(new AnimatedSpriteUpdateSystem(SceneManager.World));

            this._updateSystems.Initialize();
            this._drawSystems.Initialize();
        }

        /// <summary>
        /// Récupère les inputs nécessaires au fonctionnement des systèmes
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début de l'application</param>
        public void UpdateInput(GameTime gameTime)
        {
            this._cameraController.Update(gameTime);

            if (this._keyboardInput.IsKeyPressed(Keys.Space))
            {
                this.CreateSpriteEntities();
            }
        }

        /// <summary>
        /// Pour afficher des éléments à l'écran
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début de l'application</param>
        public void Update(GameTime gameTime)
        {
            this._updateSystems.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        /// <summary>
        /// Pour afficher des éléments à l'écran
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début de l'application</param>
        public void Draw(GameTime gameTime)
        {
            this._drawSystems.Update(0);
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
        /// Nettoie l'objet
        /// </summary>
        /// <param name="disposingManaged"><see langword="true"/> si on doit nettoyer des objets</param>
        private void Dispose(bool disposingManaged)
        {
            if (!this._disposedValue)
            {
                if (disposingManaged)
                {
                    this._cameraController.Dispose();
                    this._updateSystems.Dispose();
                    this._drawSystems.Dispose();
                }

                this._disposedValue = true;
            }
        }

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
                    Entity e = es[x + count] = SceneManager.World.Create(this._spriteArchetype);
                    SceneManager.World.Set(e, new SpritePositionCD(new Vector2(x, y) * Constants.SPRITE_SIZE_PIXELS));
                }

                count += this._size.X;
            }

            SceneManager.World.Set(in this._spriteDesc, new SpriteColorCD(Color.White));
            count = 0;

            for (int i = 0; i < this._size.X; i++)
            {
                SceneManager.World.Set(es[i], new SpriteRectCD() { Value = this._spriteAtlas.GetSpriteRect(0) });
            }

            count += _size.X;


            for (int y = 1; y < this._size.Y - 1; y++)
            {
                SceneManager.World.Set(es[count], new SpriteRectCD() { Value = this._spriteAtlas.GetSpriteRect(0) });

                for (int x = 1; x < this._size.X - 1; x++)
                {
                    SceneManager.World.Set(es[x + count], new SpriteRectCD() { Value = this._spriteAtlas.GetSpriteRect(1) });
                }

                SceneManager.World.Set(es[count + this._size.X - 1], new SpriteRectCD() { Value = this._spriteAtlas.GetSpriteRect(0) });

                count += _size.X;
            }

            for (int i = count; i < count + this._size.X; i++)
            {
                SceneManager.World.Set(es[i], new SpriteRectCD() { Value = this._spriteAtlas.GetSpriteRect(0) });
            }
        }

        #endregion
    }
}
