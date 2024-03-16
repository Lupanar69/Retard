using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.ECS.Components
{
    /// <summary>
    /// Les coordonnées de la pile de cases sur la carte
    /// </summary>
    public struct TileStackPositionCD : IComponentData
    {
        #region Variables d'instance

        /// <summary>
        /// Les coordonnées de la pile de cases sur la carte
        /// </summary>
        public int2 Value;

        #endregion
    }
}