using Arch.Core;
using Retard.Cameras.Entities;
using Retard.Core.Models.Arch;

namespace Retard.Cameras.Systems
{
    /// <summary>
    /// Màj les caméras marquées comme modifiées
    /// </summary>
    public readonly struct CameraDirtySystem : ISystem
    {
        #region Méthodes publiques

        /// <inheritdoc/>
        public void Update(World w)
        {
            Queries.ComputeViewMatricesQuery(w, w);
        }

        #endregion
    }
}
