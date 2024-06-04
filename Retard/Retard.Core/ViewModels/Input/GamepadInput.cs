using Arch.LowLevel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Retard.Core.Models.Assets.Input;

namespace Retard.Core.ViewModels.Input
{
    /// <summary>
    /// Utilitaire pour gérer les entrées manette
    /// </summary>
    public class GamePadInput : IInputScheme
    {
        #region Variables statiques privées

        /// <summary>
        /// Les entrées lors de la frame actuelle
        /// pour chaque manette connectée
        /// </summary>
        private readonly UnsafeArray<GamePadState> _curStates;

        /// <summary>
        /// Les entrées lors de la frame précédente
        /// pour chaque manette connectée
        /// </summary>
        private readonly UnsafeArray<GamePadState> _previousStates;

        /// <summary>
        /// Les axes des joysticks gauche de chaque manette
        /// </summary>
        private readonly UnsafeArray<Vector2> _leftThumbsticksAxes;

        /// <summary>
        /// Les axes des joysticks droit de chaque manette
        /// </summary>
        private readonly UnsafeArray<Vector2> _rightThumbsticksAxes;

        /// <summary>
        /// Le niveau de pression des gâchettes gauche de chaque manette
        /// </summary>
        private readonly UnsafeArray<float> _leftTriggersValues;

        /// <summary>
        /// Le niveau de pression des gâchettes droit de chaque manette
        /// </summary>
        private readonly UnsafeArray<float> _rightTriggersValues;

        /// <summary>
        /// Le nombre max de manettes pouvant être utilisées
        /// à la fois.
        /// Par défaut, Monogame peut accepter jusqu'à 16 manettes.
        /// </summary>
        private readonly int _nbMaxGamePads;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        public GamePadInput() : this(1) { }

        /// <summary>
        /// Constructeur
        /// </summary>
        public GamePadInput(int nbMaxGamePads = 1)
        {
            this._nbMaxGamePads = nbMaxGamePads;
            this._curStates = new UnsafeArray<GamePadState>(this._nbMaxGamePads);
            this._previousStates = new UnsafeArray<GamePadState>(this._nbMaxGamePads);
            this._leftThumbsticksAxes = new UnsafeArray<Vector2>(this._nbMaxGamePads);
            this._rightThumbsticksAxes = new UnsafeArray<Vector2>(this._nbMaxGamePads);
            this._leftTriggersValues = new UnsafeArray<float>(this._nbMaxGamePads);
            this._rightTriggersValues = new UnsafeArray<float>(this._nbMaxGamePads);
        }

        #endregion

        #region Méthodes statiques publiques

        /// <summary>
        /// Màj le KeyboardState
        /// </summary>
        public void Update()
        {
            for (int i = 0; i < this._nbMaxGamePads; i++)
            {
                GamePadState state = GamePad.GetState(i);
                this._curStates[i] = state;
                this._leftThumbsticksAxes[i] = state.ThumbSticks.Left;
                this._rightThumbsticksAxes[i] = state.ThumbSticks.Right;
                this._leftTriggersValues[i] = state.Triggers.Left;
                this._rightTriggersValues[i] = state.Triggers.Right;
            }
        }

        /// <summary>
        /// Màj le KeyboardState
        /// A appeler en fin d'Update pour ne pas écraser le précédent KeyboardState
        /// avant les comparaisons
        /// </summary>
        public void AfterUpdate()
        {
            for (int i = 0; i < this._nbMaxGamePads; i++)
            {
                this._previousStates[i] = GamePad.GetState(i);
            }
        }

        /// <summary>
        /// Obtient l'axe du joystick gauche de la manette sélectionnée
        /// </summary>
        /// <param name="playerIndex">L'ID de la manette</param>
        /// <returns>La valeur de l'axe du joystick gauche de la manette</returns>
        public Vector2 GetLeftThumbstickAxis(int playerIndex)
        {
            return this._leftThumbsticksAxes[playerIndex];
        }

        /// <summary>
        /// Obtient l'axe du joystick droit de la manette sélectionnée
        /// </summary>
        /// <param name="playerIndex">L'ID de la manette</param>
        /// <returns>La valeur de l'axe du joystick droit de la manette</returns>
        public Vector2 GetRightThumbstickAxis(int playerIndex)
        {
            return this._rightThumbsticksAxes[playerIndex];
        }

        /// <summary>
        /// Obtient le niveau de pression de la gâchette gauche de la manette sélectionnée
        /// </summary>
        /// <param name="playerIndex">L'ID de la manette</param>
        /// <returns>Le niveau de pression de la gâchette gauche de la manette</returns>
        public float GetLeftTriggerValue(int playerIndex)
        {
            return this._leftTriggersValues[playerIndex];
        }

        /// <summary>
        /// Obtient le niveau de pression de la gâchette droit de la manette sélectionnée
        /// </summary>
        /// <param name="playerIndex">L'ID de la manette</param>
        /// <returns>Le niveau de pression de la gâchette droit de la manette</returns>
        public float GetRightTriggerValue(int playerIndex)
        {
            return this._rightTriggersValues[playerIndex];
        }

        /// <summary>
        /// <see langword="true"/> si le bouton passe de l'état relâché à l'état pressé
        /// </summary>
        /// <param name="playerIndex">L'ID de la manette</param>
        /// <param name="btn">Le bouton pressé</param>
        /// <returns><see langword="true"/> si le bouton passe de l'état relâché à l'état pressé</returns>
        public bool IsButtonPressed(int playerIndex, Buttons btn)
        {
            return this._curStates[playerIndex].IsButtonDown(btn) && this._previousStates[playerIndex].IsButtonUp(btn);
        }

        /// <summary>
        /// <see langword="true"/> si le bouton passe de l'état pressé à l'état relâché
        /// </summary>
        /// <param name="playerIndex">L'ID de la manette</param>
        /// <param name="btn">Le bouton pressé</param>
        /// <returns><see langword="true"/> si le bouton passe de l'état pressé à l'état relâché</returns>
        public bool IsButtonReleased(int playerIndex, Buttons btn)
        {
            return this._curStates[playerIndex].IsButtonUp(btn) && this._previousStates[playerIndex].IsButtonDown(btn);
        }

        /// <summary>
        /// <see langword="true"/> si le bouton est maintenu enfoncé
        /// </summary>
        /// <param name="playerIndex">L'ID de la manette</param>
        /// <param name="btn">Le bouton maintenu enfoncé</param>
        /// <returns><see langword="true"/> si le bouton est maintenu enfoncé</returns>
        public bool IsButtonHeld(int playerIndex, Buttons btn)
        {
            return this._curStates[playerIndex].IsButtonDown(btn) && this._previousStates[playerIndex].IsButtonDown(btn);
        }

        #endregion
    }
}
