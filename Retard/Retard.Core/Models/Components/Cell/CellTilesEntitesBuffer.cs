using System.Collections.Generic;
using MonoGame.Extended.Entities;

namespace Retard.Core.Models.Components.Cell
{
    /// <summary>
    /// La liste des cases contenues dans une cellule
    /// </summary>
    internal sealed class CellTilesEntitesBuffer
    {
        #region Variables d'instance

        /// <summary>
        /// La liste des cases contenues dans cette cellule
        /// </summary>
        internal List<Entity> Value;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="length">Le nombre de cases par défaut dans cette cellule</param>
        internal CellTilesEntitesBuffer(int length)
        {
            this.Value = new List<Entity>(length);
        }

        #endregion
    }
}
