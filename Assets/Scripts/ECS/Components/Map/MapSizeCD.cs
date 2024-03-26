using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.ECS.Components
{
    /// <summary>
    /// La taille de la carte
    /// </summary>
	public struct MapSizeCD : IComponentData
    {
        #region Variables d'instance

        /// <summary>
        /// La taille de la carte
        /// </summary>
        public int2 Value;

        #endregion
    }
}