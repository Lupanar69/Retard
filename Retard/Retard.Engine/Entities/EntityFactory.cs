using System.Runtime.CompilerServices;
using Arch.Core;
using Arch.LowLevel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Retard.Core.Components.Input;
using Retard.Core.Components.Sprites;
using Retard.Core.Models.Assets.Input;
using Retard.Engine.Components.Input;
using Retard.Engine.Models.Input;

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
        public static Entity CreateInputActionEntities(World world, string name, InputActionReturnValueType valueType)
        {
            Entity e = world.Create(new InputActionIDCD { Value = name });

            switch (valueType)
            {
                case InputActionReturnValueType.ButtonState:
                    world.Add<InputActionButtonStateValueCD>(e);
                    break;
                case InputActionReturnValueType.Vector1D:
                    world.Add<InputActionVector1DValueCD>(e);
                    break;
                case InputActionReturnValueType.Vector2D:
                    world.Add<InputActionVector2DValueCD>(e);
                    break;
            }

            return e;
        }

        /// <summary>
        /// Crée les entités des entrées
        /// </summary>
        /// <param name="world">Le monde contenant ces entités</param>
        /// <param name="keySequence">La séquence d'entrées à réaliser pour exécuter l'action (ex: Ctrl+Z)</param>
        /// <param name="joystick">Le joystick à utiliser</param>
        /// <param name="joystickAxis">L'axe du joystick à évaluer</param>
        /// <param name="deadZone"> La valeur en dessous de laquelle le joystick est considéré comme inerte</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Entity CreateInputBindingEntities(World world, InputKeySequenceElement[] keySequence, JoystickType joystick, JoystickAxis joystickAxis, float deadZone)
        {
            if (joystick != JoystickType.None)
            {
                Entity e = world.Create(new InputBindingDeadZoneCD { Value = deadZone });

                switch (joystickAxis)
                {
                    case JoystickAxis.Both:
                        world.Add(e, new InputBindingJoystickXAxisCD { Value = joystick });
                        world.Add(e, new InputBindingJoystickYAxisCD { Value = joystick });
                        break;
                    case JoystickAxis.XAxis:
                        world.Add(e, new InputBindingJoystickXAxisCD { Value = joystick });
                        break;
                    case JoystickAxis.YAxis:
                        world.Add(e, new InputBindingJoystickYAxisCD { Value = joystick });
                        break;
                }

                return e;
            }

            if (keySequence != null || keySequence.Length >= 0)
            {
                UnsafeArray<InputBindingKeyType> keyTypes = new(keySequence.Length);
                UnsafeArray<int> keys = new(keySequence.Length);
                UnsafeArray<InputKeySequenceState> validStates = new(keySequence.Length);

                for (int i = 0; i < keySequence.Length; ++i)
                {
                    InputKeySequenceElement element = keySequence[i];

                    if (element.MouseKey != MouseKey.None)
                    {
                        keyTypes[i] = InputBindingKeyType.MouseKey;
                        keys[i] = (int)element.MouseKey;
                    }
                    else if (element.KeyboardKey != Keys.None)
                    {
                        keyTypes[i] = InputBindingKeyType.KeyboardKey;
                        keys[i] = (int)element.KeyboardKey;
                    }
                    else if (element.GamePadKey != Buttons.None)
                    {
                        keyTypes[i] = InputBindingKeyType.GamePadKey;
                        keys[i] = (int)element.GamePadKey;
                    }
                    else if (element.JoystickKey != JoystickKey.None)
                    {
                        keyTypes[i] = InputBindingKeyType.JoystickKey;
                        keys[i] = (int)element.JoystickKey;
                    }

                    validStates[i] = element.ValidState;
                }


                return world.Create
                    (
                    new InputBindingKeySequenceTypeBU { KeyTypes = keyTypes },
                    new InputBindingKeySequenceIDsBU { Keys = keys },
                    new InputBindingKeySequenceStatesBU { ValidStates = validStates }
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
