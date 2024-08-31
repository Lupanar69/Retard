namespace Retard.Input.Models.Assets
{
    /// <summary>
    /// Interface des différents types de lecteur d'input possibles
    /// (clavier, souris, etc.)
    /// </summary>
    public interface IInputScheme
    {
        #region Méthodes publiques

        /// <summary>
        /// Capture l'état des inputs lors de la frame actuelle.
        /// </summary>
        public void Update();

        /// <summary>
        /// Capture l'état des inputs lors de la frame précédente.
        /// A appeler en fin d'Update pour ne pas écraser le précédent état
        /// avant les comparaisons.
        /// </summary>
        public void AfterUpdate();

        #endregion
    }
}
