using Unity.Collections;
using Unity.Entities;

namespace Assets.Scripts.ECS.Components
{
    /// <summary>
    /// L'id du type d'objet que représente cette case
    /// (sol, mur, etc...)
    /// </summary>
    public struct TileIdCD : IComponentData
    {
        #region Variables d'instance

        /// <summary>
        /// L'id du type d'objet que représente cette case
        /// (sol, mur, etc...)
        /// </summary>
        public FixedString32Bytes Value;

        #endregion
    }
}