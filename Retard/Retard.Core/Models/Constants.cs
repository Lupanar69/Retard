using System;
using Microsoft.Xna.Framework.Input;
using Retard.Core.Models.Assets.Input;
using Retard.Core.Models.DTOs.Input;

namespace Retard.Core.Models
{
    /// <summary>
    /// Les constantes partagées dans tout le projet
    /// </summary>
    public static class Constants
    {
        #region Constants

        #region Content

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

        #region Input

        /// <summary>
        /// Configuration par défaut pour les entrées du joueur
        /// </summary>
        public static readonly InputConfigDTO DEFAULT_INPUT_CONFIG = new InputConfigDTO
            (
                new InputContextDTO
                    (
                        "Camera",
                         new InputActionDTO
                            (
                                "Move",
                                InputActionReturnValueType.Axis2D,
                                InputActionTriggerType.Performed,
                                new InputBindingDTO(MouseKey.Mouse0),
                                new InputBindingDTO(Keys.Left, Keys.Right, Keys.Up, Keys.Down),
                                new InputBindingDTO(Keys.Q, Keys.D, Keys.Z, Keys.S),
                                new InputBindingDTO(Buttons.LeftStick, InputBindingAxisType.Both, 0.24f)
                            )
                    )
#if TESTS
                , new InputContextDTO
                    (
                        "Test",
                         new InputActionDTO
                            (
                                "CreateSprites",
                                InputActionReturnValueType.ButtonState,
                                InputActionTriggerType.Started,
                                new InputBindingDTO(Keys.Space)
                            )
                    )
#endif
            );

        #endregion

        #region Rendering

        /// <summary>
        /// La taille d'un sprite en pixels
        /// </summary>
        internal static readonly int SPRITE_SIZE_PIXELS = 32;

        #endregion

        #endregion
    }
}