using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace Retard.Core.Models
{
    /// <summary>
    /// Contient les informations sur la session de jeu en cours
    /// </summary>
    public static class GameSession
    {
        #region Propriétés

        /// <summary>
        /// Utilisé pour la génération aléatoire.
        /// Ce random est fixe et ne change jamais au cours de la partie.
        /// </summary>
        internal static FastRandom GenerationRandom
        {
            get;
            set;
        }

        /// <summary>
        /// Utilisée pour la génération aléatoire.
        /// Peut être assignée manuellement par le joueur.
        /// </summary>
        internal static int Seed
        {
            get;
            set;
        }

        #endregion

        #region Fonctions statiques

        /// <summary>
        /// Crée un nouvel id pour l'aléatoire
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le lancement de l'application</param>
        /// <returns>Un nouvel id pour l'aléatoire</returns>
        public static int CreateNewSeed(GameTime gameTime)
        {
            Random random = new((int)gameTime.GetElapsedSeconds());
            return random.Next();
        }

        /// <summary>
        /// Initialise une nouvelle session
        /// </summary>
        /// <param name="seed">Détermine l'aléatoire pour la génération des niveaux</param>
        public static void New(int seed)
        {
            GameSession.Seed = seed;
            GameSession.GenerationRandom = new FastRandom(seed);
        }

        #endregion
    }
}
