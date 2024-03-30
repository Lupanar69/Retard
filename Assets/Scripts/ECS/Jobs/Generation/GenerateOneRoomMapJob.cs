using Assets.Scripts.Core.Models;
using Assets.Scripts.Core.Models.Generation;
using Assets.Scripts.ECS.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.ECS.Jobs.Generation
{
    /// <summary>
    /// Génère une seule salle s'étendant 
    /// sur toute la largeur et longueur du niveau
    /// </summary>
    [BurstCompile]
    public partial struct GenerateOneRoomMapJob : IJobEntity
    {
        #region Variables d'instance

        /// <summary>
        /// Le nombre total de cellules sur la carte
        /// </summary>
        public int Length;

        /// <summary>
        /// La taille de la carte sur l'axe X
        /// </summary>
        public int SizeX;

        /// <summary>
        /// La taille de la carte sur l'axe Y
        /// </summary>
        public int SizeY;

        /// <summary>
        /// Les IDs des cases à retourner
        /// </summary>
        [WriteOnly]
        public NativeArray<TilePosAndID> TilesIDsWO;

        #endregion

        #region Fonctions publiques

        /// <summary>
        /// Génère une seule salle s'étendant 
        /// sur toute la largeur et longueur du niveau
        /// </summary>
        /// <param name="e">L'entité de l'algorithme</param>
        /// <param name="settings">Les paramètres de l'algorithme</param>
        [BurstCompile]
        public void Execute(Entity e, in OneRoomMapGenAlgorithmCD settings)
        {
            // Liste de tous les IDs et leur positions
            // (x: posX, y: posY, z: ID)
            NativeArray<TilePosAndID> arr = new(this.Length, Allocator.Temp);
            int count = 0;

            // Crée le mur du bas
            for (int x = 0; x < this.SizeX; x++)
            {
                arr[x] = new TilePosAndID(new int2(x, 0), Constants.WALL_TILE_KEY);
            }

            count += this.SizeX;

            // Crée le sol de la salle et les murs latéraux
            for (int y = 1; y < this.SizeY - 1; y++)
            {
                // Crée le mur de gauche
                arr[count] = new TilePosAndID(new int2(0, y), Constants.WALL_TILE_KEY);
                count++;

                // Crée le sol
                for (int x = 1; x < this.SizeX - 1; x++)
                {
                    arr[count] = new TilePosAndID(new int2(x, y), Constants.FLOOR_TILE_KEY);
                    count++;
                }

                // Crée le mur de droite
                arr[count] = new TilePosAndID(new int2(this.SizeX - 1, y), Constants.WALL_TILE_KEY);
                count++;
            }

            // Crée le mur du haut
            for (int x = 0; x < this.SizeX; x++)
            {
                arr[count + x] = new TilePosAndID(new int2(x, this.SizeY - 1), Constants.WALL_TILE_KEY);
            }

            arr.CopyTo(this.TilesIDsWO);
        }

        #endregion
    }
}