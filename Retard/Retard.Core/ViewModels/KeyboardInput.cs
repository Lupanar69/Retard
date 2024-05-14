using Microsoft.Xna.Framework.Input;

namespace Retard.Core.ViewModels
{
    /// <summary>
    /// Utilitaire pour gérer les entrées clavier
    /// </summary>
    public static class KeyboardInput
    {
        #region Variables statiques privées

        /// <summary>
        /// Les touches pressées lors de la frame précédente
        /// </summary>
        private static KeyboardState _previousState;

        #endregion

        #region Méthodes statiques publiques

        /// <summary>
        /// Màj le keyboardState
        /// </summary>
        public static void RefreshKeyboardState()
        {
            KeyboardInput._previousState = Keyboard.GetState();
        }

        /// <summary>
        /// <see langword="true"/> si la touche passe de l'état relâché à l'état pressé
        /// </summary>
        /// <param name="key">La touche pressée</param>
        /// <returns><see langword="true"/> si la touche passe de l'état relâché à l'état pressé</returns>
        public static bool IsKeyDown(Keys key)
        {
            return Keyboard.GetState().IsKeyDown(key) && KeyboardInput._previousState.IsKeyUp(key);
        }

        /// <summary>
        /// <see langword="true"/> si la touche passe de l'état pressé à l'état relâché
        /// </summary>
        /// <param name="key">La touche relâchée</param>
        /// <returns><see langword="true"/> si la touche passe de l'état pressé à l'état relâché</returns>
        public static bool IsKeyUp(Keys key)
        {
            return Keyboard.GetState().IsKeyUp(key) && KeyboardInput._previousState.IsKeyDown(key);
        }

        #endregion
    }
}
