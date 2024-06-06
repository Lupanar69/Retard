using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using Retard.Core.Models.Assets.Input;

namespace Retard.Core.Models.DTOs.Input
{
    /// <summary>
    /// Représente les données d'un InputBinding
    /// </summary>
    public sealed class InputBindingDTO
    {
        #region Propriétés

        /// <summary>
        /// Le bouton de la souris à évaluer
        /// </summary>
        public MouseKey MouseKey
        {
            get;
            private set;
        }

        /// <summary>
        /// Le bouton du clavier à évaluer (ou LES boutons, si c'est un axe)
        /// </summary>
        public Keys[] KeyboardKeys
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
        /// Le type d'axe de joystick représenté par ce binding
        /// </summary>
        public InputBindingAxisType AxisType
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
        /// <param name="mouseKey">L'entrée souris s'il y en a une</param>
        /// <param name="keyboardKey">L'entrée clavier s'il y en a une (ou LES entrées, si c'est un axe)</param>
        /// <param name="gamePadKey">L'entrée manette s'il y en a une</param>
        /// <param name="axisType">Le type d'axe s'il y en a un</param>
        /// <param name="deadZone">La zone morte de l'entrée si c'est un axe</param>
        [JsonConstructor]
        public InputBindingDTO(MouseKey mouseKey, Keys[] keyboardKey, Buttons gamePadKey, InputBindingAxisType axisType, float deadZone)
        {
            this.MouseKey = mouseKey;
            this.KeyboardKeys = keyboardKey;
            this.GamePadKey = gamePadKey;
            this.AxisType = axisType;
            this.DeadZone = deadZone;
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="mouseKey">L'entrée souris s'il y en a une</param>
        public InputBindingDTO(MouseKey mouseKey) : this(mouseKey, null, Buttons.None, InputBindingAxisType.None, 0f)
        {

        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="keyboardKeys">L'entrée clavier s'il y en a une (ou LES entrées, si c'est un axe)</param>
        public InputBindingDTO(params Keys[] keyboardKeys) : this(MouseKey.None, keyboardKeys, Buttons.None, InputBindingAxisType.None, 0f)
        {

        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="gamePadKey">L'entrée manette s'il y en a une</param>
        public InputBindingDTO(Buttons gamePadKey) : this(MouseKey.None, null, gamePadKey, InputBindingAxisType.None, 0f)
        {

        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="gamePadKey">L'entrée manette s'il y en a une</param>
        /// <param name="axisType">Le type d'axe s'il y en a un</param>
        /// <param name="deadZone">La zone morte de l'entrée si c'est un axe</param>
        public InputBindingDTO(Buttons gamePadKey, InputBindingAxisType axisType, float deadZone) : this(MouseKey.None, null, gamePadKey, axisType, deadZone)
        {

        }

        #endregion
    }
}
