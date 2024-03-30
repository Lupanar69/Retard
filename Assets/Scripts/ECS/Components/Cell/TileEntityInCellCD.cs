using Unity.Entities;

namespace Assets.Scripts.ECS.Components
{
    /// <summary>
    /// Repr�sente la case que contient la cellule
    /// </summary>
    public struct TileEntityInCellCD : IComponentData
    {
        #region Variables d'instance

        /// <summary>
        /// Repr�sente une case dans la pile de cases aux coordonn�es
        /// correspondantes sur la carte.
        /// </summary>
        public Entity Value;

        #endregion
    }
}