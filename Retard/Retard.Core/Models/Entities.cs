using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using Retard.Core.Models.Assets;
using Retard.Core.Models.Components.Cell;
using Retard.Core.Models.Components.Map;
using Retard.Core.Models.Components.Tiles;

namespace Retard.Core.Models
{
    /// <summary>
    /// Contient la liste des entités créés par les systèmes
    /// </summary>
    internal static class Entities
    {
        #region Fonctions statiques

        /// <summary>
        /// Crée une entité représentant la carte du niveau
        /// </summary>
        /// <param name="e">L'entité</param>
        /// <param name="length">Le nombre total de cellules sur la carte</param>
        /// <param name="sizeX">La taille de la carte sur l'axe X</param>
        /// <param name="sizeY">La taille de la carte sur l'axe Y</param>
        /// <param name="cellEs">Les entités des cellules de la carte</param>
        internal static void CreateMapEntity(in Entity e, int length, int sizeX, int sizeY, Entity[] cellEs)
        {
            e.Attach(new MapTag());
            e.Attach(new MapSizeCD(sizeX, sizeY));
            e.Attach(new MapCellsPositionsBuffer(length));
            e.Attach(new MapCellsEntitiesBuffer(cellEs));
        }

        /// <summary>
        /// Crée une entité représentant une cellule de la carte
        /// </summary>
        /// <param name="e">L'entité</param>
        /// <param name="row">Le n° de ligne de la cellule</param>
        /// <param name="column">Le n° de colonne de la cellule</param>
        /// <param name="pos">La position de la cellule</param>
        /// <param name="tileE">L'entité de la case par défaut</param>
        internal static void CreateCellEntity(in Entity e, int row, int column, Vector2 pos, Entity tileE)
        {
            e.Attach(new CellTag());
            e.Attach(new CellRowColumnCD(row, column));
            e.Attach(new CellPositionCD(pos));
            e.Attach(new CellTilesEntitesBuffer(tileE));
        }

        /// <summary>
        /// Crée une entité représentant une case dans une cellule
        /// </summary>
        /// <param name="e">L'entité</param>
        /// <param name="pos">La position de la cellule</param>
        internal static void CreateTileEntity(in Entity e, in SpriteAtlas atlas, Vector2 pos, int frame)
        {
            e.Attach(new TileTag());
            e.Attach(new TilePositionCD(pos));
            e.Attach(new TileSpriteCD(atlas, frame, Color.White));
        }

        /// <summary>
        /// Ajoute un component à l'entité
        /// </summary>
        /// <param name="entity">L'entité</param>
        /// <typeparam name="T">Le type du component à ajouter</typeparam>
        internal static void AddComponent<T>(in Entity entity) where T : class, new()
        {
            entity.Attach(new T());
        }

        /// <summary>
        /// Ajoute un component à l'entité
        /// </summary>
        /// <param name="entity">L'entité</param>
        /// <param name="comp">Le component à ajouter</param>
        /// <typeparam name="T">Le type du component à ajouter</typeparam>
        internal static void AddComponent<T>(in Entity entity, T comp) where T : class, new()
        {
            entity.Attach(comp);
        }

        #endregion
    }
}
