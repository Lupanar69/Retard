using Retard.Input.Models;

namespace Retard.Input.Components
{
    /// <summary>
    /// Le type du trigger de l'InputBinding
    /// </summary>
    /// <param name="value">Le type du trigger de l'InputBinding</param>
    public struct InputBindingTriggerTypeCD(TriggerType value)
    {
        #region Variables d'instance

        /// <summary>
        /// Le type du trigger de l'InputBinding
        /// </summary>
        public TriggerType Value = value;

        #endregion
    }
}
