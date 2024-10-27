using System;
using Arch.LowLevel;
using FixedStrings;

namespace Retard.Core.Models.ValueTypes
{
    /// <summary>
    /// Version blittable de System.String
    /// </summary>
    public readonly struct NativeString : IEquatable<NativeString>, IComparable<NativeString>, IDisposable
    {
        #region Variables d'instance

        /// <summary>
        /// Longueur de la string
        /// </summary>
        public readonly short Length;

        /// <summary>
        /// Le tableau contenant chaque caractère de la string.
        /// Nécessaire d'être non-géré pour garder la struct blittable.
        /// </summary>
        private readonly UnsafeArray<char> _arr;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="str">La string à convertir en NativeString</param>
        public NativeString(NativeString str)
        {
            this.Length = str.Length;
            this._arr = new UnsafeArray<char>(this.Length);

            for (int i = 0; i < this.Length; ++i)
            {
                this._arr[i] = str[i];
            }
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="str">La string à convertir en NativeString</param>
        public NativeString(string str) : this(str.AsSpan())
        {

        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="str">La string à convertir en NativeString</param>
        public NativeString(ReadOnlySpan<char> str)
        {
            this.Length = (short)str.Length;
            this._arr = new UnsafeArray<char>(this.Length);

            for (int i = 0; i < this.Length; ++i)
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

            for (int i = 0; i < minLength; ++i)
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
            return obj is NativeString ns && Equals(ns);
        }

        /// <summary>
        /// Comparateur d'égalité
        /// </summary>
        /// <param name="other">La NativeString à comparer</param>
        /// <returns><see langword="true"/> si les deux instances sont égales</returns>
        public bool Equals(NativeString other)
        {
            if (this.Length != other.Length)
            {
                return false;
            }

            for (int i = 0; i < this.Length; ++i)
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
            for (int i = 0; i < this.Length; ++i)
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
        /// Convertisseur
        /// </summary>
        /// <param name="str">La string à convertir</param>
        public static implicit operator NativeString(string str)
        {
            return new NativeString(str.AsSpan());
        }

        /// <summary>
        /// Convertisseur
        /// </summary>
        /// <param name="str">La string à convertir</param>
        public static implicit operator NativeString(ReadOnlySpan<char> str)
        {
            return new NativeString(str);
        }

        /// <summary>
        /// Convertisseur
        /// </summary>
        /// <param name="str">La string à convertir</param>
        public static implicit operator NativeString(FixedString8 str)
        {
            return new NativeString(str.AsSpan());
        }

        /// <summary>
        /// Convertisseur
        /// </summary>
        /// <param name="str">La string à convertir</param>
        public static implicit operator NativeString(FixedString16 str)
        {
            return new NativeString(str.AsSpan());
        }

        /// <summary>
        /// Convertisseur
        /// </summary>
        /// <param name="str">La string à convertir</param>
        public static implicit operator NativeString(FixedString32 str)
        {
            return new NativeString(str.AsSpan());
        }

        /// <summary>
        /// Convertisseur
        /// </summary>
        /// <param name="str">La string à convertir</param>
        public static implicit operator NativeString(FixedString64 str)
        {
            return new NativeString(str.AsSpan());
        }

        /// <summary>
        /// Convertisseur
        /// </summary>
        /// <param name="str">La string à convertir</param>
        public static implicit operator string(NativeString str)
        {
            return str.ToString();
        }

        /// <summary>
        /// Convertisseur
        /// </summary>
        /// <param name="str">La string à convertir</param>
        public static implicit operator ReadOnlySpan<char>(NativeString str)
        {
            return str.AsSpan();
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
        public readonly override string ToString()
        {
            return this._arr.AsSpan().ToString();
        }

        /// <summary>
        /// Convertit la NativeString en string
        /// </summary>
        /// <returns>La chaîne de caractère contenue dans cette struct</returns>
        public readonly ReadOnlySpan<char> AsSpan()
        {
            return this._arr.AsSpan();
        }

        #endregion
    }
}