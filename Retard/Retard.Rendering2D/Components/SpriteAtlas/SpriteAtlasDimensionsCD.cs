using Arch.AOT.SourceGenerator;

namespace Retard.Sprites.Components.SpriteAtlas
{
    /// <summary>
    /// Les IDs des sprites de début et fin de l'animation
    /// </summary>
    /// <remarks>
    /// Constructeur
    /// </remarks>
    /// <param name="rows">Le nombre de lignes de l'atlas</param>
    /// <param name="columns">Le nombre de colonnes de l'atlas</param>
    [Component]
    public readonly struct SpriteAtlasDimensionsCD(int rows, int columns)
    {
        #region Variables d'instance

        /// <summary>
        /// Le nombre de lignes de l'atlas
        /// </summary>
        public readonly int Rows = rows;

        /// <summary>
        /// Le nombre de colonnes de l'atlas
        /// </summary>
        public readonly int Columns = columns;

        #endregion
    }
}
