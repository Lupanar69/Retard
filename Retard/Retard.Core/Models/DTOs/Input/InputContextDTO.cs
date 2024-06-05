using Retard.Core.Models.ValueTypes;

namespace Retard.Core.Models.DTOs.Input
{
    /// <summary>
    /// Représente les données d'un InputContext
    /// </summary>
    public sealed class InputContextDTO
    {
        #region Propriétés

        /// <summary>
        /// L'ID de ce contexte
        /// </summary>
        public NativeString Name
        {
            get;
            private set;
        }

        /// <summary>
        /// La liste des actions de ce contexte
        /// </summary>
        public InputActionDTO[] Actions
        {
            get;
            private set;
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="name">ID du contexte</param>
        /// <param name="actions">Les actions possibles dans ce contexte</param>
        public InputContextDTO(NativeString name, params InputActionDTO[] actions)
        {
            this.Name = name;
            this.Actions = actions;
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="name">ID du contexte</param>
        /// <param name="actions">Les actions possibles dans ce contexte</param>
        public InputContextDTO(string name, params InputActionDTO[] actions)
        {
            this.Name = new NativeString(name);
            this.Actions = actions;
        }

        #endregion
    }
}
