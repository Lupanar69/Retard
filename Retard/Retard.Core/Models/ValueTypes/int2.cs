using System;
using Microsoft.Xna.Framework;

namespace Retard.Core.Models.ValueTypes
{
    // int2 is similar to Vector2, but with integers instead of floats
    // Useful for various grid related things.
    // Récupéré de https://gist.github.com/FreyaHolmer/5743d0ad1c09b64cb548
    public struct int2
    {
        #region Variables statiques

        public static int2 Zero = new(0, 0);
        public static int2 One = new(1, 1);
        public static int2 Right = new(1, 0);
        public static int2 Left = new(-1, 0);
        public static int2 Up = new(0, 1);
        public static int2 Down = new(0, -1);

        #endregion

        #region Propriétés

        /// <summary>
        /// Coordonnée X
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Coordonnée Y
        /// </summary>
        public int Y { get; set; }

        // Swizzling

        public int2 XX
        {
            get { return new int2(this.X, this.X); }
        }

        public int2 XY
        {
            get { return new int2(this.X, this.Y); }
        }

        public int2 YX
        {
            get { return new int2(this.Y, this.X); }
        }

        public int2 YY
        {
            get { return new int2(this.Y, this.Y); }
        }

        // Derived Data

        /// <summary>
        /// Aire de la surface représentée par ces dimensions
        /// </summary>
        public int Area
        {
            get { return Math.Abs(X * Y); }
        }

        /// <summary>
        /// Aire de la surface représentée par ces dimensions
        /// </summary>
        public int SignedArea
        {
            get { return X * Y; }
        }

        /// <summary>
        /// <see langword="true"/> si la surface est un carré
        /// </summary>
        public bool IsSquare
        {
            get { return X == Y; }
        }

        /// <summary>
        /// La coordonnée la plus petite
        /// </summary>
        public float Min
        {
            get { return Math.Min(this.X, this.Y); }
        }

        /// <summary>
        /// La coordonnée la plus grande
        /// </summary>
        public float Max
        {
            get { return Math.Max(this.X, this.Y); }
        }

        /// <summary>
        /// Permet de choisir une coordonnée via un numéro
        /// </summary>
        public int this[int i]
        {
            get
            {
                if (i == 0)
                    return X;
                else if (i == 1)
                    return Y;
                else
                    throw new System.IndexOutOfRangeException("Expected an index of 0 or 1. " + i + " is out of range");
            }
            set
            {
                if (i == 0)
                    X = value;
                else if (i == 1)
                    Y = value;
                else
                    throw new System.IndexOutOfRangeException("Expected an index of 0 or 1. " + i + " is out of range");
            }
        }

        #endregion

        #region Constructeurs

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="x">Coordonnée X</param>
        /// <param name="y">Coordonnée Y</param>
        public int2(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="x">Coordonnée X et Y</param>
        public int2(int size)
        {
            this.X = size;
            this.Y = size;
        }

        #endregion

        #region Fonctions statiques publiques

        #region Opérateurs

        // Typecasting

        /// <summary>
        /// int2 à Vector2
        /// </summary>
        public static implicit operator Vector2(int2 i)
        {
            return new Vector2(i.X, i.Y);
        }

        /// <summary>
        /// Vector2 to int2. Explicite dû à la perte de précision
        /// </summary>
        public static explicit operator int2(Vector2 v)
        {
            return v.FloorToInt2();
        }

        // Add

        public static int2 operator +(int2 a, int2 b)
        {
            return new int2(a.X + b.X, a.Y + b.Y);
        }

        public static int2 operator +(int2 a, int v)
        {
            return new int2(a.X + v, a.Y + v);
        }

        public static int2 operator +(int v, int2 a)
        {
            return new int2(a.X + v, a.Y + v);
        }

        public static Vector2 operator +(int2 a, float v)
        {
            return new Vector2(a.X + v, a.Y + v);
        }

        public static Vector2 operator +(float v, int2 a)
        {
            return new Vector2(a.X + v, a.Y + v);
        }

        // Subtract

        public static int2 operator -(int2 a, int2 b)
        {
            return new int2(a.X - b.X, a.Y - b.Y);
        }

        public static int2 operator -(int2 a, int v)
        {
            return new int2(a.X - v, a.Y - v);
        }

        public static int2 operator -(int v, int2 a)
        {
            return new int2(v - a.X, v - a.Y);
        }

        public static Vector2 operator -(int2 a, float v)
        {
            return new Vector2(a.X - v, a.Y - v);
        }

        public static Vector2 operator -(float v, int2 a)
        {
            return new Vector2(v - a.X, v - a.Y);
        }

        // Multiply

        public static int2 operator *(int2 a, int2 b)
        {
            return new int2(a.X * b.X, a.Y * b.Y);
        }

        public static int2 operator *(int2 a, int v)
        {
            return new int2(a.X * v, a.Y * v);
        }

        public static int2 operator *(int v, int2 a)
        {
            return new int2(a.X * v, a.Y * v);
        }

        public static Vector2 operator *(int2 a, float v)
        {
            return new Vector2(a.X * v, a.Y * v);
        }

        public static Vector2 operator *(float v, int2 a)
        {
            return new Vector2(a.X * v, a.Y * v);
        }

        // Divide

        public static int2 operator /(int2 a, int2 b)
        {
            return new int2(a.X / b.X, a.Y / b.Y);
        }

        public static int2 operator /(int2 a, int v)
        {
            return new int2(a.X / v, a.Y / v);
        }

        public static int2 operator /(int v, int2 a)
        {
            return new int2(v / a.X, v / a.Y);
        }

        public static Vector2 operator /(int2 a, float v)
        {
            return new Vector2(a.X / v, a.Y / v);
        }

        public static Vector2 operator /(float v, int2 a)
        {
            return new Vector2(v / a.X, v / a.Y);
        }

        #endregion

        /// <summary>
        /// Lerp de a à b
        /// </summary>
        /// <param name="a">Départ</param>
        /// <param name="b">Arrivée</param>
        /// <param name="t">Temps</param>
        /// <param name="extrapolate">Si <see langword="true"/>, permet au t de dépasser la limite 0-1</param>
        /// <returns>Le lerp de a à b</returns>
        public static Vector2 Lerp(int2 a, int2 b, float t, bool extrapolate = false)
        {
            t = extrapolate ? t : Math.Clamp(t, 0, 1);
            return a * (1f - t) + b * t;
        }

        /// <summary>
        /// Pour afficher les coordonnées dans la console
        /// </summary>
        public override string ToString()
        {
            return "[ " + this.X + " , " + this.Y + " ]";
        }

        #endregion
    }

    /// <summary>
    /// Contient les méthodes de conversion
    /// </summary>
    public static class int2extensions
    {
        #region Fonctions statiques publiques

        public static int2 RoundToInt2(this Vector2 v)
        {
            return new int2((int)Math.Round(v.X), (int)Math.Round(v.Y));
        }
        public static int2 FloorToInt2(this Vector2 v)
        {
            return new int2((int)Math.Floor(v.X), (int)Math.Floor(v.Y));
        }
        public static int2 CeilToInt2(this Vector2 v)
        {
            return new int2((int)Math.Ceiling(v.X), (int)Math.Ceiling(v.Y));
        }

        #endregion
    }
}
