using Arch.AOT.SourceGenerator;

namespace Retard.Input.Components
{
    /// <summary>
    /// La valeur en dessous de laquelle l'input 
    /// est considéré comme inerte
    /// </summary>
    [Component]
    public struct InputBindingDeadZoneCD
    {
        #region Variables d'instance

        /// <summary>
        /// La valeur en dessous de laquelle l'input 
        /// est considéré comme inerte
        /// </summary>
        public float Value;

        #endregion
    }
}
