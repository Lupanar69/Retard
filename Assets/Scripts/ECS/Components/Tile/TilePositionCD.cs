using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.ECS.Components
{
    /// <summary>
    /// Les coordonnées de la case sur la carte
    /// </summary>
    public struct TilePositionCD : IComponentData
    {
        #region Variables d'instance

        /// <summary>
        /// Les coordonnées de la case sur la carte
        /// </summary>
        public int2 Value;

        #endregion
    }
}