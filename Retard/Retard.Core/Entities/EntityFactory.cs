using System.Runtime.CompilerServices;
using Arch.Core;
using Arch.LowLevel;
using Microsoft.Xna.Framework;
using Retard.Core.Components.Sprites;

namespace Retard.Core.Entities
{
    /// <summary>
    /// Contient les méthodes de création
    /// des différentes entités
    /// </summary>
    public static class EntityFactory
    {
        /* NOTE : 
         * On les crée sans archétype car d'après la doc,
         * c'est pus lent d'utiliser l'archétype et d'appeler Set() manuellement
         * pour chaque entité
         */

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
