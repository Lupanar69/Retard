using Arch.AOT.SourceGenerator;
using Arch.LowLevel;
using Retard.Engine.Models;

namespace Retard.Core.Components.Input
{
    /// <summary>
    /// Indique si la séquence de ce binding est valide pour le n° de joueur donné.
    /// S'il n'y a aucune manette ou qu'elles ne sont pas prises en charge,
    /// le buffer est de taille 1.
    /// </summary>
    [Component]
    public struct InputBindingButtonStateValuesBU
    {
        #region Variables d'instance

        /// <summary>
        /// Indique si la séquence de ce binding est valide pour le n° de joueur donné.
        /// S'il n'y a aucune manette ou qu'elles ne sont pas prises en charge,
        /// le buffer est de taille 1.
        /// </summary>
        public UnsafeArray<ButtonStateType> Value;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="length">La taille de la collection</param>
        public InputBindingButtonStateValuesBU(int length)
        {
            this.Value = new UnsafeArray<ButtonStateType>(length);

            for (int i = 0; i < length; ++i)
            {
                this.Value[i] = ButtonStateType.Inert;
            }
        }

        #endregion
    }
}
