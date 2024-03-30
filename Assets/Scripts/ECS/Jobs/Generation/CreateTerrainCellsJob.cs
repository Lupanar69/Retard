using Assets.Scripts.ECS.Entities;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;

/// <summary>
/// Crée les cellules des cases de terrain
/// </summary>
[BurstCompile]
public partial struct CreateTerrainCellsJob : IJobFor
{
    #region Variables d'instance

    /// <summary>
    /// Pour créer les entités en parallèle
    /// </summary>
    public EntityCommandBuffer.ParallelWriter Ecb;

    /// <summary>
    /// La position de la cellule sur la carte
    /// </summary>
    public int2 Size;

    /// <summary>
    /// L'archétype des entités des cases
    /// </summary>
    public EntityArchetype TerrainCellArchetype;

    #endregion

    #region Fonctions publiques

    /// <summary>
    /// Crée les piles de cases pour chaque cellule de la carte
    /// </summary>
    /// <param name="index">La position actuelle dans la liste</param>
    [BurstCompile]
    public void Execute(int index)
    {
        EntityFactory.CreateTileStackEntity(index, ref Ecb, in this.Size, this.TerrainCellArchetype, out _);
    }

    #endregion
}