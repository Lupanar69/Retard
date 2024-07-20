namespace Retard.Engine.Models.DTOs.Input
{
    /// <summary>
    /// Représente les données du fichier de configuration 
    /// des entrées du joueur
    /// </summary>
    public sealed class InputConfigDTO
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
            this.Actions = actions;
        }

        #endregion
    }
}
