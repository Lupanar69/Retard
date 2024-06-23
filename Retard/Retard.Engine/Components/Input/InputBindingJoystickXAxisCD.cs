using Arch.AOT.SourceGenerator;
using Retard.Engine.Models.Input;

namespace Retard.Core.Components.Input
{
    /// <summary>
    /// Tag indiquant qu'on évalue l'axe X d'un joystick
    /// </summary>
    [Component]
    public struct InputBindingJoystickXAxisCD
    {
        #region Variables d'instance

        /// <summary>
        /// Le joystick utilisé
        /// </summary>
        public JoystickType Value;

        #endregion
    }
}
