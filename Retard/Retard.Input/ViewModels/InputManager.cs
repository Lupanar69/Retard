using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Arch.Core;
using Arch.LowLevel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Retard.Core.Models.Arch;
using Retard.Core.Models.ValueTypes;
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
        public Resources<Action<int>> ActionButtonResources { get; private set; }

        /// <summary>
        /// Permet d'accéder aux events de type Vector1D
        /// </summary>
        public Resources<Action<int, float>> ActionVector1DResources { get; private set; }

        /// <summary>
        /// Permet d'accéder aux events de type Vector2D
        /// </summary>
        public Resources<Action<int, Vector2>> ActionVector2DResources { get; private set; }

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

        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Récupère les événements liés un InputAction de type ButtonState à partir de son ID.
        /// </summary>
        /// <param name="key">L'ID de l'action</param>
        /// <returns>Les actions liées à cet id</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref readonly InputActionButtonStateHandles GetButtonEvent(NativeString key)
        {
            return ref this.Handles.GetButtonEvent(key);
        }

        /// <summary>
        /// Récupère les événements liés un InputAction de type Vector1D à partir de son ID.
        /// </summary>
        /// <param name="key">L'ID de l'action</param>
        /// <returns>Les actions liées à cet id</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref readonly InputActionVector1DHandles GetVector1DEvent(NativeString key)
        {
            return ref this.Handles.GetVector1DEvent(key);
        }

        /// <summary>
        /// Récupère les événements liés un InputAction de type Vector2D à partir de son ID.
        /// </summary>
        /// <param name="key">L'ID de l'action</param>
        /// <returns>Les actions liées à cet id</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref readonly InputActionVector2DHandles GetVector2DEvent(NativeString key)
        {
            return ref this.Handles.GetVector2DEvent(key);
        }

        #endregion

        #region Méthodes internes

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
        /// <param name="inputConfig">Les données de configuration des entrées à observer</param>
        /// <param name="world">Le monde contenant les entités</param>
        public void InitializeSystems(InputConfigDTO inputConfig, World world)
        {
            // Initalise les systèmes

            this._updateSystems = new Group("Update Systems");
            this._updateSystems.Add(new InputSystem(world, inputConfig));
            this._updateSystems.Initialize();
        }

        /// <summary>
        /// Crée les InputSchemes pour chaque type de contrôleur souhaité
        /// </summary>
        /// <param name="buttonIDs">La liste des IDs des actions de type ButtonState</param>
        /// <param name="vector1DIDs">La liste des IDs des actions de type Vector1D</param>
        /// <param name="vector2DIDs">La liste des IDs des actions de type Vector2D</param>
        public void InitializeInputActionEvents(UnsafeList<NativeString> buttonIDs, UnsafeList<NativeString> vector1DIDs, UnsafeList<NativeString> vector2DIDs)
        {
            this.ActionButtonResources = new Resources<Action<int>>(Math.Max(1, buttonIDs.Count * 3));
            this.ActionVector1DResources = new Resources<Action<int, float>>(Math.Max(1, vector1DIDs.Count));
            this.ActionVector2DResources = new Resources<Action<int, Vector2>>(Math.Max(1, vector2DIDs.Count));

            this.Handles = new InputHandles(buttonIDs, vector1DIDs, vector2DIDs);
        }

        /// <summary>
        /// Capture l'état des inputs lors de la frame actuelle
        /// </summary>
        public void Update()
        {
            foreach (KeyValuePair<Type, IInputScheme> pair in this._inputSchemes)
            {
                pair.Value.Update();
            }

            this._updateSystems.Update();
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
        /// Retourne l'état de la touche
        /// </summary>
        /// <param name="mouseKey">La touche de la souris à evaluer</param>
        /// <returns>L'état de l'élément de la séquence de l'InputBinding</returns>
        /// <exception cref="Exception">La touche renseignée est invalide</exception>
        public InputKeySequenceState GetMouseKeyState(MouseKey mouseKey)
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
        public InputKeySequenceState GetKeyboardKeyState(Keys keyboardKey)
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
        public InputKeySequenceState GetGamePadKeyState(int playerIndex, Buttons gamePadKey)
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
        public InputKeySequenceState GetJoystickKeyState(int playerIndex, JoystickKey joystickKey)
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
    }
}