using Arch.LowLevel;
using Retard.Input.Models;

namespace Retard.Input.Components
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

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="value">Le tableau à copier</param>
        public InputBindingKeySequenceTypesBU(UnsafeArray<InputBindingKeyType> value)
        {
            Value = new UnsafeArray<InputBindingKeyType>(value.Length);

            for (int i = 0; i < value.Length; ++i)
            {
                Value[i] = value[i];
            }
        }

        #endregion
    }
}
