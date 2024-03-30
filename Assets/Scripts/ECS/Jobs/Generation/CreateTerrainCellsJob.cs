using Assets.Scripts.ECS.Entities;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;

/// <summary>
/// Cr�e les cellules des cases de terrain
/// </summary>
[BurstCompile]
public partial struct CreateTerrainCellsJob : IJobFor
{
    #region Variables d'instance

    /// <summary>
    /// Pour cr�er les entit�s en parall�le
    /// </summary>
    public EntityCommandBuffer.ParallelWriter Ecb;

    /// <summary>
    /// La position de la cellule sur la carte
    /// </summary>
    public int2 Size;

    /// <summary>
    /// L'arch�type des entit�s des cases
    /// </summary>
    public EntityArchetype TerrainCellArchetype;

    #endregion

    #region Fonctions publiques

    /// <summary>
    /// Cr�e les piles de cases pour chaque cellule de la carte
    /// </summary>
    /// <param name="index">La position actuelle dans la liste</param>
    [BurstCompile]
    public void Execute(int index)
    {
        EntityFactory.CreateTileStackEntity(index, ref Ecb, in this.Size, this.TerrainCellArchetype, out _);
    }

    #endregion
}