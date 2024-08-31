using System.Runtime.CompilerServices;
using Arch.Core;
using Arch.LowLevel;
using Microsoft.Xna.Framework;
using Retard.Engine.Components.Sprites;

namespace Retard.Engine.Entities
{
    /// <summary>
    /// Contient les méthodes de création
    /// des différentes entités
    /// </summary>
    public static class EntityFactory
    {
        #region Sprites

        /// <summary>
        /// Crée les entités des sprites
        /// </summary>
        /// <param name="world">Le monde contenant ces entités</param>
        /// <param name="positions">Les positions des sprites</param>
        /// <param name="rects">Les dimensions des sprites</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateSpriteEntities(World world, UnsafeArray<Vector2> positions, UnsafeArray<Rectangle> rects)
        {
            for (int i = 0; i < positions.Length; ++i)
            {
                world.Create
                    (
                    new SpritePositionCD { Value = positions[i] },
                    new SpriteRectCD { Value = rects[i] },
                    new SpriteColorCD { Value = Color.White }
                    );
            }
        }

        #endregion
    }
}
