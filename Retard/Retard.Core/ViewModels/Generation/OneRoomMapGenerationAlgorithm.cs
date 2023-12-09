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
        /// Génère le niveau
        /// </summary>
        /// <param name="length">Le nombre total de cellules sur la carte</param>
        /// <param name="sizeX">La taille de la carte sur l'axe X</param>
        /// <param name="sizeY">La taille de la carte sur l'axe Y</param>
        /// <returns>Les IDs de toutes les cases à instancier par cellule</returns>
        int[] IMapGenerationAlgorithm.Execute(int length, int sizeX, int sizeY)
        {
            int[] tilesIDs = new int[length];
            int count = 0;

            // Crée le mur du bas
            for (int x = 0; x < sizeX; x++)
            {
                tilesIDs[count] = 0;
                count++;
            }

            // Crée le sol de la salle et les murs latéraux
            for (int y = 1; y < sizeY - 1; y++)
            {
                // Crée le mur de gauche
                tilesIDs[count] = 0;
                count++;

                // Crée le sol
                for (int x = 1; x < sizeX - 1; x++)
                {
                    tilesIDs[count] = 1;
                    count++;
                }

                // Crée le mur de droite
                tilesIDs[count] = 0;
                count++;
            }

            // Crée le mur du haut
            for (int x = 0; x < sizeX; x++)
            {
                tilesIDs[count] = 0;
                count++;
            }

            return tilesIDs;
        }

        #endregion
    }
}
