using System.Runtime.CompilerServices;
using Arch.Core;
using Arch.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Retard.Rendering2D.Components;
using Retard.Rendering2D.Models;

namespace Retard.Rendering2D.Entities
{
    /// <summary>
    /// Regroupe les queries Arch pouvant être parallélisées
    /// </summary>
    internal static partial class Queries
    {
        #region Sprites

        /// <summary>
        /// Màj le rect du sprite
        /// </summary>
        /// <param name="spriteAtlas">Le SpriteAtlas source</param>
        /// <param name="frame">L'ID du sprite actuel</param>
        /// <param name="rect">Les dimensions du sprite dans le SpriteAtlas</param>
        [Query]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void UpdateAnimatedSpriteRect(
            [Data] in SpriteAtlas spriteAtlas,
            in SpriteFrameCD frame,
            ref SpriteRectCD rect)
        {
            rect.Value = spriteAtlas.GetSpriteRect(frame.Value);
        }

        /// <summary>
        /// Màj le rect du sprite
        /// </summary>
        /// <param name="spriteAtlas">Le SpriteAtlas source</param>
        /// <param name="frame">L'ID du sprite actuel</param>
        /// <param name="rect">Les dimensions du sprite dans le SpriteAtlas</param>
        [Query]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void DrawSprites(
            [Data] in SpriteAtlas spriteAtlas,
            [Data] in SpriteBatch spriteBatch,
            in SpritePositionCD pos,
            in SpriteRectCD rect,
            in SpriteColorCD color)
        {

            Rectangle destinationRectangle = new((int)pos.Value.X, (int)pos.Value.Y, rect.Value.Width, rect.Value.Height);
            spriteBatch.Draw(spriteAtlas.Texture, destinationRectangle, rect.Value, color.Value);
        }

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

        #endregion
    }
}
