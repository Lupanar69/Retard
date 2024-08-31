using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using Retard.Input.Models;

namespace Retard.Input.Models.Assets
{
    /// <summary>
    /// Un élément d'une séquence d'entrées à réaliser 
    /// pour exécuter une action.
    /// </summary>
    public readonly struct InputKeySequenceElement
    {
        #region Variables d'instance

        /// <summary>
        /// Le bouton de la souris à évaluer
        /// </summary>
        public readonly MouseKey MouseKey;

        /// <summary>
        /// Le bouton du clavier à évaluer
        /// </summary>
        public readonly Keys KeyboardKey;

        /// <summary>
        /// Le bouton de la manette à évaluer
        /// </summary>
        public readonly Buttons GamePadKey;

        /// <summary>
        /// La direction du joystick à évaluer
        /// </summary>
        public readonly JoystickKey JoystickKey;

        /// <summary>
        /// L'état que doit avoir un InputKeySequenceElement
        /// pour être considéré actif
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public readonly InputKeySequenceState ValidState;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="mouseKey">L'entrée souris s'il y en a une</param>
        /// <param name="keyboardKey">L'entrée clavier s'il y en a une </param>
        /// <param name="gamePadKey">L'entrée manette s'il y en a une</param>
        /// <param name="validState"> L'état que doit avoir un InputKeySequenceElement pour être considéré actif</param>
        [JsonConstructor]
        public InputKeySequenceElement(MouseKey mouseKey, Keys keyboardKey, Buttons gamePadKey, JoystickKey joystickKey, InputKeySequenceState validState)
        {
            MouseKey = mouseKey;
            KeyboardKey = keyboardKey;
            GamePadKey = gamePadKey;
            ValidState = validState;
            JoystickKey = joystickKey;
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="mouseKey">L'entrée souris s'il y en a une</param>
        /// <param name="validState"> L'état que doit avoir un InputKeySequenceElement pour être considéré actif</param>
        public InputKeySequenceElement(MouseKey mouseKey, InputKeySequenceState validState) : this(mouseKey, Keys.None, Buttons.None, JoystickKey.None, validState)
        {

        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="keyboardKey">L'entrée clavier s'il y en a une</param>
        /// <param name="validState"> L'état que doit avoir un InputKeySequenceElement pour être considéré actif</param>
        public InputKeySequenceElement(Keys keyboardKey, InputKeySequenceState validState) : this(MouseKey.None, keyboardKey, Buttons.None, JoystickKey.None, validState)
        {

        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="gamePadKey">L'entrée manette s'il y en a une</param>
        /// <param name="validState"> L'état que doit avoir un InputKeySequenceElement pour être considéré actif</param>
        public InputKeySequenceElement(Buttons gamePadKey, InputKeySequenceState validState) : this(MouseKey.None, Keys.None, gamePadKey, JoystickKey.None, validState)
        {

        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="joystickKey">La direction du joystick à évaluer</param>
        /// <param name="validState"> L'état que doit avoir un InputKeySequenceElement pour être considéré actif</param>
        public InputKeySequenceElement(JoystickKey joystickKey, InputKeySequenceState validState) : this(MouseKey.None, Keys.None, Buttons.None, joystickKey, validState)
        {

        }

        #endregion
    }
}
