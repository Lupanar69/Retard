using System.Runtime.CompilerServices;
using Arch.Core;
using Arch.LowLevel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Retard.Core.Components.Input;
using Retard.Core.Components.Sprites;
using Retard.Core.Models.Assets.Input;
using Retard.Core.Models.DTOs.Input;

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
        /// Crée les entités des contextes des entrées
        /// </summary>
        /// <param name="world">Le monde contenant ces entités</param>
        /// <param name="name">L'ID du contexte</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Entity CreateInputContextEntities(World world, string name)
        {
            return world.Create(new InputContextIDCD { Value = name });
        }

        /// <summary>
        /// Crée les entités des actions des entrées
        /// </summary>
        /// <param name="world">Le monde contenant ces entités</param>
        /// <param name="name">L'ID du contexte</param>
        /// <param name="valueType">La valeur retournée par l'action</param>
        /// <param name="triggerType">Le type de l'action</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Entity CreateInputActionEntities(World world, string name, InputActionReturnValueType valueType, InputActionTriggerType triggerType)
        {
            Entity e = world.Create(new InputActionIDCD { Value = name });

            switch (valueType)
            {
                case InputActionReturnValueType.ButtonState:
                    world.Add<InputActionButtonStateCD>(e);
                    break;
                case InputActionReturnValueType.Axis1D:
                    world.Add<InputAction1DAxisCD>(e);
                    break;
                case InputActionReturnValueType.Axis2D:
                    world.Add<InputAction2DAxisCD>(e);
                    break;
            }

            switch (triggerType)
            {
                case InputActionTriggerType.Started:
                    world.Add<InputActionStartedCD>(e);
                    break;
                case InputActionTriggerType.Performed:
                    switch (valueType)
                    {
                        case InputActionReturnValueType.ButtonState:
                            world.Add<InputActionPerformedCD>(e);
                            break;
                        default:
                            world.Add<InputActionPerformedFloatCD>(e);
                            break;
                    }
                    break;
                case InputActionTriggerType.Finished:
                    world.Add<InputActionFinishedCD>(e);
                    break;
            }

            return e;
        }

        /// <summary>
        /// Crée les entités des entrées
        /// </summary>
        /// <param name="world">Le monde contenant ces entités</param>
        /// <param name="name">L'ID du contexte</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Entity CreateInputBindingEntities(World world, MouseKey mouseKey, Keys[] keyboardKeys, Buttons gamePadKey, InputBindingAxisType axisType, float deadZone)
        {
            if (mouseKey != MouseKey.None)
            {
                return world.Create(new InputBindingMouseKeyCD { Value = mouseKey });
            }

            if (gamePadKey != Buttons.None)
            {
                Entity e = world.Create(new InputBindingGamePadKeyCD { Value = gamePadKey });

                if (axisType != InputBindingAxisType.None)
                {
                    world.Add(e, new InputBindingDeadZoneCD { Value = deadZone });
                }

                return e;
            }

            if (keyboardKeys != null && keyboardKeys.Length > 0)
            {
                UnsafeArray<Keys> keys = new(keyboardKeys.Length);

                for (int i = 0; i < keyboardKeys.Length; ++i)
                {
                    keys[i] = keyboardKeys[i];
                }

                return world.Create(new InputBindingKeyboardKeysBE { Value = keys });
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
