using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.ECS.Components
{
    /// <summary>
    /// Les coordonn�es de la cellule sur la carte
    /// </summary>
    public struct CellPositionCD : IComponentData
    {
        #region Variables d'instance

        /// <summary>
        /// Les coordonn�es de la cellule sur la carte
        /// </summary>
        public int2 Value;

        #endregion
    }
}