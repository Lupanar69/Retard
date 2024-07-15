using System;
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
        /// <param name="idsCD">Les IDs de chaque élément de la séquence de touches</param>
        /// <param name="typesCD">Les types de touche de chaque élément de la séquence de touches</param>
        /// <param name="returnValuesBU">Les valeurs du binding à retourner</param>
        [Query]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ProcessVector1DKeysInputBindings(
            in InputBindingVector1DKeysIDsCD idsCD,
            in InputBindingVector1DKeysTypesCD typesCD,
            ref InputVector1DValuesBU returnValuesBU)
        {
            for (int i = 0; i < returnValuesBU.Value.Length; ++i)
            {
                float positiveValue = 0f;
                float negativeValue = 0f;

                switch (typesCD.PositiveType)
                {
                    case InputBindingKeyType.MouseKey:

                        // Seul le contrôleur 1 peut utiliser la souris et le clavier
                        // (les autres contrôleurs sont des manettes)

                        if (i > 0)
                        {
                            return;
                        }

                        MouseKey mouseKey = (MouseKey)idsCD.PositiveID;
                        InputKeySequenceState mouseKeyState = InputManager.GetMouseKeyState(mouseKey);

                        if (mouseKeyState == InputKeySequenceState.Pressed || mouseKeyState == InputKeySequenceState.Held)
                        {
                            positiveValue = 1f;
                        }
                        break;

                    case InputBindingKeyType.KeyboardKey:

                        // Seul le contrôleur 1 peut utiliser la souris et le clavier
                        // (les autres contrôleurs sont des manettes)

                        if (i > 0)
                        {
                            return;
                        }

                        Keys keyboardKey = (Keys)idsCD.PositiveID;
                        InputKeySequenceState keyboardKeyState = InputManager.GetKeyboardKeyState(keyboardKey);

                        if (keyboardKeyState == InputKeySequenceState.Pressed || keyboardKeyState == InputKeySequenceState.Held)
                        {
                            positiveValue = 1f;
                        }
                        break;

                    case InputBindingKeyType.GamePadKey:
                        Buttons gamePadKey = (Buttons)idsCD.PositiveID;
                        InputKeySequenceState gamePadKeyState = InputManager.GetGamePadKeyState(i, gamePadKey);

                        if (gamePadKeyState == InputKeySequenceState.Pressed || gamePadKeyState == InputKeySequenceState.Held)
                        {
                            positiveValue = 1f;
                        }
                        break;

                    case InputBindingKeyType.JoystickKey:
                        JoystickKey joystickKey = (JoystickKey)idsCD.PositiveID;
                        InputKeySequenceState joystickKeyState = InputManager.GetJoystickKeyState(i, joystickKey);

                        if (joystickKeyState == InputKeySequenceState.Pressed || joystickKeyState == InputKeySequenceState.Held)
                        {
                            positiveValue = 1f;
                        }
                        break;
                }

                switch (typesCD.NegativeType)
                {
                    case InputBindingKeyType.MouseKey:

                        // Seul le contrôleur 1 peut utiliser la souris et le clavier
                        // (les autres contrôleurs sont des manettes)

                        if (i > 0)
                        {
                            return;
                        }

                        MouseKey mouseKey = (MouseKey)idsCD.NegativeID;
                        InputKeySequenceState mouseKeyState = InputManager.GetMouseKeyState(mouseKey);

                        if (mouseKeyState == InputKeySequenceState.Pressed || mouseKeyState == InputKeySequenceState.Held)
                        {
                            negativeValue = -1f;
                        }
                        break;

                    case InputBindingKeyType.KeyboardKey:

                        // Seul le contrôleur 1 peut utiliser la souris et le clavier
                        // (les autres contrôleurs sont des manettes)

                        if (i > 0)
                        {
                            return;
                        }

                        Keys keyboardKey = (Keys)idsCD.NegativeID;
                        InputKeySequenceState keyboardKeyState = InputManager.GetKeyboardKeyState(keyboardKey);

                        if (keyboardKeyState == InputKeySequenceState.Pressed || keyboardKeyState == InputKeySequenceState.Held)
                        {
                            negativeValue = -1f;
                        }
                        break;

                    case InputBindingKeyType.GamePadKey:
                        Buttons gamePadKey = (Buttons)idsCD.NegativeID;
                        InputKeySequenceState gamePadKeyState = InputManager.GetGamePadKeyState(i, gamePadKey);

                        if (gamePadKeyState == InputKeySequenceState.Pressed || gamePadKeyState == InputKeySequenceState.Held)
                        {
                            negativeValue = -1f;
                        }
                        break;

                    case InputBindingKeyType.JoystickKey:
                        JoystickKey joystickKey = (JoystickKey)idsCD.NegativeID;
                        InputKeySequenceState joystickKeyState = InputManager.GetJoystickKeyState(i, joystickKey);

                        if (joystickKeyState == InputKeySequenceState.Pressed || joystickKeyState == InputKeySequenceState.Held)
                        {
                            negativeValue = -1f;
                        }
                        break;
                }

                returnValuesBU.Value[i] = positiveValue + negativeValue;
            }
        }

        /// <summary>
        /// Calcule les valeurs de chaque InputBinding
        /// </summary>
        /// <param name="idsCD">Les IDs de chaque élément de la séquence de touches</param>
        /// <param name="typesCD">Les types de touche de chaque élément de la séquence de touches</param>
        /// <param name="returnValuesBU">Les valeurs du binding à retourner</param>
        [Query]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ProcessVector2DKeysInputBindings(
            in InputBindingVector2DKeysIDsCD idsCD,
            in InputBindingVector2DKeysTypesCD typesCD,
            ref InputVector2DValuesBU returnValuesBU)
        {
            for (int i = 0; i < returnValuesBU.Value.Length; ++i)
            {
                float positiveXValue = 0f;
                float negativeXValue = 0f;
                float positiveYValue = 0f;
                float negativeYValue = 0f;

                switch (typesCD.PositiveXType)
                {
                    case InputBindingKeyType.MouseKey:

                        // Seul le contrôleur 1 peut utiliser la souris et le clavier
                        // (les autres contrôleurs sont des manettes)

                        if (i > 0)
                        {
                            return;
                        }

                        MouseKey mouseKey = (MouseKey)idsCD.PositiveXID;
                        InputKeySequenceState mouseKeyState = InputManager.GetMouseKeyState(mouseKey);

                        if (mouseKeyState == InputKeySequenceState.Pressed || mouseKeyState == InputKeySequenceState.Held)
                        {
                            positiveXValue = 1f;
                        }
                        break;

                    case InputBindingKeyType.KeyboardKey:

                        // Seul le contrôleur 1 peut utiliser la souris et le clavier
                        // (les autres contrôleurs sont des manettes)

                        if (i > 0)
                        {
                            return;
                        }

                        Keys keyboardKey = (Keys)idsCD.PositiveXID;
                        InputKeySequenceState keyboardKeyState = InputManager.GetKeyboardKeyState(keyboardKey);

                        if (keyboardKeyState == InputKeySequenceState.Pressed || keyboardKeyState == InputKeySequenceState.Held)
                        {
                            positiveXValue = 1f;
                        }
                        break;

                    case InputBindingKeyType.GamePadKey:
                        Buttons gamePadKey = (Buttons)idsCD.PositiveXID;
                        InputKeySequenceState gamePadKeyState = InputManager.GetGamePadKeyState(i, gamePadKey);

                        if (gamePadKeyState == InputKeySequenceState.Pressed || gamePadKeyState == InputKeySequenceState.Held)
                        {
                            positiveXValue = 1f;
                        }
                        break;

                    case InputBindingKeyType.JoystickKey:
                        JoystickKey joystickKey = (JoystickKey)idsCD.PositiveXID;
                        InputKeySequenceState joystickKeyState = InputManager.GetJoystickKeyState(i, joystickKey);

                        if (joystickKeyState == InputKeySequenceState.Pressed || joystickKeyState == InputKeySequenceState.Held)
                        {
                            positiveXValue = 1f;
                        }
                        break;
                }

                switch (typesCD.NegativeXType)
                {
                    case InputBindingKeyType.MouseKey:

                        // Seul le contrôleur 1 peut utiliser la souris et le clavier
                        // (les autres contrôleurs sont des manettes)

                        if (i > 0)
                        {
                            return;
                        }

                        MouseKey mouseKey = (MouseKey)idsCD.NegativeXID;
                        InputKeySequenceState mouseKeyState = InputManager.GetMouseKeyState(mouseKey);

                        if (mouseKeyState == InputKeySequenceState.Pressed || mouseKeyState == InputKeySequenceState.Held)
                        {
                            negativeXValue = -1f;
                        }
                        break;

                    case InputBindingKeyType.KeyboardKey:

                        // Seul le contrôleur 1 peut utiliser la souris et le clavier
                        // (les autres contrôleurs sont des manettes)

                        if (i > 0)
                        {
                            return;
                        }

                        Keys keyboardKey = (Keys)idsCD.NegativeXID;
                        InputKeySequenceState keyboardKeyState = InputManager.GetKeyboardKeyState(keyboardKey);

                        if (keyboardKeyState == InputKeySequenceState.Pressed || keyboardKeyState == InputKeySequenceState.Held)
                        {
                            negativeXValue = -1f;
                        }
                        break;

                    case InputBindingKeyType.GamePadKey:
                        Buttons gamePadKey = (Buttons)idsCD.NegativeXID;
                        InputKeySequenceState gamePadKeyState = InputManager.GetGamePadKeyState(i, gamePadKey);

                        if (gamePadKeyState == InputKeySequenceState.Pressed || gamePadKeyState == InputKeySequenceState.Held)
                        {
                            negativeXValue = -1f;
                        }
                        break;

                    case InputBindingKeyType.JoystickKey:
                        JoystickKey joystickKey = (JoystickKey)idsCD.NegativeXID;
                        InputKeySequenceState joystickKeyState = InputManager.GetJoystickKeyState(i, joystickKey);

                        if (joystickKeyState == InputKeySequenceState.Pressed || joystickKeyState == InputKeySequenceState.Held)
                        {
                            negativeXValue = -1f;
                        }
                        break;
                }

                switch (typesCD.PositiveYType)
                {
                    case InputBindingKeyType.MouseKey:

                        // Seul le contrôleur 1 peut utiliser la souris et le clavier
                        // (les autres contrôleurs sont des manettes)

                        if (i > 0)
                        {
                            return;
                        }

                        MouseKey mouseKey = (MouseKey)idsCD.PositiveYID;
                        InputKeySequenceState mouseKeyState = InputManager.GetMouseKeyState(mouseKey);

                        if (mouseKeyState == InputKeySequenceState.Pressed || mouseKeyState == InputKeySequenceState.Held)
                        {
                            positiveYValue = 1f;
                        }
                        break;

                    case InputBindingKeyType.KeyboardKey:

                        // Seul le contrôleur 1 peut utiliser la souris et le clavier
                        // (les autres contrôleurs sont des manettes)

                        if (i > 0)
                        {
                            return;
                        }

                        Keys keyboardKey = (Keys)idsCD.PositiveYID;
                        InputKeySequenceState keyboardKeyState = InputManager.GetKeyboardKeyState(keyboardKey);

                        if (keyboardKeyState == InputKeySequenceState.Pressed || keyboardKeyState == InputKeySequenceState.Held)
                        {
                            positiveYValue = 1f;
                        }
                        break;

                    case InputBindingKeyType.GamePadKey:
                        Buttons gamePadKey = (Buttons)idsCD.PositiveYID;
                        InputKeySequenceState gamePadKeyState = InputManager.GetGamePadKeyState(i, gamePadKey);

                        if (gamePadKeyState == InputKeySequenceState.Pressed || gamePadKeyState == InputKeySequenceState.Held)
                        {
                            positiveYValue = 1f;
                        }
                        break;

                    case InputBindingKeyType.JoystickKey:
                        JoystickKey joystickKey = (JoystickKey)idsCD.PositiveYID;
                        InputKeySequenceState joystickKeyState = InputManager.GetJoystickKeyState(i, joystickKey);

                        if (joystickKeyState == InputKeySequenceState.Pressed || joystickKeyState == InputKeySequenceState.Held)
                        {
                            positiveYValue = 1f;
                        }
                        break;
                }

                switch (typesCD.NegativeYType)
                {
                    case InputBindingKeyType.MouseKey:

                        // Seul le contrôleur 1 peut utiliser la souris et le clavier
                        // (les autres contrôleurs sont des manettes)

                        if (i > 0)
                        {
                            return;
                        }

                        MouseKey mouseKey = (MouseKey)idsCD.NegativeYID;
                        InputKeySequenceState mouseKeyState = InputManager.GetMouseKeyState(mouseKey);

                        if (mouseKeyState == InputKeySequenceState.Pressed || mouseKeyState == InputKeySequenceState.Held)
                        {
                            negativeYValue = -1f;
                        }
                        break;

                    case InputBindingKeyType.KeyboardKey:

                        // Seul le contrôleur 1 peut utiliser la souris et le clavier
                        // (les autres contrôleurs sont des manettes)

                        if (i > 0)
                        {
                            return;
                        }

                        Keys keyboardKey = (Keys)idsCD.NegativeYID;
                        InputKeySequenceState keyboardKeyState = InputManager.GetKeyboardKeyState(keyboardKey);

                        if (keyboardKeyState == InputKeySequenceState.Pressed || keyboardKeyState == InputKeySequenceState.Held)
                        {
                            negativeYValue = -1f;
                        }
                        break;

                    case InputBindingKeyType.GamePadKey:
                        Buttons gamePadKey = (Buttons)idsCD.NegativeYID;
                        InputKeySequenceState gamePadKeyState = InputManager.GetGamePadKeyState(i, gamePadKey);

                        if (gamePadKeyState == InputKeySequenceState.Pressed || gamePadKeyState == InputKeySequenceState.Held)
                        {
                            negativeYValue = -1f;
                        }
                        break;

                    case InputBindingKeyType.JoystickKey:
                        JoystickKey joystickKey = (JoystickKey)idsCD.NegativeYID;
                        InputKeySequenceState joystickKeyState = InputManager.GetJoystickKeyState(i, joystickKey);

                        if (joystickKeyState == InputKeySequenceState.Pressed || joystickKeyState == InputKeySequenceState.Held)
                        {
                            negativeYValue = -1f;
                        }
                        break;
                }

                returnValuesBU.Value[i] = new Vector2(positiveXValue + negativeXValue, positiveYValue + negativeYValue);
            }
        }

        /// <summary>
        /// Calcule les valeurs de chaque InputBinding
        /// </summary>
        /// <param name="deadZoneCD">La valeur en dessous de laquelle l'input est considéré comme inerte</param>
        /// <param name="triggerTypeCD">Le type du trigger de l'InputBinding</param>
        /// <param name="returnValuesBU">Les valeurs du binding à retourner</param>
        [Query]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ProcessVector1DTriggerInputBindings(
            in InputBindingDeadZoneCD deadZoneCD,
            in InputBindingTriggerTypeCD triggerTypeCD,
            ref InputVector1DValuesBU returnValuesBU)
        {
            for (int i = 0; i < returnValuesBU.Value.Length; ++i)
            {
                switch (triggerTypeCD.Value)
                {
                    case TriggerType.LeftTrigger:
                        GamePadInput gamePadInput1 = InputManager.GetScheme<GamePadInput>();
                        float value1 = gamePadInput1.GetLeftTriggerValue(i);
                        returnValuesBU.Value[i] = Math.Abs(value1) > deadZoneCD.Value ? value1 : 0f;
                        break;
                    case TriggerType.RightTrigger:
                        GamePadInput gamePadInput2 = InputManager.GetScheme<GamePadInput>();
                        float value2 = gamePadInput2.GetRightTriggerValue(i);
                        returnValuesBU.Value[i] = Math.Abs(value2) > deadZoneCD.Value ? value2 : 0f;
                        break;
                    case TriggerType.MouseWheel:

                        // Seul le contrôleur 1 peut utiliser la souris

                        if (i > 0)
                        {
                            return;
                        }

                        MouseInput mouseInput = InputManager.GetScheme<MouseInput>();
                        returnValuesBU.Value[0] = Math.Clamp(mouseInput.GetMouseWheelScrollValue(), -1f, 1f);
                        break;
                }
            }
        }

        /// <summary>
        /// Calcule les valeurs de chaque InputBinding
        /// </summary>
        /// <param name="deadZoneCD">La valeur en dessous de laquelle l'input est considéré comme inerte</param>
        /// <param name="joystickTypeCD">Le type du joystick de l'InputBinding</param>
        /// <param name="returnValuesBU">Les valeurs du binding à retourner</param>
        [Query]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ProcessVector2DJoystickInputBindings(
            in InputBindingDeadZoneCD deadZoneCD,
            in InputBindingJoystickTypeCD joystickTypeCD,
            ref InputVector2DValuesBU returnValuesBU)
        {
            GamePadInput gamePadInput = InputManager.GetScheme<GamePadInput>();

            for (int i = 0; i < returnValuesBU.Value.Length; ++i)
            {
                switch (joystickTypeCD.Value)
                {
                    case JoystickType.Left:
                        Vector2 value1 = gamePadInput.GetLeftThumbstickAxis(i);
                        returnValuesBU.Value[i] = value1.Length() > deadZoneCD.Value ? value1 : Vector2.Zero;
                        break;
                    case JoystickType.Right:
                        Vector2 value2 = gamePadInput.GetRightThumbstickAxis(i);
                        returnValuesBU.Value[i] = value2.Length() > deadZoneCD.Value ? value2 : Vector2.Zero;
                        break;
                }
            }
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
        /// <param name="nbControllers">Le nombre max de contrôleurs pris en charge par l'InputSystem</param>
        /// <param name="actionE">L'entité de l'InputAction</param>
        /// <param name="actionID">l'ID de l'InputAction</param>
        [Query]
        [All(typeof(InputActionVector1DTag))]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ProcessVector1DInputActions(
            [Data] World w,
            [Data] int nbControllers,
            in Entity actionE,
            in InputActionIDCD actionID)
        {
            ref var rel = ref w.GetRelationships<InputActionOf>(actionE);

            for (int i = 0; i < nbControllers; ++i)
            {
                // Si certains bindings sont actifs, on lance l'action

                float returnValue = 0f;

                foreach (KeyValuePair<Entity, InputActionOf> child in rel)
                {
                    Entity bindingE = child.Key;
                    InputVector1DValuesBU bindingValues = w.Get<InputVector1DValuesBU>(bindingE);

                    if (Math.Abs(bindingValues.Value[i]) > Math.Abs(returnValue))
                    {
                        returnValue = bindingValues.Value[i];
                    }
                }

                if (Math.Abs(returnValue) > float.Epsilon)
                {
                    InputManager.GetVector1DEvent(actionID.Value).Performed?.Invoke(i, returnValue);
                }
            }
        }

        /// <summary>
        /// Détermine l'action à activer pour chaque InputAction
        /// </summary>
        /// <param name="w">Le monde contenant les entités</param>
        /// <param name="nbControllers">Le nombre max de contrôleurs pris en charge par l'InputSystem</param>
        /// <param name="actionE">L'entité de l'InputAction</param>
        /// <param name="actionID">l'ID de l'InputAction</param>
        [Query]
        [All(typeof(InputActionVector2DTag))]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ProcessVector2DInputActions(
            [Data] World w,
            [Data] int nbControllers,
            in Entity actionE,
            in InputActionIDCD actionID)
        {
            ref var rel = ref w.GetRelationships<InputActionOf>(actionE);

            for (int i = 0; i < nbControllers; ++i)
            {
                // Si certains bindings sont actifs, on lance l'action

                Vector2 returnValue = Vector2.Zero;

                foreach (KeyValuePair<Entity, InputActionOf> child in rel)
                {
                    Entity bindingE = child.Key;
                    InputVector2DValuesBU bindingValues = w.Get<InputVector2DValuesBU>(bindingE);

                    if (Math.Abs(bindingValues.Value[i].LengthSquared()) > Math.Abs(returnValue.LengthSquared()))
                    {
                        returnValue = bindingValues.Value[i];
                    }
                }

                if (Math.Abs(returnValue.LengthSquared()) > float.Epsilon)
                {
                    InputManager.GetVector2DEvent(actionID.Value).Performed?.Invoke(i, returnValue);
                }
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
