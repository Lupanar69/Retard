using System;
using Assets.Scripts.ECS.Components;
using Assets.Scripts.Mono.Models.SerializedData.MapGeneration;
using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.Mono.Authoring
{
    /// <summary>
    /// Convertit les param�tres de g�n�ration d'une carte en entit�
    /// </summary>
    public sealed class MapGenSettingsAuthoring : MonoBehaviour
    {
        #region Variables Unity

        /// <summary>
        /// Les param�tres de g�n�ration d'une carte
        /// </summary>
        [SerializeField]
        private MapGenSettingsSO _settings;

        /// <summary>
        /// La liste des algorithmes de g�n�ration d'une carte
        /// </summary>
        [SerializeField]
        private MapGenAlgorithmsListSO _algorithmsList;

        #endregion

        #region Baker

        /// <summary>
        /// Convertit les param�tres de g�n�ration d'une carte en entit�
        /// </summary>
        private sealed class MapGenSettingsBaker : Baker<MapGenSettingsAuthoring>
        {
            #region Fonctions publiques

            /// <summary>
            /// Convertit les param�tres de g�n�ration d'une carte en entit�
            /// </summary>
            public override void Bake(MapGenSettingsAuthoring authoring)
            {
                this.DependsOn(authoring._settings);
                this.DependsOn(authoring._algorithmsList);

                if (authoring._settings == null)
                {
                    Debug.LogError("Erreur : La variable \"Settings\" de MapGenSettingsAuthoring n'est pas assign�e.");
                    return;
                }

                if (authoring._algorithmsList == null)
                {
                    Debug.LogError("Erreur : La variable \"Algorithms List\" de MapGenSettingsAuthoring n'est pas assign�e.");
                    return;
                }

                if (authoring._settings.Algorithms.Length == 0)
                {
                    Debug.LogError("Erreur : Aucun algorithme renseign� dans les param�tres.");
                    return;
                }

                if (authoring._algorithmsList.Values.Count == 0)
                {
                    Debug.LogError("Erreur : Aucun algorithme renseign� dans le dictionnaire.");
                    return;
                }

                // Cr�e l'entit�

                try
                {


                    Entity e = this.GetEntity(TransformUsageFlags.None);
                    this.AddComponent(e, new MapGenSettingsMinMaxSizeCD { Value = authoring._settings.MinMaxMapSize });
                    DynamicBuffer<MapGenSettingsAlgorithmIDBE> algorithms = this.AddBuffer<MapGenSettingsAlgorithmIDBE>(e);

                    for (int i = 0; i < authoring._settings.Algorithms.Length; i++)
                    {
                        // Ajoute les IDs des algorithmes � utiliser

                        MapGenAlgorithmSO alg = authoring._settings.Algorithms[i];
                        int indexOfAlgo = authoring._algorithmsList.Values.IndexOf(alg);
                        algorithms.Add(new MapGenSettingsAlgorithmIDBE { Value = indexOfAlgo });

                        // Pour chaque algorithme, cr�er l'entit� de ses param�tres

                        this.AddAlgorithmEntities(alg);
                    }
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }

            /// <summary>
            /// Cr�e l'entit� poss�dant les param�tres d'entr�e
            /// sp�cifiques � l'algorithme renseign�
            /// </summary>
            /// <param name="alg">L'algorithme � convertir</param>
            private void AddAlgorithmEntities(MapGenAlgorithmSO alg)
            {
                Entity algE = this.CreateAdditionalEntity(TransformUsageFlags.None, false, alg.Tag);
                this.AddComponent<MapGenAlgorithmTag>(algE);

                switch (alg)
                {
                    case OneRoomMapGenAlgorithmSO:
                        this.AddComponent(algE, new OneRoomMapGenAlgorithmCD());
                        break;

                    default:
                        break;
                }
            }

            #endregion
        }

        #endregion
    }
}