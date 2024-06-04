using Arch.AOT.SourceGenerator;
using Microsoft.Xna.Framework.Input;

namespace Retard.Core.Components.Input
{
    /// <summary>
    /// Le bouton de la manette à évaluer
    /// </summary>
    [Component]
    public struct InputBindingGamePadKeyCD
    {
        #region Variables d'instance

        /// <summary>
        /// Le bouton de la manette à évaluer
        /// </summary>
        public Buttons Value;

        #endregion
    }
}
