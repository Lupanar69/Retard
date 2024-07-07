﻿using Arch.AOT.SourceGenerator;
using Arch.LowLevel;

namespace Retard.Core.Components.Input
{
    /// <summary>
    /// L'état de l'InputAction pressée
    /// pour chaque joueur connecté.
    /// S'il n'y a aucune manette ou qu'elles ne sont pas prises en charge,
    /// le buffer est de taille 1.
    /// </summary>
    [Component]
    public struct InputActionVector1DValuesBU
    {
        #region Variables d'instance

        /// <summary>
        /// L'état du bouton lié à une InputAction
        /// pour chaque joueur connecté.
        /// S'il n'y a aucune manette ou qu'elles ne sont pas prises en charge,
        /// le buffer est de taille 1.
        /// </summary>
        public UnsafeArray<float> Value;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="length">La taille de la collection</param>
        public InputActionVector1DValuesBU(int length)
        {
            this.Value = new UnsafeArray<float>(length);

            for (int i = 0; i < length; ++i)
            {
                this.Value[i] = 0f;
            }
        }

        #endregion
    }
}
