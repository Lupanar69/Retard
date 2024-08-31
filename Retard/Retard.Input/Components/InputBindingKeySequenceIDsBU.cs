using Arch.LowLevel;

namespace Retard.Input.Components
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
        public UnsafeArray<int> Value;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="value">Le tableau à copier</param>
        public InputBindingKeySequenceIDsBU(UnsafeArray<int> value)
        {
            Value = new UnsafeArray<int>(value.Length);

            for (int i = 0; i < value.Length; ++i)
            {
                Value[i] = value[i];
            }
        }

        #endregion
    }
}
