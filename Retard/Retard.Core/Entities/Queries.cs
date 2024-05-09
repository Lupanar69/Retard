using System.Runtime.CompilerServices;
using Arch.System;
using Retard.Core.Components.Sprites;

namespace Retard.Core.Entities
{
    /// <summary>
    /// Regroupe les queries Arch pouvant être parallélisées
    /// </summary>
    public static partial class Queries
    {
        #region Méthodes statiques

        /// <summary>
        /// Màj l'animation du sprite
        /// </summary>
        /// <param name="_">Temps depuis la dernière frame (inutilisé)</param>
        /// <param name="frame">L'ID du sprite actuel</param>
        /// <param name="relativeFrame">L'ID du sprite dans l'animation</param>
        /// <param name="animation">Les IDs de début et fin de l'animation</param>
        /// <param name="speed">La vitesse de l'animation</param>
        // Generates a query and calls this annotated method for all entities with position and velocity components.
        [Query]  // Marks method inside BaseSystem for source generation.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UpdateSpriteAnimation(ref SpriteFrameCD frame,
            ref AnimatedSpriteRelativeFrameCD relativeFrame,
            ref AnimatedSpriteAnimationCD animation,
            ref AnimatedSpriteSpeedCD speed)  // Entity requires atleast those components. "in Entity" can also be passed. 
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
