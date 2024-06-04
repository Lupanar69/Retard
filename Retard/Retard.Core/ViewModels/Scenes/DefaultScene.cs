using System;
using Arch.System;
using Microsoft.Xna.Framework;
using Retard.Core.Models.Assets.Scene;

namespace Retard.Core.ViewModels.Scenes.Tests
{
    /// <summary>
    /// Scène par défaut lancée avant toutes les autres
    /// </summary>
    public sealed class DefaultScene : IScene, IDisposable
    {
        #region Properties

        /// <summary>
        /// <see langword="true"/> si la scène doit bloquer les inputs 
        /// pour les scènes qui suivent
        /// (ex: une scène de pause superposée à la scène de jeu)
        /// </summary>
        public bool ConsumeInput { get; init; }

        #endregion

        #region Variables d'instance

        /// <summary>
        /// <see langword="true"/> si l'on a appelé Dispose()
        /// </summary>
        private bool _disposedValue;

        /// <summary>
        /// Les systèmes du monde à màj dans Update()
        /// </summary>
        private Group<byte> _updateSystems;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="camera">La caméra du jeu</param>
        public DefaultScene()
        {

        }

        /// <summary>
        /// Nettoie l'objet
        /// </summary>
        //// TODO: override finalizer only if 'Dispose(bool disposingManaged)' has code to free unmanaged resources
        //~SpriteDrawTestScene()
        //{
        //    // Do not change this code. Put cleanup code in 'Dispose(bool disposingManaged)' method
        //    Dispose(disposingManaged: false);
        //}

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Chargement du contenu
        /// </summary>
        public void Initialize()
        {
            this._updateSystems = new Group<byte>("Update Systems");
        }

        /// <summary>
        /// Màj à chaque frame
        /// </summary>
        public void LoadContent()
        {
            // Créé ici car on a besoin de récupérer les fichiers

            this._updateSystems.Initialize();
        }

        /// <summary>
        /// Récupère les inputs nécessaires au fonctionnement des systèmes
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début de l'application</param>
        public void UpdateInput(GameTime gameTime)
        {

        }

        /// <summary>
        /// Pour afficher des éléments à l'écran
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début de l'application</param>
        public void Update(GameTime gameTime)
        {
            this._updateSystems.Update(0);
        }

        /// <summary>
        /// Pour afficher des éléments à l'écran
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début de l'application</param>
        public void Draw(GameTime gameTime)
        {

        }

        /// <summary>
        /// Nettoie l'objet
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            this.Dispose(disposingManaged: true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Méthodes privées

        /// <summary>
        /// Nettoie l'objet
        /// </summary>
        /// <param name="disposingManaged"><see langword="true"/> si on doit nettoyer des objets</param>
        private void Dispose(bool disposingManaged)
        {
            if (!this._disposedValue)
            {
                if (disposingManaged)
                {
                    this._updateSystems.Dispose();
                }

                this._disposedValue = true;
            }
        }

        #endregion
    }
}
