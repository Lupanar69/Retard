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
    }
}
