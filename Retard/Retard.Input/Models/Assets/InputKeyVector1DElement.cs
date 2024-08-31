using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using Retard.Input.Models;

namespace Retard.Input.Models.Assets
{
    /// <summary>
    /// Les touches pour actionner un seul axe (X ou Y).
    /// Il ne peut y avoir que 2 touches (positive et négative).
    /// </summary>
    public readonly struct InputKeyVector1DElement
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

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="mouseKey">L'entrée souris s'il y en a une</param>
        /// <param name="keyboardKey">L'entrée clavier s'il y en a une </param>
        /// <param name="gamePadKey">L'entrée manette s'il y en a une</param>
        [JsonConstructor]
        public InputKeyVector1DElement(MouseKey mouseKey, Keys keyboardKey, Buttons gamePadKey, JoystickKey joystickKey)
        {
            MouseKey = mouseKey;
            KeyboardKey = keyboardKey;
            GamePadKey = gamePadKey;
            JoystickKey = joystickKey;
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="mouseKey">L'entrée souris s'il y en a une</param>
        public InputKeyVector1DElement(MouseKey mouseKey) : this(mouseKey, Keys.None, Buttons.None, JoystickKey.None)
        {

        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="keyboardKey">L'entrée clavier s'il y en a une</param>
        public InputKeyVector1DElement(Keys keyboardKey) : this(MouseKey.None, keyboardKey, Buttons.None, JoystickKey.None)
        {

        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="gamePadKey">L'entrée manette s'il y en a une</param>
        public InputKeyVector1DElement(Buttons gamePadKey) : this(MouseKey.None, Keys.None, gamePadKey, JoystickKey.None)
        {

        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="joystickKey">La direction du joystick à évaluer</param>
        public InputKeyVector1DElement(JoystickKey joystickKey) : this(MouseKey.None, Keys.None, Buttons.None, joystickKey)
        {

        }

        #endregion
    }
}
