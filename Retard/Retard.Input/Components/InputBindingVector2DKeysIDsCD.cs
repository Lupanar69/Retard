namespace Retard.Input.Components
{
    /// <summary>
    /// Les IDs des touches positives et négatives d'un axe Vector2D
    /// </summary>
    /// <param name="positiveXID">L'ID de la touche positive sur l'axe X</param>
    /// <param name="negativeXID">L'ID de la touche négative sur l'axe X</param>
    /// <param name="positiveYID">L'ID de la touche positive sur l'axe Y</param>
    /// <param name="negativeYID">L'ID de la touche négative sur l'axe Y</param>
    public struct InputBindingVector2DKeysIDsCD(int positiveXID, int negativeXID, int positiveYID, int negativeYID)
    {
        #region Variables d'instance

        /// <summary>
        /// L'ID de la touche positive sur l'axe X
        /// </summary>
        public int PositiveXID = positiveXID;

        /// <summary>
        /// L'ID de la touche négative sur l'axe X
        /// </summary>
        public int NegativeXID = negativeXID;

        /// <summary>
        /// L'ID de la touche positive sur l'axe Y
        /// </summary>
        public int PositiveYID = positiveYID;

        /// <summary>
        /// L'ID de la touche négative sur l'axe Y
        /// </summary>
        public int NegativeYID = negativeYID;

        #endregion
    }
}
