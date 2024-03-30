using Assets.Scripts.ECS.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

/// <summary>
/// Ajoute les cases aux piles
/// </summary>
[BurstCompile]
public partial struct AddTerrainTilesToCellsJob : IJobEntity
{
    #region Variables d'instance

    /// <summary>
    /// La taille de la carte sur l'axe X
    /// </summary>
    public int SizeX;

    /// <summary>
    /// Pour manipuler les entités
    /// </summary>
    public EntityCommandBuffer.ParallelWriter Ecb;

    /// <summary>
    /// Les entités des piles de cases
    /// </summary>
    [ReadOnly]
    public NativeArray<Entity>.ReadOnly TileStacksEs;

    #endregion

    #region Fonctions publiques

    /// <summary>
    /// Ajoute les cases aux piles
    /// </summary>
    /// <param name="tileE">L'entité récupérée par la query</param>
    /// <param name="pos">La position de la case sur la carte</param>
    /// <param name="sortKey">La position de l'entité dans la liste</param>
    [BurstCompile]
    public void Execute(Entity tileE, TilePositionCD pos, [EntityIndexInQuery] int sortKey)
    {
        int stackIndex = pos.Value.y * this.SizeX + pos.Value.x;
        Entity tileStackE = this.TileStacksEs[stackIndex];
        this.Ecb.SetComponent(sortKey, tileStackE, new TileEntityInCellCD { Value = tileE });
    }

    #endregion
}