using Arch.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using Retard.Core.Models;
using Retard.Core.ViewModels.App;
using Retard.Core.ViewModels.Input;
using Retard.Core.ViewModels.Scenes;
using Retard.Engine.ViewModels.Input;

namespace Retard.Engine.ViewModels
{
    /// <summary>
    /// Chargée d'initialiser et màj tous les systèmes nécessaires
    /// au fonctionnement du jeu
    /// </summary>
    public static class Engine
    {
        #region Propriétés

        /// <summary>
        /// Pour charger les ressources du jeu
        /// </summary>
        public static ContentManager Content => Engine._content;

        /// <summary>
        /// Pour afficher les sprites à l'écran
        /// </summary>
        public static SpriteBatch SpriteBatch => Engine._spriteBatch;

        /// <summary>
        /// La caméra du jeu
        /// </summary>
        public static OrthographicCamera Camera => Engine._camera;

        /// <summary>
        /// Le monde contenant les entités
        /// </summary>
        public static World World => Engine._world;

        #endregion

        #region Variables statiques

        /// <summary>
        /// Pour charger les ressources du jeu
        /// </summary>
        private static ContentManager _content;

        /// <summary>
        /// Pour afficher les sprites à l'écran
        /// </summary>
        private static SpriteBatch _spriteBatch;

        /// <summary>
        /// La caméra du jeu
        /// </summary>
        private static OrthographicCamera _camera;

        /// <summary>
        /// Le monde contenant les entités
        /// </summary>
        private static World _world;

        /// <summary>
        /// Pour s'abonner aux inputs
        /// </summary>
        private static InputControls _controls;

        #endregion

        #region Méthodes statiques publiques

        /// <summary>
        /// Initialise le jeu
        /// </summary>
        /// <param name="game">L'application</param>
        public static void Initialize(Game game)
        {
            game.Content.RootDirectory = "Content";
            game.Activated += GameState.OnFocusEvent;
            game.Deactivated += GameState.OnFocusLostEvent;

            AppViewport.Initialize(game);
            AppPerformance.Initialize(game);

            // Initialise les components

            Engine._content = game.Content;
            Engine._spriteBatch = new SpriteBatch(game.GraphicsDevice);
            Engine._camera = new OrthographicCamera(game.GraphicsDevice);
            Engine._world = World.Create();

            // Initialise les inputs

            InputManager.InitializeSchemes(new KeyboardInput(), new MouseInput(), new GamePadInput(GamePad.MaximumGamePadCount));
            InputManager.InitializeSystems(Engine._world);

            Engine._controls = new InputControls();
            Engine._controls.GetButtonEvent("Exit").Started += (_) => { game.Exit(); };
            Engine._controls.Enable();

            // Initialise le SceneManager

            SceneManager.Initialize(Engine._world, Engine._spriteBatch);
        }

        /// <summary>
        /// Màj à chaque frame
        /// </summary>
        /// <param name="game">L'application</param>
        /// <param name="gameTime">Le temps écoulé depuis le début du jeu</param>
        public static void Update(Game game, GameTime gameTime)
        {
            if (SceneManager.IsEmpty)
            {
                game.Exit();
            }

            // Màj les inputs

            InputManager.Update();

            // Màj les scènes

            SceneManager.UpdateInput(gameTime);
            SceneManager.Update(gameTime);
        }

        /// <summary>
        /// Màj à chaque frame
        /// </summary>
        public static void AfterUpdate()
        {
            // Appelé en dernier pour ne pas écraser le précédent KeyboardState
            // avant les comparaisons

            InputManager.AfterUpdate();
        }

        /// <summary>
        /// Pour afficher des éléments à l'écran
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début du jeu</param>
        public static void Draw(GraphicsDevice graphicsDevice, GameTime gameTime)
        {
            graphicsDevice.Clear(Color.Black);

            SceneManager.Draw(gameTime);
        }

        #endregion
    }
}
