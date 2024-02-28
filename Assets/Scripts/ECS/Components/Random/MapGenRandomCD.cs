using Unity.Entities;
using Unity.Mathematics;

namespace Retard.ECS
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