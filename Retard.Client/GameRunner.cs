using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Entities;

namespace Retard.Client
{
    public class GameRunner : Game
    {
        #region Variables d'instance

        /// <summary>
        /// Contient les entités et systèmes
        /// </summary>
        private World _world;

        Texture2D t;
        private GraphicsDeviceManager _graphics;
        SpriteBatch sb;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public GameRunner()
        {
            _graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
            this.Window.AllowUserResizing = true;
        }

        #endregion

        #region Fonctions protégées

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            this._world = new WorldBuilder().Build();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here

            sb = new SpriteBatch(GraphicsDevice);
            t = Content.Load<Texture2D>("Resources/Textures/Debug/tiles_test2");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            this._world.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            this._world.Draw(gameTime);

            sb.Begin();
            sb.Draw(t, new Rectangle(0, 0, 128, 128), Color.White);
            sb.End();

            base.Draw(gameTime);
        }

        #endregion
    }
}