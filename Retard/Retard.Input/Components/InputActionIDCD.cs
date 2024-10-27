using Arch.AOT.SourceGenerator;
using FixedStrings;

namespace Retard.Input.Components
{
    /// <summary>
    /// L'ID d'une InputAction
    /// </summary>
    /// <param name="value">L'ID d'une InputAction</param>
    [Component]
    public struct InputActionIDCD(FixedString32 value)
    {
        #region Variables d'instance

        /// <summary>
        /// L'ID d'une InputAction
        /// </summary>
        public FixedString32 Value = value;

        #endregion
    }
}
