using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.ECS.Components
{
    /// <summary>
    /// Les dimensions de la carte
    /// </summary>
	public struct MapDimensionsCD : IComponentData
    {
        #region Variables d'instance

        /// <summary>
        /// Les dimensions de la carte
        /// </summary>
        public int2 Value;

        #endregion
    }
}