using System.Collections.Generic;
using Arch.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Retard.Core.Models;
using Retard.Core.Models.Assets.Input;
using Retard.Core.Models.ValueTypes;
using Retard.Core.ViewModels.App;
using Retard.Core.ViewModels.Input;
using Retard.Core.ViewModels.Scenes;

namespace Retard.Engine.ViewModels.Engine
{
    /// <summary>
    /// Chargée d'initialiser et màj tous les systèmes nécessaires
    /// au fonctionnement du jeu
    /// </summary>
    public abstract class BaseEngine
    {
        #region Variables d'instance

        /// <summary>
        /// Pour charger les ressources du jeu
        /// </summary>
        protected readonly ContentManager _content;

        /// <summary>
        /// Pour afficher les sprites à l'écran
        /// </summary>
        protected readonly SpriteBatch _spriteBatch;

        /// <summary>
        /// Le monde contenant les entités
        /// </summary>
        protected readonly World _world;

        /// <summary>
        /// Passerelle entre les entrées du joueur et les commandes à effectuer
        /// </summary>
        protected readonly InputManager _inputManager;

        /// <summary>
        /// Gère l'ajout, màj et suppression des scènes
        /// </summary>
        protected readonly SceneManager _sceneManager;

        /// <summary>
        /// Gère les paramètres de la fenêtre de jeu
        /// </summary>
        protected readonly AppViewport _appViewport;

        /// <summary>
        /// Gère les paramètres de la fenêtre de jeu
        /// </summary>
        protected readonly AppPerformance _appPerformance;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="game">Le jeu</param>
        /// <param name="graphicsDeviceManager">Configurateur des paramètres de la fenêtre du jeu</param>
        /// <param name="inputSchemes">Les types de contrôles acceptées par le moteur</param>
        public BaseEngine(Game game, GraphicsDeviceManager graphicsDeviceManager, params IInputScheme[] inputSchemes)
        {
            // Initialise les components

            game.Activated += GameState.OnFocusEvent;
            game.Deactivated += GameState.OnFocusLostEvent;

            this._appViewport = new AppViewport(game, graphicsDeviceManager);
            this._appPerformance = new AppPerformance(game);
            this._content = game.Content;
            this._spriteBatch = new SpriteBatch(game.GraphicsDevice);
            this._world = World.Create();

            // Initialise les inputs

            this._inputManager = new InputManager(inputSchemes);

            // Initialise le SceneManager

            this._sceneManager = new SceneManager(1);
        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Charge le contenu externe du jeu
        /// </summary>
        public abstract void LoadContent();

        /// <summary>
        /// Crée les scènes du jeu
        /// </summary>
        /// <param name="textures2D">Les Textures2D du jeu</param>
        protected abstract void CreateScenes(Dictionary<NativeString, Texture2D> textures2D);

        /// <summary>
        /// Màj à chaque frame
        /// </summary>
        /// <param name="game">L'application</param>
        /// <param name="gameTime">Le temps écoulé depuis le début du jeu</param>
        public void Update(Game game, GameTime gameTime)
        {
            if (this._sceneManager.IsEmpty)
            {
                game.Exit();
            }

            // Màj les inputs

            this._inputManager.Update();

            // Màj les scènes

            this._sceneManager.UpdateInput(gameTime);
            this._sceneManager.Update(gameTime);
        }

        /// <summary>
        /// Màj à chaque frame
        /// </summary>
        public void AfterUpdate()
        {
            // Appelé en dernier pour ne pas écraser le précédent KeyboardState
            // avant les comparaisons

            this._inputManager.AfterUpdate();
        }

        /// <summary>
        /// Pour afficher des éléments à l'écran
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début du jeu</param>
        public void Draw(GraphicsDevice graphicsDevice, GameTime gameTime)
        {
            graphicsDevice.Clear(Color.LightBlue);

            this._sceneManager.Draw(gameTime);
        }

        #endregion
    }
}