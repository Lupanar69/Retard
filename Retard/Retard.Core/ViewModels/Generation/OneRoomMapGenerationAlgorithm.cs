using Retard.Core.Models.Generation;
using Retard.Core.Models.ValueTypes;

namespace Retard.Core.ViewModels.Generation
{
    /// <summary>
    /// Génère une seule salle s'étendant 
    /// sur toute la largeur et longueur du niveau
    /// </summary>
    internal class OneRoomMapGenerationAlgorithm : IMapGenerationAlgorithm
    {
        #region Fonctins publiques

        /// <summary>
        /// Génère un nouveau niveau selon l'algorithme implémenté
        /// </summary>
        /// <param name="size">La taille de la carte</param>
        /// <param name="mapGenerationData">Contient les infos sur la carte générée</param>
        public void Execute(int2 size, out MapGenerationData mapGenerationData)
        {
            int[] tilesIDs = new int[size.X * size.Y];
            int count = 0;

            // Crée le mur du bas
            for (int x = 0; x < size.X; x++)
            {
                tilesIDs[count] = 0;
                count++;
            }

            // Crée le sol de la salle et les murs latéraux
            for (int y = 1; y < size.Y - 1; y++)
            {
                // Crée le mur de gauche
                tilesIDs[count] = 0;
                count++;

                // Crée le sol
                for (int x = 1; x < size.X - 1; x++)
                {
                    tilesIDs[count] = 1;
                    count++;
                }

                // Crée le mur de droite
                tilesIDs[count] = 0;
                count++;
            }

            // Crée le mur du haut
            for (int x = 0; x < size.X; x++)
            {
                tilesIDs[count] = 0;
                count++;
            }

            // Assigne les données de retour

            mapGenerationData = new MapGenerationData
            {
                TilesIDs = tilesIDs,
                RoomPoses = new int2[1] { int2.Zero },
                RoomSizes = new int2[1] { size - int2.One }
            };
        }

        #endregion
    }
}
