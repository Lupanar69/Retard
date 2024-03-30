using Unity.Collections;

namespace Assets.Scripts.Core.Models
{
    /// <summary>
    /// Les constantes partag�es dans tout le projet
    /// </summary>
    public static class Constants
    {
        #region Constants

        #region Generation

        // Constants utilis�es pour la s�lection des algorithmes
        // et types de cases � g�n�rer

        public static readonly FixedString32Bytes ONEROOM_ALG_KEY = "OneRoom";

        // Constants utilis�es pour les types de cases � g�n�rer

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