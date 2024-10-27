using System;
using FixedStrings;
using Microsoft.Xna.Framework;
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
            this._handles = new InputHandles();
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
                InputManager.Instance.Handles += this._handles;
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
                InputManager.Instance.Handles -= this._handles;
                _enabled = false;
            }
        }

        /// <summary>
        /// Assigne un callback à un InputAction de type ButtonState à partir de son ID.
        /// </summary>
        /// <param name="key">L'ID de l'action</param>
        /// <param name="handleType">Le type de handle auquel s'abonner</param>
        /// <param name="callback">La méthode à exécuter</param>
        public void AddAction(FixedString32 key, InputEventHandleType handleType, Action<int> callback)
        {
            // Si cette action n'existe pas dans la liste, on la crée

            if (!this._handles.ButtonStateHandleExists(key))
            {
                this._handles.AddButtonStateEvent(key);
            }

            // Assigne l'action

            ref readonly InputActionButtonStateHandles thisHandles = ref this._handles.GetButtonEvent(key);

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
                if (!InputManager.Instance.Handles.ButtonStateHandleExists(key))
                {
                    InputManager.Instance.Handles.AddButtonStateEvent(key);
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
        public void AddAction(FixedString32 key, Action<int, float> callback)
        {
            // Si cette action n'existe pas dans la liste, on la crée

            if (!this._handles.Vector1DHandleExists(key))
            {
                this._handles.AddVector1DEvent(key);
            }

            // Assigne l'action

            ref readonly InputActionVector1DHandles thisHandles = ref this._handles.GetVector1DEvent(key);
            thisHandles.Performed += callback;

            // Si l'objet est actif, on ajoute également cette nouvelle action
            // aux Handles de l'InputManager

            if (_enabled)
            {
                if (!InputManager.Instance.Handles.ButtonStateHandleExists(key))
                {
                    InputManager.Instance.Handles.AddVector1DEvent(key);
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
        public void AddAction(FixedString32 key, Action<int, Vector2> callback)
        {
            // Si cette action n'existe pas dans la liste, on la crée

            if (!this._handles.Vector2DHandleExists(key))
            {
                this._handles.AddVector2DEvent(key);
            }

            // Assigne l'action

            ref readonly InputActionVector2DHandles thisHandles = ref this._handles.GetVector2DEvent(key);
            thisHandles.Performed += callback;

            // Si l'objet est actif, on ajoute également cette nouvelle action
            // aux Handles de l'InputManager

            if (_enabled)
            {
                if (!InputManager.Instance.Handles.Vector2DHandleExists(key))
                {
                    InputManager.Instance.Handles.AddVector2DEvent(key);
                }

                // Assigne l'action

                ref readonly InputActionVector2DHandles mainHandles = ref InputManager.Instance.Handles.GetVector2DEvent(key);
                mainHandles.Performed += callback;
            }
        }

        #endregion
    }
}
