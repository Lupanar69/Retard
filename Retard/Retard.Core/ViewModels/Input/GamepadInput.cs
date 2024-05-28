using Arch.LowLevel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Retard.Core.ViewModels.Input
{
    /// <summary>
    /// Utilitaire pour gérer les entrées manette
    /// </summary>
    public static class GamePadInput
    {
        #region Variables statiques privées

        /// <summary>
        /// Les entrées lors de la frame actuelle
        /// pour chaque manette connectée
        /// </summary>
        private static UnsafeArray<GamePadState> _curStates;

        /// <summary>
        /// Les entrées lors de la frame précédente
        /// pour chaque manette connectée
        /// </summary>
        private static UnsafeArray<GamePadState> _previousStates;

        /// <summary>
        /// Les axes des joysticks gauche de chaque manette
        /// </summary>
        private static UnsafeArray<Vector2> _leftThumbsticksAxes;

        /// <summary>
        /// Les axes des joysticks droit de chaque manette
        /// </summary>
        private static UnsafeArray<Vector2> _rightThumbsticksAxes;

        /// <summary>
        /// Le niveau de pression des gâchettes gauche de chaque manette
        /// </summary>
        private static UnsafeArray<float> _leftTriggersValues;

        /// <summary>
        /// Le niveau de pression des gâchettes droit de chaque manette
        /// </summary>
        private static UnsafeArray<float> _rightTriggersValues;

        /// <summary>
        /// Le nombre max de manettes pouvant être utilisées
        /// à la fois
        /// </summary>
        private static int _nbMaxGamePads = GamePad.MaximumGamePadCount;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        static GamePadInput()
        {
            // Monogame peut accepter jusqu'à 16 manettes

            GamePadInput._curStates = new UnsafeArray<GamePadState>(GamePadInput._nbMaxGamePads);
            GamePadInput._previousStates = new UnsafeArray<GamePadState>(GamePadInput._nbMaxGamePads);
            GamePadInput._leftThumbsticksAxes = new UnsafeArray<Vector2>(GamePadInput._nbMaxGamePads);
            GamePadInput._rightThumbsticksAxes = new UnsafeArray<Vector2>(GamePadInput._nbMaxGamePads);
            GamePadInput._leftTriggersValues = new UnsafeArray<float>(GamePadInput._nbMaxGamePads);
            GamePadInput._rightTriggersValues = new UnsafeArray<float>(GamePadInput._nbMaxGamePads);
        }

        #endregion

        #region Méthodes statiques publiques

        /// <summary>
        /// Màj le KeyboardState
        /// </summary>
        public static void Update()
        {
            for (int i = 0; i < GamePadInput._nbMaxGamePads; i++)
            {
                GamePadState state = GamePad.GetState(i);
                GamePadInput._curStates[i] = state;
                GamePadInput._leftThumbsticksAxes[i] = state.ThumbSticks.Left;
                GamePadInput._rightThumbsticksAxes[i] = state.ThumbSticks.Right;
                GamePadInput._leftTriggersValues[i] = state.Triggers.Left;
                GamePadInput._rightTriggersValues[i] = state.Triggers.Right;
            }
        }

        /// <summary>
        /// Màj le KeyboardState
        /// A appeler en fin d'Update pour ne pas écraser le précédent KeyboardState
        /// avant les comparaisons
        /// </summary>
        public static void AfterUpdate()
        {
            for (int i = 0; i < GamePadInput._nbMaxGamePads; i++)
            {
                GamePadInput._previousStates[i] = GamePad.GetState(i);
            }
        }

        /// <summary>
        /// Obtient l'axe du joystick gauche de la manette sélectionnée
        /// </summary>
        /// <param name="playerIndex">L'ID de la manette</param>
        /// <returns>La valeur de l'axe du joystick gauche de la manette</returns>
        public static Vector2 GetLeftThumbstickAxis(int playerIndex)
        {
            return GamePadInput._leftThumbsticksAxes[playerIndex];
        }

        /// <summary>
        /// Obtient l'axe du joystick droit de la manette sélectionnée
        /// </summary>
        /// <param name="playerIndex">L'ID de la manette</param>
        /// <returns>La valeur de l'axe du joystick droit de la manette</returns>
        public static Vector2 GetRightThumbstickAxis(int playerIndex)
        {
            return GamePadInput._rightThumbsticksAxes[playerIndex];
        }

        /// <summary>
        /// Obtient le niveau de pression de la gâchette gauche de la manette sélectionnée
        /// </summary>
        /// <param name="playerIndex">L'ID de la manette</param>
        /// <returns>Le niveau de pression de la gâchette gauche de la manette</returns>
        public static float GetLeftTriggerValue(int playerIndex)
        {
            return GamePadInput._leftTriggersValues[playerIndex];
        }

        /// <summary>
        /// Obtient le niveau de pression de la gâchette droit de la manette sélectionnée
        /// </summary>
        /// <param name="playerIndex">L'ID de la manette</param>
        /// <returns>Le niveau de pression de la gâchette droit de la manette</returns>
        public static float GetRightTriggerValue(int playerIndex)
        {
            return GamePadInput._rightTriggersValues[playerIndex];
        }

        /// <summary>
        /// <see langword="true"/> si le bouton passe de l'état relâché à l'état pressé
        /// </summary>
        /// <param name="playerIndex">L'ID de la manette</param>
        /// <param name="btn">Le bouton pressé</param>
        /// <returns><see langword="true"/> si le bouton passe de l'état relâché à l'état pressé</returns>
        public static bool IsButtonPressed(int playerIndex, Buttons btn)
        {
            return GamePadInput._curStates[playerIndex].IsButtonDown(btn) && GamePadInput._previousStates[playerIndex].IsButtonUp(btn);
        }

        /// <summary>
        /// <see langword="true"/> si le bouton passe de l'état pressé à l'état relâché
        /// </summary>
        /// <param name="playerIndex">L'ID de la manette</param>
        /// <param name="btn">Le bouton pressé</param>
        /// <returns><see langword="true"/> si le bouton passe de l'état pressé à l'état relâché</returns>
        public static bool IsButtonReleased(int playerIndex, Buttons btn)
        {
            return GamePadInput._curStates[playerIndex].IsButtonUp(btn) && GamePadInput._previousStates[playerIndex].IsButtonDown(btn);
        }

        /// <summary>
        /// <see langword="true"/> si le bouton est maintenu enfoncé
        /// </summary>
        /// <param name="playerIndex">L'ID de la manette</param>
        /// <param name="btn">Le bouton maintenu enfoncé</param>
        /// <returns><see langword="true"/> si le bouton est maintenu enfoncé</returns>
        public static bool IsButtonHeld(int playerIndex, Buttons btn)
        {
            return GamePadInput._curStates[playerIndex].IsButtonDown(btn) && GamePadInput._previousStates[playerIndex].IsButtonDown(btn);
        }

        #endregion
    }
}
