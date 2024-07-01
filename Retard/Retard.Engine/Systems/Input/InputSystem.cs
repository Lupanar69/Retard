using System.Collections.Generic;
using Arch.Core;
using Arch.LowLevel;
using Arch.Relationships;
using Retard.Core.Components.Input;
using Retard.Core.Entities;
using Retard.Core.Models;
using Retard.Core.Models.Arch;
using Retard.Core.Models.ValueTypes;
using Retard.Core.ViewModels.Input;
using Retard.Engine.Models.DTOs.Input;
using Retard.Engine.ViewModels.Utilities;

namespace Retard.Core.Systems.Input
{
    /// <summary>
    /// Affiche les sprites à l'écran
    /// </summary>
    public struct InputSystem : ISystemWorld
    {
        #region Propriétés

        /// <summary>
        /// Le monde contenant les entités
        /// </summary>
        public World World { get; set; }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="world">Le monde contenant les entités des sprites</param>
        public InputSystem(World world)
        {
            this.World = world;

            string customInputConfigPath = $"{Constants.GAME_DIR_PATH}/{Constants.CUSTOM_INPUT_CONFIG_PATH}";
            string json = JsonUtilities.ReadFile(customInputConfigPath);
            var config = JsonUtilities.DeserializeObject<InputConfigDTO>(json);

            InputSystem.CreateInputEntities(this.World, config);
            InputSystem.CreateInputActionEvents(this.World);
        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Màj à chaque frame
        /// </summary>
        public void Update()
        {
            var query1 = new QueryDescription().WithAll<InputActionIDCD, InputActionButtonStateValueCD>();
            var query2 = new QueryDescription().WithAll<InputActionIDCD, InputActionVector1DValueCD>();
            var query3 = new QueryDescription().WithAll<InputActionIDCD, InputActionVector2DValueCD>();

            World w = this.World;

            this.World.Query(in query1, (Entity actionE, ref InputActionIDCD id, ref InputActionButtonStateValueCD returnValue) =>
            {
                ref var rel = ref w.GetRelationships<InputActionOf>(actionE);

                foreach (KeyValuePair<Entity, InputActionOf> child in rel)
                {
                    Entity bindingE = child.Key;
                    InputManager.SetButtonStateReturnValue(w, bindingE, ref returnValue);
                }
            });
        }

        /// <summary>
        /// Libère les allocations
        /// </summary>
        public void Dispose()
        {

        }

        #endregion

        #region Méthodes statiques privées

        /// <summary>
        /// Crée les entités des inputs à partir des données de config
        /// </summary>
        /// <param name="world">Le monde contenant les entités</param>
        /// <param name="config">Les données de config</param>
        private static void CreateInputEntities(World world, InputConfigDTO config)
        {
            bool usesMouse = InputManager.HasScheme<MouseInput>();
            bool usesKeyboard = InputManager.HasScheme<KeyboardInput>();
            bool usesGamePad = InputManager.HasScheme<GamePadInput>();

            for (int i = 0; i < config.Actions.Length; ++i)
            {
                InputActionDTO action = config.Actions[i];

                if (action.Bindings == null || action.Bindings.Length == 0)
                {
                    continue;
                }

                UnsafeList<Entity> bindingEs = new(action.Bindings.Length);

                for (int j = 0; j < action.Bindings.Length; ++j)
                {
                    InputBindingDTO binding = action.Bindings[j];
                    Entity bindingE = EntityFactory.CreateInputBindingEntities
                        (world, usesMouse, usesKeyboard, usesGamePad, binding.KeySequence, binding.Joystick, binding.JoystickAxis, binding.DeadZone);

                    // Si un binding est null (aucune touche renseignée ou aucun IScheme correspondant dans l'InputManager),
                    // on se contente de l'ignorer

                    if (bindingE != Entity.Null)
                    {
                        bindingEs.Add(bindingE);
                    }

                }

                if (bindingEs.Count > 0)
                {
                    Entity actionE = EntityFactory.CreateInputActionEntities(world, action.Name, action.ValueType);

                    for (int j = 0; j < bindingEs.Count; ++j)
                    {
                        world.AddRelationship<InputActionOf>(actionE, bindingEs[j]);
                    }
                }
            }
        }

        /// <summary>
        /// Pour chaque InputAction, crée ses events
        /// et les enregistre dans l'InputManager
        /// </summary>
        /// <param name="world">Le monde contenant les entités</param>
        private static void CreateInputActionEvents(World world)
        {
            var query1 = new QueryDescription().WithAll<InputActionIDCD, InputActionButtonStateValueCD>();
            var query2 = new QueryDescription().WithAll<InputActionIDCD, InputActionVector1DValueCD>();
            var query3 = new QueryDescription().WithAll<InputActionIDCD, InputActionVector2DValueCD>();

            UnsafeList<NativeString> list1 = new(1);
            UnsafeList<NativeString> list2 = new(1);
            UnsafeList<NativeString> list3 = new(1);

            world.Query(in query1, (ref InputActionIDCD actionID) =>
            {
                list1.Add(actionID.Value);
            });

            world.Query(in query2, (ref InputActionIDCD actionID) =>
            {
                list2.Add(actionID.Value);
            });

            world.Query(in query3, (ref InputActionIDCD actionID) =>
            {
                list3.Add(actionID.Value);
            });

            InputManager.InitializeInputActionEvents(list1, list2, list3);
        }

        #endregion
    }
}
