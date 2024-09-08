using Retard.Input.Models;

namespace Retard.Input.Components
{
    /// <summary>
    /// Les types des touches positives et négatives d'un axe Vector1D
    /// </summary>
    /// <param name="positiveType">Le type de la touche positive</param>
    /// <param name="positiveType">Le type de la touche négative</param>
    public struct InputBindingVector1DKeysTypesCD(InputBindingKeyType positiveType, InputBindingKeyType negativeType)
    {
        #region Variables d'instance

        /// <summary>
        /// Le type de la touche positive
        /// </summary>
        public InputBindingKeyType PositiveType = positiveType;

        /// <summary>
        /// Le type de la touche négative
        /// </summary>
        public InputBindingKeyType NegativeType = negativeType;

        #endregion
    }
}
