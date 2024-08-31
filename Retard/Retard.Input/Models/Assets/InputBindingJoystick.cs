using Newtonsoft.Json;
using Retard.Input.Models;

namespace Retard.Input.Models.Assets
{
    /// <summary>
    /// Contient les infos d'un binding utilisant un joystick
    /// (type du joystick, axe et zone inerte)
    /// </summary>
    public readonly struct InputBindingJoystick
    {
        #region Variables d'instance

        /// <summary>
        /// Le joystick utilisé pour les InputActions de type Vector1D et Vector2D
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public readonly JoystickType Type;

        /// <summary>
        /// L'axe de joystick à évaluer pour les InputActions de type Vector1D
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public readonly JoystickAxisType Axis;

        /// <summary>
        /// La valeur en dessous de laquelle l'input 
        /// est considéré comme inerte
        /// </summary>
        public readonly float DeadZone;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="joystick">Le joystick utilisé pour les InputActions de type Vector1D et Vector2D</param>
        /// <param name="joystickAxis">L'axe de joystick à évaluer</param>
        /// <param name="deadZone">La valeur en dessous de laquelle l'input est considéré comme inerte</param>
        [JsonConstructor]
        public InputBindingJoystick(JoystickType joystick, JoystickAxisType joystickAxis, float deadZone)
        {
            Type = joystick;
            Axis = joystickAxis;
            DeadZone = deadZone;
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="joystick">Le joystick utilisé pour les InputActions de type Vector1D et Vector2D</param>
        /// <param name="joystickAxis">L'axe de joystick à évaluer</param>
        /// <param name="deadZone">La valeur en dessous de laquelle l'input est considéré comme inerte</param>
        public InputBindingJoystick(JoystickType joystick, float deadZone) : this(joystick, JoystickAxisType.Both, deadZone)
        {
            Type = joystick;
            DeadZone = deadZone;
        }

        #endregion
    }
}
