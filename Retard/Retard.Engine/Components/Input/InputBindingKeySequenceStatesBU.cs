using Arch.LowLevel;
using Retard.Engine.Models;

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
        public UnsafeArray<InputKeySequenceState> Value;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="value">Le tableau à copier</param>
        public InputBindingKeySequenceStatesBU(UnsafeArray<InputKeySequenceState> value)
        {
            this.Value = new UnsafeArray<InputKeySequenceState>(value.Length);

            for (int i = 0; i < value.Length; ++i)
            {
                this.Value[i] = value[i];
            }
        }

        #endregion
    }
}
