using System;
using Arch.LowLevel;
using Microsoft.Xna.Framework;
using Retard.Core.Models.ValueTypes;
using Retard.Core.ViewModels.Input;
using Retard.Engine.Models;
using Retard.Engine.Models.Assets.Input;

namespace Retard.Engine.ViewModels.Input
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
        public bool Enabled => this._enabled;

        #endregion

        #region Variables d'instance

        /// <summary>
        /// La liste des abonnements pour chaque Input action de type ButtonState
        /// </summary>
        private UnsafeList<NativeString> _buttonStateHandlesIDs;

        /// <summary>
        /// La liste des abonnements pour chaque Input action de type Vector1D
        /// </summary>
        private UnsafeList<NativeString> _vector1DHandlesIDs;

        /// <summary>
        /// La liste des abonnements pour chaque Input action de type Vector2D
        /// </summary>
        private UnsafeList<NativeString> _vector2DHandlesIDs;

        /// <summary>
        /// La liste des abonnements pour chaque Input action de type ButtonState
        /// </summary>
        private UnsafeList<InputActionButtonStateHandles> _buttonStateHandles;

        /// <summary>
        /// La liste des abonnements pour chaque Input action de type Vector1D
        /// </summary>
        private UnsafeList<InputActionVector1DHandles> _vector1DHandles;

        /// <summary>
        /// La liste des abonnements pour chaque Input action de type Vector2D
        /// </summary>
        private UnsafeList<InputActionVector2DHandles> _vector2DHandles;

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
            this._enabled = false;

            this._buttonStateHandlesIDs = new UnsafeList<NativeString>(1);
            this._vector1DHandlesIDs = new UnsafeList<NativeString>(1);
            this._vector2DHandlesIDs = new UnsafeList<NativeString>(1);

            this._buttonStateHandles = new UnsafeList<InputActionButtonStateHandles>(1);
            this._vector1DHandles = new UnsafeList<InputActionVector1DHandles>(1);
            this._vector2DHandles = new UnsafeList<InputActionVector2DHandles>(1);
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="inputControls">Les InputControls à copier</param>
        public InputControls(in InputControls inputControls) :
            this(inputControls._buttonStateHandlesIDs, inputControls._vector1DHandlesIDs, inputControls._vector2DHandlesIDs)
        {
            for (int i = 0; i < inputControls._buttonStateHandlesIDs.Count; ++i)
            {
                NativeString rKey = inputControls._buttonStateHandlesIDs[i];
                int leftIndexOf = this._buttonStateHandlesIDs.IndexOf(rKey);
                ref readonly InputActionButtonStateHandles lHandles = ref this._buttonStateHandles[leftIndexOf];
                ref readonly InputActionButtonStateHandles rHandles = ref inputControls._buttonStateHandles[i];
                lHandles.Started += rHandles.Started;
                lHandles.Performed += rHandles.Performed;
                lHandles.Finished += rHandles.Finished;
            }

            for (int i = 0; i < inputControls._vector1DHandlesIDs.Count; ++i)
            {
                NativeString rKey = inputControls._vector1DHandlesIDs[i];
                int leftIndexOf = this._vector1DHandlesIDs.IndexOf(rKey);
                ref readonly InputActionVector1DHandles lHandles = ref this._vector1DHandles[leftIndexOf];
                ref readonly InputActionVector1DHandles rHandles = ref inputControls._vector1DHandles[i];
                lHandles.Performed += rHandles.Performed;
            }

            for (int i = 0; i < inputControls._vector2DHandlesIDs.Count; ++i)
            {
                NativeString rKey = inputControls._vector2DHandlesIDs[i];
                int leftIndexOf = this._vector2DHandlesIDs.IndexOf(rKey);
                ref readonly InputActionVector2DHandles lHandles = ref this._vector2DHandles[leftIndexOf];
                ref readonly InputActionVector2DHandles rHandles = ref inputControls._vector2DHandles[i];
                lHandles.Performed += rHandles.Performed;
            }
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="buttonIDs">La liste des IDs des actions de type ButtonState</param>
        /// <param name="vector1DIDs">La liste des IDs des actions de type Vector1D</param>
        /// <param name="vector2DIDs">La liste des IDs des actions de type Vector2D</param>
        public InputControls(UnsafeList<NativeString> buttonIDs, UnsafeList<NativeString> vector1DIDs, UnsafeList<NativeString> vector2DIDs)
        {
            this._enabled = false;

            this._buttonStateHandlesIDs = new UnsafeList<NativeString>(buttonIDs.Capacity);
            this._vector1DHandlesIDs = new UnsafeList<NativeString>(vector1DIDs.Capacity);
            this._vector2DHandlesIDs = new UnsafeList<NativeString>(vector2DIDs.Capacity);

            this._buttonStateHandles = new UnsafeList<InputActionButtonStateHandles>(buttonIDs.Capacity);
            this._vector1DHandles = new UnsafeList<InputActionVector1DHandles>(vector1DIDs.Capacity);
            this._vector2DHandles = new UnsafeList<InputActionVector2DHandles>(vector2DIDs.Capacity);

            for (int i = 0; i < buttonIDs.Count; ++i)
            {
                var started = InputManager.ActionResources.Add(delegate
                { });
                var performed = InputManager.ActionResources.Add(delegate
                { });
                var finished = InputManager.ActionResources.Add(delegate
                { });

                this._buttonStateHandlesIDs[i] = buttonIDs[i];
                this._buttonStateHandles[i] = new InputActionButtonStateHandles(started, performed, finished);
            }

            for (int i = 0; i < vector1DIDs.Count; ++i)
            {
                var performed = InputManager.ActionVector1DResources.Add(delegate
                { });

                this._vector1DHandlesIDs[i] = vector1DIDs[i];
                this._vector1DHandles[i] = new InputActionVector1DHandles(performed);
            }

            for (int i = 0; i < vector2DIDs.Count; ++i)
            {
                var performed = InputManager.ActionVector2DResources.Add(delegate
                { });

                this._vector2DHandlesIDs[i] = vector2DIDs[i];
                this._vector2DHandles[i] = new InputActionVector2DHandles(performed);
            }
        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Abonne cet objet à l'InputManager pour recevoir les événements
        /// </summary>
        public void Enable()
        {
            if (!this._enabled)
            {
                InputManager.Handles += this;
                this._enabled = true;
            }
        }

        /// <summary>
        /// Désbonne cet objet à l'InputManager pour recevoir les événements
        /// </summary>
        public void Disable()
        {
            if (this._enabled)
            {
                InputManager.Handles -= this;
                this._enabled = false;
            }
        }

        /// <summary>
        /// Récupère les événements liés un InputAction de type ButtonState à partir de son ID.
        /// </summary>
        /// <param name="key">L'ID de l'action</param>
        /// <returns>Les actions liées à cet id</returns>
        public ref InputActionButtonStateHandles GetButtonEvent(NativeString key)
        {
            // Si cette action n'existe pas dans la liste, on la crée

            if (!this._buttonStateHandlesIDs.Contains(key))
            {
                this._buttonStateHandlesIDs.Add(key);

                var started = InputManager.ActionResources.Add(delegate
                { });
                var performed = InputManager.ActionResources.Add(delegate
                { });
                var finished = InputManager.ActionResources.Add(delegate
                { });

                this._buttonStateHandles.Add(new InputActionButtonStateHandles(started, performed, finished));
            }

            // Si l'objet est actif, on ajoute également cette nouvelle action
            // aux Handles de l'InputManager

            if (this._enabled)
            {
                if (!InputManager.Handles._buttonStateHandlesIDs.Contains(key))
                {
                    InputManager.Handles._buttonStateHandlesIDs.Add(key);

                    var started = InputManager.ActionResources.Add(delegate
                    { });
                    var performed = InputManager.ActionResources.Add(delegate
                    { });
                    var finished = InputManager.ActionResources.Add(delegate
                    { });

                    InputManager.Handles._buttonStateHandles.Add(new InputActionButtonStateHandles(started, performed, finished));
                }

                int lIndexOf = InputManager.Handles._buttonStateHandlesIDs.IndexOf(key);
                int rIndexOf = this._buttonStateHandlesIDs.IndexOf(key);
                ref readonly InputActionButtonStateHandles lHandles = ref InputManager.Handles._buttonStateHandles[lIndexOf];
                ref readonly InputActionButtonStateHandles rHandles = ref this._buttonStateHandles[rIndexOf];
                lHandles.Started += rHandles.Started;
                lHandles.Performed += rHandles.Performed;
                lHandles.Finished += rHandles.Finished;
            }

            return ref this._buttonStateHandles[this._buttonStateHandlesIDs.IndexOf(key)];
        }

        /// <summary>
        /// Récupère les événements liés un InputAction de type Vector1D à partir de son ID.
        /// </summary>
        /// <param name="key">L'ID de l'action</param>
        /// <returns>Les actions liées à cet id</returns>
        public ref InputActionVector1DHandles GetVector1DEvent(NativeString key)
        {
            // Si cette action n'existe pas dans la liste, on la crée

            if (!this._vector1DHandlesIDs.Contains(key))
            {
                this._vector1DHandlesIDs.Add(key);

                var performed = InputManager.ActionVector1DResources.Add(delegate
                { });

                this._vector1DHandles.Add(new InputActionVector1DHandles(performed));
            }

            // Si l'objet est actif, on ajoute également cette nouvelle action
            // aux Handles de l'InputManager

            if (this._enabled)
            {
                if (!InputManager.Handles._vector1DHandlesIDs.Contains(key))
                {
                    InputManager.Handles._vector1DHandlesIDs.Add(key);

                    var performed = InputManager.ActionVector1DResources.Add(delegate
                    { });

                    InputManager.Handles._vector1DHandles.Add(new InputActionVector1DHandles(performed));
                }

                int lIndexOf = InputManager.Handles._vector1DHandlesIDs.IndexOf(key);
                int rIndexOf = this._vector1DHandlesIDs.IndexOf(key);
                ref readonly InputActionVector1DHandles lHandles = ref InputManager.Handles._vector1DHandles[lIndexOf];
                ref readonly InputActionVector1DHandles rHandles = ref this._vector1DHandles[rIndexOf];
                lHandles.Performed += rHandles.Performed;
            }

            return ref this._vector1DHandles[this._vector1DHandlesIDs.IndexOf(key)];
        }

        /// <summary>
        /// Récupère les événements liés un InputAction de type Vector2D à partir de son ID.
        /// </summary>
        /// <param name="key">L'ID de l'action</param>
        /// <returns>Les actions liées à cet id</returns>
        public ref InputActionVector2DHandles GetVector2DEvent(NativeString key)
        {
            // Si cette action n'existe pas dans la liste, on la crée

            if (!this._vector2DHandlesIDs.Contains(key))
            {
                this._vector2DHandlesIDs.Add(key);

                var performed = InputManager.ActionVector2DResources.Add(delegate
                { });

                this._vector2DHandles.Add(new InputActionVector2DHandles(performed));
            }

            // Si l'objet est actif, on ajoute également cette nouvelle action
            // aux Handles de l'InputManager

            if (this._enabled)
            {
                if (!InputManager.Handles._vector2DHandlesIDs.Contains(key))
                {
                    InputManager.Handles._vector2DHandlesIDs.Add(key);

                    var performed = InputManager.ActionVector2DResources.Add(delegate
                    { });

                    InputManager.Handles._vector2DHandles.Add(new InputActionVector2DHandles(performed));
                }

                int lIndexOf = InputManager.Handles._vector2DHandlesIDs.IndexOf(key);
                int rIndexOf = this._vector2DHandlesIDs.IndexOf(key);
                ref readonly InputActionVector2DHandles lHandles = ref InputManager.Handles._vector2DHandles[lIndexOf];
                ref readonly InputActionVector2DHandles rHandles = ref this._vector2DHandles[rIndexOf];
                lHandles.Performed += rHandles.Performed;
            }

            return ref this._vector2DHandles[this._vector2DHandlesIDs.IndexOf(key)];
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

            if (!this._buttonStateHandlesIDs.Contains(key))
            {
                this._buttonStateHandlesIDs.Add(key);

                var started = InputManager.ActionResources.Add(delegate
                { });
                var performed = InputManager.ActionResources.Add(delegate
                { });
                var finished = InputManager.ActionResources.Add(delegate
                { });

                this._buttonStateHandles.Add(new InputActionButtonStateHandles(started, performed, finished));
            }

            // Assigne l'action

            int thisIndexOf = this._buttonStateHandlesIDs.IndexOf(key);
            ref readonly InputActionButtonStateHandles thisHandles = ref this._buttonStateHandles[thisIndexOf];

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

            if (this._enabled)
            {
                if (!InputManager.Handles._buttonStateHandlesIDs.Contains(key))
                {
                    InputManager.Handles._buttonStateHandlesIDs.Add(key);

                    var started = InputManager.ActionResources.Add(delegate
                    { });
                    var performed = InputManager.ActionResources.Add(delegate
                    { });
                    var finished = InputManager.ActionResources.Add(delegate
                    { });

                    InputManager.Handles._buttonStateHandles.Add(new InputActionButtonStateHandles(started, performed, finished));
                }

                // Assigne l'action

                int mainIndexOf = InputManager.Handles._buttonStateHandlesIDs.IndexOf(key);
                ref readonly InputActionButtonStateHandles mainHandles = ref InputManager.Handles._buttonStateHandles[mainIndexOf];

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

            if (!this._vector1DHandlesIDs.Contains(key))
            {
                this._vector1DHandlesIDs.Add(key);

                var performed = InputManager.ActionVector1DResources.Add(delegate
                { });

                this._vector1DHandles.Add(new InputActionVector1DHandles(performed));
            }

            int thisIndexOf = this._vector1DHandlesIDs.IndexOf(key);
            ref readonly InputActionVector1DHandles thisHandles = ref this._vector1DHandles[thisIndexOf];
            thisHandles.Performed += callback;

            // Si l'objet est actif, on ajoute également cette nouvelle action
            // aux Handles de l'InputManager

            if (this._enabled)
            {
                if (!InputManager.Handles._vector1DHandlesIDs.Contains(key))
                {
                    InputManager.Handles._vector1DHandlesIDs.Add(key);

                    var performed = InputManager.ActionVector1DResources.Add(delegate
                    { });

                    InputManager.Handles._vector1DHandles.Add(new InputActionVector1DHandles(performed));
                }

                int mainIndexOf = InputManager.Handles._vector1DHandlesIDs.IndexOf(key);
                ref readonly InputActionVector1DHandles mainHandles = ref InputManager.Handles._vector1DHandles[mainIndexOf];
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

            if (!this._vector2DHandlesIDs.Contains(key))
            {
                this._vector2DHandlesIDs.Add(key);

                var performed = InputManager.ActionVector2DResources.Add(delegate
                { });

                this._vector2DHandles.Add(new InputActionVector2DHandles(performed));
            }

            int thisIndexOf = this._vector2DHandlesIDs.IndexOf(key);
            ref readonly InputActionVector2DHandles thisHandles = ref this._vector2DHandles[thisIndexOf];
            thisHandles.Performed += callback;

            // Si l'objet est actif, on ajoute également cette nouvelle action
            // aux Handles de l'InputManager

            if (this._enabled)
            {
                if (!InputManager.Handles._vector2DHandlesIDs.Contains(key))
                {
                    InputManager.Handles._vector2DHandlesIDs.Add(key);

                    var performed = InputManager.ActionVector2DResources.Add(delegate
                    { });

                    InputManager.Handles._vector2DHandles.Add(new InputActionVector2DHandles(performed));
                }

                int mainIndexOf = InputManager.Handles._vector2DHandlesIDs.IndexOf(key);
                ref readonly InputActionVector2DHandles mainHandles = ref InputManager.Handles._vector2DHandles[mainIndexOf];
                mainHandles.Performed += callback;
            }
        }

        #endregion

        #region Opérateurs

        /// <summary>
        /// Abonne les handles de <paramref name="right"/> aux handles de <paramref name="left"/>
        /// </summary>
        /// <param name="left">L'objet auquel s'abonner</param>
        /// <param name="right">L'abonné</param>
        /// <returns><paramref name="left"/>, avec les abonnements de <paramref name="right"/></returns>
        public static InputControls operator +(InputControls left, InputControls right)
        {
            for (int i = 0; i < right._buttonStateHandlesIDs.Count; ++i)
            {
                NativeString rKey = right._buttonStateHandlesIDs[i];

                if (!left._buttonStateHandlesIDs.Contains(rKey))
                {
                    left._buttonStateHandlesIDs.Add(rKey);

                    var started = InputManager.ActionResources.Add(delegate
                    { });
                    var performed = InputManager.ActionResources.Add(delegate
                    { });
                    var finished = InputManager.ActionResources.Add(delegate
                    { });

                    left._buttonStateHandles.Add(new InputActionButtonStateHandles(started, performed, finished));
                }

                int leftIndexOf = left._buttonStateHandlesIDs.IndexOf(rKey);
                ref readonly InputActionButtonStateHandles lHandles = ref left._buttonStateHandles[leftIndexOf];
                ref readonly InputActionButtonStateHandles rHandles = ref right._buttonStateHandles[i];
                lHandles.Started += rHandles.Started;
                lHandles.Performed += rHandles.Performed;
                lHandles.Finished += rHandles.Finished;
            }

            for (int i = 0; i < right._vector1DHandlesIDs.Count; ++i)
            {
                NativeString rKey = right._vector1DHandlesIDs[i];

                if (!left._vector1DHandlesIDs.Contains(rKey))
                {
                    left._vector1DHandlesIDs.Add(rKey);

                    var performed = InputManager.ActionVector1DResources.Add(delegate
                    { });

                    left._vector1DHandles.Add(new InputActionVector1DHandles(performed));
                }

                int leftIndexOf = left._vector1DHandlesIDs.IndexOf(rKey);
                ref readonly InputActionVector1DHandles lHandles = ref left._vector1DHandles[leftIndexOf];
                ref readonly InputActionVector1DHandles rHandles = ref right._vector1DHandles[i];
                lHandles.Performed += rHandles.Performed;
            }

            for (int i = 0; i < right._vector2DHandlesIDs.Count; ++i)
            {
                NativeString rKey = right._vector2DHandlesIDs[i];

                if (!left._vector2DHandlesIDs.Contains(rKey))
                {
                    left._vector2DHandlesIDs.Add(rKey);

                    var performed = InputManager.ActionVector2DResources.Add(delegate
                    { });

                    left._vector2DHandles.Add(new InputActionVector2DHandles(performed));
                }

                int leftIndexOf = left._vector2DHandlesIDs.IndexOf(rKey);
                ref readonly InputActionVector2DHandles lHandles = ref left._vector2DHandles[leftIndexOf];
                ref readonly InputActionVector2DHandles rHandles = ref right._vector2DHandles[i];
                lHandles.Performed += rHandles.Performed;
            }

            return left;
        }

        /// <summary>
        /// Désbonne les handles de <paramref name="right"/> aux handles de <paramref name="left"/>
        /// </summary>
        /// <param name="left">L'objet auquel s'abonner</param>
        /// <param name="right">L'abonné</param>
        /// <returns><paramref name="left"/>, sans les abonnements de <paramref name="right"/></returns>
        public static InputControls operator -(InputControls left, InputControls right)
        {
            for (int i = 0; i < right._buttonStateHandlesIDs.Count; ++i)
            {
                int leftIndexOf = left._buttonStateHandlesIDs.IndexOf(right._buttonStateHandlesIDs[i]);

                if (leftIndexOf != -1)
                {
                    ref readonly InputActionButtonStateHandles lHandles = ref left._buttonStateHandles[leftIndexOf];
                    ref readonly InputActionButtonStateHandles rHandles = ref right._buttonStateHandles[i];
                    lHandles.Started -= rHandles.Started;
                    lHandles.Performed -= rHandles.Performed;
                    lHandles.Finished -= rHandles.Finished;
                }
            }

            for (int i = 0; i < right._vector1DHandlesIDs.Count; ++i)
            {
                int leftIndexOf = left._vector1DHandlesIDs.IndexOf(right._vector1DHandlesIDs[i]);

                if (leftIndexOf != -1)
                {
                    ref readonly InputActionVector1DHandles lHandles = ref left._vector1DHandles[leftIndexOf];
                    ref readonly InputActionVector1DHandles rHandles = ref right._vector1DHandles[i];
                    lHandles.Performed -= rHandles.Performed;
                }
            }

            for (int i = 0; i < right._vector2DHandlesIDs.Count; ++i)
            {
                int leftIndexOf = left._vector2DHandlesIDs.IndexOf(right._vector2DHandlesIDs[i]);

                if (leftIndexOf != -1)
                {
                    ref readonly InputActionVector2DHandles lHandles = ref left._vector2DHandles[i];
                    ref readonly InputActionVector2DHandles rHandles = ref right._vector2DHandles[i];
                    lHandles.Performed -= rHandles.Performed;
                }
            }

            return left;
        }

        #endregion
    }
}
