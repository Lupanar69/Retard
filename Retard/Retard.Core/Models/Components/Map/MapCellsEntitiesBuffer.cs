using MonoGame.Extended.Entities;

namespace Retard.Core.Models.Components.Map
{
    /// <summary>
    /// La liste des entités représentant les cellules de la carte
    /// </summary>
    internal sealed class MapCellsEntitiesBuffer
    {
        #region Variables d'instance

        /// <summary>
        /// La liste des entités représentant les cellules de la carte
        /// </summary>
        internal Entity[] Value;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="cellEs">Les cellules de la carte</param>
        internal MapCellsEntitiesBuffer(Entity[] cellEs)
        {
            this.Value = cellEs;
        }

        #endregion
    }
}
