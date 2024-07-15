using Newtonsoft.Json;

namespace Retard.Engine.Models.Assets.Input
{
    /// <summary>
    /// Contient les infos d'un binding utilisant une gâchette
    /// (type de la gâchette, axe et zone inerte)
    /// </summary>
    public struct InputBindingTrigger
    {
        #region Variables d'instance

        /// <summary>
        /// La gâchette utilisé pour les InputActions de type Vector1D
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public TriggerType Type
        {
            get;
            init;
        }

        /// <summary>
        /// La valeur en dessous de laquelle l'input 
        /// est considéré comme inerte
        /// </summary>
        public float DeadZone
        {
            get;
            init;
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="trigger">La gâchette utilisée pour les InputActions de type Vector1D</param>
        /// <param name="deadZone">La valeur en dessous de laquelle l'input est considéré comme inerte</param>
        public InputBindingTrigger(TriggerType trigger, float deadZone)
        {
            this.Type = trigger;
            this.DeadZone = deadZone;
        }

        #endregion
    }
}

