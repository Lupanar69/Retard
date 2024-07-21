using Arch.Core;
using Microsoft.Xna.Framework.Graphics;
using Retard.Core.Models.Arch;
using Retard.Core.Systems.Sprite;

namespace Retard.Engine.ViewModels.Drawing
{
    /// <summary>
    /// Chargé du rendu des sprites à l'écran
    /// </summary>
    internal static class SpriteManager
    {
        #region Variables d'instance

        /// <summary>
        /// Les systèmes du monde à màj dans Update()
        /// </summary>
        private static readonly Group _updateSystems;

        /// <summary>
        /// Les systèmes du monde à màj dans Draw()
        /// </summary>
        private static readonly Group _drawSystems;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        static SpriteManager()
        {
            SpriteManager._updateSystems = new Group("Update Systems");
            SpriteManager._drawSystems = new Group("Draw Systems");
        }

        #endregion

        #region Méthodes statiques internes

        /// <summary>
        /// Initialise les scènes ECS
        /// </summary>
        /// <param name="world">Contient les entités du jeu</param>
        /// <param name="spriteBatch">Pour afficher les sprites à l'écran</param>
        internal static void Initialize(World world, SpriteBatch spriteBatch)
        {
            //SpriteManager._drawSystems.Add(new SpriteDrawSystem(world, spriteBatch, this._spriteAtlas, this._camera));
            SpriteManager._updateSystems.Add(new AnimatedSpriteUpdateSystem(world));

            SpriteManager._updateSystems.Initialize();
            SpriteManager._drawSystems.Initialize();

        }

        /// <summary>
        /// Màj à chaque frame
        /// </summary>
        internal static void Update()
        {
            SpriteManager._updateSystems.Update();
        }

        /// <summary>
        /// Pour afficher les sprites à l'écran
        /// </summary>
        internal static void Draw()
        {
            SpriteManager._drawSystems.Update();
        }

        #endregion
    }
}
