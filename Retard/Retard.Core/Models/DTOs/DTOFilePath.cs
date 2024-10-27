using FixedStrings;

namespace Retard.Core.Models.DTOs
{
    /// <summary>
    /// Fournit les chemins d'accès vers les fichiers JSON
    /// des DTOS à sauvegarder
    /// </summary>
    public sealed class DTOFilePath
    {
        #region Propriétés

        /// <summary>
        /// Chemin d'accès du fichier de réserve
        /// </summary>
        public FixedString32 DefaultFilePath { get; init; }

        /// <summary>
        /// Chemin d'accès du fichier modifiable par le joueur
        /// </summary>
        public FixedString32 CustomFilePath { get; init; }

        /// <summary>
        /// Le DTO à sauvegarder
        /// </summary>
        public DataTransferObject DTO { get; init; }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        public DTOFilePath()
        {

        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="defaultFilePath">Chemin d'accès du fichier de réserve</param>
        /// <param name="customFilePath">Chemin d'accès du fichier modifiable par le joueur</param>
        /// <param name="dto">Le DTO à sauvegarder</param>
        public DTOFilePath(FixedString32 defaultFilePath, FixedString32 customFilePath, DataTransferObject dto)
        {
            this.DefaultFilePath = defaultFilePath;
            this.CustomFilePath = customFilePath;
            this.DTO = dto;
        }

        #endregion
    }
}
