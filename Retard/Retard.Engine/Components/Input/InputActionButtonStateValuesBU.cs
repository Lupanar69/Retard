using Arch.AOT.SourceGenerator;
using Arch.LowLevel;
using Retard.Core.Models.Assets.Input;

namespace Retard.Core.Components.Input
{
    /// <summary>
    /// L'état du bouton lié à une InputAction
    /// pour chaque joueur connecté.
    /// S'il n'y a aucune manette ou qu'elles ne sont pas prises en charge,
    /// le buffer est de taille 1.
    /// </summary>
    [Component]
    public struct InputActionButtonStateValuesBU
    {
        #region Variables d'instance

        /// <summary>
        /// L'état du bouton lié à une InputAction
        /// pour chaque joueur connecté.
        /// S'il n'y a aucune manette ou qu'elles ne sont pas prises en charge,
        /// le buffer est de taille 1.
        /// </summary>
        public UnsafeArray<InputActionButtonState> Value;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="length">La taille de la collection</param>
        public InputActionButtonStateValuesBU(int length)
        {
            this.Value = new UnsafeArray<InputActionButtonState>(length);

            for (int i = 0; i < length; ++i)
            {
                this.Value[i] = InputActionButtonState.Inert;
            }
        }

        #endregion
    }
}
