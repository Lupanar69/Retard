using Arch.AOT.SourceGenerator;
using Retard.Core.Models.Assets.Input;

namespace Retard.Core.Components.Input
{
    /// <summary>
    /// L'état du bouton lié à une InptuAction
    /// </summary>
    [Component]
    public struct InputActionButtonStateCD
    {
        #region Variables d'instance

        /// <summary>
        /// L'état du bouton lié à une InputAction
        /// </summary>
        public InputActionButtonState Value;

        #endregion
    }
}
