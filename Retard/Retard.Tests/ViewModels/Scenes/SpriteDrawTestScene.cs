using System.Runtime.CompilerServices;
using Arch.Core;
using Arch.LowLevel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using Retard.Core.Models.Arch;
using Retard.Input.Models.Assets;
using Retard.Rendering2D.Components.Sprite;
using Retard.Rendering2D.Components.SpriteAtlas;
using Retard.Rendering2D.Entities;
using Retard.Rendering2D.Systems;
using Retard.Rendering2D.ViewModels;
using Retard.SceneManagement.Models;

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
        /// Les systèmes du monde à màj dans Update()
        /// </summary>
        private readonly Group _updateSystems;

        /// <summary>
        /// Les systèmes du monde à màj dans Draw()
        /// </summary>
        private readonly Group _drawSystems;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="world">Le monde contenant les entités</param>
        /// <param name="spriteBatch">Pour afficher les sprites à l'écran</param>
        /// <param name="camera">La caméra du jeu</param>
        /// <param name="debugTex">La texture de debug</param>
        /// <param name="size">La taille de la carte à dessiner</param>
        /// <param name="spriteResolution">La résolution d'un sprite en pixels</param>
        public SpriteDrawTestScene(World world, SpriteBatch spriteBatch, OrthographicCamera camera, Texture2D debugTex, Point size, int spriteResolution)
        {
            this._camera = camera;

            // Charge les textures

            Entity spriteAtlasE = EntityFactory.CreateSpriteAtlasEntity(world, debugTex, 4, 4);

            // Initialise les systèmes

            world.Reserve([typeof(SpriteRectCD), typeof(SpritePositionCD), typeof(SpriteColorCD)], size.X * size.Y);
            this._updateSystems = new Group("Update Systems");
            this._drawSystems = new Group("Draw Systems");

            this._drawSystems.Add(new SpriteDrawSystem(world, spriteBatch, this._camera));
            this._updateSystems.Add(new AnimatedSpriteUpdateSystem(world));

            this._updateSystems.Initialize();
            this._drawSystems.Initialize();

            CreateSpriteEntities(world, spriteAtlasE, size, spriteResolution);
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

        #region Méthodes statiques privées

        /// <summary>
        /// Crée les entités des sprites
        /// pour tester les systèmes d'affihage
        /// </summary>
        /// <param name="world">Le monde contenant les entités</param>
        /// <param name="size">La taille de la carte à dessiner</param>
        /// <param name="spriteAtlasE">L'entité du SpriteAtlas</param>
        /// <param name="spriteResolution">La résolution d'un sprite en pixels</param>
        private static void CreateSpriteEntities(World world, Entity spriteAtlasE, Point size, int spriteResolution)
        {
            SpriteAtlasTextureCD texCD = world.Get<SpriteAtlasTextureCD>(spriteAtlasE);
            SpriteAtlasDimensionsCD dimensionsCD = world.Get<SpriteAtlasDimensionsCD>(spriteAtlasE);

            using UnsafeArray<Rectangle> rects = GetSpritesRects(size, texCD.Value, dimensionsCD.Rows, dimensionsCD.Columns);
            using UnsafeArray<Vector2> positions = GetSpritesPositions(size, spriteResolution);

            // Crée tous les sprites en un seul appel

            EntityFactory.CreateSpriteEntities(world, spriteAtlasE, positions, rects);
        }

        /// <summary>
        /// Calcule les dimensions de chaque sprite sur la grille
        /// </summary>
        /// <param name="size">La taille de la carte à dessiner</param>
        /// <param name="tex">La texture du SpritAtlas</param>
        /// <param name="rows">Le nombre de lignes de l'atlas</param>
        /// <param name="columns">Le nombre de colonnes de l'atlas</param>
        /// <returns>Les dimensions des sprites</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static UnsafeArray<Rectangle> GetSpritesRects(Point size, Texture2D tex, int rows, int columns)
        {
            UnsafeArray<Rectangle> rects = new(size.X * size.Y);
            Rectangle wallRect = SpriteManager.GetSpriteRect(in tex, rows, columns, 0);
            Rectangle floorRect = SpriteManager.GetSpriteRect(in tex, rows, columns, 1);
            int count = 0;

            for (int i = 0; i < size.X; ++i)
            {
                rects[i] = wallRect;
            }

            count += size.X;

            for (int y = 1; y < size.Y - 1; ++y)
            {
                rects[count] = wallRect;

                for (int x = 1; x < size.X - 1; ++x)
                {
                    rects[x + count] = floorRect;
                }

                rects[count + size.X - 1] = wallRect;
                count += size.X;
            }

            for (int i = count; i < count + size.X; ++i)
            {
                rects[i] = wallRect;
            }

            return rects;
        }

        /// <summary>
        /// Calcule les positions de chaque sprite sur la grille
        /// </summary>
        /// <returns>Les positions des sprites</returns>
        /// <param name="size">La taille de la carte à dessiner</param>
        /// <param name="spriteResolution">La résolution d'un sprite en pixels</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static UnsafeArray<Vector2> GetSpritesPositions(Point size, int spriteResolution)
        {
            UnsafeArray<Vector2> positions = new(size.X * size.Y);
            int count = 0;

            for (int y = 0; y < size.Y; ++y)
            {
                for (int x = 0; x < size.X; ++x)
                {
                    positions[x + count] = new Vector2(x, y) * spriteResolution;
                }

                count += size.X;
            }

            return positions;
        }

        #endregion
    }
}
