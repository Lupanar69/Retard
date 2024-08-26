using System.Runtime.CompilerServices;
using Arch.LowLevel;
using Retard.Engine.Models.ValueTypes;
using Retard.Engine.ViewModels.Input;

namespace Retard.Engine.Models.Assets.Input
{
    /// <summary>
    /// Contient les handles des différents types d'action
    /// </summary>
    public class InputHandles
    {
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

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        public InputHandles()
        {
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
        /// <param name="handles">Les InputHandles à copier</param>
        public InputHandles(in InputHandles handles) : this(handles._buttonStateHandlesIDs, handles._vector1DHandlesIDs, handles._vector2DHandlesIDs)
        {
            for (int i = 0; i < handles._buttonStateHandlesIDs.Count; ++i)
            {
                NativeString rKey = handles._buttonStateHandlesIDs[i];
                int leftIndexOf = this._buttonStateHandlesIDs.IndexOf(rKey);
                ref readonly InputActionButtonStateHandles lHandles = ref this._buttonStateHandles[leftIndexOf];
                ref readonly InputActionButtonStateHandles rHandles = ref handles._buttonStateHandles[i];
                lHandles.Started += rHandles.Started;
                lHandles.Performed += rHandles.Performed;
                lHandles.Finished += rHandles.Finished;
            }

            for (int i = 0; i < handles._vector1DHandlesIDs.Count; ++i)
            {
                NativeString rKey = handles._vector1DHandlesIDs[i];
                int leftIndexOf = this._vector1DHandlesIDs.IndexOf(rKey);
                ref readonly InputActionVector1DHandles lHandles = ref this._vector1DHandles[leftIndexOf];
                ref readonly InputActionVector1DHandles rHandles = ref handles._vector1DHandles[i];
                lHandles.Performed += rHandles.Performed;
            }

            for (int i = 0; i < handles._vector2DHandlesIDs.Count; ++i)
            {
                NativeString rKey = handles._vector2DHandlesIDs[i];
                int leftIndexOf = this._vector2DHandlesIDs.IndexOf(rKey);
                ref readonly InputActionVector2DHandles lHandles = ref this._vector2DHandles[leftIndexOf];
                ref readonly InputActionVector2DHandles rHandles = ref handles._vector2DHandles[i];
                lHandles.Performed += rHandles.Performed;
            }
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="buttonIDs">La liste des IDs des actions de type ButtonState</param>
        /// <param name="vector1DIDs">La liste des IDs des actions de type Vector1D</param>
        /// <param name="vector2DIDs">La liste des IDs des actions de type Vector2D</param>
        public InputHandles(UnsafeList<NativeString> buttonIDs, UnsafeList<NativeString> vector1DIDs, UnsafeList<NativeString> vector2DIDs)
        {
            this._buttonStateHandlesIDs = new UnsafeList<NativeString>(buttonIDs.Capacity);
            this._vector1DHandlesIDs = new UnsafeList<NativeString>(vector1DIDs.Capacity);
            this._vector2DHandlesIDs = new UnsafeList<NativeString>(vector2DIDs.Capacity);

            this._buttonStateHandles = new UnsafeList<InputActionButtonStateHandles>(buttonIDs.Capacity);
            this._vector1DHandles = new UnsafeList<InputActionVector1DHandles>(vector1DIDs.Capacity);
            this._vector2DHandles = new UnsafeList<InputActionVector2DHandles>(vector2DIDs.Capacity);


            for (int i = 0; i < buttonIDs.Count; ++i)
            {
                var started = InputManager.Instance.ActionButtonResources.Add(delegate
                { });
                var performed = InputManager.Instance.ActionButtonResources.Add(delegate
                { });
                var finished = InputManager.Instance.ActionButtonResources.Add(delegate
                { });

                this._buttonStateHandlesIDs.Add(buttonIDs[i]);
                this._buttonStateHandles.Add(new InputActionButtonStateHandles(started, performed, finished));
            }

            for (int i = 0; i < vector1DIDs.Count; ++i)
            {
                var performed = InputManager.Instance.ActionVector1DResources.Add(delegate
                { });

                this._vector1DHandlesIDs.Add(vector1DIDs[i]);
                this._vector1DHandles.Add(new InputActionVector1DHandles(performed));
            }

            for (int i = 0; i < vector2DIDs.Count; ++i)
            {
                var performed = InputManager.Instance.ActionVector2DResources.Add(delegate
                { });

                this._vector2DHandlesIDs.Add(vector2DIDs[i]);
                this._vector2DHandles.Add(new InputActionVector2DHandles(performed));
            }
        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Indique si l'id est enregistré dans la liste des actions de type ButtonState
        /// </summary>
        /// <param name="key">L'ID de l'action</param>
        /// <returns>TRUE si l'action est enregistrée</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ButtonStateHandlesExist(NativeString key)
        {
            return this._buttonStateHandlesIDs.Contains(key);
        }

        /// <summary>
        /// Indique si l'id est enregistré dans la liste des actions de type Vector1D
        /// </summary>
        /// <param name="key">L'ID de l'action</param>
        /// <returns>TRUE si l'action est enregistrée</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Vector1DHandlesExist(NativeString key)
        {
            return this._vector1DHandlesIDs.Contains(key);
        }

        /// <summary>
        /// Indique si l'id est enregistré dans la liste des actions de type Vector2D
        /// </summary>
        /// <param name="key">L'ID de l'action</param>
        /// <returns>TRUE si l'action est enregistrée</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Vector2DHandleExist(NativeString key)
        {
            return this._vector2DHandlesIDs.Contains(key);
        }

        /// <summary>
        /// Ajoute un événement lié un InputAction de type ButtonState à partir de son ID.
        /// </summary>
        /// <param name="key">L'ID de l'action</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddButtonStateHandles(NativeString key)
        {
            this._buttonStateHandlesIDs.Add(key);

            var started = InputManager.Instance.ActionButtonResources.Add(delegate
            { });
            var performed = InputManager.Instance.ActionButtonResources.Add(delegate
            { });
            var finished = InputManager.Instance.ActionButtonResources.Add(delegate
            { });

            this._buttonStateHandles.Add(new InputActionButtonStateHandles(started, performed, finished));
        }

        /// <summary>
        /// Ajoute un événement lié un InputAction de type Vector1D à partir de son ID.
        /// </summary>
        /// <param name="key">L'ID de l'action</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddVector1DHandles(NativeString key)
        {
            this._vector1DHandlesIDs.Add(key);

            var performed = InputManager.Instance.ActionVector1DResources.Add(delegate
            { });

            this._vector1DHandles.Add(new InputActionVector1DHandles(performed));
        }

        /// <summary>
        /// Ajoute un événement lié un InputAction de type Vector2D à partir de son ID.
        /// </summary>
        /// <param name="key">L'ID de l'action</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddVector2DHandles(NativeString key)
        {
            this._vector2DHandlesIDs.Add(key);

            var performed = InputManager.Instance.ActionVector2DResources.Add(delegate
            { });

            this._vector2DHandles.Add(new InputActionVector2DHandles(performed));
        }

        /// <summary>
        /// Récupère les événements liés un InputAction de type ButtonState à partir de son ID.
        /// </summary>
        /// <param name="key">L'ID de l'action</param>
        /// <returns>Les actions liées à cet id</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref readonly InputActionButtonStateHandles GetButtonEvent(NativeString key)
        {
            return ref this._buttonStateHandles[this._buttonStateHandlesIDs.IndexOf(key)];
        }

        /// <summary>
        /// Récupère les événements liés un InputAction de type Vector1D à partir de son ID.
        /// </summary>
        /// <param name="key">L'ID de l'action</param>
        /// <returns>Les actions liées à cet id</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref readonly InputActionVector1DHandles GetVector1DEvent(NativeString key)
        {
            return ref this._vector1DHandles[this._vector1DHandlesIDs.IndexOf(key)];
        }

        /// <summary>
        /// Récupère les événements liés un InputAction de type Vector2D à partir de son ID.
        /// </summary>
        /// <param name="key">L'ID de l'action</param>
        /// <returns>Les actions liées à cet id</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref readonly InputActionVector2DHandles GetVector2DEvent(NativeString key)
        {
            return ref this._vector2DHandles[this._vector2DHandlesIDs.IndexOf(key)];
        }

        #endregion

        #region Opérateurs

        /// <summary>
        /// Abonne les handles de <paramref name="right"/> aux handles de <paramref name="left"/>
        /// </summary>
        /// <param name="left">L'objet auquel s'abonner</param>
        /// <param name="right">L'abonné</param>
        /// <returns><paramref name="left"/>, avec les abonnements de <paramref name="right"/></returns>
        public static InputHandles operator +(in InputHandles left, in InputHandles right)
        {
            for (int i = 0; i < right._buttonStateHandlesIDs.Count; ++i)
            {
                NativeString rKey = right._buttonStateHandlesIDs[i];

                if (!left._buttonStateHandlesIDs.Contains(rKey))
                {
                    left._buttonStateHandlesIDs.Add(rKey);

                    var started = InputManager.Instance.ActionButtonResources.Add(delegate
                    { });
                    var performed = InputManager.Instance.ActionButtonResources.Add(delegate
                    { });
                    var finished = InputManager.Instance.ActionButtonResources.Add(delegate
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

                    var performed = InputManager.Instance.ActionVector1DResources.Add(delegate
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

                    var performed = InputManager.Instance.ActionVector2DResources.Add(delegate
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
        public static InputHandles operator -(in InputHandles left, in InputHandles right)
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