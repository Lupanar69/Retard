using Arch.Core;
using Arch.Relationships;
using Retard.Core.Components.Input;
using Retard.Core.Entities;
using Retard.Core.Models;
using Retard.Core.Models.Arch;
using Retard.Core.Models.DTOs.Input;
using Retard.Core.ViewModels.JSON;

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

            CreateInputEntities(this.World, config);
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
        /// <param name="config">Le monde contenant les entités</param>
        /// <param name="config">Les données de config</param>
        private static void CreateInputEntities(World world, InputConfigDTO config)
        {
            for (int i = 0; i < config.Contexts.Length; ++i)
            {
                InputContextDTO context = config.Contexts[i];
                Entity contextE = EntityFactory.CreateInputContextEntities(world, context.Name);

                for (int j = 0; j < context.Actions.Length; ++j)
                {
                    InputActionDTO action = context.Actions[j];
                    Entity actionE = EntityFactory.CreateInputActionEntities(world, action.Name, action.ValueType, action.TriggerType);

                    for (int k = 0; k < action.Bindings.Length; ++k)
                    {
                        InputBindingDTO binding = action.Bindings[k];
                        Entity bindingE = EntityFactory.CreateInputBindingEntities
                            (world, binding.MouseKey, binding.KeyboardKeys, binding.GamePadKey, binding.AxisType, binding.DeadZone);

                        // Si un binding est null (aucune touche renseignée), on se contente de l'ignorer

                        if (bindingE == Entity.Null)
                        {
                            continue;
                        }

                        actionE.AddRelationship<InputActionOf>(bindingE);
                    }

                    contextE.AddRelationship<InputContextOf>(actionE);
                }
            }
        }

        #endregion
    }
}
