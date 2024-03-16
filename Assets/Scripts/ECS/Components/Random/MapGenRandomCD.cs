using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.ECS.Components
{
    /// <summary>
    /// Composant d'aléatoire pour la génération de la carte
    /// </summary>
    public struct MapGenRandomCD : IComponentData
    {
        #region Variables d'instance

        /// <summary>
        /// Composant d'aléatoire
        /// </summary>
        public Random Value;

        #endregion
    }
}