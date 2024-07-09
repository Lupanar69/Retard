using System;
using System.Collections.Generic;
using Arch.Core;
using Arch.LowLevel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Retard.Core.Components.Input;
using Retard.Core.Models.Arch;
using Retard.Core.Models.Assets.Input;
using Retard.Core.Models.ValueTypes;
using Retard.Core.Systems.Input;
using Retard.Engine.Components.Input;
using Retard.Engine.Models;
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
        public static Resources<Action<int>> ActionResources { get; private set; }

        /// <summary>
        /// Permet d'accéder aux events de type Vector1D
        /// </summary>
        public static Resources<Action<int, float>> ActionVector1DResources { get; private set; }

        /// <summary>
        /// Permet d'accéder aux events de type Vector2D
        /// </summary>
        public static Resources<Action<int, Vector2>> ActionVector2DResources { get; private set; }

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

        #endregion

        #region Schemes

        /// <summary>
        /// Récupère le contrôleur du type souhaité
        /// </summary>
        /// <typeparam name="T">Le type du contrôleur souhaité</typeparam>
        /// <returns>Le contrôleur souhaité</returns>
        public static T GetScheme<T>() where T : IInputScheme, new()
        {
            return (T)InputManager._inputSchemes[typeof(T)];
        }

        /// <summary>
        /// Indique si le contrôleur du type souhaité existe et le retourne si c'est le cas
        /// </summary>
        /// <typeparam name="T">Le type du contrôleur souhaité</typeparam>
        /// <returns><see langword="true"/> si le contrôleur souhaité existe</returns>
        public static bool TryGetScheme<T>(out T scheme) where T : IInputScheme, new()
        {
            if (InputManager.HasScheme<T>())
            {
                scheme = InputManager.GetScheme<T>();
                return true;
            }

            scheme = default;
            return false;
        }

        /// <summary>
        /// Indique si le type d'entrée est disponible pour cette application
        /// </summary>
        /// <typeparam name="T">Le type du contrôleur souhaité</typeparam>
        /// <returns><see langword="true"/> si le contrôleur souhaité existe</returns>
        public static bool HasScheme<T>() where T : IInputScheme, new()
        {
            return InputManager._inputSchemes.ContainsKey(typeof(T));
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

        /// <summary>
        /// Calcule la valeur ButtonState de l'InputAction à partir de l'InputBinding renseigné
        /// </summary>
        /// <param name="w">Le monde contenant les entités</param>
        /// <param name="bindingE">L'entité de l'InputBinding</param>
        /// <param name="inputActionID">L'ID de l'action</param>
        /// <param name="returnValue">La valeur de l'action</param>
        internal static void SetButtonStateReturnValue(World w, Entity bindingE, in NativeString inputActionID, ref InputBindingButtonStateValuesBU returnValue)
        {
            // Commence avec des inputs inertes

            int returnValuesCount = returnValue.Value.Length;
            using UnsafeArray<ButtonStateType> states = new(returnValuesCount);

            for (int i = 0; i < returnValuesCount; ++i)
            {
                states[i] = ButtonStateType.Inert;
            }

            #region Si l'InputBinding utilise un joystick...

            if (w.TryGet(bindingE, out InputBindingJoystickTypeCD joystickTypeCD))
            {
                InputBindingDeadZoneCD deadZoneCD = w.Get<InputBindingDeadZoneCD>(bindingE);
                GamePadInput gamePadInput = InputManager.GetScheme<GamePadInput>();

                switch (joystickTypeCD.Value)
                {
                    case JoystickType.Left:
                        for (int i = 0; i < gamePadInput.NbConnected; ++i)
                        {
                            ButtonStateType actionState = returnValue.Value[i];

                            if (actionState == ButtonStateType.Inert &&
                                gamePadInput.IsLeftThumbstickPressed(i, deadZoneCD.Value))
                            {
                                states[i] = ButtonStateType.Pressed;
                                InputManager.GetButtonEvent(inputActionID).Started?.Invoke(i);
                            }

                            if (actionState == ButtonStateType.Pressed &&
                                gamePadInput.IsLeftThumbstickHeld(i, deadZoneCD.Value))
                            {
                                states[i] = ButtonStateType.Held;
                                InputManager.GetButtonEvent(inputActionID).Performed?.Invoke(i);
                            }

                            if (actionState == ButtonStateType.Held &&
                                gamePadInput.IsLeftThumbstickReleased(i, deadZoneCD.Value))
                            {
                                states[i] = ButtonStateType.Released;
                                InputManager.GetButtonEvent(inputActionID).Finished?.Invoke(i);
                            }
                        }
                        break;

                    case JoystickType.Right:
                        for (int i = 0; i < gamePadInput.NbConnected; ++i)
                        {
                            ButtonStateType actionState = returnValue.Value[i];

                            if (actionState == ButtonStateType.Inert &&
                                gamePadInput.IsRightThumbstickPressed(i, deadZoneCD.Value))
                            {
                                states[i] = ButtonStateType.Pressed;
                                InputManager.GetButtonEvent(inputActionID).Started?.Invoke(i);
                            }

                            if (actionState == ButtonStateType.Pressed &&
                                gamePadInput.IsRightThumbstickHeld(i, deadZoneCD.Value))
                            {
                                states[i] = ButtonStateType.Held;
                                InputManager.GetButtonEvent(inputActionID).Performed?.Invoke(i);
                            }

                            if (actionState == ButtonStateType.Held &&
                                gamePadInput.IsRightThumbstickReleased(i, deadZoneCD.Value))
                            {
                                states[i] = ButtonStateType.Released;
                                InputManager.GetButtonEvent(inputActionID).Finished?.Invoke(i);
                            }
                        }
                        break;
                }
            }

            #endregion

            #region Sinon, s'il utilise des touches...

            else
            {
                var sequenceIDs = w.Get<InputBindingKeySequenceIDsBU>(bindingE);
                var sequenceTypes = w.Get<InputBindingKeySequenceTypesBU>(bindingE);
                var sequenceStates = w.Get<InputBindingKeySequenceStatesBU>(bindingE);
                int sequenceLength = sequenceIDs.Value.Length;

                /* Pour chaque élément de la séquence, on regarde si tous
                 * ses éléments sont entrés correctement.
                 * Si le moindre élément fait défaut, la séquence entière est invalide.
                 * 
                 * Pour les boutons de manette :
                 *      - Si tous les éléments sont des boutons de manette, la séquence
                 *        est considérée comme ayant un ID de manette
                 *      - Si tous ne sont pas des boutons de manette, 
                 *        l'ID du joueur est considéré comme étant 0 (le joueur 1)
                */

                bool gamePadOnly = true;

                #region Détermine d'abord si la séquence ne contient que des boutons de manette

                for (int i = 0; i < sequenceLength; ++i)
                {
                    InputBindingKeyType keyType = sequenceTypes.Value[i];

                    switch (keyType)
                    {
                        case InputBindingKeyType.MouseKey:
                        case InputBindingKeyType.KeyboardKey:
                            gamePadOnly = false;
                            goto Next;
                    }
                }

                #endregion

                Next:

                #region Si manette uniquement, on évalue la séquence pour chacune d'entre elles

                if (gamePadOnly)
                {
                    GamePadInput gamePadInput = InputManager.GetScheme<GamePadInput>();

                    for (int i = 0; i < gamePadInput.NbConnected; ++i)
                    {
                        for (int j = 0; j < sequenceLength; ++j)
                        {
                            int id = sequenceIDs.Value[j];
                            InputBindingKeyType keyType = sequenceTypes.Value[j];
                            InputKeySequenceState validState = sequenceStates.Value[j];

                            #region Si la séquence est invalide, on passe à la manette suivante

                            switch (keyType)
                            {
                                case InputBindingKeyType.GamePadKey:
                                    Buttons gamePadKey = (Buttons)id;
                                    if (InputManager.GetGamePadKeyState(i, gamePadKey, gamePadInput) != validState)
                                    {
                                        goto NextGamePad;
                                    }
                                    break;

                                case InputBindingKeyType.JoystickKey:
                                    JoystickKey joystickKey = (JoystickKey)id;
                                    if (InputManager.GetJoystickKeyState(i, joystickKey, gamePadInput) != validState)
                                    {
                                        goto NextGamePad;
                                    }
                                    break;
                            }

                            #endregion
                        }

                        #region La séquence est valide, on appelle l'event associé

                        switch (returnValue.Value[i])
                        {
                            case ButtonStateType.Inert:
                                states[i] = ButtonStateType.Pressed;
                                InputManager.GetButtonEvent(inputActionID).Started?.Invoke(i);
                                break;

                            case ButtonStateType.Pressed:
                                states[i] = ButtonStateType.Held;
                                InputManager.GetButtonEvent(inputActionID).Performed?.Invoke(i);
                                break;

                            case ButtonStateType.Held:
                                states[i] = ButtonStateType.Released;
                                InputManager.GetButtonEvent(inputActionID).Finished?.Invoke(i);
                                break;
                        }

                        #endregion

                        NextGamePad:
                        continue;
                    }
                }

                #endregion

                #region Sinon, on évalue uniquement pour le joueur 1

                else
                {
                    for (int i = 0; i < sequenceLength; ++i)
                    {
                        int id = sequenceIDs.Value[i];
                        InputBindingKeyType keyType = sequenceTypes.Value[i];
                        InputKeySequenceState validState = sequenceStates.Value[i];

                        switch (keyType)
                        {
                            case InputBindingKeyType.MouseKey:
                                MouseKey mouseKey = (MouseKey)id;
                                if (InputManager.GetMouseKeyState(mouseKey) != validState)
                                {
                                    goto End;
                                }
                                break;

                            case InputBindingKeyType.KeyboardKey:
                                Keys keyboardKey = (Keys)id;
                                if (InputManager.GetKeyboardKeyState(keyboardKey) != validState)
                                {
                                    goto End;
                                }
                                break;

                            case InputBindingKeyType.GamePadKey:
                                Buttons gamePadKey = (Buttons)id;
                                if (InputManager.GetGamePadKeyState(0, gamePadKey) != validState)
                                {
                                    goto End;
                                }
                                break;

                            case InputBindingKeyType.JoystickKey:
                                JoystickKey joystickKey = (JoystickKey)id;
                                if (InputManager.GetJoystickKeyState(0, joystickKey) != validState)
                                {
                                    goto End;
                                }
                                break;
                        }
                    }

                    #region La séquence est valide, on appelle l'event associé

                    switch (returnValue.Value[0])
                    {
                        case ButtonStateType.Inert:
                            states[0] = ButtonStateType.Pressed;
                            InputManager.GetButtonEvent(inputActionID).Started?.Invoke(0);
                            break;

                        case ButtonStateType.Pressed:
                            states[0] = ButtonStateType.Held;
                            InputManager.GetButtonEvent(inputActionID).Performed?.Invoke(0);
                            break;

                        case ButtonStateType.Held:
                            states[0] = ButtonStateType.Released;
                            InputManager.GetButtonEvent(inputActionID).Finished?.Invoke(0);
                            break;
                    }


                    #endregion
                }

                #endregion
            }

            #endregion

            End:

            for (int i = 0; i < returnValuesCount; ++i)
            {
                returnValue.Value[i] = states[i];
            }
        }

        /// <summary>
        /// Retourne l'état de la touche
        /// </summary>
        /// <param name="mouseKey">La touche de la souris à evaluer</param>
        /// <returns>L'état de l'élément de la séquence de l'InputBinding</returns>
        /// <exception cref="Exception">La touche renseignée est invalide</exception>
        public static InputKeySequenceState GetMouseKeyState(MouseKey mouseKey)
        {
            MouseInput mouseInput = InputManager.GetScheme<MouseInput>();

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
        public static InputKeySequenceState GetKeyboardKeyState(Keys keyboardKey)
        {
            KeyboardInput keyboardInput = InputManager.GetScheme<KeyboardInput>();

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
        /// <exception cref="Exception">La touche renseignée est invalide</exception>
        public static InputKeySequenceState GetGamePadKeyState(int playerIndex, Buttons gamePadKey)
        {
            GamePadInput gamePadInput = InputManager.GetScheme<GamePadInput>();

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
        /// <param name="gamePadKey">La touche de la manette à evaluer</param>
        /// <param name="gamePadInput">Les contrôles de la manette</param>
        /// <returns>L'état de l'élément de la séquence de l'InputBinding</returns>
        /// <exception cref="Exception">La touche renseignée est invalide</exception>
        public static InputKeySequenceState GetGamePadKeyState(int playerIndex, Buttons gamePadKey, GamePadInput gamePadInput)
        {
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
        public static InputKeySequenceState GetJoystickKeyState(int playerIndex, JoystickKey joystickKey)
        {
            GamePadInput gamePadInput = InputManager.GetScheme<GamePadInput>();
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

                _ => throw new Exception($"Erreur : MouseKey{joystickKey} invalide"),
            };
        }

        /// <summary>
        /// Retourne l'état de la touche
        /// </summary>
        /// <param name="playerIndex">L'ID de la manette</param>
        /// <param name="joystickKey">La touche de la manette à evaluer</param>
        /// <param name="gamePadInput">Les contrôles de la manette</param>
        /// <returns>L'état de l'élément de la séquence de l'InputBinding</returns>
        /// <exception cref="Exception">La touche renseignée est invalide</exception>
        public static InputKeySequenceState GetJoystickKeyState(int playerIndex, JoystickKey joystickKey, GamePadInput gamePadInput)
        {
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

                _ => throw new Exception($"Erreur : MouseKey{joystickKey} invalide"),
            };
        }

        #endregion

        #endregion
    }
}