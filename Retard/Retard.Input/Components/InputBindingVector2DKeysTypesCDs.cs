using Retard.Input.Models;

namespace Retard.Input.Components
{
    /// <summary>
    /// Les types des touches positives et négatives d'un axe Vector2D
    /// </summary>
    public struct InputBindingVector2DKeysTypesCD
    {
        #region Variables d'instance

        /// <summary>
        /// Le type de la touche positive sur l'axe X
        /// </summary>
        public InputBindingKeyType PositiveXType;

        /// <summary>
        /// Le type de la touche négative sur l'axe X
        /// </summary>
        public InputBindingKeyType NegativeXType;

        /// <summary>
        /// Le type de la touche positive sur l'axe Y
        /// </summary>
        public InputBindingKeyType PositiveYType;

        /// <summary>
        /// Le type de la touche négative sur l'axe Y
        /// </summary>
        public InputBindingKeyType NegativeYType;

        #endregion
    }
}
