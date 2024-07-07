using System.Runtime.CompilerServices;
using Arch.Core;
using Arch.LowLevel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Retard.Core.Components.Input;
using Retard.Core.Components.Sprites;
using Retard.Core.Models.Assets.Input;
using Retard.Engine.Components.Input;
using Retard.Engine.Models.Assets.Input;

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

        #region Input

        /// <summary>
        /// Crée les entités des actions des entrées
        /// </summary>
        /// <param name="world">Le monde contenant ces entités</param>
        /// <param name="name">L'ID du contexte</param>
        /// <param name="valueType">La valeur retournée par l'action</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Entity CreateInputActionEntities(World world, string name, InputActionReturnValueType valueType, int actionStateLength)
        {
            Entity e = world.Create(new InputActionIDCD { Value = name });

            switch (valueType)
            {
                case InputActionReturnValueType.ButtonState:
                    world.Add(e, new InputActionButtonStateValuesBU(actionStateLength));
                    break;
                case InputActionReturnValueType.Vector1D:
                    world.Add(e, new InputActionVector1DValuesBU(actionStateLength));
                    break;
                case InputActionReturnValueType.Vector2D:
                    world.Add(e, new InputActionVector2DValuesBU(actionStateLength));
                    break;
            }

            return e;
        }

        /// <summary>
        /// Crée les entités des entrées
        /// </summary>
        /// <param name="world">Le monde contenant ces entités</param>
        /// <param name="usesMouse"><see langword="true"/> si l'InputManager prend en charge la souris</param>
        /// <param name="usesKeyboard"><see langword="true"/> si l'InputManager prend en charge le clavier</param>
        /// <param name="usesGamePad"><see langword="true"/> si l'InputManager prend en charge la manette</param>
        /// <param name="keySequence">La séquence d'entrées à réaliser pour exécuter l'action (ex: Ctrl+Z)</param>
        /// <param name="joystick">Le joystick à utiliser</param>
        /// <param name="joystickAxis">L'axe du joystick à évaluer</param>
        /// <param name="deadZone"> La valeur en dessous de laquelle le joystick est considéré comme inerte</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Entity CreateInputBindingEntities(World world, bool usesMouse, bool usesKeyboard, bool usesGamePad,
            InputKeySequenceElement[] keySequence, JoystickType joystick, JoystickAxis joystickAxis, float deadZone)
        {
            if (joystick != JoystickType.None)
            {
                if (!usesGamePad)
                {
                    return Entity.Null;
                }

                Entity e = world.Create
                    (
                        new InputBindingDeadZoneCD { Value = deadZone },
                        new InputBindingJoystickTypeCD { Value = joystick }
                    );

                switch (joystickAxis)
                {
                    case JoystickAxis.Both:
                        world.Add<InputBindingJoystickXAxisTag>(e);
                        world.Add<InputBindingJoystickYAxisTag>(e);
                        break;
                    case JoystickAxis.XAxis:
                        world.Add<InputBindingJoystickXAxisTag>(e);
                        break;
                    case JoystickAxis.YAxis:
                        world.Add<InputBindingJoystickYAxisTag>(e);
                        break;
                }

                return e;
            }

            if (keySequence != null || keySequence.Length >= 0)
            {
                UnsafeArray<InputBindingKeyType> keyTypes = new(keySequence.Length);
                UnsafeArray<int> keys = new(keySequence.Length);
                UnsafeArray<InputKeySequenceState> validStates = new(keySequence.Length);

                // Pour chaque élément de la séquence, on regarde
                // si l'InputManager prend en charge son IScheme.
                // Si non, le binding ne peut pas être lu et est invalide.

                for (int i = 0; i < keySequence.Length; ++i)
                {
                    InputKeySequenceElement element = keySequence[i];

                    if (element.MouseKey != MouseKey.None)
                    {
                        if (!usesMouse)
                        {
                            keyTypes.Dispose();
                            keys.Dispose();
                            validStates.Dispose();
                            return Entity.Null;
                        }

                        keyTypes[i] = InputBindingKeyType.MouseKey;
                        keys[i] = (int)element.MouseKey;
                    }
                    else if (element.KeyboardKey != Keys.None)
                    {
                        if (!usesKeyboard)
                        {
                            keyTypes.Dispose();
                            keys.Dispose();
                            validStates.Dispose();
                            return Entity.Null;
                        }

                        keyTypes[i] = InputBindingKeyType.KeyboardKey;
                        keys[i] = (int)element.KeyboardKey;
                    }
                    else if (element.GamePadKey != Buttons.None)
                    {
                        if (!usesGamePad)
                        {
                            keyTypes.Dispose();
                            keys.Dispose();
                            validStates.Dispose();
                            return Entity.Null;
                        }

                        keyTypes[i] = InputBindingKeyType.GamePadKey;
                        keys[i] = (int)element.GamePadKey;
                    }
                    else if (element.JoystickKey != JoystickKey.None)
                    {
                        if (!usesGamePad)
                        {
                            keyTypes.Dispose();
                            keys.Dispose();
                            validStates.Dispose();
                            return Entity.Null;
                        }

                        keyTypes[i] = InputBindingKeyType.JoystickKey;
                        keys[i] = (int)element.JoystickKey;
                    }

                    validStates[i] = element.ValidState;
                }


                return world.Create
                    (
                    new InputBindingKeySequenceTypesBU { Value = keyTypes },
                    new InputBindingKeySequenceIDsBU { Value = keys },
                    new InputBindingKeySequenceStatesBU { Value = validStates }
                    );
            }

            return Entity.Null;
        }

        #endregion

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
