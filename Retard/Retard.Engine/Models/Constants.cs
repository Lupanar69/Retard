using System;
using FixedStrings;
using Retard.Core.Models.ValueTypes;

namespace Retard.Engine.Models
{
    /// <summary>
    /// Les constantes partagées dans tout le projet
    /// </summary>
    public static class Constants
    {
        #region Constants

        #region Paths

        /// <summary>
        /// Le chemin d'accès au dossier contenant l'exécutable du jeu
        /// </summary>
        public static readonly NativeString GAME_DIR_PATH = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// Le chemin d'accès aux textures du projet
        /// </summary>
        public static readonly FixedString32 SHADERS_DIR_PATH = "Resources/Shaders";

        /// <summary>
        /// Le chemin d'accès aux textures de débogage du projet
        /// </summary>
        public static readonly FixedString32 SHADERS_DIR_PATH_DEBUG = "Resources/Shaders/Test";

        #endregion

        #endregion
    }
}
