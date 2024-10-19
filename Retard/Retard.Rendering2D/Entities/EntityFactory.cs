using System.Runtime.CompilerServices;
using Arch.Core;
using Arch.LowLevel;
using Arch.Relationships;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Retard.Cameras.Components.Layers;
using Retard.Cameras.Models;
using Retard.Rendering2D.Components.Sprite;
using Retard.Rendering2D.Components.SpriteAtlas;
using Retard.Rendering2D.Components.UI;
using Retard.Rendering2D.ViewModels;

namespace Retard.Rendering2D.Entities
{
    /// <summary>
    /// Contient les méthodes de création
    /// des différentes entités
    /// </summary>
    public static class EntityFactory
    {
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
            Handle<Texture2D> handle = SpriteManager.Instance.RegisterTexture(in texture);

            return w.Create
                (
                    new SpriteAtlasTextureCD(handle),
                    new SpriteAtlasDimensionsCD(rows, columns)
                );
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
            Entity spriteE = w.Create
            (
                new SpritePositionCD(position),
                new SpriteRectCD(rect),
                new SpriteColorCD(Color.White)
            );

            w.AddRelationship<SpriteOf>(spriteE, spriteAtlasE);

            return spriteE;
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
            Entity spriteE = w.Create
            (
                new SpritePositionCD(position),
                new SpriteRectCD(rect),
                new SpriteColorCD(Color.White)
            );

            w.AddRelationship<SpriteOf>(spriteE, spriteAtlasE);

            int flagMask = 1 << 30; // start with high-order bit...
            while (flagMask != 0)   // loop terminates once all flags have been compared
            {
                // switch on only a single bit...

                switch (layers & (RenderingLayer)flagMask)
                {
                    case RenderingLayer.Default:
                        w.Add<DefaultLayerTag>(spriteE);
                        break;

                    case RenderingLayer.UI:
                        w.Add<UILayerTag>(spriteE);
                        break;
                }

                flagMask >>= 1;  // bit-shift the flag value one bit to the right
            }

            return spriteE;
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
        public static Entity CreateUISpriteEntity(World w, Entity spriteAtlasE, Vector2 position, Rectangle rect, bool worldSpace = false)
        {
            Entity spriteE = w.Create
            (
                new SpritePositionCD(position),
                new SpriteRectCD(rect),
                new SpriteColorCD(Color.White),
                new UILayerTag()
            );

            if (worldSpace)
            {
                w.Add<WorldSpaceUITag>(spriteE);
            }

            w.AddRelationship<SpriteOf>(spriteE, spriteAtlasE);

            return spriteE;
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
        public static void CreateSpriteEntities(World w, Entity spriteAtlasE, int count, UnsafeArray<Vector2> positions, UnsafeArray<Rectangle> rects)
        {
            for (int i = 0; i < count; ++i)
            {
                EntityFactory.CreateSpriteEntity(w, spriteAtlasE, positions[i], rects[i]);
            }
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
        public static void CreateSpriteEntities(World w, Entity spriteAtlasE, int count, UnsafeArray<Vector2> positions, UnsafeArray<Rectangle> rects, RenderingLayer layers = RenderingLayer.Default)
        {
            // Crée un LayerTag pour chaque layer renseigné

            int flagMask = 1 << 30; // start with high-order bit...
            while (flagMask != 0)   // loop terminates once all flags have been compared
            {
                // switch on only a single bit...

                switch (layers & (RenderingLayer)flagMask)
                {
                    case RenderingLayer.Default:
                        for (int j = 0; j < count; ++j)
                        {
                            w.Add<DefaultLayerTag>(EntityFactory.CreateSpriteEntity(w, spriteAtlasE, positions[j], rects[j]));
                        }
                        break;

                    case RenderingLayer.UI:
                        for (int j = 0; j < count; ++j)
                        {
                            w.Add<UILayerTag>(EntityFactory.CreateSpriteEntity(w, spriteAtlasE, positions[j], rects[j]));
                        }
                        break;
                }

                flagMask >>= 1;  // bit-shift the flag value one bit to the right
            }
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
        public static void CreateUISpriteEntities(World w, Entity spriteAtlasE, int count, UnsafeArray<Vector2> positions, UnsafeArray<Rectangle> rects, bool worldSpace = false)
        {
            for (int i = 0; i < count; ++i)
            {
                EntityFactory.CreateUISpriteEntity(w, spriteAtlasE, positions[i], rects[i], worldSpace);
            }
        }

        #endregion
    }
}
