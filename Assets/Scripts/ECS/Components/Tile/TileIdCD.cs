using Unity.Entities;

namespace Retard.ECS
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
        public int Value;

        #endregion
    }
}