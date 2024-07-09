using Arch.AOT.SourceGenerator;
using Arch.LowLevel;
using Microsoft.Xna.Framework;

namespace Retard.Core.Components.Input
{
    /// <summary>
    /// L'état du bouton lié à une InputAction
    /// pour chaque joueur connecté.
    /// S'il n'y a aucune manette ou qu'elles ne sont pas prises en charge,
    /// le buffer est de taille 1.
    /// </summary>
    [Component]
    public struct InputBindingVector2DValuesBU
    {
        #region Variables d'instance

        /// <summary>
        /// L'état du bouton lié à une InputAction
        /// pour chaque joueur connecté.
        /// S'il n'y a aucune manette ou qu'elles ne sont pas prises en charge,
        /// le buffer est de taille 1.
        /// </summary>
        public UnsafeArray<Vector2> Value;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="length">La taille de la collection</param>
        public InputBindingVector2DValuesBU(int length)
        {
            this.Value = new UnsafeArray<Vector2>(length);

            for (int i = 0; i < length; ++i)
            {
                this.Value[i] = Vector2.Zero;
            }
        }

        #endregion
    }
}
