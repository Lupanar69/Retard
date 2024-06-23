using Arch.Core;
using Arch.Relationships;
using Retard.Core.Components.Input;
using Retard.Core.Entities;
using Retard.Core.Models;
using Retard.Core.Models.Arch;
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
            for (int i = 0; i < config.Actions.Length; ++i)
            {
                InputActionDTO action = config.Actions[i];

                if (action.Bindings == null ^ action.Bindings.Length == 0)
                {
                    continue;
                }

                Entity actionE = EntityFactory.CreateInputActionEntities(world, action.Name, action.ValueType);

                for (int k = 0; k < action.Bindings.Length; ++k)
                {
                    InputBindingDTO binding = action.Bindings[k];
                    Entity bindingE = EntityFactory.CreateInputBindingEntities
                        (world, binding.KeySequence, binding.Joystick, binding.JoystickAxis, binding.DeadZone);

                    // Si un binding est null (aucune touche renseignée), on se contente de l'ignorer

                    if (bindingE == Entity.Null)
                    {
                        continue;
                    }

                    world.AddRelationship<InputActionOf>(actionE, bindingE);
                }
            }
        }

        /// <summary>
        /// Pour chaque InputAction, crée ses events
        /// et les passe à l'InputManager
        /// </summary>
        /// <param name="world">Le monde contenant les entités</param>
        private static void CreateInputActionEvents(World world)
        {
            //var query = new QueryDescription().WithAll<InputActionIDCD>();
            //world.Query(in query, (Entity e, ref InputActionIDCD contextID) =>
            //{
            //    ref var actionOfRel = ref e.GetRelationships<InputActionOf>();

            //    foreach (KeyValuePair<Entity, InputActionOf> pair in actionOfRel)
            //    {
            //        NativeString eventID = $"{contextID}/{world.Get<InputActionIDCD>(pair.Key).Value}";

            //        if (world.Has<InputActionPerformedCD>(pair.Key))
            //        {
            //            var handlesRefs = world.Get<InputActionStartedCD, InputActionFinishedCD, InputActionPerformedCD>(pair.Key);
            //        }
            //        else if (world.Has<InputActionPerformed1DAxisCD>(pair.Key))
            //        {
            //            var handlesRefs = world.Get<InputActionStartedCD, InputActionFinishedCD, InputActionPerformed1DAxisCD>(pair.Key);
            //        }
            //        else if (world.Has<InputActionPerformed2DAxisCD>(pair.Key))
            //        {
            //            var handlesRefs = world.Get<InputActionStartedCD, InputActionFinishedCD, InputActionPerformed2DAxisCD>(pair.Key);
            //        }
            //    }
            //});
        }

        #endregion
    }
}
