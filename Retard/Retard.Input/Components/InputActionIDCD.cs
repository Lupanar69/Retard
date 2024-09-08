using Arch.AOT.SourceGenerator;
using Retard.Core.Models.ValueTypes;

namespace Retard.Input.Components
{
    /// <summary>
    /// L'ID d'une InputAction
    /// </summary>
    /// <param name="value">L'ID d'une InputAction</param>
    [Component]
    public struct InputActionIDCD(NativeString value)
    {
        #region Variables d'instance

        /// <summary>
        /// L'ID d'une InputAction
        /// </summary>
        public NativeString Value = value;

        #endregion
    }
}
