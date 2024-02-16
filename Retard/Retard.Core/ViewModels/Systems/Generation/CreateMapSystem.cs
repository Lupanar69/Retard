using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Input;
using Retard.Core.Models;
using Retard.Core.Models.Assets;
using Retard.Core.Models.Components.Cell;
using Retard.Core.Models.Components.Map;
using Retard.Core.Models.Components.MapElements;
using Retard.Core.Models.Components.Other;
using Retard.Core.Models.Components.Tiles;
using Retard.Core.Models.Generation;
using Retard.Core.Models.ValueTypes;
using Retard.Core.ViewModels.Generation;

namespace Retard.Core.ViewModels.Systems.Generation
{
    /// <summary>
    /// Crée l'entité représentant la carte du niveau
    /// </summary>
    public sealed class CreateMapSystem : EntityUpdateSystem
    {
        #region Variables d'instance

        /// <summary>
        /// L'atlas contenant les sprites des cases
        /// </summary>
        private SpriteAtlas _atlas;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="atlas">L'atlas contenant les sprites des cases</param>
        public CreateMapSystem(SpriteAtlas atlas)
            : base(Aspect.One(typeof(MapTag), typeof(CellTag), typeof(TileTag), typeof(RoomTag)))
        {
            this._atlas = atlas;
        }

        #endregion

        #region Fonctions publiques

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="mapperService">Inutilisé</param>
        public override void Initialize(IComponentMapperService mapperService)
        {

        }

        /// <summary>
        /// Màj à chaque frame
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le lancement de l'application</param>
        public override void Update(GameTime gameTime)
        {
            if (KeyboardExtended.GetState().WasKeyJustDown(Keys.Space))
            {
                // Détruit l'ancienne carte

                foreach (int entityID in this.ActiveEntities)
                {
                    Entity e = this.GetEntity(entityID);
                    Entities.AddComponent<DestroyTag>(in e);
                }

                // Génère une nouvelle carte

                int sizeX = (int)GameSession.GenerationRandom.NextSingle(Constants.MIN_MAX_MAP_SIZE.X, Constants.MIN_MAX_MAP_SIZE.Y);
                int sizeY = (int)GameSession.GenerationRandom.NextSingle(Constants.MIN_MAX_MAP_SIZE.X, Constants.MIN_MAX_MAP_SIZE.Y);
                int2 size = new(sizeX, sizeY);
                int mapGenIndex = (int)GameSession.GenerationRandom.NextSingle(Constants.MAP_GENERATION_ALGORITHMS.Length);

                IMapGenerationAlgorithm mapGen = Constants.MAP_GENERATION_ALGORITHMS[mapGenIndex];
                mapGen.Execute(size, out MapGenerationData mapGenData);
                Entity[] roomsEs = this.CreateMapRooms(mapGenData.RoomPoses, mapGenData.RoomSizes);
                Entity[] tilesEs = this.CreateMapTiles(mapGenData.TilesIDs, size, in this._atlas);
                Entity[] cellsEs = this.CreateCells(size, in tilesEs);
                this.CreateMap(size, in cellsEs, in roomsEs);
            }
        }

        #endregion

        #region Fonctions privées

        #region Cells

        /// <summary>
        /// Crée l'entité de la carte
        /// </summary>
        /// <param name="size">La taille de la carte</param>
        /// <param name="cellsEs">Les entités des cellules de la carte</param>
        /// <param name="roomsEs">Les entités des salles de la carte</param>
        /// <returns>L'entité de la carte</returns>
        private void CreateMap(int2 size, in Entity[] cellsEs, in Entity[] roomsEs)
        {
            Entity e = this.CreateEntity();
            Entities.CreateMapEntity(in e, size, in cellsEs, in roomsEs);
        }

        /// <summary>
        /// Crée les entités des cellules de la carte
        /// </summary>
        /// <param name="size">La taille de la carte</param>
        /// <param name="tilesEs">Les entités des cases de chaque cellule</param>
        /// <returns>Les entités de chaque cellule</returns>
        private Entity[] CreateCells(int2 size, in Entity[] tilesEs)
        {
            Entity[] cellEs = new Entity[size.X * size.Y];
            int count = 0;

            for (int y = 0; y < size.Y; y++)
            {
                for (int x = 0; x < size.X; x++)
                {
                    int2 pos = new int2(x, y) * Constants.SPRITE_SIZE_PIXELS;
                    Entity e = this.CreateEntity();
                    Entities.CreateCellEntity(in e, x, y, pos, tilesEs[count]);
                    cellEs[count] = e;

                    count++;
                }
            }

            return cellEs;
        }

        /// <summary>
        /// Crée les entités des cases représentant les murs et le sol
        /// </summary>
        /// <param name="tilesIDs">Les IDs des cases à créer</param>
        /// <param name="size">La taille de la carte</param>
        /// <param name="atlas">La texture à afficher</param>
        /// <returns>Les entités de chaque case</returns>
        private Entity[] CreateMapTiles(int[] tilesIDs, int2 size, in SpriteAtlas atlas)
        {
            Entity[] tilesEs = new Entity[size.X * size.Y];
            int count = 0;

            for (int y = 0; y < size.Y; y++)
            {
                for (int x = 0; x < size.X; x++)
                {
                    int2 pos = new int2(x, y) * Constants.SPRITE_SIZE_PIXELS;
                    Entity e = this.CreateEntity();
                    Entities.CreateTileEntity(in e, atlas, pos, tilesIDs[count]);
                    tilesEs[count] = e;

                    count++;
                }
            }

            return tilesEs;
        }

        #endregion

        #region Features

        /// <summary>
        /// Crée les entités des salles
        /// </summary>
        /// <param name="roomPoses">Les positions de chaque salle</param>
        /// <param name="roomSizes">Les dimensions de chaque salle</param>
        /// <returns>Les entités de chaque salle</returns>
        private Entity[] CreateMapRooms(int2[] roomPoses, int2[] roomSizes)
        {
            Entity[] roomsEs = new Entity[roomPoses.Length];
            int count = 0;

            for (int i = 0; i < roomPoses.Length; i++)
            {
                Entity e = this.CreateEntity();
                Entities.CreateFeatureEntity(in e);
                Entities.CreateRoomEntity(in e, roomPoses[i], roomSizes[i]);
                roomsEs[count] = e;

                count++;
            }

            return roomsEs;
        }

        #endregion

        #endregion
    }
}