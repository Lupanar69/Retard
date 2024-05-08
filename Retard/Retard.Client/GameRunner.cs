using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Retard.Core.Models;
using Retard.Core.Models.Assets.Camera;
using Retard.Core.Models.Assets.Sprites;

namespace Retard.Client
{
    /// <summary>
    /// Callbacks du jeu
    /// </summary>
    internal sealed class GameRunner : Game
    {
        #region Variables d'instance

        /// <summary>
        /// Permet de modifier les paramètres de la fenêtre
        /// </summary>
        private readonly GraphicsDeviceManager _graphics;

        /// <summary>
        /// Pour afficher les sprites à l'écran
        /// </summary>
        private SpriteBatch _spriteBatch;

        /// <summary>
        /// La caméra du jeu
        /// </summary>
        private SpriteAtlas _debugAtlas;

        /// <summary>
        /// La caméra du jeu
        /// </summary>
        private AnimatedSprite _animatedSprite;

        /// <summary>
        /// La caméra du jeu
        /// </summary>
        private Camera _camera;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public GameRunner()
        {
            this._graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";


            this.SetupGameWindow(800, 600, false);
        }

        #endregion

        #region Fonctions protégées

        /// <summary>
        /// Init
        /// </summary>
        protected override void Initialize()
        {
            this._camera = new Camera();

            base.Initialize();
        }

        /// <summary>
        /// Chargement du contenu
        /// </summary>
        protected override void LoadContent()
        {
            this._spriteBatch = new SpriteBatch(this.GraphicsDevice);
            Texture2D debugTex = Content.Load<Texture2D>($"{Constants.TEXTURES_DIR_PATH_DEBUG}tiles_test2");
            this._debugAtlas = new(debugTex, 4, 4);
            this._animatedSprite = new AnimatedSprite(this._debugAtlas, 0, 8);
        }

        /// <summary>
        /// Màj à chaque frame
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début de l'application</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            _animatedSprite.Update();

            this._camera.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// Pour afficher des éléments à l'écran
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début de l'application</param>
        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.Black);

            //this._spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, null/*_camera.View()*/);
            //_animatedSprite.Draw(in _debugAtlas, in this._spriteBatch, this._camera.Position, Color.White);
            //this._spriteBatch.End();
            base.Draw(gameTime);
        }

        #endregion

        #region Fonctions privées

        /// <summary>
        /// Initialise la fenêtre de jeu
        /// </summary>
        /// <param name="resolutionX">La résolution en X de la fenêtre</param>
        /// <param name="resolutionY">La résolution en Y de la fenêtre</param>
        /// <param name="fullScreen"><see langword="true"/> pour passer la fenêtre en plein écran</param>
        /// <param name="mouseVisible"><see langword="true"/> si la souris doit rester visible</param>
        /// <param name="allowUserResizing"><see langword="true"/> si le joueur peut redimensionner la fenêtre</param>
        private void SetupGameWindow(int resolutionX, int resolutionY, bool fullScreen = true, bool mouseVisible = true, bool allowUserResizing = true)
        {
            this.IsMouseVisible = mouseVisible;
            this.Window.AllowUserResizing = allowUserResizing;

            this._graphics.PreferredBackBufferWidth = resolutionX;
            this._graphics.PreferredBackBufferHeight = resolutionY;
            this._graphics.IsFullScreen = fullScreen;
            this._graphics.ApplyChanges();
        }

        #endregion
    }
}