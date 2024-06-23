using Arch.LowLevel;
using Retard.Engine.Models.Input;

namespace Retard.Engine.Components.Input
{
    /// <summary>
    /// Un élément d'une séquence d'entrées à réaliser 
    /// pour exécuter une action.
    /// </summary>
    public struct InputBindingKeySequenceBU
    {
        #region Variables d'instance

        /// <summary>
        /// Les types des bouton des éléments de la séquence
        /// </summary>
        public UnsafeArray<InputBindingKeyType> KeyTypes;

        /// <summary>
        /// Les IDs des entrées à évaluer (à convertir en enum en fonction du KeyType)
        /// </summary>
        public UnsafeArray<int> Keys;

        /// <summary>
        /// Les états que doivent avoir les InputKeySequenceElements
        /// pour être considérés actifs
        /// </summary>
        public UnsafeArray<InputKeySequenceState> ValidStates;

        #endregion
    }
}
