using Assets.Scripts.ECS.Jobs.Generation;
using Assets.Scripts.Mono.Models.SerializedData.MapGeneration;
using Unity.Collections;
using UnityEngine;

namespace Assets.Scripts.Mono.ViewModels.Generation
{

    /// <summary>
    /// Màj le dictionnaire d'algorithms du GenerationAlgorithms.cs
    /// pour que la version Burst puisse lier les algos avec leurs bons IDs
    /// </summary>
    public class MapGenDictionaryInitializer : MonoBehaviour
    {
        #region Variables Unity

        /// <summary>
        /// Liste tous les algorithmes de génération de la carte
        /// </summary>
        [SerializeField]
        private MapGenAlgorithmsListSO _algorithmsList;

        #endregion

        #region Fonctions Unity

#if UNITY_EDITOR

        /// <summary>
        /// Quand un changement est effectué dans l'Inspector
        /// </summary>
        private void OnValidate()
        {
            this.UpdateBurstAlgorithmsList(this._algorithmsList);
        }

        /// <summary>
        /// Quand le script est (ré)initialisé
        /// </summary>
        private void Reset()
        {
            this.UpdateBurstAlgorithmsList(this._algorithmsList);
        }

#endif
        /// <summary>
        /// Init
        /// </summary>
        private void Start()
        {
            this.UpdateBurstAlgorithmsList(this._algorithmsList);
        }

        #endregion

        #region Fonctions privées

        /// <summary>
        /// On màj le dictionnaire d'algorithms du GenerationAlgorithms.cs
        /// pour que la version Burst puisse lier les algos avec leurs bons IDs
        /// </summary>
        /// <param name="algorithmsListSO">La liste des algorithmes à convertir</param>
        private void UpdateBurstAlgorithmsList(MapGenAlgorithmsListSO algorithmsListSO)
        {
            GenerationAlgorithms._mapGenAlgorithms.Clear();

            if (this._algorithmsList == null ^ this._algorithmsList.Values.Count == 0)
            {
                return;
            }

            for (int i = 0; i < algorithmsListSO.Values.Count; i++)
            {
                var alg = algorithmsListSO.Values[i];

                if (alg != null)
                {
                    FixedString64Bytes tag = new(algorithmsListSO.Values[i].Tag);
                    GenerationAlgorithms._mapGenAlgorithms.Add(i, tag);
                }
            }
        }

        #endregion
    }
}