using Assets.Scripts.ECS.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;

/// <summary>
/// Crée les cases pour chaque cellule de la carte renseignée
/// </summary>
[BurstCompile]
public partial struct CreateTilesJob : IJobFor
{
    #region Variables d'instance

    /// <summary>
    /// Pour créer les entités en parallèle
    /// </summary>
    public EntityCommandBuffer.ParallelWriter Ecb;

    /// <summary>
    /// L'archétype des entités des cases
    /// </summary>
    public EntityArchetype TileArchetype;

    /// <summary>
    /// Les IDs des cases ainsi que leur position
    /// </summary>
    [ReadOnly]
    public NativeArray<int3>.ReadOnly TilesIDsRO;

    #endregion

    #region Fonctions publiques

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index">La position actuelle dans la liste</param>
    [BurstCompile]
    public void Execute(int index)
    {
        int3 data = this.TilesIDsRO[index];
        Entity tileE = this.Ecb.CreateEntity(index, this.TileArchetype);
        this.Ecb.SetName(index, tileE, "Tile Entity");
        this.Ecb.SetComponent(index, tileE, new TilePositionCD { Value = data.xy });
        this.Ecb.SetComponent(index, tileE, new TileIdCD { Value = data.z });
    }

    #endregion
}