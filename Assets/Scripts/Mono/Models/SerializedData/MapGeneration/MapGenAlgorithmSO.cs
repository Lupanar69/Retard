using UnityEngine;

namespace Assets.Scripts.Mono.Models.SerializedData.MapGeneration
{
    /// <summary>
    /// Chaque classe file représente les paramètres d'entrée 
    /// d'un différent algorithme de génération de carte.
    /// L'implémentation des algos en eux-mêmes est faite par des jobs.
    /// </summary>
    public abstract class MapGenAlgorithmSO : ScriptableObject
    {
        #region Propriétés

        /// <summary>
        /// L'ID de l'algorithme
        /// </summary>
        public string Tag { get => _tag; }

        #endregion

        #region Variables d'instance

        /// <summary>
        /// L'ID de l'algorithme
        /// </summary>
        [SerializeField]
        private string _tag;

        #endregion
    }
}