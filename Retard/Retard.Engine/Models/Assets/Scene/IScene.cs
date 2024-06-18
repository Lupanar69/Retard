using Microsoft.Xna.Framework;

namespace Retard.Core.Models.Assets.Scene
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
        /// <see langword="true"/> si la scène doit bloquer le rendu 
        /// pour les scènes qui suivent
        /// (ex: une scène de pause superposée à la scène de jeu)
        /// </summary>
        public bool ConsumeDraw { get; init; }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Init
        /// </summary
        public abstract void OnInitialize();

        /// <summary>
        /// Chargement du contenu
        /// </summary>
        public abstract void OnLoadContent();

        /// <summary>
        /// Appelée à chaque fois que la scène devient active
        /// </summary>
        public abstract void OnSetActive();

        /// <summary>
        /// Màj à chaque frame
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début de l'application</param>
        public abstract void OnUpdateInput(GameTime gameTime);

        /// <summary>
        /// Màj à chaque frame
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début de l'application</param>
        public abstract void OnUpdate(GameTime gameTime);

        /// <summary>
        /// Pour afficher des éléments à l'écran
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début de l'application</param>
        public abstract void OnDraw(GameTime gameTime);

        #endregion
    }
}
