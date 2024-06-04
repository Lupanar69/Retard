using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Retard.Core.Models.Assets.Input;

namespace Retard.Core.ViewModels.Input
{
    /// <summary>
    /// Utilitaire pour gérer les entrées souris
    /// </summary>
    public class MouseInput : IInputScheme
    {
        #region Propriétés

        /// <summary>
        /// La position en pixels de la souris dans la fenêtre
        /// </summary>
        public Vector2 MousePos
        {
            get;
            private set;
        }

        /// <summary>
        /// La différence de position de la souris entre la frame actuelle
        /// et la précédente
        /// </summary>
        public Vector2 MousePosDelta
        {
            get;
            private set;
        }

        #endregion

        #region Variables statiques privées

        /// <summary>
        /// Les entrées lors de la frame actuelle
        /// </summary>
        private MouseState _curState;

        /// <summary>
        /// Les entrées lors de la frame précédente
        /// </summary>
        private MouseState _previousState;

        #endregion

        #region Méthodes statiques publiques

        /// <summary>
        /// Màj le MouseState actuel
        /// </summary>
        public void Update()
        {
            this._curState = Mouse.GetState();

            Point curMousePos = this._curState.Position;
            Point mouseDelta = this._curState.Position - this._previousState.Position;
            this.MousePosDelta = new Vector2(mouseDelta.X, mouseDelta.Y);
            this.MousePos = new Vector2(curMousePos.X, curMousePos.Y);
        }

        /// <summary>
        /// Màj le <see cref="_previousState"/>.
        /// A appeler en fin d'Update pour ne pas écraser le précédent MouseState
        /// avant les comparaisons
        /// </summary>
        public void AfterUpdate()
        {
            this._previousState = this._curState;
        }

        /// <summary>
        /// <see langword="true"/> si le bouton gauche de la souris est pressé
        /// </summary>
        /// <returns><see langword="true"/> si le bouton passe de l'état relâché à l'état pressé</returns>
        public bool LeftMousePressed()
        {
            return this._curState.LeftButton == ButtonState.Pressed && this._previousState.LeftButton == ButtonState.Released;
        }

        /// <summary>
        /// <see langword="true"/> si le bouton gauche de la souris est relâché
        /// </summary>
        /// <returns><see langword="true"/> si le bouton passe de l'état relâché à l'état pressé</returns>
        public bool LeftMouseReleased()
        {
            return this._curState.LeftButton == ButtonState.Released && this._previousState.LeftButton == ButtonState.Pressed;
        }

        /// <summary>
        /// <see langword="true"/> si le bouton gauche de la souris est maintenu enfoncé
        /// </summary>
        /// <returns><see langword="true"/> si le bouton est maintenu</returns>
        public bool LeftMouseHeld()
        {
            return this._curState.LeftButton == ButtonState.Pressed && this._previousState.LeftButton == ButtonState.Pressed;
        }

        /// <summary>
        /// <see langword="true"/> si le bouton droit de la souris est pressé
        /// </summary>
        /// <returns><see langword="true"/> si le bouton passe de l'état relâché à l'état pressé</returns>
        public bool RightMousePressed()
        {
            return this._curState.RightButton == ButtonState.Pressed && this._previousState.RightButton == ButtonState.Released;
        }

        /// <summary>
        /// <see langword="true"/> si le bouton droit de la souris est relâché
        /// </summary>
        /// <returns><see langword="true"/> si le bouton passe de l'état relâché à l'état pressé</returns>
        public bool RightMouseReleased()
        {
            return this._curState.RightButton == ButtonState.Released && this._previousState.RightButton == ButtonState.Pressed;
        }

        /// <summary>
        /// <see langword="true"/> si le bouton droit de la souris est maintenu enfoncé
        /// </summary>
        /// <returns><see langword="true"/> si le bouton est maintenu</returns>
        public bool RightMouseHeld()
        {
            return this._curState.RightButton == ButtonState.Pressed && this._previousState.RightButton == ButtonState.Pressed;
        }

        /// <summary>
        /// <see langword="true"/> si le bouton milieu de la souris est pressé
        /// </summary>
        /// <returns><see langword="true"/> si le bouton passe de l'état relâché à l'état pressé</returns>
        public bool MiddleMousePressed()
        {
            return this._curState.MiddleButton == ButtonState.Pressed && this._previousState.MiddleButton == ButtonState.Released;
        }

        /// <summary>
        /// <see langword="true"/> si le bouton milieu de la souris est relâché
        /// </summary>
        /// <returns><see langword="true"/> si le bouton passe de l'état relâché à l'état pressé</returns>
        public bool MiddleMouseReleased()
        {
            return this._curState.MiddleButton == ButtonState.Released && this._previousState.MiddleButton == ButtonState.Pressed;
        }

        /// <summary>
        /// <see langword="true"/> si le bouton milieu de la souris est maintenu enfoncé
        /// </summary>
        /// <returns><see langword="true"/> si le bouton est maintenu</returns>
        public bool MiddleMouseHeld()
        {
            return this._curState.MiddleButton == ButtonState.Pressed && this._previousState.MiddleButton == ButtonState.Pressed;
        }

        #endregion
    }
}
