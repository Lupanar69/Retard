using Unity.Entities;
using Unity.Mathematics;

namespace Retard.ECS
{
    /// <summary>
    /// Les coordonnées de chaque case de la carte
    /// </summary>
    public struct MapPositionBE : IBufferElementData
    {
        #region Variables d'instance

        /// <summary>
        /// Les coordonnées de chaque case de la carte
        /// </summary>
        public int2 Value;

        #endregion
    }
}