using Retard.Core.Models.ValueTypes;

namespace Retard.Core.Models.DTOs.Input
{
    /// <summary>
    /// Représente les données d'un InputAction
    /// </summary>
    public sealed class InputActionDTO
    {
        #region Propriétés

        /// <summary>
        /// L'ID de cette action
        /// </summary>
        public NativeString Name
        {
            get;
            private set;
        }

        /// <summary>
        /// Le type de valeur retournée par une InputAciton
        /// </summary>
        public InputActionReturnValueType ValueType
        {
            get;
            private set;
        }

        /// <summary>
        /// Le type d'action à effectuer lorsqu'on évalue une InputAction donnée
        /// </summary>
        public InputActionTriggerType TriggerType
        {
            get;
            private set;
        }

        /// <summary>
        /// La liste des bindings de cette action
        /// </summary>
        public InputBindingDTO[] Bindings
        {
            get;
            private set;
        }

        #endregion
    }
}
