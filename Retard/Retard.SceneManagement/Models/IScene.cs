﻿using Arch.Core;
using Microsoft.Xna.Framework;
using Retard.Input.Models.Assets;

namespace Retard.SceneManagement.Models
{
    /// <summary>
    /// Permet de compartimenter la logique parmi différents contextes
    /// pour éviter de tout rassembler dans la classe ppale
    /// </summary>
    public interface IScene
    {
        #region Propriétés

        /// <summary>
        /// <see langword="true"/> si la scène doit bloquer les inputs 
        /// pour les scènes qui suivent
        /// (ex: une scène de pause superposée à la scène de jeu)
        /// </summary>
        public bool ConsumeInput { get; init; }

        /// <summary>
        /// <see langword="true"/> si la scène doit bloquer l'Update 
        /// pour les scènes qui suivent
        /// (ex: une scène de pause superposée à la scène de jeu)
        /// </summary>
        public bool ConsumeUpdate { get; init; }

        /// <summary>
        /// <see langword="true"/> si la scène doit bloquer le rendu 
        /// pour les scènes qui suivent
        /// (ex: une scène de pause superposée à la scène de jeu)
        /// </summary>
        public bool ConsumeDraw { get; init; }

        /// <summary>
        /// Les contrôles de la scène
        /// </summary>
        public InputControls Controls { get; init; }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Appelée à chaque fois que la scène devient active
        /// </summary>
        public void OnEnable() { }

        /// <summary>
        /// Appelée à chaque fois que la scène devient active
        /// </summary>
        public void OnDisable() { }

        /// <summary>
        /// Active les contrôles
        /// </summary>
        public void EnableControls() => Controls?.Enable();

        /// <summary>
        /// Désactive les contrôles
        /// </summary>
        public void DisableControls() => Controls?.Disable();

        /// <summary>
        /// Màj à chaque frame
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début du jeu</param>
        public void OnUpdateInput(GameTime gameTime) { }

        /// <summary>
        /// Màj à chaque frame
        /// </summary>
        /// <param name="w">Le monde contenant les entités</param>
        /// <param name="gameTime">Le temps écoulé depuis le début du jeu</param>
        public void OnUpdate(World w, GameTime gameTime) { }

        /// <summary>
        /// Pour afficher des éléments à l'écran
        /// </summary>
        /// <param name="w">Le monde contenant les entités</param>
        /// <param name="gameTime">Le temps écoulé depuis le début du jeu</param>
        public void OnDraw(World w, GameTime gameTime) { }

        #endregion
    }
}
