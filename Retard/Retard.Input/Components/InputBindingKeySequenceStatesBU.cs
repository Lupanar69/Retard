using Arch.LowLevel;
using Retard.Input.Models;

namespace Retard.Input.Components
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
        public UnsafeArray<InputKeySequenceState> Value;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="value">Le tableau à copier</param>
        public InputBindingKeySequenceStatesBU(UnsafeArray<InputKeySequenceState> value)
        {
            Value = new UnsafeArray<InputKeySequenceState>(value.Length);

            for (int i = 0; i < value.Length; ++i)
            {
                Value[i] = value[i];
            }
        }

        #endregion
    }
}
