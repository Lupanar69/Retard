using Microsoft.Xna.Framework;
using Retard.Engine.ViewModels.Engine;
using Retard.Tests.ViewModels.Engine;

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

            BaseEngine.Initialize(this);

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
            BaseEngine.Update(this, gameTime);

            GameEntryPoint.Update(gameTime);

            BaseEngine.AfterUpdate();

            base.Update(gameTime);
        }

        /// <summary>
        /// Pour afficher des éléments à l'écran
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début du jeu</param>
        protected override void Draw(GameTime gameTime)
        {
            BaseEngine.Draw(this.GraphicsDevice, gameTime);

            base.Draw(gameTime);
        }

        #endregion
    }
}