using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Arch.Core;
using Arch.LowLevel;
using Arch.Relationships;
using FixedStrings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Retard.Core.Models.Arch;
using Retard.Engine.Entities;
using Retard.Input.Components;
using Retard.Input.Entities;
using Retard.Input.Models;
using Retard.Input.Models.Assets;
using Retard.Input.Models.DTOs;
using Retard.Input.Systems;

namespace Retard.Input.ViewModels
{
    /// <summary>
    /// Passerelle entre les entrées du joueur
    /// et les commandes à exécuter
    /// </summary>
    public sealed class InputManager
    {
        #region Singleton

        /// <summary>
        /// Singleton
        /// </summary>
        public static InputManager Instance => InputManager._instance.Value;

        /// <summary>
        /// Singleton
        /// </summary>
        private static readonly Lazy<InputManager> _instance = new(() => new InputManager());

        #endregion

        #region Propriétés

        /// <summary>
        /// Permet d'accéder aux events sans type
        /// </summary>
        public Resources<Action<int>> ActionButtonResources { get; init; }

        /// <summary>
        /// Permet d'accéder aux events de type Vector1D
        /// </summary>
        public Resources<Action<int, float>> ActionVector1DResources { get; init; }

        /// <summary>
        /// Permet d'accéder aux events de type Vector2D
        /// </summary>
        public Resources<Action<int, Vector2>> ActionVector2DResources { get; init; }

        /// <summary>
        /// Regroupe les handles de chaque InputAction
        /// </summary>
        public InputHandles Handles { get; set; }

        #endregion

        #region Variables d'instance

        /// <summary>
        /// La liste des types d'entrées autorisées pour ce jeu
        /// (clavier, souris, manette, etc.)
        /// </summary>
        private Dictionary<Type, IInputScheme> _inputSchemes;

        /// <summary>
        /// Les systèmes ECS à màj dans Update()
        /// </summary>
        private Group _updateSystems;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        private InputManager()
        {
            this.ActionButtonResources = new Resources<Action<int>>(1);
            this.ActionVector1DResources = new Resources<Action<int, float>>(1);
            this.ActionVector2DResources = new Resources<Action<int, Vector2>>(1);
            this.Handles = new InputHandles();
            this._updateSystems = new Group("Update Systems");
        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Capture l'état des inputs lors de la frame actuelle
        /// </summary>
        /// <param name="w">Le monde contenant les entités</param>
        public void Update(World w)
        {
            foreach (KeyValuePair<Type, IInputScheme> pair in this._inputSchemes)
            {
                pair.Value.Update();
            }

            this._updateSystems.Update(w);
        }

        /// <summary>
        /// Capture l'état des inputs lors de la frame précédente
        /// A appeler en fin d'Update pour ne pas écraser le précédent état
        /// avant les comparaisons
        /// </summary>
        public void AfterUpdate()
        {
            foreach (KeyValuePair<Type, IInputScheme> pair in this._inputSchemes)
            {
                pair.Value.AfterUpdate();
            }
        }

        /// <summary>
        /// Récupère le contrôleur du type souhaité
        /// </summary>
        /// <typeparam name="T">Le type du contrôleur souhaité</typeparam>
        /// <returns>Le contrôleur souhaité</returns>
        public T GetScheme<T>() where T : IInputScheme, new()
        {
            return (T)this._inputSchemes[typeof(T)];
        }

        /// <summary>
        /// Indique si le contrôleur du type souhaité existe et le retourne si c'est le cas
        /// </summary>
        /// <typeparam name="T">Le type du contrôleur souhaité</typeparam>
        /// <returns><see langword="true"/> si le contrôleur souhaité existe</returns>
        public bool TryGetScheme<T>(out T scheme) where T : IInputScheme, new()
        {
            if (this.HasScheme<T>())
            {
                scheme = this.GetScheme<T>();
                return true;
            }

            scheme = default;
            return false;
        }

        /// <summary>
        /// Indique si le type d'entrée est disponible pour ce jeu
        /// </summary>
        /// <typeparam name="T">Le type du contrôleur souhaité</typeparam>
        /// <returns><see langword="true"/> si le contrôleur souhaité existe</returns>
        public bool HasScheme<T>() where T : IInputScheme, new()
        {
            return this._inputSchemes.ContainsKey(typeof(T));
        }

        /// <summary>
        /// Récupère les événements liés un InputAction de type ButtonState à partir de son ID.
        /// </summary>
        /// <param name="key">L'ID de l'action</param>
        /// <returns>Les actions liées à cet id</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref readonly InputActionButtonStateHandles GetButtonEvent(FixedString32 key)
        {
            return ref this.Handles.GetButtonEvent(key);
        }

        /// <summary>
        /// Récupère les événements liés un InputAction de type Vector1D à partir de son ID.
        /// </summary>
        /// <param name="key">L'ID de l'action</param>
        /// <returns>Les actions liées à cet id</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref readonly InputActionVector1DHandles GetVector1DEvent(FixedString32 key)
        {
            return ref this.Handles.GetVector1DEvent(key);
        }

        /// <summary>
        /// Récupère les événements liés un InputAction de type Vector2D à partir de son ID.
        /// </summary>
        /// <param name="key">L'ID de l'action</param>
        /// <returns>Les actions liées à cet id</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref readonly InputActionVector2DHandles GetVector2DEvent(FixedString32 key)
        {
            return ref this.Handles.GetVector2DEvent(key);
        }

        /// <summary>
        /// Récupère le nombre max de contrôleurs évalués par l'InputManager.
        /// Si aucune manette n'est connectée, renvoie toujours 1.
        /// </summary>
        /// <returns>Le nombre max de contrôleurs évalués par l'InputManager (1 par défaut)</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetNbMaxControllers()
        {
            return this.TryGetScheme(out GamePadInput gamePadInput) ? gamePadInput.NbMaxGamePads : 1;
        }

        /// <summary>
        /// Crée les InputSchemes pour chaque type de contrôleur souhaité
        /// </summary>
        /// <param name="inputSchemes">Les contrôleurs pris en charge par le jeu</param>
        public void InitializeInputSchemes(IInputScheme[] inputSchemes)
        {
            // Initialise les contrôleurs

            this._inputSchemes = new Dictionary<Type, IInputScheme>(inputSchemes.Length);

            for (int i = 0; i < inputSchemes.Length; ++i)
            {
                this._inputSchemes.Add(inputSchemes[i].GetType(), inputSchemes[i]);
            }
        }

        /// <summary>
        /// Initialise les systèmes
        /// </summary>
        public void InitializeSystems(int nbMaxControllers)
        {
            // Initalise les systèmes

            ISystem inputSystem = new InputSystem(nbMaxControllers);
            inputSystem.Initialize();
            this._updateSystems.Add(inputSystem);
        }

        /// <summary>
        /// Enregistre les inputActions renseignées dans la liste des actions actives
        /// </summary>
        /// <param name="w">Le monde contenant les entités</param>
        /// <param name="nbMaxControllers">Le nombre max de contrôleurs pris en charge par l'InputSystem</param>
        /// <param name="inputActions">La liste des actions à convertir en entités</param>
        public void RegisterInputActions(World w, int nbMaxControllers, params InputActionDTO[] inputActions)
        {
            // Doivent être appelées ensemble pour que les queries
            // puissent trouver l'ID du handle correspondant

            this.AddInputEntities(w, nbMaxControllers, inputActions);
            this.AddActionsIDsToHandles(inputActions);
        }

        /// <summary>
        /// Détruit toutes les entités des InputActions et InputBindings
        /// </summary>
        /// <param name="w">Le monde contenant les entités</param>
        public static void RemoveAllInputActions(World w)
        {
            Queries.DestroyAllInputEntitiesQuery(w, w);
        }

        /// <summary>
        /// Détruit les entités des InputActions selon leurs IDs
        /// ainsi que leurs bindings
        /// </summary>
        /// <param name="w">Le monde contenant les entités</param>
        /// <param name="actionsIDs">Les IDs des actions à supprimer</param>
        public static void RemoveInputActions(World w, params FixedString32[] actionsIDs)
        {
            Queries.DestroyInputEntitiesQuery(w, w, actionsIDs);
        }

        #endregion

        #region Méthodes internes

        /// <summary>
        /// Retourne l'état de la touche
        /// </summary>
        /// <param name="mouseKey">La touche de la souris à evaluer</param>
        /// <returns>L'état de l'élément de la séquence de l'InputBinding</returns>
        /// <exception cref="Exception">La touche renseignée est invalide</exception>
        internal InputKeySequenceState GetMouseKeyState(MouseKey mouseKey)
        {
            MouseInput mouseInput = this.GetScheme<MouseInput>();

            return mouseKey switch
            {
                MouseKey.Mouse0 => mouseInput.LeftMousePressed()
                                        ? InputKeySequenceState.Pressed
                                        : mouseInput.LeftMouseHeld()
                                        ? InputKeySequenceState.Held
                                        : mouseInput.LeftMouseReleased()
                                        ? InputKeySequenceState.Released
                                        : InputKeySequenceState.Inert,

                MouseKey.Mouse1 => mouseInput.RightMousePressed()
                                        ? InputKeySequenceState.Pressed
                                        : mouseInput.RightMouseHeld()
                                        ? InputKeySequenceState.Held
                                        : mouseInput.RightMouseReleased()
                                        ? InputKeySequenceState.Released
                                        : InputKeySequenceState.Inert,

                MouseKey.Mouse2 => mouseInput.MiddleMousePressed()
                                        ? InputKeySequenceState.Pressed
                                        : mouseInput.MiddleMouseHeld()
                                        ? InputKeySequenceState.Held
                                        : mouseInput.MiddleMouseReleased()
                                        ? InputKeySequenceState.Released
                                        : InputKeySequenceState.Inert,

                MouseKey.Mouse3 => mouseInput.XButton1Pressed()
                                        ? InputKeySequenceState.Pressed
                                        : mouseInput.XButton1Held()
                                        ? InputKeySequenceState.Held
                                        : mouseInput.XButton1Released()
                                        ? InputKeySequenceState.Released
                                        : InputKeySequenceState.Inert,

                MouseKey.Mouse4 => mouseInput.XButton2Pressed()
                                        ? InputKeySequenceState.Pressed
                                        : mouseInput.XButton2Held()
                                        ? InputKeySequenceState.Held
                                        : mouseInput.XButton2Released()
                                        ? InputKeySequenceState.Released
                                        : InputKeySequenceState.Inert,

                _ => throw new Exception($"Erreur : MouseKey{mouseKey} invalide"),
            };
        }

        /// <summary>
        /// Retourne l'état de la touche
        /// </summary>
        /// <param name="keyboardKey">La touche du clavier à evaluer</param>
        /// <returns>L'état de l'élément de la séquence de l'InputBinding</returns>
        /// <exception cref="Exception">La touche renseignée est invalide</exception>
        internal InputKeySequenceState GetKeyboardKeyState(Keys keyboardKey)
        {
            KeyboardInput keyboardInput = this.GetScheme<KeyboardInput>();

            return keyboardInput.IsKeyPressed(keyboardKey)
                                        ? InputKeySequenceState.Pressed
                                        : keyboardInput.IsKeyHeld(keyboardKey)
                                        ? InputKeySequenceState.Held
                                        : keyboardInput.IsKeyReleased(keyboardKey)
                                        ? InputKeySequenceState.Released
                                        : InputKeySequenceState.Inert;
        }

        /// <summary>
        /// Retourne l'état de la touche
        /// </summary>
        /// <param name="playerIndex">L'ID de la manette</param>
        /// <param name="gamePadKey">La touche de la manette à evaluer</param>
        /// <returns>L'état de l'élément de la séquence de l'InputBinding</returns>
        internal InputKeySequenceState GetGamePadKeyState(int playerIndex, Buttons gamePadKey)
        {
            GamePadInput gamePadInput = this.GetScheme<GamePadInput>();

            return gamePadInput.IsButtonPressed(playerIndex, gamePadKey)
                                        ? InputKeySequenceState.Pressed
                                        : gamePadInput.IsButtonHeld(playerIndex, gamePadKey)
                                        ? InputKeySequenceState.Held
                                        : gamePadInput.IsButtonReleased(playerIndex, gamePadKey)
                                        ? InputKeySequenceState.Released
                                        : InputKeySequenceState.Inert;
        }

        /// <summary>
        /// Retourne l'état de la touche
        /// </summary>
        /// <param name="playerIndex">L'ID de la manette</param>
        /// <param name="joystickKey">La touche de la manette à evaluer</param>
        /// <returns>L'état de l'élément de la séquence de l'InputBinding</returns>
        /// <exception cref="Exception">La touche renseignée est invalide</exception>
        internal InputKeySequenceState GetJoystickKeyState(int playerIndex, JoystickKey joystickKey)
        {
            GamePadInput gamePadInput = this.GetScheme<GamePadInput>();
            Vector2 leftAxis = gamePadInput.GetLeftThumbstickAxis(playerIndex);
            Vector2 rightAxis = gamePadInput.GetRightThumbstickAxis(playerIndex);

            return joystickKey switch
            {
                JoystickKey.LeftNorth => leftAxis.Y > 0f && gamePadInput.IsLeftThumbstickYAxisPressed(playerIndex, 0.9f)
                                        ? InputKeySequenceState.Pressed
                                        : leftAxis.Y > 0f && gamePadInput.IsLeftThumbstickYAxisHeld(playerIndex, 0.9f)
                                        ? InputKeySequenceState.Held
                                        : leftAxis.Y > 0f && gamePadInput.IsLeftThumbstickYAxisReleased(playerIndex, 0.9f)
                                        ? InputKeySequenceState.Released
                                        : InputKeySequenceState.Inert,

                JoystickKey.LeftEast => leftAxis.X > 0f && gamePadInput.IsLeftThumbstickXAxisPressed(playerIndex, 0.9f)
                                        ? InputKeySequenceState.Pressed
                                        : leftAxis.X > 0f && gamePadInput.IsLeftThumbstickXAxisHeld(playerIndex, 0.9f)
                                        ? InputKeySequenceState.Held
                                        : leftAxis.X > 0f && gamePadInput.IsLeftThumbstickXAxisReleased(playerIndex, 0.9f)
                                        ? InputKeySequenceState.Released
                                        : InputKeySequenceState.Inert,

                JoystickKey.LeftSouth => leftAxis.Y < 0f && gamePadInput.IsLeftThumbstickYAxisPressed(playerIndex, 0.9f)
                                        ? InputKeySequenceState.Pressed
                                        : leftAxis.Y < 0f && gamePadInput.IsLeftThumbstickYAxisHeld(playerIndex, 0.9f)
                                        ? InputKeySequenceState.Held
                                        : leftAxis.Y < 0f && gamePadInput.IsLeftThumbstickYAxisReleased(playerIndex, 0.9f)
                                        ? InputKeySequenceState.Released
                                        : InputKeySequenceState.Inert,

                JoystickKey.LeftWest => leftAxis.X < 0f && gamePadInput.IsLeftThumbstickXAxisPressed(playerIndex, 0.9f)
                                        ? InputKeySequenceState.Pressed
                                        : leftAxis.X < 0f && gamePadInput.IsLeftThumbstickXAxisHeld(playerIndex, 0.9f)
                                        ? InputKeySequenceState.Held
                                        : leftAxis.X < 0f && gamePadInput.IsLeftThumbstickXAxisReleased(playerIndex, 0.9f)
                                        ? InputKeySequenceState.Released
                                        : InputKeySequenceState.Inert,

                JoystickKey.LeftNorthEast => leftAxis.X > 0f && leftAxis.Y > 0f && gamePadInput.IsLeftThumbstickXAxisPressed(playerIndex, 0.5f) && gamePadInput.IsLeftThumbstickYAxisPressed(playerIndex, 0.5f)
                                        ? InputKeySequenceState.Pressed
                                        : leftAxis.X > 0f && leftAxis.Y > 0f && gamePadInput.IsLeftThumbstickXAxisHeld(playerIndex, 0.5f) && gamePadInput.IsLeftThumbstickYAxisHeld(playerIndex, 0.5f)
                                        ? InputKeySequenceState.Held
                                        : leftAxis.X > 0f && leftAxis.Y > 0f && gamePadInput.IsLeftThumbstickXAxisReleased(playerIndex, 0.5f) && gamePadInput.IsLeftThumbstickYAxisReleased(playerIndex, 0.5f)
                                        ? InputKeySequenceState.Released
                                        : InputKeySequenceState.Inert,

                JoystickKey.LeftSouthEast => leftAxis.X > 0f && leftAxis.Y < 0f && gamePadInput.IsLeftThumbstickXAxisPressed(playerIndex, 0.5f) && gamePadInput.IsLeftThumbstickYAxisPressed(playerIndex, 0.5f)
                                        ? InputKeySequenceState.Pressed
                                        : leftAxis.X > 0f && leftAxis.Y < 0f && gamePadInput.IsLeftThumbstickXAxisHeld(playerIndex, 0.5f) && gamePadInput.IsLeftThumbstickYAxisHeld(playerIndex, 0.5f)
                                        ? InputKeySequenceState.Held
                                        : leftAxis.X > 0f && leftAxis.Y < 0f && gamePadInput.IsLeftThumbstickXAxisReleased(playerIndex, 0.5f) && gamePadInput.IsLeftThumbstickYAxisReleased(playerIndex, 0.5f)
                                        ? InputKeySequenceState.Released
                                        : InputKeySequenceState.Inert,

                JoystickKey.LeftSouthWest => leftAxis.X < 0f && leftAxis.Y < 0f && gamePadInput.IsLeftThumbstickXAxisPressed(playerIndex, 0.5f) && gamePadInput.IsLeftThumbstickYAxisPressed(playerIndex, 0.5f)
                                        ? InputKeySequenceState.Pressed
                                        : leftAxis.X < 0f && leftAxis.Y < 0f && gamePadInput.IsLeftThumbstickXAxisHeld(playerIndex, 0.5f) && gamePadInput.IsLeftThumbstickYAxisHeld(playerIndex, 0.5f)
                                        ? InputKeySequenceState.Held
                                        : leftAxis.X < 0f && leftAxis.Y < 0f && gamePadInput.IsLeftThumbstickXAxisReleased(playerIndex, 0.5f) && gamePadInput.IsLeftThumbstickYAxisReleased(playerIndex, 0.5f)
                                        ? InputKeySequenceState.Released
                                        : InputKeySequenceState.Inert,

                JoystickKey.LeftNorthWest => leftAxis.X < 0f && leftAxis.Y > 0f && gamePadInput.IsLeftThumbstickXAxisPressed(playerIndex, 0.5f) && gamePadInput.IsLeftThumbstickYAxisPressed(playerIndex, 0.5f)
                                        ? InputKeySequenceState.Pressed
                                        : leftAxis.X < 0f && leftAxis.Y > 0f && gamePadInput.IsLeftThumbstickXAxisHeld(playerIndex, 0.5f) && gamePadInput.IsLeftThumbstickYAxisHeld(playerIndex, 0.5f)
                                        ? InputKeySequenceState.Held
                                        : leftAxis.X < 0f && leftAxis.Y > 0f && gamePadInput.IsLeftThumbstickXAxisReleased(playerIndex, 0.5f) && gamePadInput.IsLeftThumbstickYAxisReleased(playerIndex, 0.5f)
                                        ? InputKeySequenceState.Released
                                        : InputKeySequenceState.Inert,

                JoystickKey.RightNorth => rightAxis.Y > 0f && gamePadInput.IsRightThumbstickYAxisPressed(playerIndex, 0.9f)
                                        ? InputKeySequenceState.Pressed
                                        : rightAxis.Y > 0f && gamePadInput.IsRightThumbstickYAxisHeld(playerIndex, 0.9f)
                                        ? InputKeySequenceState.Held
                                        : rightAxis.Y > 0f && gamePadInput.IsRightThumbstickYAxisReleased(playerIndex, 0.9f)
                                        ? InputKeySequenceState.Released
                                        : InputKeySequenceState.Inert,

                JoystickKey.RightEast => rightAxis.X > 0f && gamePadInput.IsRightThumbstickXAxisPressed(playerIndex, 0.9f)
                                        ? InputKeySequenceState.Pressed
                                        : rightAxis.X > 0f && gamePadInput.IsRightThumbstickXAxisHeld(playerIndex, 0.9f)
                                        ? InputKeySequenceState.Held
                                        : rightAxis.X > 0f && gamePadInput.IsRightThumbstickXAxisReleased(playerIndex, 0.9f)
                                        ? InputKeySequenceState.Released
                                        : InputKeySequenceState.Inert,

                JoystickKey.RightSouth => rightAxis.Y < 0f && gamePadInput.IsRightThumbstickYAxisPressed(playerIndex, 0.9f)
                                        ? InputKeySequenceState.Pressed
                                        : rightAxis.Y < 0f && gamePadInput.IsRightThumbstickYAxisHeld(playerIndex, 0.9f)
                                        ? InputKeySequenceState.Held
                                        : rightAxis.Y < 0f && gamePadInput.IsRightThumbstickYAxisReleased(playerIndex, 0.9f)
                                        ? InputKeySequenceState.Released
                                        : InputKeySequenceState.Inert,

                JoystickKey.RightWest => rightAxis.X < 0f && gamePadInput.IsRightThumbstickXAxisPressed(playerIndex, 0.9f)
                                        ? InputKeySequenceState.Pressed
                                        : rightAxis.X < 0f && gamePadInput.IsRightThumbstickXAxisHeld(playerIndex, 0.9f)
                                        ? InputKeySequenceState.Held
                                        : rightAxis.X < 0f && gamePadInput.IsRightThumbstickXAxisReleased(playerIndex, 0.9f)
                                        ? InputKeySequenceState.Released
                                        : InputKeySequenceState.Inert,

                JoystickKey.RightNorthEast => rightAxis.X > 0f && rightAxis.Y > 0f && gamePadInput.IsRightThumbstickXAxisPressed(playerIndex, 0.5f) && gamePadInput.IsRightThumbstickYAxisPressed(playerIndex, 0.5f)
                                        ? InputKeySequenceState.Pressed
                                        : rightAxis.X > 0f && rightAxis.Y > 0f && gamePadInput.IsRightThumbstickXAxisHeld(playerIndex, 0.5f) && gamePadInput.IsRightThumbstickYAxisHeld(playerIndex, 0.5f)
                                        ? InputKeySequenceState.Held
                                        : rightAxis.X > 0f && rightAxis.Y > 0f && gamePadInput.IsRightThumbstickXAxisReleased(playerIndex, 0.5f) && gamePadInput.IsRightThumbstickYAxisReleased(playerIndex, 0.5f)
                                        ? InputKeySequenceState.Released
                                        : InputKeySequenceState.Inert,

                JoystickKey.RightSouthEast => rightAxis.X > 0f && rightAxis.Y < 0f && gamePadInput.IsRightThumbstickXAxisPressed(playerIndex, 0.5f) && gamePadInput.IsRightThumbstickYAxisPressed(playerIndex, 0.5f)
                                        ? InputKeySequenceState.Pressed
                                        : rightAxis.X > 0f && rightAxis.Y < 0f && gamePadInput.IsRightThumbstickXAxisHeld(playerIndex, 0.5f) && gamePadInput.IsRightThumbstickYAxisHeld(playerIndex, 0.5f)
                                        ? InputKeySequenceState.Held
                                        : rightAxis.X > 0f && rightAxis.Y < 0f && gamePadInput.IsRightThumbstickXAxisReleased(playerIndex, 0.5f) && gamePadInput.IsRightThumbstickYAxisReleased(playerIndex, 0.5f)
                                        ? InputKeySequenceState.Released
                                        : InputKeySequenceState.Inert,

                JoystickKey.RightSouthWest => rightAxis.X < 0f && rightAxis.Y < 0f && gamePadInput.IsRightThumbstickXAxisPressed(playerIndex, 0.5f) && gamePadInput.IsRightThumbstickYAxisPressed(playerIndex, 0.5f)
                                        ? InputKeySequenceState.Pressed
                                        : rightAxis.X < 0f && rightAxis.Y < 0f && gamePadInput.IsRightThumbstickXAxisHeld(playerIndex, 0.5f) && gamePadInput.IsRightThumbstickYAxisHeld(playerIndex, 0.5f)
                                        ? InputKeySequenceState.Held
                                        : rightAxis.X < 0f && rightAxis.Y < 0f && gamePadInput.IsRightThumbstickXAxisReleased(playerIndex, 0.5f) && gamePadInput.IsRightThumbstickYAxisReleased(playerIndex, 0.5f)
                                        ? InputKeySequenceState.Released
                                        : InputKeySequenceState.Inert,

                JoystickKey.RightNorthWest => rightAxis.X < 0f && rightAxis.Y > 0f && gamePadInput.IsRightThumbstickXAxisPressed(playerIndex, 0.5f) && gamePadInput.IsRightThumbstickYAxisPressed(playerIndex, 0.5f)
                                        ? InputKeySequenceState.Pressed
                                        : rightAxis.X < 0f && rightAxis.Y > 0f && gamePadInput.IsRightThumbstickXAxisHeld(playerIndex, 0.5f) && gamePadInput.IsRightThumbstickYAxisHeld(playerIndex, 0.5f)
                                        ? InputKeySequenceState.Held
                                        : rightAxis.X < 0f && rightAxis.Y > 0f && gamePadInput.IsRightThumbstickXAxisReleased(playerIndex, 0.5f) && gamePadInput.IsRightThumbstickYAxisReleased(playerIndex, 0.5f)
                                        ? InputKeySequenceState.Released
                                        : InputKeySequenceState.Inert,
                _ => throw new NotImplementedException($"JoystickKey non implémenté ({joystickKey})"),
            };
        }

        #endregion

        #region Méthodes privées

        /// <summary>
        /// Crée les entités des inputs à partir des données de config
        /// </summary>
        /// <param name="w">Le monde contenant les entités</param>
        /// <param name="nbMaxControllers">Le nombre max de contrôleurs pris en charge par l'InputSystem</param>
        /// <param name="inputActions">La liste des actions à convertir en entités</param>
        private void AddInputEntities(World w, int nbMaxControllers, params InputActionDTO[] inputActions)
        {
            bool usesMouse = this.HasScheme<MouseInput>();
            bool usesKeyboard = this.HasScheme<KeyboardInput>();
            bool usesGamePad = this.HasScheme<GamePadInput>();

            for (int i = 0; i < inputActions.Length; ++i)
            {
                InputActionDTO action = inputActions[i];

                if (action.Bindings == null || action.Bindings.Length == 0)
                {
                    continue;
                }

                #region Création des bindings

                UnsafeList<Entity> bindingEs = new(action.Bindings.Length);

                for (int j = 0; j < action.Bindings.Length; ++j)
                {
                    InputBindingDTO binding = action.Bindings[j];
                    Entity e1 = EntityFactory.CreateInputBindingKeySequenceEntity(w, nbMaxControllers, usesMouse, usesKeyboard, usesGamePad, binding.KeySequence);
                    Entity e2 = EntityFactory.CreateInputBindingVector1DKeysEntity(w, nbMaxControllers, usesMouse, usesKeyboard, usesGamePad, binding.Vector1DKeys);
                    Entity e3 = EntityFactory.CreateInputBindingVector2DKeysEntity(w, nbMaxControllers, usesMouse, usesKeyboard, usesGamePad, binding.Vector2DKeys);
                    Entity e4 = EntityFactory.CreateInputBindingJoystickEntity(w, nbMaxControllers, usesGamePad, binding.Joystick);
                    Entity e5 = EntityFactory.CreateInputBindingTriggerEntity(w, nbMaxControllers, usesMouse, usesGamePad, binding.Trigger);

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
                    Entity actionE = EntityFactory.CreateInputActionEntities(w, nbMaxControllers, action.Name, action.ValueType);

                    for (int j = 0; j < bindingEs.Count; ++j)
                    {
                        w.AddRelationship<InputActionOf>(actionE, bindingEs[j]);
                    }
                }

                #endregion
            }
        }

        /// <summary>
        /// Initialise les événements des handles pour chaque type d'action souhaitée
        /// </summary>
        /// <param name="inputActions">La liste des actions à enregistrer</param>
        private void AddActionsIDsToHandles(params InputActionDTO[] inputActions)
        {
            for (int i = 0; i < inputActions.Length; ++i)
            {
                InputActionDTO action = inputActions[i];

                switch (action.ValueType)
                {
                    case InputActionReturnValueType.ButtonState:
                        if (!this.Handles.ButtonStateHandleExists(action.Name))
                        {
                            this.Handles.AddButtonStateEvent(action.Name);
                        }
                        break;

                    case InputActionReturnValueType.Vector1D:
                        if (!this.Handles.Vector1DHandleExists(action.Name))
                        {
                            this.Handles.AddVector1DEvent(action.Name);
                        }
                        break;

                    case InputActionReturnValueType.Vector2D:
                        if (!this.Handles.Vector2DHandleExists(action.Name))
                        {
                            this.Handles.AddVector2DEvent(action.Name);
                        }
                        break;
                }
            }
        }

        #endregion
    }
}