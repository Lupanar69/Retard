using System;
using System.Collections.Generic;
using Arch.Core;
using Arch.LowLevel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using Retard.Core.Models.Arch;
using Retard.Rendering2D.Systems;

namespace Retard.Rendering2D.ViewModels
{
    /// <summary>
    /// Gère la création et destruction des entités des sprites
    /// </summary>
    public sealed class SpriteManager
    {
        #region Singleton

        /// <summary>
        /// Singleton
        /// </summary>
        public static SpriteManager Instance => SpriteManager._instance.Value;

        /// <summary>
        /// Singleton
        /// </summary>
        private static readonly Lazy<SpriteManager> _instance = new(() => new SpriteManager());

        #endregion

        #region Propriétés

        /// <summary>
        /// Permet d'accéder aux texture2Ds depuis les queries
        /// </summary>
        public Resources<Texture2D> Texture2DResources { get; init; }

        #endregion

        #region Variables d'instance

        /// <summary>
        /// Les systèmes du monde à màj dans Update()
        /// </summary>
        private readonly Group _updateSystems;

        /// <summary>
        /// Les systèmes du monde à màj dans Draw()
        /// </summary>
        private readonly Group _drawSystems;

        /// <summary>
        /// Les caméras utilisées par chaque SpriteDrawSystem
        /// </summary>
        private readonly List<OrthographicCamera> _cameras;

        /// <summary>
        /// Pour afficher les sprites à l'écran
        /// </summary>
        protected SpriteBatch _spriteBatch;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        private SpriteManager()
        {
            this.Texture2DResources = new Resources<Texture2D>(1);

            this._updateSystems = new Group("Update Systems");
            this._drawSystems = new Group("Draw Systems");
            this._cameras = new List<OrthographicCamera>();

            this._updateSystems.Add(new AnimatedSpriteUpdateSystem());

            this._updateSystems.Initialize();
            this._drawSystems.Initialize();

        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Màj à chaque frame
        /// </summary>
        /// <param name="w">Le monde contenant les entités</param>
        public void Update(World w)
        {
            this._updateSystems.Update(w);
        }

        /// <summary>
        /// Màj à chaque frame pour afficher les sprites à l'écran
        /// </summary>
        /// <param name="w">Le monde contenant les entités</param>
        public void Draw(World w)
        {
            this._drawSystems.Update(w);
        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Assigne un SpriteBatch
        /// </summary>
        /// <param name="spriteBatch">Pour afficher les sprites à l'écran</param>
        public void SetSpriteBatch(SpriteBatch spriteBatch)
        {
            this._spriteBatch = spriteBatch;
        }

        /// <summary>
        /// Enregistre une caméra pour lui assigner un SpriteDrawSystem
        /// </summary>
        /// <param name="camera">La caméra devant afficher les sprites à l'écran</param>
        public void RegisterCamera(OrthographicCamera camera)
        {
            ISystem sds = new SpriteDrawSystem(this._spriteBatch, camera);
            sds.Initialize();
            this._drawSystems.Add(sds);
            this._cameras.Add(camera);
        }

        /// <summary>
        /// Retire une caméra et son SpriteDrawSystem
        /// </summary>
        /// <param name="camera">La caméra devant afficher les sprites à l'écran</param>
        public void UnregisterCamera(OrthographicCamera camera)
        {
            int index = this._cameras.IndexOf(camera);
            this._cameras.Remove(camera);
            this._drawSystems.RemoveAt(index);
        }

        #endregion

        #region Méthodes statiques publiques

        /// <summary>
        /// Calcule les dimensions du sprite
        /// </summary>
        /// <param name="frame">L'id du sprite dans l'atlas à afficher</param>
        /// <returns>Les dimensions du sprite</returns>
        public static Rectangle GetSpriteRect(in Texture2D texture, int rows, int columns, int frame)
        {
            int width = texture.Width / columns;
            int height = texture.Height / rows;
            int row = frame / columns;
            int column = frame % columns;

            return new Rectangle(width * column, height * row, width, height);
        }

        #endregion
    }
}
