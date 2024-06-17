using Arch.System;
using Microsoft.Xna.Framework;
using Retard.Core.Models.Assets.Scene;
using Retard.Core.Systems.Input;

namespace Retard.Core.ViewModels.Scenes
{
    /// <summary>
    /// Chargé de màj l'InputSystem chaque frame
    /// </summary>
    public sealed class InputProcessingScene : IScene
    {
        #region Properties

        /// <summary>
        /// <see langword="true"/> si la scène doit bloquer les inputs 
        /// pour les scènes qui suivent
        /// (ex: une scène de pause superposée à la scène de jeu)
        /// </summary>
        public bool ConsumeInput { get; init; }

        /// <summary>
        /// <see langword="true"/> si la scène doit bloquer le rendu
        /// pour les scènes qui suivent
        /// (ex: une scène de pause superposée à la scène de jeu)
        /// </summary>
        public bool ConsumeDraw { get; init; }

        #endregion

        #region Variables d'instance

        /// <summary>
        /// Les systèmes du monde à màj dans Update()
        /// </summary>
        private readonly Group<float> _updateSystems;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        public InputProcessingScene()
        {
            // TODO : Réserver les entités
            //SceneManager.World.Reserve(_spriteArchetype, _size.X * _size.Y);
            this._updateSystems = new Group<float>("Update Systems");
        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Chargement du contenu
        /// </summary>
        public void OnInitialize()
        {
            this._updateSystems.Add(new InputSystem(SceneManager.World));
            this._updateSystems.Initialize();
        }

        /// <summary>
        /// Màj à chaque frame
        /// </summary>
        public void OnLoadContent()
        {

        }

        /// <summary>
        /// Appelée à chaque fois que la scène devient active
        /// </summary>
        public void OnSetActive()
        {

        }

        /// <summary>
        /// Récupère les inputs nécessaires au fonctionnement des systèmes
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début de l'application</param>
        public void OnUpdateInput(GameTime gameTime)
        {

        }

        /// <summary>
        /// Màj à chaque frame
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début de l'application</param>
        public void OnUpdate(GameTime gameTime)
        {
            this._updateSystems.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        /// <summary>
        /// Pour afficher des éléments à l'écran
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début de l'application</param>
        public void OnDraw(GameTime gameTime)
        {

        }

        #endregion
    }
}
