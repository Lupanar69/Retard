using Unity.Collections;

namespace Assets.Scripts.Core.ViewModels.Generation
{
    /// <summary>
    /// Génère une seule salle s'étendant 
    /// sur toute la largeur et longueur du niveau
    /// </summary>
    public struct OneRoomMapGenerationAlgorithm : IMapGenerationAlgorithm
    {
        #region Fonctions publiques

        /// <summary>
        /// Génère les IDs des cases par défaut de la carte
        /// </summary>
        /// <param name="length">Le nombre total de cellules sur la carte</param>
        /// <param name="sizeX">La taille de la carte sur l'axe X</param>
        /// <param name="sizeY">La taille de la carte sur l'axe Y</param>
        /// <returns>Un NativeArray contenant les IDs des cases à créer(alloué avec Allocator.Temp)</returns>
        public readonly NativeArray<int> Generate(int length, int sizeX, int sizeY)
        {
            NativeArray<int> tilesIDs = new(length, Allocator.Temp);
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