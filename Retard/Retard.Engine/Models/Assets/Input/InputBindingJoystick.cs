using Newtonsoft.Json;

namespace Retard.Engine.Models.Assets.Input
{
    /// <summary>
    /// Contient les infos d'un binding utilisant un joystick
    /// (type du joystick, axe et zone inerte)
    /// </summary>
    public struct InputBindingJoystick
    {
        #region Variables d'instance

        /// <summary>
        /// Le joystick utilisé pour les InputActions de type Vector1D et Vector2D
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public JoystickType Type
        {
            get;
            private set;
        }

        /// <summary>
        /// L'axe de joystick à évaluer pour les InputActions de type Vector1D
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public JoystickAxisType Axis
        {
            get;
            private set;
        }

        /// <summary>
        /// La valeur en dessous de laquelle l'input 
        /// est considéré comme inerte
        /// </summary>
        public float DeadZone
        {
            get;
            private set;
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="joystick">Le joystick utilisé pour les InputActions de type Vector1D et Vector2D</param>
        /// <param name="joystickAxis">L'axe de joystick à évaluer</param>
        /// <param name="deadZone">La valeur en dessous de laquelle l'input est considéré comme inerte</param>
        public InputBindingJoystick(JoystickType joystick, JoystickAxisType joystickAxis, float deadZone)
        {
            this.Type = joystick;
            this.Axis = joystickAxis;
            this.DeadZone = deadZone;
        }

        #endregion
    }
}
