using Retard.Input.Models;

namespace Retard.Input.Components
{
    /// <summary>
    /// Les types des touches positives et négatives d'un axe Vector2D
    /// </summary>
    /// <param name="positiveXType">Le type de la touche positive sur l'axe X</param>
    /// <param name="negativeXType">Le type de la touche négative sur l'axe X</param>
    /// <param name="positiveYType">Le type de la touche positive sur l'axe Y</param>
    /// <param name="negativeYType">Le type de la touche négative sur l'axe Y</param>
    public struct InputBindingVector2DKeysTypesCD(InputBindingKeyType positiveXType, InputBindingKeyType negativeXType, InputBindingKeyType positiveYType, InputBindingKeyType negativeYType)
    {
        #region Variables d'instance

        /// <summary>
        /// Le type de la touche positive sur l'axe X
        /// </summary>
        public InputBindingKeyType PositiveXType = positiveXType;

        /// <summary>
        /// Le type de la touche négative sur l'axe X
        /// </summary>
        public InputBindingKeyType NegativeXType = negativeXType;

        /// <summary>
        /// Le type de la touche positive sur l'axe Y
        /// </summary>
        public InputBindingKeyType PositiveYType = positiveYType;

        /// <summary>
        /// Le type de la touche négative sur l'axe Y
        /// </summary>
        public InputBindingKeyType NegativeYType = negativeYType;

        #endregion
    }
}
