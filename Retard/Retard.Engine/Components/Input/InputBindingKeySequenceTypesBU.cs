﻿using Arch.LowLevel;
using Retard.Engine.Models;

namespace Retard.Engine.Components.Input
{
    /// <summary>
    /// Un élément d'une séquence d'entrées à réaliser 
    /// pour exécuter une action.
    /// </summary>
    public struct InputBindingKeySequenceTypesBU
    {
        #region Variables d'instance

        /// <summary>
        /// Les types des bouton des éléments de la séquence
        /// </summary>
        public UnsafeArray<InputBindingKeyType> Value;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="value">Le tableau à copier</param>
        public InputBindingKeySequenceTypesBU(UnsafeArray<InputBindingKeyType> value)
        {
            this.Value = new UnsafeArray<InputBindingKeyType>(value.Length);

            for (int i = 0; i < value.Length; ++i)
            {
                this.Value[i] = value[i];
            }
        }

        #endregion
    }
}
