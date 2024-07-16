using System.Runtime.CompilerServices;
using Arch.Core;
using Arch.LowLevel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using Retard.Core.Components.Sprites;
using Retard.Core.Entities;
using Retard.Core.Models;
using Retard.Core.Models.Arch;
using Retard.Core.Models.Assets.Scene;
using Retard.Core.Models.Assets.Sprites;
using Retard.Core.Systems.Sprite;
using Retard.Core.ViewModels.Input;
using Retard.Core.ViewModels.Scenes;
using Retard.Engine.ViewModels.Input;

namespace Retard.Tests.ViewModels.Scenes
{
    /// <summary>
    /// Scène de test
    /// </summary>
    public sealed class SpriteDrawTestScene : IScene
    {
        #region Properties

        /// <inheritdoc/>
        public bool ConsumeInput { get; init; }

        /// <inheritdoc/>
        public bool ConsumeUpdate { get; init; }

        /// <inheritdoc/>
        public bool ConsumeDraw { get; init; }

        /// <inheritdoc/>
        public InputControls Controls { get; init; }

        #endregion

        #region Variables d'instance

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
        private readonly Group _updateSystems;

        /// <summary>
        /// Les systèmes du monde à màj dans Draw()
        /// </summary>
        private readonly Group _drawSystems;

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
        /// <param name="size">La taille de la carte à dessiner</param>
        public SpriteDrawTestScene(OrthographicCamera camera, Point size)
        {
            this._camera = camera;
            this._size = size;
            this._keyboardInput = InputManager.GetScheme<KeyboardInput>();
            this.Controls = new InputControls();
            this.Controls.GetButtonEvent("Test/CreateSprites").Started += this.CreateSpriteEntities;

            // Charge les textures

            Texture2D debugTex = SceneManager.Content.Load<Texture2D>($"{Constants.TEXTURES_DIR_PATH_DEBUG}tiles_test2");
            this._spriteAtlas = new SpriteAtlas(debugTex, 4, 4);

            // Initialise les systèmes

            SceneManager.World.Reserve(_spriteArchetype, this._size.X * this._size.Y);
            this._updateSystems = new Group("Update Systems");
            this._drawSystems = new Group("Draw Systems");

            this._drawSystems.Add(new SpriteDrawSystem(SceneManager.World, SceneManager.SpriteBatch, this._spriteAtlas, this._camera));
            this._updateSystems.Add(new AnimatedSpriteUpdateSystem(SceneManager.World));

            this._updateSystems.Initialize();
            this._drawSystems.Initialize();
        }

        #endregion

        #region Méthodes publiques

        /// <inheritdoc/>
        public void OnUpdate(GameTime gameTime)
        {
            this._updateSystems.Update();
        }

        /// <inheritdoc/>
        public void OnDraw(GameTime gameTime)
        {
            this._drawSystems.Update();
        }

        #endregion

        #region Méthodes privées

        /// <summary>
        /// Crée les entités des sprites
        /// pour tester les systèmes d'affihage
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void CreateSpriteEntities(int _)
        {
            using UnsafeArray<Rectangle> rects = this.GetSpritesRects();
            using UnsafeArray<Vector2> positions = this.GetSpritesPositions();

            // Crée tous les sprites en un seul appel

            EntityFactory.CreateSpriteEntities(SceneManager.World, positions, rects);
        }

        /// <summary>
        /// Calcule les dimensions de chaque sprite sur la grille
        /// </summary>
        /// <returns>Les dimensions des sprites</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private UnsafeArray<Rectangle> GetSpritesRects()
        {
            UnsafeArray<Rectangle> rects = new(this._size.X * this._size.Y);
            Rectangle wallRect = this._spriteAtlas.GetSpriteRect(0);
            Rectangle floorRect = this._spriteAtlas.GetSpriteRect(1);
            int count = 0;

            for (int i = 0; i < this._size.X; ++i)
            {
                rects[i] = wallRect;
            }

            count += _size.X;

            for (int y = 1; y < this._size.Y - 1; ++y)
            {
                rects[count] = wallRect;

                for (int x = 1; x < this._size.X - 1; ++x)
                {
                    rects[x + count] = floorRect;
                }

                rects[count + this._size.X - 1] = wallRect;
                count += _size.X;
            }

            for (int i = count; i < count + this._size.X; ++i)
            {
                rects[i] = wallRect;
            }

            return rects;
        }

        /// <summary>
        /// Calcule les positions de chaque sprite sur la grille
        /// </summary>
        /// <returns>Les positions des sprites</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private UnsafeArray<Vector2> GetSpritesPositions()
        {
            UnsafeArray<Vector2> positions = new(this._size.X * this._size.Y);
            int count = 0;

            for (int y = 0; y < this._size.Y; ++y)
            {
                for (int x = 0; x < this._size.X; ++x)
                {
                    positions[x + count] = new Vector2(x, y) * Constants.SPRITE_SIZE_PIXELS;
                }

                count += this._size.X;
            }

            return positions;
        }

        #endregion
    }
}
