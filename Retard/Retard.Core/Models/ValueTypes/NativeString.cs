using System;
using Arch.LowLevel;

namespace Retard.Core.Models.ValueTypes
{
    /// <summary>
    /// Version blittable de System.String
    /// </summary>
    public struct NativeString : IDisposable
    {
        #region Propriétés

        /// <summary>
        /// Longueur de la string
        /// </summary>
        public int Length { get; init; }

        #endregion

        #region Variables d'instance

        /// <summary>
        /// Le tableau contenant chaque caractère de la string.
        /// Nécessaire d'être non-géré pour garder la struct blittable.
        /// </summary>
        private UnsafeArray<char> _arr;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="str">La string à convertir en NativeString</param>
        public NativeString(string str)
        {
            this.Length = str.Length;
            this._arr = new UnsafeArray<char>(this.Length);

            for (int i = 0; i < this.Length; i++)
            {
                this._arr[i] = str[i];
            }
        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Indexeur
        /// </summary>
        /// <param name="index">La position de la valeur dans la liste</param>
        /// <returns>Le caractère à l'emplacement <paramref name="index"/></returns>
        /// <exception cref="IndexOutOfRangeException">L'index est en dehors du tableau</exception>
        public char this[int index]
        {
            get
            {
                if (index < 0 || index >= this.Length)
                {
                    throw new IndexOutOfRangeException($"Index {index} is out of range for NativeString of length {this.Length}.");
                }

                return this._arr[index];
            }

            set
            {
                if (index < 0 || index >= this.Length)
                {
                    throw new IndexOutOfRangeException($"Index {index} is out of range for NativeString of length {this.Length}.");
                }

                this._arr[index] = value;
            }
        }

        /// <summary>
        /// Compare l'ordre de tri de deux2 NativeStrings
        /// </summary>
        /// <param name="other">La NativeString à comparer</param>
        /// <returns>L'ordre de tri de cette NativeString</returns>
        public int CompareTo(NativeString other)
        {
            int minLength = Math.Min(this.Length, other.Length);

            for (int i = 0; i < minLength; i++)
            {
                int result = this[i].CompareTo(other[i]);

                if (result != 0)
                {
                    return result;
                }
            }

            return this.Length.CompareTo(other.Length);
        }

        /// <summary>
        /// Comparateur d'égalité
        /// </summary>
        /// <param name="obj">La NativeString à comparer</param>
        /// <returns><see langword="true"/> si les deux instances sont égales</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (NativeString)obj;

            if (this.Length != other.Length)
            {
                return false;
            }

            for (int i = 0; i < this.Length; i++)
            {
                if (this[i] != other[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Génère un hash unique pour la comparaison
        /// de deux NativeStrings
        /// </summary>
        /// <returns>Un hash unique</returns>
        public override int GetHashCode()
        {
            int hash = 17;
            for (int i = 0; i < this.Length; i++)
            {
                hash = hash * 31 + this[i].GetHashCode();
            }
            return hash;
        }

        /// <summary>
        /// Comparateur d'égalité
        /// </summary>
        /// <param name="lhs">La NativeString à comparer</param>
        /// <param name="rhs">La NativeString à comparer</param>
        /// <returns><see langword="true"/> si les deux instances sont égales</returns>
        public static bool operator ==(NativeString lhs, NativeString rhs)
        {
            return lhs.Equals(rhs);
        }

        /// <summary>
        /// Comparateur d'inégalité
        /// </summary>
        /// <param name="lhs">La NativeString à comparer</param>
        /// <param name="rhs">La NativeString à comparer</param>
        /// <returns><see langword="true"/> si les deux instances sont inégales</returns>
        public static bool operator !=(NativeString lhs, NativeString rhs)
        {
            return !(lhs == rhs);
        }

        /// <summary>
        /// Nettoie la NativeString
        /// </summary>
        public void Dispose()
        {
            this._arr.Dispose();
        }

        /// <summary>
        /// Convertit la NativeString en string
        /// </summary>
        /// <returns>La chaîne de caractère contenue dans cette struct</returns>
        public override string ToString()
        {
            return this._arr.AsSpan().ToString();
        }

        #endregion
    }
}