using Microsoft.Xna.Framework.Input;

namespace Retard.Core.ViewModels.Input
{
    /// <summary>
    /// Utilitaire pour gérer les entrées souris
    /// </summary>
    public static class MouseInput
    {
        #region Variables statiques privées

        /// <summary>
        /// Les touches pressées lors de la frame précédente
        /// </summary>
        private static MouseState _previousState;

        #endregion

        #region Méthodes statiques publiques

        /// <summary>
        /// Màj le v
        /// A appeler en fin d'Update pour ne pas écraser le précédent MouseState
        /// avant les comparaisons
        /// </summary>
        public static void Update()
        {
            MouseInput._previousState = Mouse.GetState();
        }

        /// <summary>
        /// <see langword="true"/> si le bouton gauche de la souris est pressé
        /// </summary>
        /// <returns><see langword="true"/> si le bouton passe de l'état relâché à l'état pressé</returns>
        public static bool LeftMousePressed()
        {
            return Mouse.GetState().LeftButton == ButtonState.Pressed && MouseInput._previousState.LeftButton == ButtonState.Released;
        }

        /// <summary>
        /// <see langword="true"/> si le bouton gauche de la souris est relâché
        /// </summary>
        /// <returns><see langword="true"/> si le bouton passe de l'état relâché à l'état pressé</returns>
        public static bool LeftMouseReleased()
        {
            return Mouse.GetState().LeftButton == ButtonState.Released && MouseInput._previousState.LeftButton == ButtonState.Pressed;
        }

        /// <summary>
        /// <see langword="true"/> si le bouton droit de la souris est pressé
        /// </summary>
        /// <returns><see langword="true"/> si le bouton passe de l'état relâché à l'état pressé</returns>
        public static bool RightMousePressed()
        {
            return Mouse.GetState().RightButton == ButtonState.Pressed && MouseInput._previousState.RightButton == ButtonState.Released;
        }

        /// <summary>
        /// <see langword="true"/> si le bouton droit de la souris est relâché
        /// </summary>
        /// <returns><see langword="true"/> si le bouton passe de l'état relâché à l'état pressé</returns>
        public static bool RightMouseReleased()
        {
            return Mouse.GetState().RightButton == ButtonState.Released && MouseInput._previousState.RightButton == ButtonState.Pressed;
        }

        /// <summary>
        /// <see langword="true"/> si le bouton milieu de la souris est pressé
        /// </summary>
        /// <returns><see langword="true"/> si le bouton passe de l'état relâché à l'état pressé</returns>
        public static bool MiddleMousePressed()
        {
            return Mouse.GetState().MiddleButton == ButtonState.Pressed && MouseInput._previousState.MiddleButton == ButtonState.Released;
        }

        /// <summary>
        /// <see langword="true"/> si le bouton milieu de la souris est relâché
        /// </summary>
        /// <returns><see langword="true"/> si le bouton passe de l'état relâché à l'état pressé</returns>
        public static bool MiddleMouseReleased()
        {
            return Mouse.GetState().MiddleButton == ButtonState.Released && MouseInput._previousState.MiddleButton == ButtonState.Pressed;
        }

        #endregion
    }
}
