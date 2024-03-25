using Assets.Scripts.ECS.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.ECS.Jobs.Generation
{
    /// <summary>
    /// Ne génère rien. Sert de valeur nulle pour la valeur default du switch case de GenerationAlgorithms.cs
    /// </summary>
    [BurstCompile]
    public partial struct GenerateNullMapJob : IJobEntity
    {
        #region Variables d'instance

        /// <summary>
        /// Le nombre total de cellules sur la carte
        /// </summary>
        public int Length;

        /// <summary>
        /// Les IDs des cases à retourner
        /// </summary>
        [WriteOnly]
        public NativeArray<int3> TilesIDsWO;

        #endregion

        #region Fonctions publiques

        /// <summary>
        /// Remplit le tableau avec la valeur 0.
        /// Par défaut, cela créera une carte composée entièrement de murs.
        /// </summary>
        /// <param name="e">L'entité de l'algorithme</param>
        /// <param name="settings">Les paramètres de l'algorithme</param>
        [BurstCompile]
        public void Execute(Entity e, NullMapGenAlgorithmCD settings)
        {
            NativeArray<int3> arr = new(this.Length, Allocator.Temp);
            arr.CopyTo(this.TilesIDsWO);
        }

        #endregion
    }
}