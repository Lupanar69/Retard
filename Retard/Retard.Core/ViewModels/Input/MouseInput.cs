using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Retard.Core.ViewModels.Input
{
    /// <summary>
    /// Utilitaire pour gérer les entrées souris
    /// </summary>
    public static class MouseInput
    {
        #region Propriétés

        /// <summary>
        /// La position en pixels de la souris dans la fenêtre
        /// </summary>
        public static Vector2 MousePos
        {
            get;
            private set;
        }

        /// <summary>
        /// La différence de position de la souris entre la frame actuelle
        /// et la précédente
        /// </summary>
        public static Vector2 MousePosDelta
        {
            get;
            private set;
        }

        #endregion

        #region Variables statiques privées

        /// <summary>
        /// Les entrées lors de la frame actuelle
        /// </summary>
        private static MouseState _curState;

        /// <summary>
        /// Les entrées lors de la frame précédente
        /// </summary>
        private static MouseState _previousState;

        #endregion

        #region Méthodes statiques publiques

        /// <summary>
        /// Màj le MouseState actuel
        /// </summary>
        public static void Update()
        {
            MouseInput._curState = Mouse.GetState();

            Point curMousePos = MouseInput._curState.Position;
            Point mouseDelta = MouseInput._curState.Position - MouseInput._previousState.Position;
            MouseInput.MousePosDelta = new Vector2(mouseDelta.X, mouseDelta.Y);
            MouseInput.MousePos = new Vector2(curMousePos.X, curMousePos.Y);
        }

        /// <summary>
        /// Màj le <see cref="_previousState"/>.
        /// A appeler en fin d'Update pour ne pas écraser le précédent MouseState
        /// avant les comparaisons
        /// </summary>
        public static void AfterUpdate()
        {
            MouseInput._previousState = MouseInput._curState;
        }

        /// <summary>
        /// <see langword="true"/> si le bouton gauche de la souris est pressé
        /// </summary>
        /// <returns><see langword="true"/> si le bouton passe de l'état relâché à l'état pressé</returns>
        public static bool LeftMousePressed()
        {
            return MouseInput._curState.LeftButton == ButtonState.Pressed && MouseInput._previousState.LeftButton == ButtonState.Released;
        }

        /// <summary>
        /// <see langword="true"/> si le bouton gauche de la souris est relâché
        /// </summary>
        /// <returns><see langword="true"/> si le bouton passe de l'état relâché à l'état pressé</returns>
        public static bool LeftMouseReleased()
        {
            return MouseInput._curState.LeftButton == ButtonState.Released && MouseInput._previousState.LeftButton == ButtonState.Pressed;
        }

        /// <summary>
        /// <see langword="true"/> si le bouton gauche de la souris est maintenu enfoncé
        /// </summary>
        /// <returns><see langword="true"/> si le bouton est maintenu</returns>
        public static bool LeftMouseHeld()
        {
            return MouseInput._curState.LeftButton == ButtonState.Pressed && MouseInput._previousState.LeftButton == ButtonState.Pressed;
        }

        /// <summary>
        /// <see langword="true"/> si le bouton droit de la souris est pressé
        /// </summary>
        /// <returns><see langword="true"/> si le bouton passe de l'état relâché à l'état pressé</returns>
        public static bool RightMousePressed()
        {
            return MouseInput._curState.RightButton == ButtonState.Pressed && MouseInput._previousState.RightButton == ButtonState.Released;
        }

        /// <summary>
        /// <see langword="true"/> si le bouton droit de la souris est relâché
        /// </summary>
        /// <returns><see langword="true"/> si le bouton passe de l'état relâché à l'état pressé</returns>
        public static bool RightMouseReleased()
        {
            return MouseInput._curState.RightButton == ButtonState.Released && MouseInput._previousState.RightButton == ButtonState.Pressed;
        }

        /// <summary>
        /// <see langword="true"/> si le bouton droit de la souris est maintenu enfoncé
        /// </summary>
        /// <returns><see langword="true"/> si le bouton est maintenu</returns>
        public static bool RightMouseHeld()
        {
            return MouseInput._curState.RightButton == ButtonState.Pressed && MouseInput._previousState.RightButton == ButtonState.Pressed;
        }

        /// <summary>
        /// <see langword="true"/> si le bouton milieu de la souris est pressé
        /// </summary>
        /// <returns><see langword="true"/> si le bouton passe de l'état relâché à l'état pressé</returns>
        public static bool MiddleMousePressed()
        {
            return MouseInput._curState.MiddleButton == ButtonState.Pressed && MouseInput._previousState.MiddleButton == ButtonState.Released;
        }

        /// <summary>
        /// <see langword="true"/> si le bouton milieu de la souris est relâché
        /// </summary>
        /// <returns><see langword="true"/> si le bouton passe de l'état relâché à l'état pressé</returns>
        public static bool MiddleMouseReleased()
        {
            return MouseInput._curState.MiddleButton == ButtonState.Released && MouseInput._previousState.MiddleButton == ButtonState.Pressed;
        }

        /// <summary>
        /// <see langword="true"/> si le bouton milieu de la souris est maintenu enfoncé
        /// </summary>
        /// <returns><see langword="true"/> si le bouton est maintenu</returns>
        public static bool MiddleMouseHeld()
        {
            return MouseInput._curState.MiddleButton == ButtonState.Pressed && MouseInput._previousState.MiddleButton == ButtonState.Pressed;
        }

        #endregion
    }
}
