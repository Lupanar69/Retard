using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Input;
using Retard.Core.Models;
using Retard.Core.Models.Assets;
using Retard.Core.Models.Entities;
using Retard.Core.ViewModels.Generation;

namespace Retard.Core.ViewModels.Systems
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
            : base(new AspectBuilder())
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
                int sizeX = (int)GameSession.GenerationRandom.NextSingle(Constants.MIN_MAX_MAP_SIZE.X, Constants.MIN_MAX_MAP_SIZE.Y);
                int sizeY = (int)GameSession.GenerationRandom.NextSingle(Constants.MIN_MAX_MAP_SIZE.X, Constants.MIN_MAX_MAP_SIZE.Y);
                int length = sizeX * sizeY;
                int mapGenIndex = (int)GameSession.GenerationRandom.NextSingle(Constants.MAP_GENERATION_ALGORITHMS.Length);

                IMapGenerationAlgorithm mapGen = Constants.MAP_GENERATION_ALGORITHMS[mapGenIndex];
                int[] tilesIDs = mapGen.Execute(length, sizeX, sizeY);
                Entity[] tiles = this.CreateMapTiles(tilesIDs, sizeX, sizeY, in this._atlas);
                Entity[] cellEs = this.CreateCells(length, sizeX, sizeY, in tiles);
                this.CreateMap(length, sizeX, sizeY, in cellEs);
            }
        }

        #endregion

        #region Fonctions privées

        /// <summary>
        /// Crée l'entité de la carte
        /// </summary>
        /// <param name="length">Le nombre total de cellules sur la carte</param>
        /// <param name="sizeX">La taille de la carte sur l'axe X</param>
        /// <param name="sizeY">La taille de la carte sur l'axe Y</param>
        /// <param name="cellEs">Les entités des cellules de la carte</param>
        /// <returns>L'entité de la carte</returns>
        private void CreateMap(int length, int sizeX, int sizeY, in Entity[] cellEs)
        {
            Entity e = this.CreateEntity();
            Entities.CreateMapEntity(in e, length, sizeX, sizeY, cellEs);
        }

        /// <summary>
        /// Crée les entités des cellules de la carte
        /// </summary>
        /// <param name="length">Le nombre total de cellules sur la carte</param>
        /// <param name="sizeX">La taille de la carte sur l'axe X</param>
        /// <param name="sizeY">La taille de la carte sur l'axe Y</param>
        /// <param name="tilesEs">Les entités des cases de chaque cellule</param>
        /// <returns>Les entités de chaque cellule</returns>
        private Entity[] CreateCells(int length, int sizeX, int sizeY, in Entity[] tilesEs)
        {
            Entity[] cellEs = new Entity[length];
            int count = 0;

            for (int y = 0; y < sizeY; y++)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    Vector2 pos = new Vector2(x, y) * Constants.SPRITE_SIZE_PIXELS;
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
        /// <returns>Les entités de chaque case</returns>
        private Entity[] CreateMapTiles(int[] tilesIDs, int sizeX, int sizeY, in SpriteAtlas atlas)
        {
            Entity[] tilesEs = new Entity[tilesIDs.Length];
            int count = 0;

            for (int y = 0; y < sizeY; y++)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    Vector2 pos = new Vector2(x, y) * Constants.SPRITE_SIZE_PIXELS;
                    Entity e = this.CreateEntity();
                    Entities.CreateTileEntity(in e, atlas, pos, tilesIDs[count]);
                    tilesEs[count] = e;

                    count++;
                }
            }

            return tilesEs;
        }

        #endregion
    }
}
