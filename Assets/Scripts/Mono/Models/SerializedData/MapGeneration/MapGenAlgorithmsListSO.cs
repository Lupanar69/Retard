using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Mono.Models.SerializedData.MapGeneration
{
    /// <summary>
    /// Liste tous les algorithmes de génération de la carte
    /// </summary>
    [CreateAssetMenu(fileName = "New Map Generation Algorithm List", menuName = "Retard/Generation/Map Generation Algorithm List", order = 2)]
    public class MapGenAlgorithmsListSO : ScriptableObject
    {
        #region Propriétés

        /// <summary>
        /// Liste tous les algorithmes de génération de la carte
        /// </summary>
        public List<string> Values { get => this._algorithmsTypes; }

        #endregion

        #region Variables Unity

        /// <summary>
        /// Liste tous les algorithmes de génération de la carte
        /// </summary>
        [SerializeField]
        private List<string> _algorithmsTypes;

        #endregion
    }
}