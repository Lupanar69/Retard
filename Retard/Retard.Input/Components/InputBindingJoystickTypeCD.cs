using Arch.AOT.SourceGenerator;
using Retard.Input.Models;

namespace Retard.Input.Components
{
    /// <summary>
    /// Le type du joystick utilisé
    /// </summary>
    /// <param name="value">Le type du joystick utilisé</param>
    [Component]
    public struct InputBindingJoystickTypeCD(JoystickType value)
    {
        #region Variables d'instance

        /// <summary>
        /// Le type du joystick utilisé
        /// </summary>
        public JoystickType Value = value;

        #endregion
    }
}