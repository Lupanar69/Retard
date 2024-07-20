using Microsoft.Xna.Framework;
using Retard.Core;
using E = Retard.Engine.ViewModels.Engine;

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

            E.Initialize(this);

            // Initialise les scènes utilisées par le jeu

            GameEntryPoint.CreateScenes();
        }

        #endregion

        #region Fonctions protégées

        /// <summary>
        /// Màj à chaque frame
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début du jeu</param>
        protected override void Update(GameTime gameTime)
        {
            E.Update(this, gameTime);

            GameEntryPoint.Update(gameTime);

            E.AfterUpdate();

            base.Update(gameTime);
        }

        /// <summary>
        /// Pour afficher des éléments à l'écran
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début du jeu</param>
        protected override void Draw(GameTime gameTime)
        {
            E.Draw(this.GraphicsDevice, gameTime);

            base.Draw(gameTime);
        }

        #endregion
    }
}