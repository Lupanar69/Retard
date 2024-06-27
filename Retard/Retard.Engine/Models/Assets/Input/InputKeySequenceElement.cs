using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using Retard.Core.Models.Assets.Input;

namespace Retard.Engine.Models.Assets.Input
{
    /// <summary>
    /// Un élément d'une séquence d'entrées à réaliser 
    /// pour exécuter une action.
    /// </summary>
    public struct InputKeySequenceElement
    {
        #region Variables d'instance

        /// <summary>
        /// Le bouton de la souris à évaluer
        /// </summary>
        public MouseKey MouseKey
        {
            get;
            private set;
        }

        /// <summary>
        /// Le bouton du clavier à évaluer
        /// </summary>
        public Keys KeyboardKey
        {
            get;
            private set;
        }

        /// <summary>
        /// Le bouton de la manette à évaluer
        /// </summary>
        public Buttons GamePadKey
        {
            get;
            private set;
        }

        /// <summary>
        /// La direction du joystick à évaluer
        /// </summary>
        public JoystickKey JoystickKey
        {
            get;
            private set;
        }

        /// <summary>
        /// L'état que doit avoir un InputKeySequenceElement
        /// pour être considéré actif
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public InputKeySequenceState ValidState
        {
            get;
            private set;
        }

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
