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

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Init
        /// </summary
        public abstract void Initialize();

        /// <summary>
        /// Chargement du contenu
        /// </summary>
        public abstract void LoadContent();

        /// <summary>
        /// Màj à chaque frame
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début de l'application</param>
        public abstract void UpdateInput(GameTime gameTime);

        /// <summary>
        /// Màj à chaque frame
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début de l'application</param>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// Pour afficher des éléments à l'écran
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début de l'application</param>
        public abstract void Draw(GameTime gameTime);

        #endregion
    }
}
