using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using Retard.Core.Models.Assets;
using Retard.Core.Models.Components.Cell;
using Retard.Core.Models.Components.Map;
using Retard.Core.Models.Components.Tiles;

namespace Retard.Core.Models.Entities
{
    /// <summary>
    /// Contient la liste des entités créés par les systèmes
    /// </summary>
    internal static class Entities
    {
        #region Génération procédurale

        /// <summary>
        /// Crée une entité représentant la carte du niveau
        /// </summary>
        /// <param name="e">L'entité</param>
        /// <param name="size">La taille de la carte</param>
        internal static void CreateMapEntity(in Entity e, Vector2 size)
        {
            int length = (int)(size.X * size.Y);
            e.Attach(new MapSizeCD(size));
            e.Attach(new MapCellsPositionsBuffer(length));
            e.Attach(new MapCellsEntitiesBuffer(length));
        }

        /// <summary>
        /// Crée une entité représentant une cellule de la carte
        /// </summary>
        /// <param name="e">L'entité</param>
        /// <param name="pos">La position de la cellule</param>
        internal static void CreateCellEntity(in Entity e, Vector2 pos)
        {
            e.Attach(new CellPositionCD(pos));
            e.Attach(new CellTilesEntitesBuffer(1));
        }

        /// <summary>
        /// Crée une entité représentant une case dans une cellule
        /// </summary>
        /// <param name="e">L'entité</param>
        /// <param name="pos">La position de la cellule</param>
        internal static void CreateTileEntity(in Entity e, in SpriteAtlas atlas, int frame)
        {
            e.Attach(new TileSpriteCD(atlas, frame, Color.White));
        }

        #endregion
    }
}
