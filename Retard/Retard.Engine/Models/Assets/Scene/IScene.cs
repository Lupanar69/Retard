using Microsoft.Xna.Framework;
using Retard.Engine.Models.Assets.Input;

namespace Retard.Engine.Models.Assets.Scene
{
    /// <summary>
    /// Permet de compartimenter la logique parmi différents contextes
    /// pour éviter de tout rassembler dans la classe ppale
    /// </summary>
    public interface IScene
    {
        #region Propriétés

        /// <summary>
        /// <see langword="true"/> si la scène doit bloquer les inputs 
        /// pour les scènes qui suivent
        /// (ex: une scène de pause superposée à la scène de jeu)
        /// </summary>
        public bool ConsumeInput { get; init; }

        /// <summary>
        /// <see langword="true"/> si la scène doit bloquer l'Update 
        /// pour les scènes qui suivent
        /// (ex: une scène de pause superposée à la scène de jeu)
        /// </summary>
        public bool ConsumeUpdate { get; init; }

        /// <summary>
        /// <see langword="true"/> si la scène doit bloquer le rendu 
        /// pour les scènes qui suivent
        /// (ex: une scène de pause superposée à la scène de jeu)
        /// </summary>
        public bool ConsumeDraw { get; init; }

        /// <summary>
        /// Les contrôles de la scène
        /// </summary>
        public InputControls Controls { get; init; }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Appelée à chaque fois que la scène devient active
        /// </summary>
        public void OnSetActive() { }

        /// <summary>
        /// Active les contrôles
        /// </summary>
        public void EnableControls() => this.Controls?.Enable();

        /// <summary>
        /// Désactive les contrôles
        /// </summary>
        public void DisableControls() => this.Controls?.Disable();

        /// <summary>
        /// Màj à chaque frame
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début du jeu</param>
        public void OnUpdateInput(GameTime gameTime) { }

        /// <summary>
        /// Màj à chaque frame
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début du jeu</param>
        public void OnUpdate(GameTime gameTime) { }

        /// <summary>
        /// Pour afficher des éléments à l'écran
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début du jeu</param>
        public void OnDraw(GameTime gameTime) { }

        #endregion
    }
}
