using System;
using Microsoft.Xna.Framework;
using Retard.Core.Models.ValueTypes;
using Retard.Input.Models;
using Retard.Input.ViewModels;

namespace Retard.Input.Models.Assets
{
    /// <summary>
    /// Regroupe les handles de chaque InputAction
    /// </summary>
    public sealed class InputControls
    {
        #region Propriétés

        /// <summary>
        /// <see langword="true"/> si l'objet est abonné à l'InputManager
        /// </summary>
        public bool Enabled => _enabled;

        #endregion

        #region Variables d'instance

        /// <summary>
        /// Contient les handles des différents types d'action
        /// </summary>
        private readonly InputHandles _handles;

        /// <summary>
        /// <see langword="true"/> si l'objet est abonné à l'InputManager
        /// </summary>
        private bool _enabled;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        public InputControls()
        {
            _enabled = false;
            _handles = new InputHandles();
        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Abonne cet objet à l'InputManager pour recevoir les événements
        /// </summary>
        public void Enable()
        {
            if (!_enabled)
            {
                InputManager.Instance.Handles += _handles;
                _enabled = true;
            }
        }

        /// <summary>
        /// Désabonne cet objet à l'InputManager pour recevoir les événements
        /// </summary>
        public void Disable()
        {
            if (_enabled)
            {
                InputManager.Instance.Handles -= _handles;
                _enabled = false;
            }
        }

        /// <summary>
        /// Assigne un callback à un InputAction de type ButtonState à partir de son ID.
        /// </summary>
        /// <param name="key">L'ID de l'action</param>
        /// <param name="handleType">Le type de handle auquel s'abonner</param>
        /// <param name="callback">La méthode à exécuter</param>
        public void AddAction(NativeString key, InputEventHandleType handleType, Action<int> callback)
        {
            // Si cette action n'existe pas dans la liste, on la crée

            if (!_handles.ButtonStateHandlesExist(key))
            {
                _handles.AddButtonStateHandles(key);
            }

            // Assigne l'action

            if (!_handles.ButtonStateHandlesExist(key))
            {
                throw new ArgumentException($"Erreur : La clé \"{key}\" n'existe pas dans la liste des handles de type ButtonState.");
            }

            ref readonly InputActionButtonStateHandles thisHandles = ref _handles.GetButtonEvent(key);

            switch (handleType)
            {
                case InputEventHandleType.Started:
                    thisHandles.Started += callback;
                    break;
                case InputEventHandleType.Performed:
                    thisHandles.Performed += callback;
                    break;
                case InputEventHandleType.Finished:
                    thisHandles.Finished += callback;
                    break;
            }

            // Si l'objet est actif, on ajoute également cette nouvelle action
            // aux Handles de l'InputManager

            if (_enabled)
            {
                if (!InputManager.Instance.Handles.ButtonStateHandlesExist(key))
                {
                    InputManager.Instance.Handles.AddButtonStateHandles(key);
                }

                // Assigne l'action

                ref readonly InputActionButtonStateHandles mainHandles = ref InputManager.Instance.Handles.GetButtonEvent(key);

                switch (handleType)
                {
                    case InputEventHandleType.Started:
                        mainHandles.Started += callback;
                        break;
                    case InputEventHandleType.Performed:
                        mainHandles.Performed += callback;
                        break;
                    case InputEventHandleType.Finished:
                        mainHandles.Finished += callback;
                        break;
                }
            }
        }

        /// <summary>
        /// Assigne un callback à un InputAction de type Vector1D à partir de son ID.
        /// </summary>
        /// <param name="key">L'ID de l'action</param>
        /// <param name="callback">La méthode à exécuter</param>
        public void AddAction(NativeString key, Action<int, float> callback)
        {
            // Si cette action n'existe pas dans la liste, on la crée

            if (!_handles.Vector1DHandlesExist(key))
            {
                _handles.AddVector1DHandles(key);
            }

            // Assigne l'action

            ref readonly InputActionVector1DHandles thisHandles = ref _handles.GetVector1DEvent(key);
            thisHandles.Performed += callback;

            // Si l'objet est actif, on ajoute également cette nouvelle action
            // aux Handles de l'InputManager

            if (_enabled)
            {
                if (!InputManager.Instance.Handles.ButtonStateHandlesExist(key))
                {
                    InputManager.Instance.Handles.AddVector1DHandles(key);
                }

                // Assigne l'action

                ref readonly InputActionVector1DHandles mainHandles = ref InputManager.Instance.Handles.GetVector1DEvent(key);
                mainHandles.Performed += callback;
            }
        }

        /// <summary>
        /// Assigne un callback à un InputAction de type Vector2D à partir de son ID.
        /// </summary>
        /// <param name="key">L'ID de l'action</param>
        /// <param name="callback">La méthode à exécuter</param>
        public void AddAction(NativeString key, Action<int, Vector2> callback)
        {
            // Si cette action n'existe pas dans la liste, on la crée

            if (!_handles.Vector2DHandleExist(key))
            {
                _handles.AddVector2DHandles(key);
            }

            // Assigne l'action

            ref readonly InputActionVector2DHandles thisHandles = ref _handles.GetVector2DEvent(key);
            thisHandles.Performed += callback;

            // Si l'objet est actif, on ajoute également cette nouvelle action
            // aux Handles de l'InputManager

            if (_enabled)
            {
                if (!InputManager.Instance.Handles.Vector2DHandleExist(key))
                {
                    InputManager.Instance.Handles.AddVector2DHandles(key);
                }

                // Assigne l'action

                ref readonly InputActionVector2DHandles mainHandles = ref InputManager.Instance.Handles.GetVector2DEvent(key);
                mainHandles.Performed += callback;
            }
        }

        #endregion
    }
}
