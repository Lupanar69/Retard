using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Retard.App.Models;
using Retard.App.Models.DTOs;
using Retard.Core.Models.DTOs;
using Retard.Input.Models;
using Retard.Input.Models.Assets;
using Retard.Input.Models.DTOs;

namespace Retard.Tests.Models
{
    /// <summary>
    /// Les constantes partagées dans tout le projet
    /// </summary>
    public static class Constants
    {
        #region Constants

        #region Config

        /// <summary>
        /// <see langword="true"/> si on veut réécrire les fichiers de config
        /// pour recommencer de zéro
        /// </summary>
        public const bool OVERRIDE_DEFAULT_AND_CUSTOM_FILES = true;

        /// <summary>
        /// Configuration par défaut des paramètres du jeu
        /// </summary>
        public static readonly DTOFilePath DEFAULT_APP_SETTINGS = new()
        {
            DefaultFilePath = "settings/default.json",
            CustomFilePath = "settings/settings.json",
            DTO = new AppSettingsDTO
            (
                new WindowSettings
                {
                    WindowSize = new Point(800, 600),
                    FullScreen = false,
                    MouseVisible = true,
                    AllowUserResizing = true
                }
            )
        };

        /// <summary>
        /// Configuration par défaut pour les entrées du joueur
        /// </summary>
        public static readonly DTOFilePath DEFAULT_INPUT_CONFIG = new()
        {
            DefaultFilePath = "input/default.json",
            CustomFilePath = "input/input.json",
            DTO = new InputConfigDTO
                (
                    new InputActionDTO
                    (
                        "Camera/Move",
                        InputActionReturnValueType.Vector2D,
                        new InputBindingDTO(
                            new InputKeyVector2DElement(Keys.Right),
                            new InputKeyVector2DElement(Keys.Left),
                            new InputKeyVector2DElement(Keys.Up),
                            new InputKeyVector2DElement(Keys.Down)
                            ),
                        new InputBindingDTO(
                            new InputKeyVector2DElement(Keys.D),
                            new InputKeyVector2DElement(Keys.Q),
                            new InputKeyVector2DElement(Keys.Z),
                            new InputKeyVector2DElement(Keys.S)
                            ),
                        new InputBindingDTO(
                            new InputKeyVector2DElement(Buttons.DPadRight),
                            new InputKeyVector2DElement(Buttons.DPadLeft),
                            new InputKeyVector2DElement(Buttons.DPadUp),
                            new InputKeyVector2DElement(Buttons.DPadDown)
                            ),

                        new InputBindingDTO(
                            new InputKeyVector2DElement(JoystickKey.LeftEast),
                            new InputKeyVector2DElement(JoystickKey.LeftWest),
                            new InputKeyVector2DElement(JoystickKey.LeftNorth),
                            new InputKeyVector2DElement(JoystickKey.LeftSouth)
                            ),
                        new InputBindingDTO(new InputBindingJoystick(JoystickType.Right, 0.24f))
                    ),
                    new InputActionDTO
                    (
                        "Camera/Reset",
                        InputActionReturnValueType.ButtonState,
                        new InputBindingDTO(new InputKeySequenceElement(Keys.R, InputKeySequenceState.Released))
                    ),
                    new InputActionDTO
                    (
                        "Camera/LeftMouseHeld",
                        InputActionReturnValueType.ButtonState,
                        new InputBindingDTO(new InputKeySequenceElement(MouseKey.Mouse0, InputKeySequenceState.Held))
                    ),
                    new InputActionDTO
                    (
                        "Exit",
                        InputActionReturnValueType.ButtonState,
                        new InputBindingDTO(new InputKeySequenceElement(Keys.Escape, InputKeySequenceState.Pressed)),
                        new InputBindingDTO(new InputKeySequenceElement(Buttons.Back, InputKeySequenceState.Pressed))
                    ),
                    new InputActionDTO
                    (
                        "Test/Space",
                        InputActionReturnValueType.ButtonState,
                        new InputBindingDTO(new InputKeySequenceElement(Keys.Space, InputKeySequenceState.Pressed))
                    ),
                    new InputActionDTO
                    (
                        "Test/Enter",
                        InputActionReturnValueType.ButtonState,
                        new InputBindingDTO(new InputKeySequenceElement(Keys.Enter, InputKeySequenceState.Pressed))
                    ),
                    new InputActionDTO
                    (
                        "Test/Numpad1",
                        InputActionReturnValueType.ButtonState,
                        new InputBindingDTO(new InputKeySequenceElement(Keys.NumPad1, InputKeySequenceState.Pressed))
                    ),
                    new InputActionDTO
                    (
                        "Test/Numpad2",
                        InputActionReturnValueType.ButtonState,
                        new InputBindingDTO(new InputKeySequenceElement(Keys.NumPad2, InputKeySequenceState.Pressed))
                    ),
                    new InputActionDTO
                    (
                        "Test/Numpad3",
                        InputActionReturnValueType.ButtonState,
                        new InputBindingDTO(new InputKeySequenceElement(Keys.NumPad3, InputKeySequenceState.Pressed))
                    ),
                    new InputActionDTO
                    (
                        "Test/Numpad7",
                        InputActionReturnValueType.ButtonState,
                        new InputBindingDTO(new InputKeySequenceElement(Keys.NumPad7, InputKeySequenceState.Pressed))
                    ),
                    new InputActionDTO
                    (
                        "Test/Numpad8",
                        InputActionReturnValueType.ButtonState,
                        new InputBindingDTO(new InputKeySequenceElement(Keys.NumPad8, InputKeySequenceState.Pressed))
                    ),
                    new InputActionDTO
                    (
                        "Test/Numpad9",
                        InputActionReturnValueType.ButtonState,
                        new InputBindingDTO(new InputKeySequenceElement(Keys.NumPad9, InputKeySequenceState.Pressed))
                    )
            )
        };

        #endregion

        #region Draw

        /// <summary>
        /// La résolution des sprites du jeu en pixels
        /// </summary>
        public const int SPRITE_SIZE_PIXELS = 32;

        #endregion

        #region Paths

        /// <summary>
        /// Le chemin d'accès au dossier contenant l'exécutable du jeu
        /// </summary>
        public static readonly string GAME_DIR_PATH = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// Le chemin d'accès aux textures du projet
        /// </summary>
        public const string TEXTURES_DIR_PATH = "Resources/Textures";

        /// <summary>
        /// Le chemin d'accès aux textures de débogage du projet
        /// </summary>
        public const string TEXTURES_DIR_PATH_DEBUG = "Resources/Textures/Test";

        #endregion

        #endregion
    }
}