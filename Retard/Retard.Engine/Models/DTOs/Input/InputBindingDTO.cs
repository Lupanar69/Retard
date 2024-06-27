using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using Retard.Engine.Models.Assets.Input;

namespace Retard.Engine.Models.DTOs.Input
{
    /// <summary>
    /// Représente les données d'un InputBinding
    /// </summary>
    public sealed class InputBindingDTO
    {
        #region Propriétés

        /// <summary>
        /// La séquence d'entrées à réaliser pour exécuter l'action (ex: Ctrl+Z).
        /// Pour les axes, cela équivaut à un tableau d'entrées pour chaque
        /// extrémité de chaque axe.
        /// </summary>
        public InputKeySequenceElement[] KeySequence
        {
            get;
            private set;
        }

        /// <summary>
        /// Le joystick utilisé pour les InputActions de type Vector1D et Vector2D
        /// </summary>
        public JoystickType Joystick
        {
            get;
            private set;
        }

        /// <summary>
        /// L'axe de joystick à évaluer pour les InputActions de type Vector1D
        /// </summary>
        public JoystickAxis JoystickAxis
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
        /// <param name="joystickAxis">L'axe de joystick à évaluer pour les InputActions de type Vector1D</param>
        /// <param name="deadZone"> La valeur en dessous de laquelle l'input est considéré comme inerte</param>
        /// <param name="keySequence">La séquence d'entrées à réaliser pour exécuter l'action (ex: Ctrl+Z).
        /// Pour les axes, cela équivaut à un tableau d'entrées pour chaque
        /// extrémité de chaque axe.</param>
        [JsonConstructor]
        public InputBindingDTO(JoystickType joystick, JoystickAxis joystickAxis, float deadZone, InputKeySequenceElement[] keySequence)
        {
            this.Joystick = joystick;
            this.JoystickAxis = joystickAxis;
            this.DeadZone = deadZone;
            this.KeySequence = keySequence;
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="joystick">Le joystick utilisé pour les InputActions de type Vector1D et Vector2D</param>
        /// <param name="joystickAxis">L'axe de joystick à évaluer pour les InputActions de type Vector1D</param>
        /// <param name="deadZone"> La valeur en dessous de laquelle l'input est considéré comme inerte</param>
        public InputBindingDTO(JoystickType joystick, JoystickAxis joystickAxis, float deadZone) : this(joystick, joystickAxis, deadZone, null)
        {

        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="keySequence">La séquence d'entrées à réaliser pour exécuter l'action (ex: Ctrl+Z).
        /// Pour les axes, cela équivaut à un tableau d'entrées pour chaque
        /// extrémité de chaque axe.</param>
        public InputBindingDTO(params InputKeySequenceElement[] keySequence) : this(JoystickType.None, JoystickAxis.None, 0f, keySequence)
        {

        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="keyboardKeys">La séquence d'entrées à réaliser pour exécuter l'action (ex: Ctrl+Z).
        /// Pour les axes, cela équivaut à un tableau d'entrées pour chaque
        /// extrémité de chaque axe.</param>
        public InputBindingDTO(params Keys[] keyboardKeys) : this(JoystickType.None, JoystickAxis.None, 0f)
        {
            this.KeySequence = new InputKeySequenceElement[keyboardKeys.Length];

            for (int i = 0; i < keyboardKeys.Length; ++i)
            {
                this.KeySequence[i] = new InputKeySequenceElement(keyboardKeys[i], InputKeySequenceState.Held);
            }
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="gamePadButtons">La séquence d'entrées à réaliser pour exécuter l'action (ex: Ctrl+Z).
        /// Pour les axes, cela équivaut à un tableau d'entrées pour chaque
        /// extrémité de chaque axe.</param>
        public InputBindingDTO(params Buttons[] gamePadButtons) : this(JoystickType.None, JoystickAxis.None, 0f)
        {
            this.KeySequence = new InputKeySequenceElement[gamePadButtons.Length];

            for (int i = 0; i < gamePadButtons.Length; ++i)
            {
                this.KeySequence[i] = new InputKeySequenceElement(gamePadButtons[i], InputKeySequenceState.Held);
            }
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="joystick">Le joystick utilisé pour les InputActions de type Vector1D et Vector2D</param>
        /// <param name="joystickKeys">La séquence d'entrées à réaliser pour exécuter l'action (ex: Ctrl+Z).
        /// Pour les axes, cela équivaut à un tableau d'entrées pour chaque
        /// extrémité de chaque axe.</param>
        public InputBindingDTO(JoystickType joystick, params JoystickKey[] joystickKeys) : this(joystick, JoystickAxis.None, 0f)
        {
            this.KeySequence = new InputKeySequenceElement[joystickKeys.Length];

            for (int i = 0; i < joystickKeys.Length; ++i)
            {
                this.KeySequence[i] = new InputKeySequenceElement(joystickKeys[i], InputKeySequenceState.Held);
            }
        }

        #endregion
    }
}
