namespace Retard.Input.Components
{
    /// <summary>
    /// Les IDs des touches positives et négatives d'un axe Vector1D
    /// </summary>
    /// <param name="positiveID">L'ID de la touche positive</param>
    /// <param name="positiveID">L'ID de la touche négative</param>
    public struct InputBindingVector1DKeysIDsCD(int positiveID, int negativeID)
    {
        #region Variables d'instance

        /// <summary>
        /// L'ID de la touche positive
        /// </summary>
        public int PositiveID = positiveID;

        /// <summary>
        /// L'ID de la touche négative
        /// </summary>
        public int NegativeID = negativeID;

        #endregion
    }
}
