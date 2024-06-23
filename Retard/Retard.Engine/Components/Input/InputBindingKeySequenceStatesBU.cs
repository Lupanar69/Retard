using Arch.LowLevel;
using Retard.Engine.Models.Input;

namespace Retard.Engine.Components.Input
{
    /// <summary>
    /// Un élément d'une séquence d'entrées à réaliser 
    /// pour exécuter une action.
    /// </summary>
    public struct InputBindingKeySequenceStatesBU
    {
        #region Variables d'instance

        /// <summary>
        /// Les états que doivent avoir les InputKeySequenceElements
        /// pour être considérés actifs
        /// </summary>
        public UnsafeArray<InputKeySequenceState> ValidStates;

        #endregion
    }
}
