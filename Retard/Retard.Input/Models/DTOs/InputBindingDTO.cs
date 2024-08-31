using Newtonsoft.Json;
using Retard.Core.Models.DTOs;
using Retard.Input.Models.Assets;

namespace Retard.Input.Models.DTOs
{
    /// <summary>
    /// Représente les données d'un InputBinding
    /// </summary>
    public sealed class InputBindingDTO : DataTransferObject
    {
        #region Propriétés

        /// <summary>
        /// La séquence d'entrées à réaliser pour exécuter l'action (ex: Ctrl+Z, Ctrl+clic gauche).
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public readonly InputKeySequenceElement[] KeySequence;

        /// <summary>
        /// Les touches pour actionner un seul axe (X ou Y).
        /// Il ne peut y avoir que 2 touches (positive et négative).
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public readonly InputKeyVector1DElement[] Vector1DKeys;

        /// <summary>
        /// Les touches pour actionner un axe 2D.
        /// Il ne peut y avoir que 4 touches, dans l'ordre : gauche, droite, haut, bas.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public readonly InputKeyVector2DElement[] Vector2DKeys;

        /// <summary>
        /// Contient les infos d'un binding utilisant un joystick
        /// (type du joystick, axe et zone inerte)
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public readonly InputBindingJoystick Joystick;

        /// <summary>
        /// Contient les infos d'un binding utilisant une gâchette
        /// (type de la gâchette et zone inerte)
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public readonly InputBindingTrigger Trigger;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="keySequence">La séquence d'entrées à réaliser pour exécuter l'action (ex: Ctrl+Z)</param>
        /// <param name="vector1DKeys">Les touches pour actionner un seul axe (X ou Y)</param>
        /// <param name="vector2DKeys">Les touches pour actionner un axe 2D</param>
        /// <param name="joystick">Le joystick utilisé pour les InputActions de type Vector1D et Vector2D</param>
        /// <param name="trigger">La gâchette utilisée pour les InputActions de type Vector1D</param>
        [JsonConstructor]
        public InputBindingDTO(InputKeySequenceElement[] keySequence, InputKeyVector1DElement[] vector1DKeys,
                               InputKeyVector2DElement[] vector2DKeys, InputBindingJoystick joystick, InputBindingTrigger trigger)
        {
            KeySequence = keySequence;
            Vector1DKeys = vector1DKeys;
            Vector2DKeys = vector2DKeys;
            Joystick = joystick;
            Trigger = trigger;
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="trigger">La gâchette utilisée pour les InputActions de type Vector1D</param>
        public InputBindingDTO(InputBindingTrigger trigger) : this(null, null, null, default, trigger)
        {

        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="joystick">Le joystick utilisé pour les InputActions de type Vector1D et Vector2D</param>
        public InputBindingDTO(InputBindingJoystick joystick) : this(null, null, null, joystick, default)
        {

        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="keySequence">La séquence d'entrées à réaliser pour exécuter l'action (ex: Ctrl+Z).
        /// Pour les axes, cela équivaut à un tableau d'entrées pour chaque
        /// extrémité de chaque axe.</param>
        public InputBindingDTO(params InputKeySequenceElement[] keySequence) : this(keySequence, null, null, default, default)
        {

        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="vector1DKeys">Les touches pour actionner un seul axe (X ou Y).
        /// Il ne peut y avoir que 2 touches (positive et négative).</param>
        public InputBindingDTO(params InputKeyVector1DElement[] vector1DKeys) : this(null, vector1DKeys, null, default, default)
        {

        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="vector2DKeys">Les touches pour actionner un axe 2D.
        /// Il ne peut y avoir que 4 touches, dans l'ordre : gauche, droite, haut, bas.</param>
        public InputBindingDTO(params InputKeyVector2DElement[] vector2DKeys) : this(null, null, vector2DKeys, default, default)
        {

        }

        #endregion
    }
}
