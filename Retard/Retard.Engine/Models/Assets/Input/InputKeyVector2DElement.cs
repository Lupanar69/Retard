using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using Retard.Core.Models.Assets.Input;

namespace Retard.Engine.Models.Assets.Input
{
    /// <summary>
    /// Les touches pour actionner un axe 2D.
    /// Il ne peut y avoir que 4 touches, dans l'ordre : gauche, droite, haut, bas.
    /// </summary>
    public struct InputKeyVector2DElement
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
            MouseKey = mouseKey;
            KeyboardKey = keyboardKey;
            GamePadKey = gamePadKey;
            JoystickKey = joystickKey;
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
