using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Retard.Core.Models.Assets.Input;
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

        #region App

        /// <summary>
        /// Le framerate par défaut de l'appli si active
        /// </summary>
        public static readonly int DEFAULT_FOCUSED_FRAMERATE = 60;

        /// <summary>
        /// Le framerate par défaut de l'appli si active
        /// </summary>
        public static readonly int DEFAULT_UNFOCUSED_FRAMERATE = 30;

        #endregion

        #region Paths

        /// <summary>
        /// Le chemin d'accès au dossier contenant l'exécutable du jeu
        /// </summary>
        public static readonly string GAME_DIR_PATH = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// Le chemin d'accès au fichier de configuration des inputs par défaut
        /// </summary>
        public const string DEFAULT_INPUT_CONFIG_PATH = "input/default.json";

        /// <summary>
        /// Le chemin d'accès au fichier de configuration des inputs définis par le joueur
        /// </summary>
        public const string CUSTOM_INPUT_CONFIG_PATH = "input/input.json";

        /// <summary>
        /// Le chemin d'accès au fichier de configuration 
        /// des paramètres de l'app par défaut
        /// </summary>
        public const string DEFAULT_APP_SETTINGS_CONFIG_PATH = "settings/default.json";

        /// <summary>
        /// Le chemin d'accès au fichier de configuration 
        /// des paramètres de l'app définis par le joueur
        /// </summary>
        public const string CUSTOM_APP_SETTINGS_CONFIG_PATH = "settings/settings.json";

        /// <summary>
        /// Le chemin d'accès aux textures du projet
        /// </summary>
        public const string TEXTURES_DIR_PATH = "Resources/Textures/";

#if TESTS

        /// <summary>
        /// Le chemin d'accès aux textures de débogage du projet
        /// </summary>
        public const string TEXTURES_DIR_PATH_DEBUG = "Resources/Textures/Test/";

#endif

        #endregion

        #region Config

        /// <summary>
        /// <see langword="true"/> si on veut réécrire les fichiers de config
        /// pour recommencer de zéro
        /// </summary>
        public const bool OVERRIDE_DEFAULT_AND_CUSTOM_FILES = true;

        /// <summary>
        /// Configuration par défaut des paramètres de l'application
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
                    new InputBindingDTO(Keys.Left, Keys.Right, Keys.Up, Keys.Down),
                    new InputBindingDTO(Keys.Q, Keys.D, Keys.Z, Keys.S),
                    new InputBindingDTO(Buttons.DPadLeft, Buttons.DPadRight, Buttons.DPadUp, Buttons.DPadDown),
                    new InputBindingDTO(JoystickKey.LeftWest, JoystickKey.LeftEast, JoystickKey.LeftNorth, JoystickKey.LeftSouth),
                    new InputBindingDTO(JoystickType.Right, JoystickAxis.Both, 0.24f)
                ),
                new InputActionDTO
                (
                    "Camera/LeftMousePressed",
                    InputActionReturnValueType.ButtonState,
                    new InputBindingDTO(new InputKeySequenceElement(MouseKey.Mouse0, InputKeySequenceState.Held))
                ),
                new InputActionDTO
                (
                    "Camera/Reset",
                    InputActionReturnValueType.ButtonState,
                    new InputBindingDTO(
                        new InputKeySequenceElement(Keys.LeftControl, InputKeySequenceState.Held),
                        new InputKeySequenceElement(MouseKey.Mouse1, InputKeySequenceState.Pressed)
                        ),
                    new InputBindingDTO(
                        new InputKeySequenceElement(Buttons.RightShoulder, InputKeySequenceState.Held),
                        new InputKeySequenceElement(Buttons.A, InputKeySequenceState.Pressed)
                        )
                )
#if TESTS

            , new InputActionDTO
                  (
                      "Test/CreateSprites",
                      InputActionReturnValueType.ButtonState,
                      new InputBindingDTO(new InputKeySequenceElement(Keys.Space, InputKeySequenceState.Pressed)),
                      new InputBindingDTO(new InputKeySequenceElement(Buttons.Start, InputKeySequenceState.Pressed))
                  )
            , new InputActionDTO
                  (
                      "Test/DisableBlockInput",
                      InputActionReturnValueType.ButtonState,
                      new InputBindingDTO(new InputKeySequenceElement(Keys.Space, InputKeySequenceState.Pressed)),
                      new InputBindingDTO(new InputKeySequenceElement(Buttons.Start, InputKeySequenceState.Pressed))
                  )
#endif
            );

        #endregion

        #region Draw

        /// <summary>
        /// La taille d'un sprite en pixels
        /// </summary>
        public static readonly int SPRITE_SIZE_PIXELS = 32;

        #endregion

        #endregion
    }
}