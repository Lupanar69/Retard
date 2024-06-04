namespace Retard.Core.Models.DTOs.Input
{
    /// <summary>
    /// Représente les données du fichier de configuration 
    /// des entrées du joueur
    /// </summary>
    public sealed class InputConfigDTO
    {
        #region Propriétés

        /// <summary>
        /// La liste des contextes de ce fichier
        /// </summary>
        public InputContextDTO[] Contexts
        {
            get;
            private set;
        }

        #endregion
    }
}
