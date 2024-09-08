using Arch.AOT.SourceGenerator;

namespace Retard.Input.Components
{
    /// <summary>
    /// La valeur en dessous de laquelle l'input 
    /// est considéré comme inerte
    /// </summary>
    /// <param name="value">La valeur en dessous de laquelle l'input est considéré comme inerte</param>
    [Component]
    public struct InputBindingDeadZoneCD(float value)
    {
        #region Variables d'instance

        /// <summary>
        /// La valeur en dessous de laquelle l'input est considéré comme inerte
        /// </summary>
        public float Value = value;

        #endregion
    }
}
