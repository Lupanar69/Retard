using Microsoft.Xna.Framework.Input;
using Retard.Core.Models.Assets.Input;

namespace Retard.Core.ViewModels.Input
{
    /// <summary>
    /// Utilitaire pour gérer les entrées clavier
    /// </summary>
    public sealed class KeyboardInput : IInputScheme
    {
        #region Variables statiques privées

        /// <summary>
        /// Les entrées lors de la frame actuelle
        /// </summary>
        private KeyboardState _curState;

        /// <summary>
        /// Les entrées lors de la frame précédente
        /// </summary>
        private KeyboardState _previousState;

        #endregion

        #region Méthodes statiques publiques

        /// <summary>
        /// Màj le KeyboardState
        /// </summary>
        public void Update()
        {
            this._curState = Keyboard.GetState();
        }

        /// <summary>
        /// Màj le KeyboardState
        /// A appeler en fin d'Update pour ne pas écraser le précédent KeyboardState
        /// avant les comparaisons
        /// </summary>
        public void AfterUpdate()
        {
            this._previousState = this._curState;
        }

        /// <summary>
        /// <see langword="true"/> si la touche passe de l'état relâché à l'état pressé
        /// </summary>
        /// <param name="key">La touche pressée</param>
        /// <returns><see langword="true"/> si la touche passe de l'état relâché à l'état pressé</returns>
        public bool IsKeyPressed(Keys key)
        {
            return this._curState.IsKeyDown(key) && this._previousState.IsKeyUp(key);
        }

        /// <summary>
        /// <see langword="true"/> si la touche passe de l'état pressé à l'état relâché
        /// </summary>
        /// <param name="key">La touche relâchée</param>
        /// <returns><see langword="true"/> si la touche passe de l'état pressé à l'état relâché</returns>
        public bool IsKeyReleased(Keys key)
        {
            return this._curState.IsKeyUp(key) && this._previousState.IsKeyDown(key);
        }

        /// <summary>
        /// <see langword="true"/> si la touche est maintenue enfoncée
        /// </summary>
        /// <param name="key">La touche maintenue enfoncée</param>
        /// <returns><see langword="true"/> si la touche est maintenue enfoncée</returns>
        public bool IsKeyHeld(Keys key)
        {
            return this._curState.IsKeyDown(key) && this._previousState.IsKeyDown(key);
        }

        #endregion
    }
}
