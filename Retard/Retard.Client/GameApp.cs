using Microsoft.Xna.Framework;
using Retard.Core;
using Retard.Engine.ViewModels;

namespace Retard.Client
{
    /// <summary>
    /// Callbacks du jeu
    /// </summary>
    internal sealed class GameApp : Game
    {
        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public GameApp()
        {
            GameEntryPoint.Initialize();

            // Initialise le moteur

            GameEngine.Initialize(this);

            // Initialise les scènes utilisées par le jeu

            GameEntryPoint.Start(this);
        }

        #endregion

        #region Fonctions protégées

        /// <summary>
        /// Màj à chaque frame
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début du jeu</param>
        protected override void Update(GameTime gameTime)
        {
            GameEngine.Update(this, gameTime);

            GameEntryPoint.Update(gameTime);

            GameEngine.AfterUpdate();

            base.Update(gameTime);
        }

        /// <summary>
        /// Pour afficher des éléments à l'écran
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début du jeu</param>
        protected override void Draw(GameTime gameTime)
        {
            GameEngine.Draw(this.GraphicsDevice, gameTime);

            base.Draw(gameTime);
        }

        #endregion
    }
}