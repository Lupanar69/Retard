using Arch.AOT.SourceGenerator;
using Arch.LowLevel;
using Microsoft.Xna.Framework.Input;

namespace Retard.Core.Components.Input
{
    /// <summary>
    /// Las touches du clavier à évaluer
    /// </summary>
    [Component]
    public struct InputBindingKeyboardKeysBE
    {
        #region Variables d'instance

        /// <summary>
        /// Las touches du clavier à évaluer
        /// </summary>
        public UnsafeArray<Keys> Value;

        #endregion
    }
}
