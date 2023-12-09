namespace Retard.Core.Models.Components.Cell
{
    /// <summary>
    /// Les n°s de ligne et colonne d'une cellule sur la carte
    /// </summary>
    internal sealed class CellRowColumnCD
    {
        #region Variables d'instance

        /// <summary>
        /// Le n° de ligne de la cellule
        /// </summary>
        internal int Row;

        /// <summary>
        /// Le n° de colonne de la cellule
        /// </summary>
        internal int Column;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="row">Le n° de ligne de la cellule</param>
        /// <param name="column">Le n° de colonne de la cellule</param>
        internal CellRowColumnCD(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }

        #endregion
    }
}
