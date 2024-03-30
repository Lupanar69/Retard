using Unity.Entities;

namespace Assets.Scripts.ECS.Components
{
    /// <summary>
    /// Représente la case que contient la cellule
    /// </summary>
    public struct TileEntityInCellCD : IComponentData
    {
        #region Variables d'instance

        /// <summary>
        /// Représente une case dans la pile de cases aux coordonnées
        /// correspondantes sur la carte.
        /// </summary>
        public Entity Value;

        #endregion
    }
}