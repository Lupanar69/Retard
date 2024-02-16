using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using Retard.Core.Models.Assets;
using Retard.Core.Models.Components.Cell;
using Retard.Core.Models.Components.Map;
using Retard.Core.Models.Components.MapElements;
using Retard.Core.Models.Components.Tiles;
using Retard.Core.Models.ValueTypes;

namespace Retard.Core.Models
{
    /// <summary>
    /// Contient la liste des entités créés par les systèmes
    /// </summary>
    internal static class Entities
    {
        #region Fonctions statiques

        #region Cells

        /// <summary>
        /// Crée une entité représentant la carte du niveau
        /// </summary>
        /// <param name="e">L'entité</param>
        /// <param name="size">La taille de la carte</param>
        /// <param name="cellEs">Les entités des cellules de la carte</param>
        /// <param name="roomsEs">Les entités des salles de la carte</param>
        internal static void CreateMapEntity(in Entity e, int2 size, in Entity[] cellEs, in Entity[] roomsEs)
        {
            e.Attach(new MapTag());
            e.Attach(new MapSizeCD(size));
            e.Attach(new MapCellsPositionsBuffer(size.X * size.Y));
            e.Attach(new MapCellsEntitiesBuffer(cellEs));
            e.Attach(new MapRoomsEntitiesBuffer(roomsEs));
        }

        /// <summary>
        /// Crée une entité représentant une cellule de la carte
        /// </summary>
        /// <param name="e">L'entité</param>
        /// <param name="row">Le n° de ligne de la cellule</param>
        /// <param name="column">Le n° de colonne de la cellule</param>
        /// <param name="pos">La position de la cellule</param>
        /// <param name="tileE">L'entité de la case par défaut</param>
        internal static void CreateCellEntity(in Entity e, int row, int column, int2 pos, Entity tileE)
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
        internal static void CreateTileEntity(in Entity e, in SpriteAtlas atlas, int2 pos, int frame)
        {
            e.Attach(new TileTag());
            e.Attach(new TilePositionCD(pos));
            e.Attach(new TileSpriteCD(atlas, frame, Color.White));
        }

        #endregion

        #region Features

        /// <summary>
        /// Crée une entité représentant une structure sur la carte
        /// </summary>
        /// <param name="e">L'entité</param>
        internal static void CreateFeatureEntity(in Entity e)
        {
            e.Attach(new MapFeatureTag());
        }

        /// <summary>
        /// Crée une entité représentant une salle de la carte
        /// </summary>
        /// <param name="e">L'entité</param>
        /// <param name="pos">La position de la salle</param>
        internal static void CreateRoomEntity(in Entity e, int2 pos, int2 size)
        {
            e.Attach(new RoomTag());
            e.Attach(new RoomDimensionsCD(pos, size));
        }

        #endregion

        #region Manipulation de components

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

        #endregion
    }
}
