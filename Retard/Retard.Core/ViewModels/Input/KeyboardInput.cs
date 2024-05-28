using Microsoft.Xna.Framework.Input;

namespace Retard.Core.ViewModels.Input
{
    /// <summary>
    /// Utilitaire pour gérer les entrées clavier
    /// </summary>
    public static class KeyboardInput
    {
        #region Variables statiques privées

        /// <summary>
        /// Les entrées lors de la frame actuelle
        /// </summary>
        private static KeyboardState _curState;

        /// <summary>
        /// Les entrées lors de la frame précédente
        /// </summary>
        private static KeyboardState _previousState;

        #endregion

        #region Méthodes statiques publiques

        /// <summary>
        /// Màj le KeyboardState
        /// </summary>
        public static void Update()
        {
            KeyboardInput._curState = Keyboard.GetState();
        }

        /// <summary>
        /// Màj le KeyboardState
        /// A appeler en fin d'Update pour ne pas écraser le précédent KeyboardState
        /// avant les comparaisons
        /// </summary>
        public static void AfterUpdate()
        {
            KeyboardInput._previousState = KeyboardInput._curState;
        }

        /// <summary>
        /// <see langword="true"/> si la touche passe de l'état relâché à l'état pressé
        /// </summary>
        /// <param name="key">La touche pressée</param>
        /// <returns><see langword="true"/> si la touche passe de l'état relâché à l'état pressé</returns>
        public static bool IsKeyPressed(Keys key)
        {
            return KeyboardInput._curState.IsKeyDown(key) && KeyboardInput._previousState.IsKeyUp(key);
        }

        /// <summary>
        /// <see langword="true"/> si la touche passe de l'état pressé à l'état relâché
        /// </summary>
        /// <param name="key">La touche relâchée</param>
        /// <returns><see langword="true"/> si la touche passe de l'état pressé à l'état relâché</returns>
        public static bool IsKeyReleased(Keys key)
        {
            return KeyboardInput._curState.IsKeyUp(key) && KeyboardInput._previousState.IsKeyDown(key);
        }

        /// <summary>
        /// <see langword="true"/> si la touche est maintenue enfoncée
        /// </summary>
        /// <param name="key">La touche maintenue enfoncée</param>
        /// <returns><see langword="true"/> si la touche est maintenue enfoncée</returns>
        public static bool IsKeyHeld(Keys key)
        {
            return KeyboardInput._curState.IsKeyDown(key) && KeyboardInput._previousState.IsKeyDown(key);
        }

        #endregion
    }
}
