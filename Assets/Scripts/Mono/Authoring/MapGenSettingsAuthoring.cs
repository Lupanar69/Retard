using Assets.Scripts.Mono.Models.SerializedData.MapGeneration;
using Retard.ECS;
using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.Mono.Authoring
{
    /// <summary>
    /// Convertit les paramètres de génération d'une carte en entité
    /// </summary>
    public class MapGenSettingsAuthoring : MonoBehaviour
    {
        #region Variables Unity

        /// <summary>
        /// Les paramètres de génération d'une carte
        /// </summary>
        [SerializeField]
        private MapGenSettingsSO _settings;

        /// <summary>
        /// La liste des algorithmes de génération d'une carte
        /// </summary>
        [SerializeField]
        private MapGenAlgorithmsListSO _algorithmsList;

        #endregion

        #region Baker

        /// <summary>
        /// Convertit les paramètres de génération d'une carte en entité
        /// </summary>
        private class MapGenerationSettingsBaker : Baker<MapGenSettingsAuthoring>
        {
            #region Fonctions publiques

            /// <summary>
            /// Convertit les paramètres de génération d'une carte en entité
            /// </summary>
            public override void Bake(MapGenSettingsAuthoring authoring)
            {
                this.DependsOn(authoring._settings);

                if (authoring._settings == null)
                    return;

                Entity e = this.GetEntity(TransformUsageFlags.None);
                this.AddComponent(e, new MapGenerationSettingsMinMaxSizeCD { Value = authoring._settings.MinMaxMapSize });
                var algorithms = this.AddBuffer<MapGenerationSettingsAlgorithmIDBE>(e);

                for (int i = 0; i < authoring._settings.AlgorithmsNames.Length; i++)
                {
                    int indexOfAlgo = authoring._algorithmsList.Values.IndexOf(authoring._settings.AlgorithmsNames[i]);
                    algorithms.Add(new MapGenerationSettingsAlgorithmIDBE { Value = indexOfAlgo });
                }
            }

            #endregion
        }

        #endregion
    }
}