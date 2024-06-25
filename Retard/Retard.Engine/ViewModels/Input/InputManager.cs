using System;
using System.Collections.Generic;
using Arch.LowLevel;
using Microsoft.Xna.Framework;
using Retard.Core.Models.Assets.Input;
using Retard.Core.Models.ValueTypes;
using Retard.Engine.Models.Input;

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
        public static Resources<Action> ActionResources => InputManager._actionResources;

        /// <summary>
        /// Permet d'accéder aux events de type Vector1D
        /// </summary>
        public static Resources<Action<float>> ActionVector1DResources => InputManager._actionVector1DResources;

        /// <summary>
        /// Permet d'accéder aux events de type Vector2D
        /// </summary>
        public static Resources<Action<Vector2>> ActionVector2DResources => InputManager._actionVector2DResources;

        #endregion

        #region Variables d'instance

        /// <summary>
        /// La liste des types d'entrées autorisées pour ce jeu
        /// (clavier, souris, manette, etc.)
        /// </summary>
        private static Dictionary<Type, IInputScheme> _inputSchemes;

        /// <summary>
        /// La liste des abonnements pour chaque Input action de type ButtonState
        /// </summary>
        private static UnsafeList<NativeString> _buttonStateEventsIDs;

        /// <summary>
        /// La liste des abonnements pour chaque Input action de type Vector1D
        /// </summary>
        private static UnsafeList<NativeString> _vector1DEventsIDs;

        /// <summary>
        /// La liste des abonnements pour chaque Input action de type Vector2D
        /// </summary>
        private static UnsafeList<NativeString> _vector2DEventsIDs;

        /// <summary>
        /// La liste des abonnements pour chaque Input action de type ButtonState
        /// </summary>
        private static UnsafeArray<InputActionButtonStateEvents> _buttonStateEvents;

        /// <summary>
        /// La liste des abonnements pour chaque Input action de type Vector1D
        /// </summary>
        private static UnsafeArray<InputActionVector1DEvent> _vector1DEvents;

        /// <summary>
        /// La liste des abonnements pour chaque Input action de type Vector2D
        /// </summary>
        private static UnsafeArray<InputActionVector2DEvent> _vector2DEvents;

        /// <summary>
        /// Permet d'accéder aux events sans type
        /// </summary>
        private static Resources<Action> _actionResources;

        /// <summary>
        /// Permet d'accéder aux events de type Vector1D
        /// </summary>
        private static Resources<Action<float>> _actionVector1DResources;

        /// <summary>
        /// Permet d'accéder aux events de type Vector2D
        /// </summary>
        private static Resources<Action<Vector2>> _actionVector2DResources;

        #endregion

        #region Méthodes statiques publiques

        #region Schemes

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
        /// Récupère le contrôleur du type souhaité
        /// </summary>
        /// <typeparam name="T">Le type du contrôleur souhaité</typeparam>
        /// <returns>Le contrôleur souhaité</returns>
        public static T GetScheme<T>() where T : IInputScheme, new()
        {
            Type t = typeof(T);
            return (T)InputManager._inputSchemes[t];
        }

        /// <summary>
        /// Capture l'état des inputs lors de la frame actuelle
        /// </summary>
        public static void Update()
        {
            foreach (KeyValuePair<Type, IInputScheme> pair in InputManager._inputSchemes)
            {
                pair.Value.Update();
            }
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

        #endregion

        #region InputActions

        /// <summary>
        /// Crée les InputSchemes pour chaque type de contrôleur souhaité
        /// </summary>
        /// <param name="buttonIDs">La liste des IDs des actions de type ButtonState</param>
        /// <param name="vector1DIDs">La liste des IDs des actions de type Vector1D</param>
        /// <param name="vector2DIDs">La liste des IDs des actions de type Vector2D</param>
        public static void InitializeInputActionEvents(UnsafeList<NativeString> buttonIDs, UnsafeList<NativeString> vector1DIDs, UnsafeList<NativeString> vector2DIDs)
        {
            InputManager._actionResources = new(buttonIDs.Count * 3);
            InputManager._actionVector1DResources = new(vector1DIDs.Count * 3);
            InputManager._actionVector2DResources = new(vector1DIDs.Count * 3);

            InputManager._buttonStateEventsIDs = buttonIDs;
            InputManager._vector1DEventsIDs = vector1DIDs;
            InputManager._vector2DEventsIDs = vector2DIDs;

            InputManager._buttonStateEvents = new UnsafeArray<InputActionButtonStateEvents>(buttonIDs.Count);
            InputManager._vector1DEvents = new UnsafeArray<InputActionVector1DEvent>(vector1DIDs.Count);
            InputManager._vector2DEvents = new UnsafeArray<InputActionVector2DEvent>(vector2DIDs.Count);

            for (int i = 0; i < buttonIDs.Count; ++i)
            {
                var started = InputManager._actionResources.Add(delegate
                { });
                var performed = InputManager._actionResources.Add(delegate
                { });
                var finished = InputManager._actionResources.Add(delegate
                { });
                InputManager._buttonStateEvents[i] = new InputActionButtonStateEvents(started, performed, finished);
            }

            for (int i = 0; i < vector1DIDs.Count; ++i)
            {
                var performed = InputManager._actionVector1DResources.Add(delegate
                { });
                InputManager._vector1DEvents[i] = new InputActionVector1DEvent(performed);
            }

            for (int i = 0; i < vector2DIDs.Count; ++i)
            {
                var performed = InputManager._actionVector2DResources.Add(delegate
                { });
                InputManager._vector2DEvents[i] = new InputActionVector2DEvent(performed);
            }
        }

        /// <summary>
        /// Récupère les événements liés un InputAction de type ButtonState à partir de son ID.
        /// </summary>
        /// <param name="key">L'ID de l'action</param>
        /// <returns>Les actions liées à cet id</returns>
        public static ref InputActionButtonStateEvents GetButtonEvent(NativeString key)
        {
            return ref InputManager._buttonStateEvents[InputManager._buttonStateEventsIDs.IndexOf(key)];
        }

        /// <summary>
        /// Récupère les événements liés un InputAction de type Vector1D à partir de son ID.
        /// </summary>
        /// <param name="key">L'ID de l'action</param>
        /// <returns>Les actions liées à cet id</returns>
        public static ref InputActionVector1DEvent GetVector1DEvent(NativeString key)
        {
            return ref InputManager._vector1DEvents[InputManager._vector1DEventsIDs.IndexOf(key)];
        }

        /// <summary>
        /// Récupère les événements liés un InputAction de type Vector2D à partir de son ID.
        /// </summary>
        /// <param name="key">L'ID de l'action</param>
        /// <returns>Les actions liées à cet id</returns>
        public static ref InputActionVector2DEvent GetVector2DEvent(NativeString key)
        {
            return ref InputManager._vector2DEvents[InputManager._vector2DEventsIDs.IndexOf(key)];
        }

        #endregion

        #endregion
    }
}
