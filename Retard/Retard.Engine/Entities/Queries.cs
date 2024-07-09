using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Arch.Core;
using Arch.Relationships;
using Arch.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Retard.Core.Components.Input;
using Retard.Core.Components.Sprites;
using Retard.Core.Models.Assets.Sprites;
using Retard.Engine.Components.Input;

namespace Retard.Core.Entities
{
    /// <summary>
    /// Regroupe les queries Arch pouvant être parallélisées
    /// </summary>
    public static partial class Queries
    {
        #region Input Bindings

        /// <summary>
        /// Calcule les valeurs de chaque InputBinding
        /// </summary>
        /// <param name="idsBU">Les IDs de chaque élément de la séquence de touches</param>
        /// <param name="typesBU">Les types de touche de chaque élément de la séquence de touches</param>
        /// <param name="statesBU">Les états valides de chaque élément de la séquence de touches</param>
        /// <param name="returnValuesBU">Les valeurs du binding à retourner</param>
        [Query]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ProcessButtonStateInputBindings(
            in InputBindingKeySequenceIDsBU idsBU,
            in InputBindingKeySequenceTypesBU typesBU,
            in InputBindingKeySequenceStatesBU statesBU,
            ref InputBindingButtonStateValuesBU returnValuesBU)
        {

        }

        /// <summary>
        /// Calcule les valeurs de chaque InputBinding
        /// </summary>
        /// <param name="idsBU">Les IDs de chaque élément de la séquence de touches</param>
        /// <param name="typesBU">Les types de touche de chaque élément de la séquence de touches</param>
        /// <param name="returnValuesBU">Les valeurs du binding à retourner</param>
        [Query]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ProcessVector1DKeysInputBindings(
            in InputBindingVector1DKeysIDsCD idsBU,
            in InputBindingVector1DKeysTypesCD typesBU,
            ref InputBindingVector1DValuesBU returnValuesBU)
        {

        }

        /// <summary>
        /// Calcule les valeurs de chaque InputBinding
        /// </summary>
        /// <param name="idsBU">Les IDs de chaque élément de la séquence de touches</param>
        /// <param name="typesBU">Les types de touche de chaque élément de la séquence de touches</param>
        /// <param name="returnValuesBU">Les valeurs du binding à retourner</param>
        [Query]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ProcessVector2DKeysInputBindings(
            in InputBindingVector2DKeysIDsCD idsBU,
            in InputBindingVector2DKeysTypesCD typesBU,
            ref InputBindingVector2DValuesBU returnValuesBU)
        {

        }

        /// <summary>
        /// Calcule les valeurs de chaque InputBinding
        /// </summary>
        /// <param name="deadZoneCD">La valeur en dessous de laquelle l'input est considéré comme inerte</param>
        /// <param name="triggerType">Le type du trigger de l'InputBinding</param>
        /// <param name="returnValuesBU">Les valeurs du binding à retourner</param>
        [Query]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ProcessVector1DTriggerInputBindings(
            in InputBindingDeadZoneCD deadZoneCD,
            in InputBindingTriggerTypeCD triggerType,
            ref InputBindingVector1DValuesBU returnValuesBU)
        {

        }

        /// <summary>
        /// Calcule les valeurs de chaque InputBinding
        /// </summary>
        /// <param name="deadZoneCD">La valeur en dessous de laquelle l'input est considéré comme inerte</param>
        /// <param name="joystickType">Le type du joystick de l'InputBinding</param>
        /// <param name="returnValuesBU">Les valeurs du binding à retourner</param>
        [Query]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ProcessVector2DJoystickInputBindings(
            in InputBindingDeadZoneCD deadZoneCD,
            in InputBindingJoystickTypeCD joystickType,
            ref InputBindingVector2DValuesBU returnValuesBU)
        {

        }

        /// <summary>
        /// Calcule les valeurs de chaque InputBinding
        /// </summary>
        /// <param name="deadZoneCD">La valeur en dessous de laquelle l'input est considéré comme inerte</param>
        /// <param name="joystickType">Le type du joystick de l'InputBinding</param>
        /// <param name="returnValuesBU">Les valeurs du binding à retourner</param>
        [Query]
        [All(typeof(InputBindingJoystickXAxisTag)), None(typeof(InputBindingJoystickYAxisTag))]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ProcessVector1DJoystickXInputBindings(
            in InputBindingDeadZoneCD deadZoneCD,
            in InputBindingJoystickTypeCD joystickType,
            ref InputBindingVector1DValuesBU returnValuesBU)
        {

        }


        /// <summary>
        /// Calcule les valeurs de chaque InputBinding
        /// </summary>
        /// <param name="deadZoneCD">La valeur en dessous de laquelle l'input est considéré comme inerte</param>
        /// <param name="joystickType">Le type du joystick de l'InputBinding</param>
        /// <param name="returnValuesBU">Les valeurs du binding à retourner</param>
        [Query]
        [All(typeof(InputBindingJoystickYAxisTag)), None(typeof(InputBindingJoystickXAxisTag))]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ProcessVector1DJoystickYInputBindings(
            in InputBindingDeadZoneCD deadZoneCD,
            in InputBindingJoystickTypeCD joystickType,
            ref InputBindingVector1DValuesBU returnValuesBU)
        {

        }

        #endregion

        #region Input Actions

        /// <summary>
        /// Détermine l'action à activer pour chaque InputAction
        /// </summary>
        /// <param name="w">Le monde contenant les entités</param>
        /// <param name="actionE">L'entité de l'InputAction</param>
        /// <param name="actionID">l'ID de l'InputAction</param>
        [Query]
        [All(typeof(InputActionButtonStateTag))]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ProcessButtonStateInputActions([Data] World w, in Entity actionE, in InputActionIDCD actionID)
        {
            ref var rel = ref w.GetRelationships<InputActionOf>(actionE);

            foreach (KeyValuePair<Entity, InputActionOf> child in rel)
            {
                Entity bindingE = child.Key;
            }
        }

        /// <summary>
        /// Détermine l'action à activer pour chaque InputAction
        /// </summary>
        /// <param name="w">Le monde contenant les entités</param>
        /// <param name="actionE">L'entité de l'InputAction</param>
        /// <param name="actionID">l'ID de l'InputAction</param>
        [Query]
        [All(typeof(InputActionVector1DTag))]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ProcessVector1DInputActions([Data] World w, in Entity actionE, in InputActionIDCD actionID)
        {
            ref var rel = ref w.GetRelationships<InputActionOf>(actionE);

            foreach (KeyValuePair<Entity, InputActionOf> child in rel)
            {
                Entity bindingE = child.Key;
            }
        }

        /// <summary>
        /// Détermine l'action à activer pour chaque InputAction
        /// </summary>
        /// <param name="w">Le monde contenant les entités</param>
        /// <param name="actionE">L'entité de l'InputAction</param>
        /// <param name="actionID">l'ID de l'InputAction</param>
        [Query]
        [All(typeof(InputActionVector2DTag))]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ProcessVector2DInputActions([Data] World w, in Entity actionE, in InputActionIDCD actionID)
        {
            ref var rel = ref w.GetRelationships<InputActionOf>(actionE);

            foreach (KeyValuePair<Entity, InputActionOf> child in rel)
            {
                Entity bindingE = child.Key;
            }
        }

        #endregion

        #region Sprites

        /// <summary>
        /// Màj le rect du sprite
        /// </summary>
        /// <param name="spriteAtlas">Le SpriteAtlas source</param>
        /// <param name="frame">L'ID du sprite actuel</param>
        /// <param name="rect">Les dimensions du sprite dans le SpriteAtlas</param>
        [Query]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UpdateAnimatedSpriteRect(
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
        public static void DrawSprites(
            [Data] in SpriteAtlas spriteAtlas,
            [Data] in SpriteBatch spriteBatch,
            in SpritePositionCD pos,
            in SpriteRectCD rect,
            in SpriteColorCD color)
        {
            Rectangle destinationRectangle =
                new((int)pos.Value.X, (int)pos.Value.Y, rect.Value.Width, rect.Value.Height);

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
        public static void UpdateAnimatedSpriteFrame(ref SpriteFrameCD frame,
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
