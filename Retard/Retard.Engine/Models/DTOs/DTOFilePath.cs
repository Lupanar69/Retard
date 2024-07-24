using Retard.Core.Models.ValueTypes;

namespace Retard.Engine.Models.DTOs
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
        public NativeString DefaultFilePath { get; init; }

        /// <summary>
        /// Chemin d'accès du fichier modifiable par le joueur
        /// </summary>
        public NativeString CustomFilePath { get; init; }

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
        public DTOFilePath(NativeString defaultFilePath, NativeString customFilePath, DataTransferObject dto)
        {
            this.DefaultFilePath = defaultFilePath;
            this.CustomFilePath = customFilePath;
            this.DTO = dto;
        }

        #endregion
    }
}
