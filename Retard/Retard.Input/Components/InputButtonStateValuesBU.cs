using Arch.AOT.SourceGenerator;
using Arch.LowLevel;

namespace Retard.Input.Components
{
    /// <summary>
    /// Indique si la séquence de ce binding est valide pour le n° de joueur donné.
    /// S'il n'y a aucune manette ou qu'elles ne sont pas prises en charge,
    /// le buffer est de taille 1.
    /// </summary>
    [Component]
    public struct InputButtonStateValuesBU
    {
        #region Variables d'instance

        /// <summary>
        /// Indique si la séquence de ce binding est valide pour le n° de joueur donné.
        /// S'il n'y a aucune manette ou qu'elles ne sont pas prises en charge,
        /// le buffer est de taille 1.
        /// </summary>
        public UnsafeArray<bool> Value;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="length">La taille de la collection</param>
        public InputButtonStateValuesBU(int length)
        {
            Value = new UnsafeArray<bool>(length);

            for (int i = 0; i < length; ++i)
            {
                Value[i] = false;
            }
        }

        #endregion
    }
}
