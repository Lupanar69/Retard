using System;
using System.Runtime.CompilerServices;
using Arch.Core;
using Arch.LowLevel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Retard.Cameras.Models;
using Retard.Rendering2D.Entities;

namespace Retard.Rendering2D.ViewModels
{
    /// <summary>
    /// Gère la création et destruction des entités des sprites
    /// </summary>
    public sealed class SpriteManager
    {
        #region Singleton

        /// <summary>
        /// Singleton
        /// </summary>
        public static SpriteManager Instance => SpriteManager._instance.Value;

        /// <summary>
        /// Singleton
        /// </summary>
        private static readonly Lazy<SpriteManager> _instance = new(() => new SpriteManager());

        #endregion

        #region Variables d'instance

        /// <summary>
        /// Permet d'accéder aux texture2Ds depuis les queries
        /// </summary>
        private Resources<Texture2D> _texture2DResources { get; init; }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        private SpriteManager()
        {
            this._texture2DResources = new Resources<Texture2D>(1);
        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Enregistre la texture2D et retourne un handle pour permettre 
        /// de l'associer à une entité
        /// </summary>
        /// <param name="texture">La texture à enregistrer</param>
        /// <returns>Une référence unsafe à la texture</returns>
        public Handle<Texture2D> RegisterTexture(in Texture2D texture)
        {
            return this._texture2DResources.Add(in texture);
        }

        /// <summary>
        /// Retire la texture2D de la liste de ressources
        /// </summary>
        /// <param name="handle">La référence de la texture à enregistrer</param>
        public void UnregisterTexture(in Handle<Texture2D> handle)
        {
            this._texture2DResources.Remove(in handle);
        }

        /// <summary>
        /// Obtient la texture2D depuis la liste de ressources
        /// </summary>
        /// <param name="handle">La référence de la texture à récupérer</param>
        public Texture2D GetTexture(in Handle<Texture2D> handle)
        {
            return this._texture2DResources.Get(in handle);
        }

        #endregion

        #region Méthodes statiques publiques

        /// <summary>
        /// Crée l'entité d'un SpriteAtlas
        /// </summary>
        /// <param name="w">Le monde contenant ces entités</param>
        /// <param name="texture">La texture source du sprite</param>
        /// <param name="rows">Le nombre de lignes de sprite</param>
        /// <param name="columns">Le nombre de colonnes de sprite</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Entity CreateSpriteAtlasEntity(World w, Texture2D texture, int rows, int columns)
        {
            return EntityFactory.CreateSpriteAtlasEntity(w, texture, rows, columns);
        }

        /// <summary>
        /// Crée l'entité d'un sprite
        /// </summary>
        /// <param name="w">Le monde contenant ces entités</param>
        /// <param name="spriteAtlasE">L'entité de leur SpriteAtlas</param>
        /// <param name="position">La position du sprite</param>
        /// <param name="rect">Les dimensions du sprite</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Entity CreateSpriteEntity(World w, Entity spriteAtlasE, Vector2 position, Rectangle rect)
        {
            return EntityFactory.CreateSpriteEntity(w, spriteAtlasE, position, rect);
        }

        /// <summary>
        /// Crée l'entité d'un sprite
        /// </summary>
        /// <param name="w">Le monde contenant ces entités</param>
        /// <param name="spriteAtlasE">L'entité de leur SpriteAtlas</param>
        /// <param name="position">La position du sprite</param>
        /// <param name="rect">Les dimensions du sprite</param>
        /// <param name="layers">Les layers à appliquer au sprite</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Entity CreateSpriteEntity(World w, Entity spriteAtlasE, Vector2 position, Rectangle rect, RenderingLayer layers = RenderingLayer.Default)
        {
            return EntityFactory.CreateSpriteEntity(w, spriteAtlasE, position, rect, layers);
        }

        /// <summary>
        /// Crée l'entité d'un sprite devant apparaître en tant qu'UI. Recommendé pour les sprites d'UI au lieu de CreateSpriteEntity().
        /// </summary>
        /// <param name="w">Le monde contenant ces entités</param>
        /// <param name="spriteAtlasE">L'entité de leur SpriteAtlas</param>
        /// <param name="position">La position du sprite</param>
        /// <param name="rect">Les dimensions du sprite</param>
        /// <param name="worldSpace"><see langword="true"/> si l'ui est fixe sur l'écran, <see langword="false"/> si elle dépend de la matrice de la caméra</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Entity CreateWorldSpaceUISpriteEntity(World w, Entity spriteAtlasE, Vector2 position, Rectangle rect)
        {
            return EntityFactory.CreateWorldSpaceUISpriteEntity(w, spriteAtlasE, position, rect);
        }

        /// <summary>
        /// Crée les entités des sprites
        /// </summary>
        /// <param name="w">Le monde contenant ces entités</param>
        /// <param name="spriteAtlasE">L'entité de leur SpriteAtlas</param>
        /// <param name="count">Le nombre de sprites à créer</param>
        /// <param name="positions">Les positions des sprites</param>
        /// <param name="rects">Les dimensions des sprites</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnsafeArray<Entity> CreateSpriteEntities(World w, Entity spriteAtlasE, int count, UnsafeArray<Vector2> positions, UnsafeArray<Rectangle> rects)
        {
            return EntityFactory.CreateSpriteEntities(w, spriteAtlasE, count, positions, rects);
        }

        /// <summary>
        /// Crée les entités des sprites
        /// </summary>
        /// <param name="w">Le monde contenant ces entités</param>
        /// <param name="spriteAtlasE">L'entité de leur SpriteAtlas</param>
        /// <param name="count">Le nombre de sprites à créer</param>
        /// <param name="positions">Les positions des sprites</param>
        /// <param name="rects">Les dimensions des sprites</param>
        /// <param name="layers">Les layers à appliquer au sprite</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnsafeArray<Entity> CreateSpriteEntities(World w, Entity spriteAtlasE, int count, UnsafeArray<Vector2> positions, UnsafeArray<Rectangle> rects, RenderingLayer layers = RenderingLayer.Default)
        {
            return EntityFactory.CreateSpriteEntities(w, spriteAtlasE, count, positions, rects, layers);
        }

        /// <summary>
        /// Crée les entités des sprites devant apparaître en tant qu'UI. Recommendé pour les sprites d'UI au lieu de CreateSpriteEntities().
        /// </summary>
        /// <param name="w">Le monde contenant ces entités</param>
        /// <param name="spriteAtlasE">L'entité de leur SpriteAtlas</param>
        /// <param name="count">Le nombre de sprites à créer</param>
        /// <param name="positions">Les positions des sprites</param>
        /// <param name="rects">Les dimensions des sprites</param>
        /// <param name="worldSpace"><see langword="true"/> si l'ui est fixe sur l'écran, <see langword="false"/> si elle dépend de la matrice de la caméra</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnsafeArray<Entity> CreateWorldSpaceUISpriteEntities(World w, Entity spriteAtlasE, int count, UnsafeArray<Vector2> positions, UnsafeArray<Rectangle> rects)
        {
            return EntityFactory.CreateWorldSpaceUISpriteEntities(w, spriteAtlasE, count, positions, rects);
        }

        /// <summary>
        /// Calcule les dimensions du sprite
        /// </summary>
        /// <param name="texture">La source</param>
        /// <param name="rows">Le nombre de lignes de l'atlas</param>
        /// <param name="columns">Le nombre de colonnes de l'atlas</param>
        /// <param name="frame">L'id du sprite dans l'atlas à afficher</param>
        /// <returns>Les dimensions du sprite</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle GetSpriteRect(in Texture2D texture, int rows, int columns, int frame)
        {
            int width = texture.Width / columns;
            int height = texture.Height / rows;
            int row = frame / columns;
            int column = frame % columns;

            return new Rectangle(width * column, height * row, width, height);
        }

        #endregion
    }
}
