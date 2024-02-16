using Retard.Core.Models.ValueTypes;

namespace Retard.Core.Models.Generation
{
    /// <summary>
    /// Contient les infos sur la carte générée
    /// </summary>
    internal class MapGenerationData
    {
        #region Propriétés

        /// <summary>
        /// Les IDs de toutes les cases à instancier par cellule
        /// </summary>
        public int[] TilesIDs { get; set; }

        /// <summary>
        /// Les dimensions de chaque salle à instancier
        /// </summary>
        public int2[] RoomPoses { get; set; }

        /// <summary>
        /// Les dimensions de chaque salle à instancier
        /// </summary>
        public int2[] RoomSizes { get; set; }

        #endregion
    }
}
