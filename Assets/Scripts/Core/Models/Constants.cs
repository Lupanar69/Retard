using Unity.Collections;

namespace Assets.Scripts.Core.Models
{
    /// <summary>
    /// Les constantes partagées dans tout le projet
    /// </summary>
    public static class Constants
    {
        #region Constants

        #region Generation

        // Constants utilisées pour la sélection des algorithmes
        // et types de cases à générer

        public static readonly FixedString32Bytes ONEROOM_ALG_KEY = "OneRoom";

        // Constants utilisées pour les types de cases à générer

        public static readonly FixedString32Bytes WALL_TILE_KEY = "Wall";

        public static readonly FixedString32Bytes FLOOR_TILE_KEY = "Floor";

        public static readonly FixedString32Bytes STAIRSUP_TILE_KEY = "StairsUp";

        public static readonly FixedString32Bytes STAIRSDOWN_TILE_KEY = "StairsDown";

        #endregion

        #region Rendering

        /// <summary>
        /// La taille d'un sprite en pixels
        /// </summary>
        public static readonly int SPRITE_SIZE_PIXELS = 32;

        #endregion

        #endregion
    }
}