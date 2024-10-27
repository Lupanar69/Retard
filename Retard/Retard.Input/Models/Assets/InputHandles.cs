using System.Runtime.CompilerServices;
using Arch.LowLevel;
using FixedStrings;
using Retard.Input.ViewModels;

namespace Retard.Input.Models.Assets
{
    /// <summary>
    /// Contient les handles des différents types d'action
    /// </summary>
    public sealed class InputHandles
    {
        #region Variables d'instance

        /// <summary>
        /// La liste des abonnements pour chaque Input action de type ButtonState
        /// </summary>
        private UnsafeList<FixedString32> _buttonStateHandlesIDs;

        /// <summary>
        /// La liste des abonnements pour chaque Input action de type Vector1D
        /// </summary>
        private UnsafeList<FixedString32> _vector1DHandlesIDs;

        /// <summary>
        /// La liste des abonnements pour chaque Input action de type Vector2D
        /// </summary>
        private UnsafeList<FixedString32> _vector2DHandlesIDs;

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
            this._buttonStateHandlesIDs = new UnsafeList<FixedString32>(1);
            this._vector1DHandlesIDs = new UnsafeList<FixedString32>(1);
            this._vector2DHandlesIDs = new UnsafeList<FixedString32>(1);

            this._buttonStateHandles = new UnsafeList<InputActionButtonStateHandles>(1);
            this._vector1DHandles = new UnsafeList<InputActionVector1DHandles>(1);
            this._vector2DHandles = new UnsafeList<InputActionVector2DHandles>(1);
        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Indique si l'id est enregistré dans la liste des actions de type ButtonState
        /// </summary>
        /// <param name="key">L'ID de l'action</param>
        /// <returns>TRUE si l'action est enregistrée</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ButtonStateHandleExists(FixedString32 key)
        {
            return this._buttonStateHandlesIDs.Contains(key);
        }

        /// <summary>
        /// Indique si l'id est enregistré dans la liste des actions de type Vector1D
        /// </summary>
        /// <param name="key">L'ID de l'action</param>
        /// <returns>TRUE si l'action est enregistrée</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Vector1DHandleExists(FixedString32 key)
        {
            return this._vector1DHandlesIDs.Contains(key);
        }

        /// <summary>
        /// Indique si l'id est enregistré dans la liste des actions de type Vector2D
        /// </summary>
        /// <param name="key">L'ID de l'action</param>
        /// <returns>TRUE si l'action est enregistrée</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Vector2DHandleExists(FixedString32 key)
        {
            return this._vector2DHandlesIDs.Contains(key);
        }

        /// <summary>
        /// Ajoute un événement lié un InputAction de type ButtonState à partir de son ID.
        /// </summary>
        /// <param name="key">L'ID de l'action</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddButtonStateEvent(FixedString32 key)
        {
            var started = InputManager.Instance.ActionButtonResources.Add(delegate
            { });
            var performed = InputManager.Instance.ActionButtonResources.Add(delegate
            { });
            var finished = InputManager.Instance.ActionButtonResources.Add(delegate
            { });

            this._buttonStateHandlesIDs.Add(key);
            this._buttonStateHandles.Add(new InputActionButtonStateHandles(started, performed, finished));
        }

        /// <summary>
        /// Ajoute un événement lié un InputAction de type Vector1D à partir de son ID.
        /// </summary>
        /// <param name="key">L'ID de l'action</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddVector1DEvent(FixedString32 key)
        {
            var performed = InputManager.Instance.ActionVector1DResources.Add(delegate
            { });

            this._vector1DHandlesIDs.Add(key);
            this._vector1DHandles.Add(new InputActionVector1DHandles(performed));
        }

        /// <summary>
        /// Ajoute un événement lié un InputAction de type Vector2D à partir de son ID.
        /// </summary>
        /// <param name="key">L'ID de l'action</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddVector2DEvent(FixedString32 key)
        {
            var performed = InputManager.Instance.ActionVector2DResources.Add(delegate
            { });

            this._vector2DHandlesIDs.Add(key);
            this._vector2DHandles.Add(new InputActionVector2DHandles(performed));
        }

        /// <summary>
        /// Récupère les événements liés un InputAction de type ButtonState à partir de son ID.
        /// </summary>
        /// <param name="key">L'ID de l'action</param>
        /// <returns>Les actions liées à cet id</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref readonly InputActionButtonStateHandles GetButtonEvent(FixedString32 key)
        {
            return ref this._buttonStateHandles[this._buttonStateHandlesIDs.IndexOf(key)];
        }

        /// <summary>
        /// Récupère les événements liés un InputAction de type Vector1D à partir de son ID.
        /// </summary>
        /// <param name="key">L'ID de l'action</param>
        /// <returns>Les actions liées à cet id</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref readonly InputActionVector1DHandles GetVector1DEvent(FixedString32 key)
        {
            return ref this._vector1DHandles[this._vector1DHandlesIDs.IndexOf(key)];
        }

        /// <summary>
        /// Récupère les événements liés un InputAction de type Vector2D à partir de son ID.
        /// </summary>
        /// <param name="key">L'ID de l'action</param>
        /// <returns>Les actions liées à cet id</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref readonly InputActionVector2DHandles GetVector2DEvent(FixedString32 key)
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
                FixedString32 rKey = right._buttonStateHandlesIDs[i];

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
                FixedString32 rKey = right._vector1DHandlesIDs[i];

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
                FixedString32 rKey = right._vector2DHandlesIDs[i];

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