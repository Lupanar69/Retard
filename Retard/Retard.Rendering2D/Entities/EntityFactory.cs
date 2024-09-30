using System.Runtime.CompilerServices;
using Arch.Core;
using Arch.LowLevel;
using Arch.Relationships;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Retard.Rendering2D.Components.Sprite;
using Retard.Rendering2D.Components.SpriteAtlas;
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
            Handle<Texture2D> handle = SpriteManager.Instance.Texture2DResources.Add(in texture);

            return w.Create
                (
                    new SpriteAtlasTextureCD(handle),
                    new SpriteAtlasDimensionsCD(rows, columns)
                );
        }

        /// <summary>
        /// Crée les entités des sprites
        /// </summary>
        /// <param name="w">Le monde contenant ces entités</param>
        /// <param name="spriteAtlasE">L'entité de leur SpriteAtlas</param>
        /// <param name="positions">Les positions des sprites</param>
        /// <param name="rects">Les dimensions des sprites</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateSpriteEntities(World w, Entity spriteAtlasE, UnsafeArray<Vector2> positions, UnsafeArray<Rectangle> rects)
        {
            for (int i = 0; i < positions.Length; ++i)
            {
                Entity spriteE = w.Create
                    (
                        new SpritePositionCD(positions[i]),
                        new SpriteRectCD(rects[i]),
                        new SpriteColorCD(Color.White)
                    );

                w.AddRelationship<SpriteOf>(spriteE, spriteAtlasE);
            }
        }

        #endregion
    }
}
