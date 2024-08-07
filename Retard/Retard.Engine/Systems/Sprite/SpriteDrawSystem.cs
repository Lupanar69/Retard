﻿using Arch.Core;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using Retard.Core.Entities;
using Retard.Core.Models.Arch;
using Retard.Core.Models.Assets.Sprites;

namespace Retard.Core.Systems.Sprite
{
    /// <summary>
    /// Affiche les sprites à l'écran
    /// </summary>
    public readonly struct SpriteDrawSystem : ISystemWorld
    {
        #region Propriétés

        /// <summary>
        /// Le monde contenant les entités
        /// </summary>
        public readonly World World { get; init; }

        #endregion

        #region Variables d'instance

        /// <summary>
        /// L'atlas des sprites à afficher
        /// </summary>
        private readonly SpriteAtlas _spriteAtlas;

        /// <summary>
        /// Pour afficher les sprites à l'écran
        /// </summary>
        private readonly SpriteBatch _spriteBatch;

        /// <summary>
        /// La caméra du jeu
        /// </summary>
        private readonly OrthographicCamera _camera;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="world">Le monde contenant les entités des sprites</param>
        /// <param name="spriteBatch">Pour afficher les sprites à l'écran</param>
        /// <param name="spriteAtlas">L'atlas des sprites à afficher</param>
        /// <param name="camera">La caméra du jeu</param>
        public SpriteDrawSystem(World world, SpriteBatch spriteBatch, SpriteAtlas spriteAtlas, OrthographicCamera camera)
        {
            this._spriteBatch = spriteBatch;
            this._spriteAtlas = spriteAtlas;
            this._camera = camera;
            this.World = world;
        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void Update()
        {
            Queries.UpdateAnimatedSpriteRectQuery(this.World, in this._spriteAtlas);

            this._spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, this._camera.GetViewMatrix());

            Queries.DrawSpritesQuery(this.World, in this._spriteAtlas, in this._spriteBatch);

            _spriteBatch.End();
        }

        #endregion
    }
}
