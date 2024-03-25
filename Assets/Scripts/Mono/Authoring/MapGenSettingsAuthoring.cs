using Assets.Scripts.ECS.Components;
using Assets.Scripts.Mono.Models.SerializedData.MapGeneration;
using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.Mono.Authoring
{
    /// <summary>
    /// Convertit les paramètres de génération d'une carte en entité
    /// </summary>
    public sealed class MapGenSettingsAuthoring : MonoBehaviour
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
        private sealed class MapGenSettingsBaker : Baker<MapGenSettingsAuthoring>
        {
            #region Fonctions publiques

            /// <summary>
            /// Convertit les paramètres de génération d'une carte en entité
            /// </summary>
            public override void Bake(MapGenSettingsAuthoring authoring)
            {
                this.DependsOn(authoring._settings);
                this.DependsOn(authoring._algorithmsList);

                if (authoring._settings == null)
                {
                    Debug.LogError("Erreur : La variable \"Settings\" de MapGenSettingsAuthoring n'est pas assignée.");
                    return;
                }

                if (authoring._algorithmsList == null)
                {
                    Debug.LogError("Erreur : La variable \"Algorithms List\" de MapGenSettingsAuthoring n'est pas assignée.");
                    return;
                }

                if (authoring._settings.Algorithms.Length == 0)
                {
                    Debug.LogError("Erreur : Aucun algorithme renseigné dans les paramètres.");
                    return;
                }

                if (authoring._algorithmsList.Values.Count == 0)
                {
                    Debug.LogError("Erreur : Aucun algorithme renseigné dans le dictionnaire.");
                    return;
                }

                // Crée l'entité

                Entity e = this.GetEntity(TransformUsageFlags.None);
                this.AddComponent(e, new MapGenSettingsMinMaxSizeCD { Value = authoring._settings.MinMaxMapSize });
                DynamicBuffer<MapGenSettingsAlgorithmIDBE> algorithms = this.AddBuffer<MapGenSettingsAlgorithmIDBE>(e);

                for (int i = 0; i < authoring._settings.Algorithms.Length; i++)
                {
                    // Ajoute les IDs des algorithmes à utiliser

                    MapGenAlgorithmSO alg = authoring._settings.Algorithms[i];
                    int indexOfAlgo = authoring._algorithmsList.Values.IndexOf(alg);
                    algorithms.Add(new MapGenSettingsAlgorithmIDBE { Value = indexOfAlgo });

                    // Pour chaque algorithme, créer l'entité de ses paramètres

                    this.AddAlgorithmEntities(alg);
                }
            }

            /// <summary>
            /// Crée l'entité possédant les paramètres d'entrée
            /// spécifiques à l'algorithme renseigné
            /// </summary>
            /// <param name="alg">L'algorithme à convertir</param>
            private void AddAlgorithmEntities(MapGenAlgorithmSO alg)
            {
                Entity algE = this.CreateAdditionalEntity(TransformUsageFlags.None, false, alg.Tag);
                this.AddComponent<MapGenAlgorithmTag>(algE);

                switch (alg)
                {
                    case OneRoomMapGenAlgorithmSO:
                        this.AddComponent(algE, new OneRoomMapGenAlgorithmCD());
                        break;

                    case NullMapGenAlgorithmSO:
                    default:
                        this.AddComponent(algE, new NullMapGenAlgorithmCD());
                        break;
                }
            }

            #endregion
        }

        #endregion
    }
}