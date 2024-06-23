using Arch.LowLevel;

namespace Retard.Engine.Components.Input
{
    /// <summary>
    /// Un élément d'une séquence d'entrées à réaliser 
    /// pour exécuter une action.
    /// </summary>
    public struct InputBindingKeySequenceIDsBU
    {
        #region Variables d'instance

        /// <summary>
        /// Les IDs des entrées à évaluer (à convertir en enum en fonction du KeyType)
        /// </summary>
        public UnsafeArray<int> Keys;

        #endregion
    }
}
