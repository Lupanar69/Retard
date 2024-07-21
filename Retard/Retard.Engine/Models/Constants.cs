using System;

namespace Retard.Engine.Models
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
        public const int DEFAULT_FOCUSED_FRAMERATE = 60;

        /// <summary>
        /// Le framerate par défaut de l'appli si active
        /// </summary>
        public const int DEFAULT_UNFOCUSED_FRAMERATE = 30;

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

        #endregion

        #endregion
    }
}