﻿using Arch.Core;
using Arch.LowLevel;
using Arch.Relationships;
using Retard.Core.Components.Input;
using Retard.Core.Entities;
using Retard.Core.Models.Arch;
using Retard.Core.Models.ValueTypes;
using Retard.Core.ViewModels.Input;
using Retard.Engine.Models.DTOs.Input;

namespace Retard.Core.Systems.Input
{
    /// <summary>
    /// Affiche les sprites à l'écran
    /// </summary>
    public readonly struct InputSystem : ISystemWorld
    {
        #region Propriétés

        /// <summary>
        /// Le monde contenant les entités
        /// </summary>
        public readonly World World { get; init; }

        #endregion

        #region Variables d'instance

        /// <summary>
        /// Le nombre max de contrôleurs pris en charge par l'InputSystem
        /// </summary>
        private readonly int _nbMaxControllers;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="world">Le monde contenant les entités des sprites</param>
        /// <param name="inputConfig">Les données du fichier de config des entrées du joueur</param>
        public InputSystem(World world, InputConfigDTO inputConfig)
        {
            this.World = world;
            this._nbMaxControllers = InputManager.TryGetScheme(out GamePadInput gamePadInput) ? gamePadInput.NbMaxGamePads : 1;

            InputSystem.CreateInputEntities(this.World, inputConfig, this._nbMaxControllers);
            InputSystem.CreateInputActionEvents(this.World);
        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void Update()
        {
            // Calcule les résultats des InputBindings

            Queries.ProcessButtonStateInputBindingsQuery(this.World);
            Queries.ProcessVector1DKeysInputBindingsQuery(this.World);
            Queries.ProcessVector1DTriggerInputBindingsQuery(this.World);
            Queries.ProcessVector1DJoystickXInputBindingsQuery(this.World);
            Queries.ProcessVector1DJoystickYInputBindingsQuery(this.World);
            Queries.ProcessVector2DKeysInputBindingsQuery(this.World);
            Queries.ProcessVector2DJoystickInputBindingsQuery(this.World);

            // Appelle les events pour chaque InputAction

            Queries.ProcessButtonStateInputActionsQuery(this.World, this.World);
            Queries.ProcessVector1DInputActionsQuery(this.World, this.World, this._nbMaxControllers);
            Queries.ProcessVector2DInputActionsQuery(this.World, this.World, this._nbMaxControllers);
        }

        #endregion

        #region Méthodes statiques privées

        /// <summary>
        /// Crée les entités des inputs à partir des données de config
        /// </summary>
        /// <param name="world">Le monde contenant les entités</param>
        /// <param name="config">Les données de config</param>
        /// <param name="nbMaxControllers">Le nombre max de contrôleurs pris en charge par l'InputSystem</param>
        private static void CreateInputEntities(World world, InputConfigDTO config, int nbMaxControllers)
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

                #region Création des bindings

                UnsafeList<Entity> bindingEs = new(action.Bindings.Length);

                for (int j = 0; j < action.Bindings.Length; ++j)
                {
                    InputBindingDTO binding = action.Bindings[j];
                    Entity e1 = EntityFactory.CreateInputBindingEntities(world, nbMaxControllers, usesMouse, usesKeyboard, usesGamePad, binding.KeySequence);
                    Entity e2 = EntityFactory.CreateInputBindingEntities(world, nbMaxControllers, usesMouse, usesKeyboard, usesGamePad, binding.Vector1DKeys);
                    Entity e3 = EntityFactory.CreateInputBindingEntities(world, nbMaxControllers, usesMouse, usesKeyboard, usesGamePad, binding.Vector2DKeys);
                    Entity e4 = EntityFactory.CreateInputBindingEntities(world, nbMaxControllers, usesGamePad, binding.Joystick);
                    Entity e5 = EntityFactory.CreateInputBindingEntities(world, nbMaxControllers, usesMouse, usesGamePad, binding.Trigger);

                    // Si un binding est null (aucune touche renseignée ou aucun IScheme correspondant dans l'InputManager),
                    // on se contente de l'ignorer

                    if (e1 != Entity.Null)
                    {
                        bindingEs.Add(e1);
                    }

                    if (e2 != Entity.Null)
                    {
                        bindingEs.Add(e2);
                    }

                    if (e3 != Entity.Null)
                    {
                        bindingEs.Add(e3);
                    }

                    if (e4 != Entity.Null)
                    {
                        bindingEs.Add(e4);
                    }

                    if (e5 != Entity.Null)
                    {
                        bindingEs.Add(e5);
                    }
                }

                #endregion

                #region Création des actions

                if (bindingEs.Count > 0)
                {
                    Entity actionE = EntityFactory.CreateInputActionEntities(world, nbMaxControllers, action.Name, action.ValueType);

                    for (int j = 0; j < bindingEs.Count; ++j)
                    {
                        world.AddRelationship<InputActionOf>(actionE, bindingEs[j]);
                    }
                }

                #endregion
            }
        }

        /// <summary>
        /// Pour chaque InputAction, crée ses events
        /// et les enregistre dans l'InputManager
        /// </summary>
        /// <param name="world">Le monde contenant les entités</param>
        private static void CreateInputActionEvents(World world)
        {
            var query1 = new QueryDescription().WithAll<InputActionIDCD, InputButtonStateValuesBU>();
            var query2 = new QueryDescription().WithAll<InputActionIDCD, InputVector1DValuesBU>();
            var query3 = new QueryDescription().WithAll<InputActionIDCD, InputVector2DValuesBU>();

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
