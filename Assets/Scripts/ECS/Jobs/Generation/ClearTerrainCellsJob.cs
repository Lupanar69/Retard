using Assets.Scripts.ECS.Components;
using Unity.Burst;
using Unity.Entities;

/// <summary>
/// Vide les piles de cases
/// </summary>
[BurstCompile]
public partial struct ClearTerrainCellsJob : IJobEntity
{
    #region Fonctions publiques

    /// <summary>
    /// Vide les piles de cases
    /// </summary>
    /// <param name="e">L'entité récupérée par la query</param>
    /// <param name="tile">La case de la cellule</param>
    [BurstCompile]
    public void Execute(Entity _, TileEntityInCellCD tile)
    {
        tile = new TileEntityInCellCD();
    }

    #endregion
}