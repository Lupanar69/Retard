using Microsoft.Xna.Framework.Input;
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
    }
}
