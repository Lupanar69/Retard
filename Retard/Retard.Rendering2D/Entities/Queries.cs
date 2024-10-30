using System.Runtime.CompilerServices;
using Arch.Core;
using Arch.Relationships;
using Arch.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Retard.Cameras.Components.Layers;
using Retard.Sprites.Components.Sprite;
using Retard.Sprites.Components.SpriteAtlas;
using Retard.Sprites.Components.UI;

namespace Retard.Sprites.Entities
{
    /// <summary>
    /// Regroupe les queries Arch pouvant être parallélisées
    /// </summary>
    internal static partial class Queries
    {
        #region Sprites

        /// <summary>
        /// Màj la frame du sprite
        /// </summary>
        /// <param name="frame">L'ID du sprite actuel</param>
        /// <param name="relativeFrame">L'ID du sprite dans l'animation</param>
        /// <param name="animation">Les IDs de début et fin de l'animation</param>
        /// <param name="speed">La vitesse de l'animation</param>
        [Query]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void UpdateAnimatedSpriteFrame(ref SpriteFrameCD frame,
            ref AnimatedSpriteRelativeFrameCD relativeFrame,
            in AnimatedSpriteAnimationCD animation,
            ref AnimatedSpriteSpeedCD speed)
        {
            speed.ElapsedFrames++;

            if (speed.ElapsedFrames == speed.TotalFrames)
            {
                speed.ElapsedFrames = 0;
                relativeFrame.Value = (relativeFrame.Value + 1) % animation.Length;
                frame.Value = animation.StartFrame + relativeFrame.Value;
            }
        }

        /// <summary>
        /// Màj le rect du sprite
        /// </summary>
        /// <param name="w">Le monde contenant les entités</param>
        /// <param name="spriteE">L'entité du sprite</param>
        /// <param name="frame">L'ID du sprite actuel</param>
        /// <param name="rect">Les dimensions du sprite dans le SpriteAtlas</param>
        [Query]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void UpdateAnimatedSpriteRect(
            [Data] World w,
            in Entity spriteE,
            in SpriteFrameCD frame,
            ref SpriteRectCD rect)
        {
            ref Relationship<SpriteOf> rel = ref w.GetRelationships<SpriteOf>(spriteE);
            foreach (var child in rel)
            {
                Entity atlasE = child.Key;
                SpriteAtlasTextureCD tex = w.Get<SpriteAtlasTextureCD>(atlasE);
                SpriteAtlasDimensionsCD dimensions = w.Get<SpriteAtlasDimensionsCD>(atlasE);

                int width = tex.Value.Width / dimensions.Columns;
                int height = tex.Value.Height / dimensions.Rows;
                int row = frame.Value / dimensions.Columns;
                int column = frame.Value % dimensions.Columns;

                rect.Value = new Rectangle(width * column, height * row, width, height);
            }
        }

        /// <summary>
        /// Màj le rect du sprite
        /// </summary>
        /// <param name="w">Le monde contenant les entités</param>
        /// <param name="spriteBatch">Pour afficher les sprites à l'écran</param>
        /// <param name="spriteE">L'entité du sprite</param>
        /// <param name="pos">La position du sprite</param>
        /// <param name="rect">Les dimensions du sprite dans le SpriteAtlas</param>
        /// <param name="color">La couleur du sprite</param>
        [Query]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void DrawSprites(
            [Data] World w,
            [Data] SpriteBatch spriteBatch,
            in Entity spriteE,
            in SpritePositionCD pos,
            in SpriteRectCD rect,
            in SpriteColorCD color)
        {
            ref Relationship<SpriteOf> rel = ref w.GetRelationships<SpriteOf>(spriteE);

            foreach (var child in rel)
            {
                Entity atlasE = child.Key;
                SpriteAtlasTextureCD tex = w.Get<SpriteAtlasTextureCD>(atlasE);
                Rectangle destinationRectangle = new((int)pos.Value.X, (int)pos.Value.Y, rect.Value.Width, rect.Value.Height);
                spriteBatch.Draw(tex.Value, destinationRectangle, rect.Value, color.Value);
            }
        }

        /// <summary>
        /// Màj le rect des sprites sur le layer Default
        /// </summary>
        /// <param name="w">Le monde contenant les entités</param>
        /// <param name="spriteBatch">Pour afficher les sprites à l'écran</param>
        /// <param name="spriteE">L'entité du sprite</param>
        /// <param name="pos">La position du sprite</param>
        /// <param name="rect">Les dimensions du sprite dans le SpriteAtlas</param>
        /// <param name="color">La couleur du sprite</param>
        [All(typeof(DefaultLayerTag))]
        [Query]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void DrawDefaultLayerSprites(
            [Data] World w,
            [Data] SpriteBatch spriteBatch,
            in Entity spriteE,
            in SpritePositionCD pos,
            in SpriteRectCD rect,
            in SpriteColorCD color)
        {
            ref Relationship<SpriteOf> rel = ref w.GetRelationships<SpriteOf>(spriteE);

            foreach (var child in rel)
            {
                Entity atlasE = child.Key;
                SpriteAtlasTextureCD tex = w.Get<SpriteAtlasTextureCD>(atlasE);
                Rectangle destinationRectangle = new((int)pos.Value.X, (int)pos.Value.Y, rect.Value.Width, rect.Value.Height);
                spriteBatch.Draw(tex.Value, destinationRectangle, rect.Value, color.Value);
            }
        }

        /// <summary>
        /// Màj le rect des sprites sur le layer UI
        /// </summary>
        /// <param name="w">Le monde contenant les entités</param>
        /// <param name="spriteBatch">Pour afficher les sprites à l'écran</param>
        /// <param name="spriteE">L'entité du sprite</param>
        /// <param name="pos">La position du sprite</param>
        /// <param name="rect">Les dimensions du sprite dans le SpriteAtlas</param>
        /// <param name="color">La couleur du sprite</param>
        [All(typeof(UILayerTag), typeof(WorldSpaceUITag))]
        [Query]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void DrawWorldSpaceUILayerSprites(
            [Data] World w,
            [Data] SpriteBatch spriteBatch,
            in Entity spriteE,
            in SpritePositionCD pos,
            in SpriteRectCD rect,
            in SpriteColorCD color)
        {
            ref Relationship<SpriteOf> rel = ref w.GetRelationships<SpriteOf>(spriteE);

            foreach (var child in rel)
            {
                Entity atlasE = child.Key;
                SpriteAtlasTextureCD tex = w.Get<SpriteAtlasTextureCD>(atlasE);
                Rectangle destinationRectangle = new((int)pos.Value.X, (int)pos.Value.Y, rect.Value.Width, rect.Value.Height);
                spriteBatch.Draw(tex.Value, destinationRectangle, rect.Value, color.Value);
            }
        }

        /// <summary>
        /// Màj le rect des sprites sur le layer UI
        /// </summary>
        /// <param name="w">Le monde contenant les entités</param>
        /// <param name="spriteBatch">Pour afficher les sprites à l'écran</param>
        /// <param name="spriteE">L'entité du sprite</param>
        /// <param name="pos">La position du sprite</param>
        /// <param name="rect">Les dimensions du sprite dans le SpriteAtlas</param>
        /// <param name="color">La couleur du sprite</param>
        [All(typeof(UILayerTag))]
        [None(typeof(WorldSpaceUITag))]
        [Query]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void DrawUILayerSprites(
            [Data] World w,
            [Data] SpriteBatch spriteBatch,
            in Entity spriteE,
            in SpritePositionCD pos,
            in SpriteRectCD rect,
            in SpriteColorCD color)
        {
            ref Relationship<SpriteOf> rel = ref w.GetRelationships<SpriteOf>(spriteE);

            foreach (var child in rel)
            {
                Entity atlasE = child.Key;
                SpriteAtlasTextureCD tex = w.Get<SpriteAtlasTextureCD>(atlasE);
                Rectangle destinationRectangle = new((int)pos.Value.X, (int)pos.Value.Y, rect.Value.Width, rect.Value.Height);
                spriteBatch.Draw(tex.Value, destinationRectangle, rect.Value, color.Value);
            }
        }

        #endregion
    }
}
