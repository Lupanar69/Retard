using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Arch.Core;
using Arch.Relationships;
using Arch.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Retard.Core.Components.Input;
using Retard.Core.Components.Sprites;
using Retard.Core.Models.Assets.Sprites;
using Retard.Core.ViewModels.Input;
using Retard.Engine.Components.Input;
using Retard.Engine.Models;

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
            ref InputButtonStateValuesBU returnValuesBU)
        {
            for (int i = 0; i < returnValuesBU.Value.Length; ++i)
            {
                for (int j = 0; j < idsBU.Value.Length; ++j)
                {
                    int id = idsBU.Value[j];
                    InputBindingKeyType type = typesBU.Value[j];
                    InputKeySequenceState validState = statesBU.Value[j];

                    // Si la séquence n'est pas valide

                    switch (type)
                    {
                        case InputBindingKeyType.MouseKey:

                            // Seul le contrôleur 1 peut utiliser la souris et le clavier
                            // (les autres contrôleurs sont des manettes)

                            if (i > 0)
                            {
                                return;
                            }

                            MouseKey mouseKey = (MouseKey)id;

                            if (InputManager.GetMouseKeyState(mouseKey) != validState)
                            {
                                returnValuesBU.Value[i] = false;
                                goto NextController;
                            }
                            break;

                        case InputBindingKeyType.KeyboardKey:

                            // Seul le contrôleur 1 peut utiliser la souris et le clavier
                            // (les autres contrôleurs sont des manettes)

                            if (i > 0)
                            {
                                return;
                            }

                            Keys keyboardKey = (Keys)id;

                            if (InputManager.GetKeyboardKeyState(keyboardKey) != validState)
                            {
                                returnValuesBU.Value[i] = false;
                                goto NextController;
                            }

                            break;

                        case InputBindingKeyType.GamePadKey:
                            Buttons gamePadKey = (Buttons)id;

                            if (InputManager.GetGamePadKeyState(i, gamePadKey) != validState)
                            {
                                returnValuesBU.Value[i] = false;
                                goto NextController;
                            }
                            break;

                        case InputBindingKeyType.JoystickKey:
                            JoystickKey joystickKey = (JoystickKey)id;

                            if (InputManager.GetJoystickKeyState(i, joystickKey) != validState)
                            {
                                returnValuesBU.Value[i] = false;
                                goto NextController;
                            }
                            break;
                    }
                }

                // Si la séquence est valide

                returnValuesBU.Value[i] = true;

                NextController:
                continue;
            }
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
            ref InputVector1DValuesBU returnValuesBU)
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
            ref InputVector2DValuesBU returnValuesBU)
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
            ref InputVector1DValuesBU returnValuesBU)
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
            ref InputVector2DValuesBU returnValuesBU)
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
            ref InputVector1DValuesBU returnValuesBU)
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
            ref InputVector1DValuesBU returnValuesBU)
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
        /// <param name="actionValues">La valeur de l'InputAction pour chaque contrôleur</param>
        [Query]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ProcessButtonStateInputActions(
            [Data] World w,
            in Entity actionE,
            in InputActionIDCD actionID,
            ref InputButtonStateValuesBU actionValues)
        {
            ref var rel = ref w.GetRelationships<InputActionOf>(actionE);

            for (int i = 0; i < actionValues.Value.Length; ++i)
            {
                // On regarde si tous les bindings sont au repos

                bool allInactive = true;

                foreach (KeyValuePair<Entity, InputActionOf> child in rel)
                {
                    Entity bindingE = child.Key;
                    InputButtonStateValuesBU bindingValues = w.Get<InputButtonStateValuesBU>(bindingE);

                    if (bindingValues.Value[i] == true)
                    {
                        allInactive = false;
                        break;
                    }
                }

                // Si les bindings sont au repos, on arrête l'action

                switch (allInactive)
                {
                    case true:
                        switch (actionValues.Value[i])
                        {
                            case true:
                                actionValues.Value[i] = false;
                                InputManager.GetButtonEvent(actionID.Value).Finished?.Invoke(i);
                                break;
                        }
                        break;
                    case false:

                        // Si certains bindings sont actifs, on lance l'action

                        foreach (KeyValuePair<Entity, InputActionOf> child in rel)
                        {
                            Entity bindingE = child.Key;
                            InputButtonStateValuesBU bindingValues = w.Get<InputButtonStateValuesBU>(bindingE);

                            switch (bindingValues.Value[i])
                            {
                                case true:
                                    switch (actionValues.Value[i])
                                    {
                                        case false:
                                            actionValues.Value[i] = true;
                                            InputManager.GetButtonEvent(actionID.Value).Started?.Invoke(i);
                                            break;

                                        case true:
                                            InputManager.GetButtonEvent(actionID.Value).Performed?.Invoke(i);
                                            goto NextController;    // Empêche plusieurs bindings actifs de réinvoquer l'action
                                    }
                                    break;
                            }
                        }
                        break;
                }

                NextController:
                continue;
            }
        }

        /// <summary>
        /// Détermine l'action à activer pour chaque InputAction
        /// </summary>
        /// <param name="w">Le monde contenant les entités</param>
        /// <param name="actionE">L'entité de l'InputAction</param>
        /// <param name="actionID">l'ID de l'InputAction</param>
        /// <param name="actionValues">La valeur de l'InputAction pour chaque contrôleur</param>
        [Query]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ProcessVector1DInputActions(
            [Data] World w,
            in Entity actionE,
            in InputActionIDCD actionID,
            ref InputVector1DValuesBU actionValues)
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
        /// <param name="actionValues">La valeur de l'InputAction pour chaque contrôleur</param>
        [Query]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ProcessVector2DInputActions(
            [Data] World w,
            in Entity actionE,
            in InputActionIDCD actionID,
            ref InputVector2DValuesBU actionValues)
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
