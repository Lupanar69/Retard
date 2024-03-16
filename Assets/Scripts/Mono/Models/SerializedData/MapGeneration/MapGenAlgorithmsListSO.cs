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

        #region Fonctions publiques

#if UNITY_EDITOR

        /// <summary>
        /// Quand un changement est effectué dans l'Inspector
        /// </summary>
        private void OnValidate()
        {
            // On màj le dictionnaire d'algorithms du GenerationAlgorithms.cs
            // pour que la version Burst puisse lier les algos avec leurs bons IDs

            if (GenerationAlgorithms._mapGenAlgorithms.IsCreated)
            {
                GenerationAlgorithms._mapGenAlgorithms.Clear();

                for (int i = 0; i < this.Values.Count; i++)
                {
                    GenerationAlgorithms._mapGenAlgorithms.Add(i, this.Values[i]);
                }
            }
        }

#endif

        #endregion
    }
}