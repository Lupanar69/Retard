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
        public UnsafeArray<int> Value;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="value">Le tableau à copier</param>
        public InputBindingKeySequenceIDsBU(UnsafeArray<int> value)
        {
            this.Value = new UnsafeArray<int>(value.Length);

            for (int i = 0; i < value.Length; ++i)
            {
                this.Value[i] = value[i];
            }
        }

        #endregion
    }
}
