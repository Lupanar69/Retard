using Retard.Core.Models.DTOs;

namespace Retard.Input.Models.DTOs
{
    /// <summary>
    /// Représente les données du fichier de configuration 
    /// des entrées du joueur
    /// </summary>
    public sealed class InputConfigDTO : DataTransferObject
    {
        #region Propriétés

        /// <summary>
        /// La liste des actions de ce contexte
        /// </summary>
        public readonly InputActionDTO[] Actions;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="actions">Les actions possibles dans ce contexte</param>
        public InputConfigDTO(params InputActionDTO[] actions)
        {
            Actions = actions;
        }

        #endregion
    }
}
