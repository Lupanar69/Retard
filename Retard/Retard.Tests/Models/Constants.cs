using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Retard.Engine.Models;
using Retard.Engine.Models.App;
using Retard.Engine.Models.Assets.Input;
using Retard.Engine.Models.DTOs.App;
using Retard.Engine.Models.DTOs.Input;

namespace Retard.Core.Models
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
        public static readonly AppSettingsDTO DEFAULT_APP_SETTINGS = new
            (
                new WindowSettings
                {
                    WindowSize = new Point(800, 600),
                    FullScreen = false,
                    MouseVisible = true,
                    AllowUserResizing = true
                }
            );

        /// <summary>
        /// Configuration par défaut pour les entrées du joueur
        /// </summary>
        public static readonly InputConfigDTO DEFAULT_INPUT_CONFIG = new
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
                )
#if TESTS
                ,
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
#endif
            );

        #endregion

        #region Draw

        /// <summary>
        /// La résolution des sprites du jeu en pixels
        /// </summary>
        public const int SPRITE_SIZE_PIXELS = 32;

        #endregion

        #region Paths

#if TESTS

        /// <summary>
        /// Le chemin d'accès aux textures de débogage du projet
        /// </summary>
        public const string TEXTURES_DIR_PATH_DEBUG = "Resources/Textures/Test/";

#endif

        #endregion

        #endregion
    }
}