using System.Runtime.CompilerServices;
using Arch.Core;
using Arch.LowLevel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Retard.App.ViewModels;
using Retard.Cameras.Models;
using Retard.Core.Models.Arch;
using Retard.Input.Models.Assets;
using Retard.Rendering2D.Components.Sprite;
using Retard.Rendering2D.Components.SpriteAtlas;
using Retard.Rendering2D.Systems;
using Retard.Rendering2D.ViewModels;
using Retard.SceneManagement.Models;
using Retard.Tests.ViewModels.Controllers;

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
        /// Le contrôleur de la caméra du jeu
        /// </summary>
        private readonly OrthographicCameraController _cameraController;

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
        /// <param name="camE">L'entité de la caméra du jeu</param>
        /// <param name="debugTex">La texture de debug</param>
        /// <param name="size">La taille de la carte à dessiner</param>
        /// <param name="spriteResolution">La résolution d'un sprite en pixels</param>
        /// <param name="appViewport">Gère la fenêtre</param>
        public SpriteDrawTestScene(World world, SpriteBatch spriteBatch, Entity camE, Texture2D debugTex, Point size, int spriteResolution, AppViewport appViewport)
        {
            this.Controls = new InputControls();
            this._cameraController = new OrthographicCameraController(world, camE, this.Controls, appViewport);

            // Initialise les systèmes

            world.Reserve([typeof(SpriteRectCD), typeof(SpritePositionCD), typeof(SpriteColorCD)], size.X * size.Y);
            this._updateSystems = new Group("Update Systems");
            this._drawSystems = new Group("Draw Systems");

            this._updateSystems.Add(new AnimatedSpriteUpdateSystem());
            //this._drawSystems.Add(new SpriteDrawSystem(spriteBatch, camE));
            this._drawSystems.Add(new SpriteDefaultLayerDrawSystem(spriteBatch, camE));
            this._drawSystems.Add(new SpriteUILayerDrawSystem(spriteBatch, camE));

            this._updateSystems.Initialize();
            this._drawSystems.Initialize();

            // Crée les sprites

            Entity spriteAtlasE = SpriteManager.CreateSpriteAtlasEntity(world, debugTex, 4, 4);
            SpriteDrawTestScene.CreateSpriteEntities(world, spriteAtlasE, size, spriteResolution);
        }

        #endregion

        #region Méthodes publiques

        /// <inheritdoc/>
        public void OnUpdate(World w, GameTime gameTime)
        {
            this._updateSystems.Update(w);
        }

        /// <inheritdoc/>
        public void OnDraw(World w, GameTime gameTime)
        {
            this._drawSystems.Update(w);
        }

        #endregion

        #region Méthodes statiques privées

        /// <summary>
        /// Crée les entités des sprites
        /// pour tester les systèmes d'affihage
        /// </summary>
        /// <param name="w">Le monde contenant les entités</param>
        /// <param name="size">La taille de la carte à dessiner</param>
        /// <param name="spriteAtlasE">L'entité du SpriteAtlas</param>
        /// <param name="spriteResolution">La résolution d'un sprite en pixels</param>
        private static void CreateSpriteEntities(World w, Entity spriteAtlasE, Point size, int spriteResolution)
        {
            SpriteAtlasTextureCD texCD = w.Get<SpriteAtlasTextureCD>(spriteAtlasE);
            SpriteAtlasDimensionsCD dimensionsCD = w.Get<SpriteAtlasDimensionsCD>(spriteAtlasE);

            using UnsafeArray<Rectangle> rects = GetSpritesRects(size, texCD.Value, dimensionsCD.Rows, dimensionsCD.Columns);
            using UnsafeArray<Vector2> positions = GetSpritesPositions(size, spriteResolution);

            // Crée 2 sprites sur le layer UI, un en worldSpace et l'autre à une position fixe

            SpriteManager.CreateWorldSpaceUISpriteEntity(w, spriteAtlasE, Vector2.Zero, SpriteManager.GetSpriteRect(texCD.Value, 1, 1, 0));
            SpriteManager.CreateSpriteEntity(w, spriteAtlasE, Vector2.Zero, SpriteManager.GetSpriteRect(texCD.Value, 1, 1, 0), RenderingLayer.UI);

            // Crée tous les sprites en un seul appel

            SpriteManager.CreateSpriteEntities(w, spriteAtlasE, size.X * size.Y, positions, rects, RenderingLayer.Default);
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

        #endregion
    }
}
