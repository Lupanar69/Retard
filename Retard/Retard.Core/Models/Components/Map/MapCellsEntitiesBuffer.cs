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
        /// <param name="length">Le nombre de cellules de la carte</param>
        internal MapCellsEntitiesBuffer(int length)
        {
            this.Value = new Entity[length];
        }

        #endregion
    }
}
