using System.Runtime.CompilerServices;
using Arch.Core;
using Arch.LowLevel;
using Microsoft.Xna.Framework.Input;
using Retard.Input.Components;
using Retard.Input.Models;
using Retard.Input.Models.Assets;

namespace Retard.Input.Entities
{
    /// <summary>
    /// Contient les méthodes de création
    /// des différentes entités
    /// </summary>
    public static class EntityFactory
    {
        #region Input

        /// <summary>
        /// Crée les entités des actions des entrées
        /// </summary>
        /// <param name="world">Le monde contenant ces entités</param>
        /// <param name="nbMaxControllers">Le nombre de joueurs possibles</param>
        /// <param name="name">L'ID du contexte</param>
        /// <param name="valueType">La valeur retournée par l'action</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Entity CreateInputActionEntities(World world, int nbMaxControllers, string name, InputActionReturnValueType valueType)
        {
            Entity e = world.Create(new InputActionIDCD { Value = name });

            switch (valueType)
            {
                case InputActionReturnValueType.ButtonState:
                    world.Add(e, new InputButtonStateValuesBU(nbMaxControllers));
                    break;
                case InputActionReturnValueType.Vector1D:
                    world.Add(e, new InputActionVector1DTag());
                    break;
                case InputActionReturnValueType.Vector2D:
                    world.Add(e, new InputActionVector2DTag());
                    break;
            }

            return e;
        }

        /// <summary>
        /// Crée les entités des entrées
        /// </summary>
        /// <param name="world">Le monde contenant ces entités</param>
        /// <param name="nbMaxControllers">Le nombre de joueurs possibles</param>
        /// <param name="usesMouse"><see langword="true"/> si l'InputManager prend en charge la souris</param>
        /// <param name="usesKeyboard"><see langword="true"/> si l'InputManager prend en charge le clavier</param>
        /// <param name="usesGamePad"><see langword="true"/> si l'InputManager prend en charge la manette</param>
        /// <param name="keySequence">La liste d'entrées à exécuter</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Entity CreateInputBindingEntities(World world, int nbMaxControllers,
            bool usesMouse, bool usesKeyboard, bool usesGamePad, InputKeySequenceElement[] keySequence)
        {
            if (keySequence == null || keySequence.Length == 0)
            {
                return Entity.Null;
            }

            using UnsafeArray<InputBindingKeyType> keyTypes = new(keySequence.Length);
            using UnsafeArray<int> keysIDs = new(keySequence.Length);
            using UnsafeArray<InputKeySequenceState> validStates = new(keySequence.Length);

            // Pour chaque élément de la séquence, on regarde
            // si l'InputManager prend en charge son IScheme.
            // Si non, le binding ne peut pas être lu et est invalide.

            for (int i = 0; i < keySequence.Length; ++i)
            {
                InputKeySequenceElement element = keySequence[i];

                EntityFactory.ConvertKeySequenceElement(element, usesMouse, usesKeyboard, usesGamePad, out int id, out InputBindingKeyType type);

                if (id == -1)
                {
                    return Entity.Null;
                }

                keyTypes[i] = type;
                keysIDs[i] = id;
                validStates[i] = element.ValidState;
            }

            return world.Create
                (
                new InputBindingKeySequenceTypesBU(keyTypes),
                new InputBindingKeySequenceIDsBU(keysIDs),
                new InputBindingKeySequenceStatesBU(validStates),
                new InputButtonStateValuesBU(nbMaxControllers)
                );
        }

        /// <summary>
        /// Crée les entités des entrées
        /// </summary>
        /// <param name="world">Le monde contenant ces entités</param>
        /// <param name="nbMaxControllers">Le nombre de joueurs possibles</param>
        /// <param name="usesMouse"><see langword="true"/> si l'InputManager prend en charge la souris</param>
        /// <param name="usesKeyboard"><see langword="true"/> si l'InputManager prend en charge le clavier</param>
        /// <param name="usesGamePad"><see langword="true"/> si l'InputManager prend en charge la manette</param>
        /// <param name="vector1DKeys">Les touches pour actionner un seul axe (X ou Y)</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Entity CreateInputBindingEntities(World world, int nbMaxControllers,
            bool usesMouse, bool usesKeyboard, bool usesGamePad, InputKeyVector1DElement[] vector1DKeys)
        {
            if (vector1DKeys == null || vector1DKeys.Length != 2)
            {
                return Entity.Null;
            }

            // Pour chaque élément de la séquence, on regarde
            // si l'InputManager prend en charge son IScheme.
            // Si non, le binding ne peut pas être lu et est invalide.

            EntityFactory.ConvertVector1DKey(vector1DKeys[0], usesMouse, usesKeyboard, usesGamePad, out int positiveID, out InputBindingKeyType positiveType);

            if (positiveID == -1)
            {
                return Entity.Null;
            }

            EntityFactory.ConvertVector1DKey(vector1DKeys[1], usesMouse, usesKeyboard, usesGamePad, out int negativeID, out InputBindingKeyType negativeType);

            if (negativeID == -1)
            {
                return Entity.Null;
            }

            return world.Create
                (
                new InputBindingVector1DKeysIDsCD { PositiveID = positiveID, NegativeID = negativeID },
                new InputBindingVector1DKeysTypesCD { PositiveType = positiveType, NegativeType = negativeType },
                new InputVector1DValuesBU(nbMaxControllers)
                );
        }

        /// <summary>
        /// Crée les entités des entrées
        /// </summary>
        /// <param name="world">Le monde contenant ces entités</param>
        /// <param name="nbMaxControllers">Le nombre de joueurs possibles</param>
        /// <param name="usesMouse"><see langword="true"/> si l'InputManager prend en charge la souris</param>
        /// <param name="usesKeyboard"><see langword="true"/> si l'InputManager prend en charge le clavier</param>
        /// <param name="usesGamePad"><see langword="true"/> si l'InputManager prend en charge la manette</param>
        /// <param name="vector2DKeys">Les touches pour actionner un axe 2D</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Entity CreateInputBindingEntities(World world, int nbMaxControllers,
            bool usesMouse, bool usesKeyboard, bool usesGamePad, InputKeyVector2DElement[] vector2DKeys)
        {
            if (vector2DKeys == null || vector2DKeys.Length != 4)
            {
                return Entity.Null;
            }

            // Pour chaque élément de la séquence, on regarde
            // si l'InputManager prend en charge son IScheme.
            // Si non, le binding ne peut pas être lu et est invalide.

            EntityFactory.ConvertVector2DKey(vector2DKeys[0], usesMouse, usesKeyboard, usesGamePad, out int positiveXID, out InputBindingKeyType positiveXType);

            if (positiveXID == -1)
            {
                return Entity.Null;
            }

            EntityFactory.ConvertVector2DKey(vector2DKeys[1], usesMouse, usesKeyboard, usesGamePad, out int negativeXID, out InputBindingKeyType negativeXType);

            if (negativeXID == -1)
            {
                return Entity.Null;
            }

            EntityFactory.ConvertVector2DKey(vector2DKeys[2], usesMouse, usesKeyboard, usesGamePad, out int positiveYID, out InputBindingKeyType positiveYType);

            if (positiveYID == -1)
            {
                return Entity.Null;
            }

            EntityFactory.ConvertVector2DKey(vector2DKeys[3], usesMouse, usesKeyboard, usesGamePad, out int negativeYID, out InputBindingKeyType negativeYType);

            if (negativeYID == -1)
            {
                return Entity.Null;
            }

            return world.Create
                (
                    new InputBindingVector2DKeysIDsCD
                    {
                        PositiveXID = positiveXID,
                        NegativeXID = negativeXID,
                        PositiveYID = positiveYID,
                        NegativeYID = negativeYID
                    },
                    new InputBindingVector2DKeysTypesCD
                    {
                        PositiveXType = positiveXType,
                        NegativeXType = negativeXType,
                        PositiveYType = positiveYType,
                        NegativeYType = negativeYType
                    },
                    new InputVector2DValuesBU(nbMaxControllers)
                );
        }

        /// <summary>
        /// Crée les entités des entrées
        /// </summary>
        /// <param name="world">Le monde contenant ces entités</param>
        /// <param name="nbMaxControllers">Le nombre de joueurs possibles</param>
        /// <param name="usesGamePad"><see langword="true"/> si l'InputManager prend en charge la manette</param>
        /// <param name="joystick">Le joystick utilisé</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Entity CreateInputBindingEntities(World world, int nbMaxControllers,
            bool usesGamePad, InputBindingJoystick joystick)
        {
            if (!usesGamePad || joystick.Type == JoystickType.None)
            {
                return Entity.Null;
            }

            Entity e = world.Create
                (
                    new InputBindingDeadZoneCD { Value = joystick.DeadZone },
                    new InputBindingJoystickTypeCD { Value = joystick.Type }
                );

            switch (joystick.Axis)
            {
                case JoystickAxisType.Both:
                    world.Add(e, new InputVector2DValuesBU(nbMaxControllers));
                    break;
                case JoystickAxisType.XAxis:
                    world.Add<InputBindingJoystickXAxisTag>(e);
                    world.Add(e, new InputVector1DValuesBU(nbMaxControllers));
                    break;
                case JoystickAxisType.YAxis:
                    world.Add<InputBindingJoystickYAxisTag>(e);
                    world.Add(e, new InputVector1DValuesBU(nbMaxControllers));
                    break;
            }

            return e;
        }

        /// <summary>
        /// Crée les entités des entrées
        /// </summary>
        /// <param name="world">Le monde contenant ces entités</param>
        /// <param name="nbMaxControllers">Le nombre de joueurs possibles</param>
        /// <param name="usesMouse"><see langword="true"/> si l'InputManager prend en charge la souris</param>
        /// <param name="usesGamePad"><see langword="true"/> si l'InputManager prend en charge la manette</param>
        /// <param name="trigger">Le trigger utilisé</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Entity CreateInputBindingEntities(World world, int nbMaxControllers,
            bool usesMouse, bool usesGamePad, InputBindingTrigger trigger)
        {
            switch (trigger.Type)
            {
                case TriggerType.MouseWheel:
                    if (!usesMouse)
                    {
                        return Entity.Null;
                    }
                    break;

                case TriggerType.LeftTrigger:
                case TriggerType.RightTrigger:
                    if (!usesGamePad)
                    {
                        return Entity.Null;
                    }
                    break;

                default:
                    return Entity.Null;
            }

            return world.Create
                    (
                        new InputBindingDeadZoneCD { Value = trigger.DeadZone },
                        new InputBindingTriggerTypeCD { Value = trigger.Type },
                        new InputVector1DValuesBU(nbMaxControllers)
                    );
        }

        #endregion

        #region Fonctions statiques privées

        /// <summary>
        /// Convertit le KeySequenceElement
        /// </summary>
        /// <param name="element">Le KeySequence element</param>
        /// <param name="usesMouse"><see langword="true"/> si l'InputManager prend en charge la souris</param>
        /// <param name="usesKeyboard"><see langword="true"/> si l'InputManager prend en charge le clavier</param>
        /// <param name="usesGamePad"><see langword="true"/> si l'InputManager prend en charge la manette</param>
        /// <param name="id">L'id de la touche</param>
        /// <param name="type">Le type de la touche</param>
        private static void ConvertKeySequenceElement(InputKeySequenceElement element, bool usesMouse, bool usesKeyboard, bool usesGamePad,
            out int id, out InputBindingKeyType type)
        {
            id = -1;
            type = default;

            if (element.MouseKey != MouseKey.None)
            {
                if (!usesMouse)
                {
                    return;
                }

                type = InputBindingKeyType.MouseKey;
                id = (int)element.MouseKey;
            }
            else if (element.KeyboardKey != Keys.None)
            {
                if (!usesKeyboard)
                {
                    return;
                }

                type = InputBindingKeyType.KeyboardKey;
                id = (int)element.KeyboardKey;
            }
            else if (element.GamePadKey != Buttons.None)
            {
                if (!usesGamePad)
                {
                    return;
                }

                type = InputBindingKeyType.GamePadKey;
                id = (int)element.GamePadKey;
            }
            else if (element.JoystickKey != JoystickKey.None)
            {
                if (!usesGamePad)
                {
                    return;
                }

                type = InputBindingKeyType.JoystickKey;
                id = (int)element.JoystickKey;
            }
        }

        /// <summary>
        /// Convertit la Vector1DKey
        /// </summary>
        /// <param name="element">La Vector1DKey</param>
        /// <param name="usesMouse"><see langword="true"/> si l'InputManager prend en charge la souris</param>
        /// <param name="usesKeyboard"><see langword="true"/> si l'InputManager prend en charge le clavier</param>
        /// <param name="usesGamePad"><see langword="true"/> si l'InputManager prend en charge la manette</param>
        /// <param name="id">L'id de la touche</param>
        /// <param name="type">Le type de la touche</param>
        private static void ConvertVector1DKey(InputKeyVector1DElement element, bool usesMouse, bool usesKeyboard, bool usesGamePad,
            out int id, out InputBindingKeyType type)
        {
            id = -1;
            type = default;

            if (element.MouseKey != MouseKey.None)
            {
                if (!usesMouse)
                {
                    return;
                }

                type = InputBindingKeyType.MouseKey;
                id = (int)element.MouseKey;
            }
            else if (element.KeyboardKey != Keys.None)
            {
                if (!usesKeyboard)
                {
                    return;
                }

                type = InputBindingKeyType.KeyboardKey;
                id = (int)element.KeyboardKey;
            }
            else if (element.GamePadKey != Buttons.None)
            {
                if (!usesGamePad)
                {
                    return;
                }

                type = InputBindingKeyType.GamePadKey;
                id = (int)element.GamePadKey;
            }
            else if (element.JoystickKey != JoystickKey.None)
            {
                if (!usesGamePad)
                {
                    return;
                }

                type = InputBindingKeyType.JoystickKey;
                id = (int)element.JoystickKey;
            }
        }

        /// <summary>
        /// Convertit la Vector2DKey
        /// </summary>
        /// <param name="element">La Vector2DKey</param>
        /// <param name="usesMouse"><see langword="true"/> si l'InputManager prend en charge la souris</param>
        /// <param name="usesKeyboard"><see langword="true"/> si l'InputManager prend en charge le clavier</param>
        /// <param name="usesGamePad"><see langword="true"/> si l'InputManager prend en charge la manette</param>
        /// <param name="id">L'id de la touche</param>
        /// <param name="type">Le type de la touche</param>
        private static void ConvertVector2DKey(InputKeyVector2DElement element, bool usesMouse, bool usesKeyboard, bool usesGamePad,
            out int id, out InputBindingKeyType type)
        {
            id = -1;
            type = default;

            if (element.MouseKey != MouseKey.None)
            {
                if (!usesMouse)
                {
                    return;
                }

                type = InputBindingKeyType.MouseKey;
                id = (int)element.MouseKey;
            }
            else if (element.KeyboardKey != Keys.None)
            {
                if (!usesKeyboard)
                {
                    return;
                }

                type = InputBindingKeyType.KeyboardKey;
                id = (int)element.KeyboardKey;
            }
            else if (element.GamePadKey != Buttons.None)
            {
                if (!usesGamePad)
                {
                    return;
                }

                type = InputBindingKeyType.GamePadKey;
                id = (int)element.GamePadKey;
            }
            else if (element.JoystickKey != JoystickKey.None)
            {
                if (!usesGamePad)
                {
                    return;
                }

                type = InputBindingKeyType.JoystickKey;
                id = (int)element.JoystickKey;
            }
        }

        #endregion
    }
}
