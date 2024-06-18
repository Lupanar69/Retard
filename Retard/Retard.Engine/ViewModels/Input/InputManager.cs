using System;
using System.Collections.Generic;
using Retard.Core.Models.Assets.Input;

namespace Retard.Core.ViewModels.Input
{
    /// <summary>
    /// Passerelle entre les entrées du joueur
    /// et les commandes à exécuter
    /// </summary>
    public static class InputManager
    {
        #region Variables d'instance

        /// <summary>
        /// La liste des types d'entrées autorisées pour ce jeu
        /// (clavier, souris, manette, etc.)
        /// </summary>
        private static Dictionary<Type, IInputScheme> _inputSchemes;

        #endregion

        #region Méthodes statiques publiques

        /// <summary>
        /// Crée les InputSchemes pour chaque type de contrôleur souhaité
        /// </summary>
        /// <param name="schemes">Les types de chaque contrôleur</param>
        public static void Initialize(params IInputScheme[] schemes)
        {
            InputManager._inputSchemes = new Dictionary<Type, IInputScheme>(schemes.Length);

            for (int i = 0; i < schemes.Length; i++)
            {
                InputManager._inputSchemes.Add(schemes[i].GetType(), schemes[i]);
            }
        }

        /// <summary>
        /// Récupère le contrôleur du type souhaité
        /// </summary>
        /// <typeparam name="T">Le type du contrôleur souhaité</typeparam>
        /// <returns>Le contrôleur souhaité</returns>
        public static T GetScheme<T>() where T : IInputScheme, new()
        {
            Type t = typeof(T);
            return (T)InputManager._inputSchemes[t];
        }

        /// <summary>
        /// Capture l'état des inputs lors de la frame actuelle
        /// </summary>
        public static void Update()
        {
            foreach (KeyValuePair<Type, IInputScheme> pair in InputManager._inputSchemes)
            {
                pair.Value.Update();
            }
        }

        /// <summary>
        /// Capture l'état des inputs lors de la frame précédente
        /// A appeler en fin d'Update pour ne pas écraser le précédent état
        /// avant les comparaisons
        /// </summary>
        public static void AfterUpdate()
        {
            foreach (KeyValuePair<Type, IInputScheme> pair in InputManager._inputSchemes)
            {
                pair.Value.AfterUpdate();
            }
        }

        #endregion
    }
}
