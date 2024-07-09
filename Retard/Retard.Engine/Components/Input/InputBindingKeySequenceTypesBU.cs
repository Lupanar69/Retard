using Arch.LowLevel;
using Retard.Engine.Models;

namespace Retard.Engine.Components.Input
{
    /// <summary>
    /// Un élément d'une séquence d'entrées à réaliser 
    /// pour exécuter une action.
    /// </summary>
    public struct InputBindingKeySequenceTypesBU
    {
        #region Variables d'instance

        /// <summary>
        /// Les types des bouton des éléments de la séquence
        /// </summary>
        public UnsafeArray<InputBindingKeyType> Value;

        #endregion
    }
}
