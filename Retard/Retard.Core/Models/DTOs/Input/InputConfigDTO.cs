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

        #region Constructeur

        /// <summary>
        /// Constructeru
        /// </summary>
        /// <param name="contexts">Les contextes des inputs</param>
        public InputConfigDTO(params InputContextDTO[] contexts)
        {
            this.Contexts = contexts;
        }

        #endregion
    }
}
