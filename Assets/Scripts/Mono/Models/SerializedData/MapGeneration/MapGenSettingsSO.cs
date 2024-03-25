using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.Mono.Models.SerializedData.MapGeneration
{
    /// <summary>
    /// Paramètres de génération d'une carte
    /// </summary>
    [CreateAssetMenu(fileName = "Map Generation Settings", menuName = "Retard/Generation/Map Generation Settings", order = 0)]
    public sealed class MapGenSettingsSO : ScriptableObject
    {
        #region Propriétés

        /// <summary>
        /// L'intervalle de taille possible pour une carte
        /// </summary>
        public int2 MinMaxMapSize { get => this._minMaxMapSize; }

        /// <summary>
        /// La liste des algorithmes de génération possibles pour une carte donnée
        /// (nous permet de créer des niveaux avec différents algorithmes, comme par ex. pour différents biomes)
        /// </summary>
        public MapGenAlgorithmSO[] Algorithms { get => this._algorithms; }

        #endregion

        #region Variables Unity

        /// <summary>
        /// L'intervalle de taille possible pour une carte
        /// </summary>
        [SerializeField]
        private int2 _minMaxMapSize;

        /// <summary>
        /// La liste des algorithmes de génération possibles pour une carte donnée
        /// (nous permet de créer des niveaux avec différents algorithmes, comme par ex. pour différents biomes)
        /// </summary>
        [SerializeField]
        private MapGenAlgorithmSO[] _algorithms;

        #endregion
    }
}