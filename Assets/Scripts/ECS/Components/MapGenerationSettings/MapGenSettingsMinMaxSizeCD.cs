using Unity.Entities;
using Unity.Mathematics;

namespace Retard.ECS
{
    /// <summary>
    /// L'intervalle de taille possible pour une carte
    /// </summary>
    public struct MapGenSettingsMinMaxSizeCD : IComponentData
    {
        #region Variables d'instance

        /// <summary>
        /// L'intervalle de taille possible pour une carte
        /// </summary>
        public int2 Value;

        #endregion
    }
}