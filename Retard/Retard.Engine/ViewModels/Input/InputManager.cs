using System;
using System.Collections.Generic;
using Arch.Core;
using Arch.LowLevel;
using Microsoft.Xna.Framework;
using Retard.Core.Models.Arch;
using Retard.Core.Models.Assets.Input;
using Retard.Core.Models.ValueTypes;
using Retard.Core.Systems.Input;
using Retard.Engine.Models.Assets.Input;
using Retard.Engine.ViewModels.Input;

namespace Retard.Core.ViewModels.Input
{
    /// <summary>
    /// Passerelle entre les entrées du joueur
    /// et les commandes à exécuter
    /// </summary>
    public static class InputManager
    {
        #region Propriétés

        /// <summary>
        /// Permet d'accéder aux events sans type
        /// </summary>
        public static Resources<Action> ActionResources { get; private set; }

        /// <summary>
        /// Permet d'accéder aux events de type Vector1D
        /// </summary>
        public static Resources<Action<float>> ActionVector1DResources { get; private set; }

        /// <summary>
        /// Permet d'accéder aux events de type Vector2D
        /// </summary>
        public static Resources<Action<Vector2>> ActionVector2DResources { get; private set; }

        /// <summary>
        /// Regroupe les handles de chaque InputAction
        /// </summary>
        public static InputControls Handles { get; set; }

        #endregion

        #region Variables d'instance

        /// <summary>
        /// Les systèmes ECS à màj dans Update()
        /// </summary>
        private static readonly Group _updateSystems;

        /// <summary>
        /// La liste des types d'entrées autorisées pour ce jeu
        /// (clavier, souris, manette, etc.)
        /// </summary>
        private static Dictionary<Type, IInputScheme> _inputSchemes;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        static InputManager()
        {
            InputManager._updateSystems = new Group("Update Systems");
        }

        #endregion

        #region Méthodes statiques publiques

        #region Init

        /// <summary>
        /// Crée les InputSchemes pour chaque type de contrôleur souhaité
        /// </summary>
        /// <param name="schemes">Les types de chaque contrôleur</param>
        public static void InitializeSchemes(params IInputScheme[] schemes)
        {
            InputManager._inputSchemes = new Dictionary<Type, IInputScheme>(schemes.Length);

            for (int i = 0; i < schemes.Length; i++)
            {
                InputManager._inputSchemes.Add(schemes[i].GetType(), schemes[i]);
            }
        }

        /// <summary>
        /// Crée les systèmes ECS gérant les entrées
        /// </summary>
        /// <param name="world">Le monde contenant les entités</param>
        public static void InitializeSystems(World world)
        {
            InputManager._updateSystems.Add(new InputSystem(world));
            InputManager._updateSystems.Initialize();
        }

        /// <summary>
        /// Crée les InputSchemes pour chaque type de contrôleur souhaité
        /// </summary>
        /// <param name="buttonIDs">La liste des IDs des actions de type ButtonState</param>
        /// <param name="vector1DIDs">La liste des IDs des actions de type Vector1D</param>
        /// <param name="vector2DIDs">La liste des IDs des actions de type Vector2D</param>
        public static void InitializeInputActionEvents(UnsafeList<NativeString> buttonIDs, UnsafeList<NativeString> vector1DIDs, UnsafeList<NativeString> vector2DIDs)
        {
            InputManager.ActionResources = new(Math.Max(1, buttonIDs.Count * 3));
            InputManager.ActionVector1DResources = new(Math.Max(1, vector1DIDs.Count));
            InputManager.ActionVector2DResources = new(Math.Max(1, vector2DIDs.Count));

            InputManager.Handles = new InputControls(buttonIDs, vector1DIDs, vector2DIDs);
        }

        #endregion

        #region Update

        /// <summary>
        /// Capture l'état des inputs lors de la frame actuelle
        /// </summary>
        public static void Update()
        {
            foreach (KeyValuePair<Type, IInputScheme> pair in InputManager._inputSchemes)
            {
                pair.Value.Update();
            }

            InputManager._updateSystems.Update();
        }

        /// <summary>
        /// Capture l'état des inputs lors de la frame précédente
        /// A appeler en fin d'Update pour ne pas écraser le précédent état
        /// avant les comparaisons
        /// </summary>
        public static void AfterUpdate()
        {
            foreach (KeyValuePair<Type, IInputScheme> pair in InputManager._inputSchemes)
            {
                pair.Value.AfterUpdate();
            }
        }

        /// <summary>
        /// Récupère le contrôleur du type souhaité
        /// </summary>
        /// <typeparam name="T">Le type du contrôleur souhaité</typeparam>
        /// <returns>Le contrôleur souhaité</returns>
        public static T GetScheme<T>() where T : IInputScheme, new()
        {
            Type t = typeof(T);
            return (T)InputManager._inputSchemes[t];
        }

        #endregion

        #region InputActions

        /// <summary>
        /// Récupère les événements liés un InputAction de type ButtonState à partir de son ID.
        /// </summary>
        /// <param name="key">L'ID de l'action</param>
        /// <returns>Les actions liées à cet id</returns>
        public static ref InputActionButtonStateHandles GetButtonEvent(NativeString key)
        {
            return ref InputManager.Handles.GetButtonEvent(key);
        }

        /// <summary>
        /// Récupère les événements liés un InputAction de type Vector1D à partir de son ID.
        /// </summary>
        /// <param name="key">L'ID de l'action</param>
        /// <returns>Les actions liées à cet id</returns>
        public static ref InputActionVector1DHandles GetVector1DEvent(NativeString key)
        {
            return ref InputManager.Handles.GetVector1DEvent(key);
        }

        /// <summary>
        /// Récupère les événements liés un InputAction de type Vector2D à partir de son ID.
        /// </summary>
        /// <param name="key">L'ID de l'action</param>
        /// <returns>Les actions liées à cet id</returns>
        public static ref InputActionVector2DHandles GetVector2DEvent(NativeString key)
        {
            return ref InputManager.Handles.GetVector2DEvent(key);
        }

        #endregion

        #endregion
    }
}
