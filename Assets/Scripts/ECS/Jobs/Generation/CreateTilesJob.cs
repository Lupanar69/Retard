using Assets.Scripts.Core.Models.Generation;
using Assets.Scripts.ECS.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;

/// <summary>
/// Cr�e les cases pour chaque cellule de la carte renseign�e
/// </summary>
[BurstCompile]
public partial struct CreateTilesJob : IJobFor
{
    #region Variables d'instance

    /// <summary>
    /// Pour cr�er les entit�s en parall�le
    /// </summary>
    public EntityCommandBuffer.ParallelWriter Ecb;

    /// <summary>
    /// L'arch�type des entit�s des cases
    /// </summary>
    public EntityArchetype TileArchetype;

    /// <summary>
    /// Les IDs des cases ainsi que leur position
    /// </summary>
    [ReadOnly]
    public NativeArray<TilePosAndID>.ReadOnly TilesIDsRO;

    #endregion

    #region Fonctions publiques

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index">La position actuelle dans la liste</param>
    [BurstCompile]
    public void Execute(int index)
    {
        TilePosAndID data = this.TilesIDsRO[index];
        Entity tileE = this.Ecb.CreateEntity(index, this.TileArchetype);
        this.Ecb.SetName(index, tileE, "Tile Entity");
        this.Ecb.SetComponent(index, tileE, new TilePositionCD { Value = data.Pos });
        this.Ecb.SetComponent(index, tileE, new TileIdCD { Value = data.ID });
    }

    #endregion
}