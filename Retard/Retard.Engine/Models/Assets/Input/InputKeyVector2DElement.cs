using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;

namespace Retard.Engine.Models.Assets.Input
{
    /// <summary>
    /// Les touches pour actionner un axe 2D.
    /// Il ne peut y avoir que 4 touches, dans l'ordre : gauche, droite, haut, bas.
    /// </summary>
    public readonly struct InputKeyVector2DElement
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
        public InputKeyVector2DElement(MouseKey mouseKey, Keys keyboardKey, Buttons gamePadKey, JoystickKey joystickKey)
        {
            this.MouseKey = mouseKey;
            this.KeyboardKey = keyboardKey;
            this.GamePadKey = gamePadKey;
            this.JoystickKey = joystickKey;
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="mouseKey">L'entrée souris s'il y en a une</param>
        public InputKeyVector2DElement(MouseKey mouseKey) : this(mouseKey, Keys.None, Buttons.None, JoystickKey.None)
        {

        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="keyboardKey">L'entrée clavier s'il y en a une</param>
        public InputKeyVector2DElement(Keys keyboardKey) : this(MouseKey.None, keyboardKey, Buttons.None, JoystickKey.None)
        {

        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="gamePadKey">L'entrée manette s'il y en a une</param>
        public InputKeyVector2DElement(Buttons gamePadKey) : this(MouseKey.None, Keys.None, gamePadKey, JoystickKey.None)
        {

        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="joystickKey">La direction du joystick à évaluer</param>
        public InputKeyVector2DElement(JoystickKey joystickKey) : this(MouseKey.None, Keys.None, Buttons.None, joystickKey)
        {

        }

        #endregion
    }
}
