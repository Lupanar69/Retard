using System;
using Microsoft.Xna.Framework;

namespace Retard.Core.Models.ValueTypes
{
    /// <summary>
    /// A struct that defines a rectangle
    /// </summary>
    [Serializable]
    public struct Rectangle : IEquatable<Rectangle>
    {
        #region Propriétés

        /// <summary>
        /// Specifies the Coordinates of the Rectangle (x and y at 0 being the top left)
        /// </summary>
        public int2 Position { get; set; }

        /// <summary>
        /// Specifies the Width and Height of the Rectangle
        /// </summary>
        public int2 Size { get; set; }

        /// <summary>
        /// Returns a Rectangle with all of its values set to zero
        /// </summary>
        public static Rectangle Empty { get; } = new Rectangle();

        /// <summary>
        /// Returns the x-coordinate of the left side of the rectangle
        /// </summary>
        public int Left => Position.X;

        /// <summary>
        /// Returns the x-coordinate of the right side of the rectangle
        /// </summary>
        public int Right => Position.X + Size.X;

        /// <summary>
        /// Returns the y-coordinate of the top of the rectangle
        /// </summary>
        public int Top => Position.Y;

        /// <summary>
        /// Returns the y-coordinate of the bottom of the rectangle
        /// </summary>
        public int Bottom => Position.Y + Size.Y;

        /// <summary>
        /// Returns the Vector2 that specifies the center of the rectangle
        /// </summary>
        public Vector2 Center => new Vector2(Position.X + Size.X / 2, Position.Y + Size.Y / 2);

        /// <summary>
        /// Returns a value that indicates whether the Rectangle is empty
        /// true if the Rectangle is empty; otherwise false
        /// </summary>
        public bool IsEmpty => Size.X == 0 && Size.Y == 0 && Position.X == 0 && Position.Y == 0;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of <see cref="Rectangle"/> struct, with the specified
        /// position, width, and height.
        /// </summary>
        /// <param name="x">The x coordinate of the top-left corner of the created <see cref="Rectangle"/>.</param>
        /// <param name="y">The y coordinate of the top-left corner of the created <see cref="Rectangle"/>.</param>
        /// <param name="width">The width of the created <see cref="Rectangle"/>.</param>
        /// <param name="height">The height of the created <see cref="Rectangle"/>.</param>
        public Rectangle(int x, int y, int width, int height)
           : this()
        {
            Position = new int2(x, y);
            Size = new int2(width, height);
        }

        /// <summary>
        /// Creates a new instance of <see cref="Rectangle"/> struct, with the specified
        /// location and size.
        /// </summary>
        /// <param name="position">The x and y coordinates of the top-left corner of the created <see cref="Rectangle"/>.</param>
        /// <param name="size">The width and height of the created <see cref="Rectangle"/>.</param>
        public Rectangle(int2 position, int2 size)
           : this()
        {
            Position = position;
            Size = size;
        }

        /// <summary>
        /// Creates a new instance of <see cref="Rectangle"/> struct, with the specified
        /// location and size.
        /// </summary>
        /// <param name="position">The x and y coordinates of the top-left corner of the created <see cref="Rectangle"/>.</param>
        /// <param name="size">The width and height of the created <see cref="Rectangle"/>.</param>
        public Rectangle(Vector2 position, Vector2 size)
           : this()
        {
            Position = position.RoundToInt2();
            Size = size.RoundToInt2();
        }

        #endregion

        #region Méthodes publiques

        #region Interface

        /// <summary>
        /// Determines whether two Rectangle instances are equal
        /// </summary>
        /// <param name="other">The Rectangle to compare this instance to</param>
        /// <returns>True if the instances are equal; False otherwise</returns>
        public bool Equals(Rectangle other)
        {
            return this == other;
        }

        /// <summary>
        /// Compares two rectangles for equality
        /// </summary>
        /// <param name="a">Rectangle on the left side of the equals sign</param>
        /// <param name="b">Rectangle on the right side of the equals sign</param>
        /// <returns>True if the rectangles are equal; False otherwise</returns>
        public static bool operator ==(Rectangle a, Rectangle b)
        {
            return a.Position.X == b.Position.X && a.Position.Y == b.Position.Y && a.Size.X == b.Size.X && a.Size.Y == b.Size.Y;
        }

        /// <summary>
        /// Compares two rectangles for inequality
        /// </summary>
        /// <param name="a">Rectangle on the left side of the equals sign</param>
        /// <param name="b">Rectangle on the right side of the equals sign</param>
        /// <returns>True if the rectangles are not equal; False otherwise</returns>
        public static bool operator !=(Rectangle a, Rectangle b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Compares whether current instance is equal to specified <see cref="object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public override bool Equals(object obj)
        {
            return obj is Rectangle && this == (Rectangle)obj;
        }

        /// <summary>
        /// Returns a string that represents the current Rectangle
        /// </summary>
        /// <returns>A string that represents the current Rectangle</returns>
        public override string ToString()
        {
            return $"{{x:{Position.X} y:{Position.Y} Width:{Size.X} Height:{Size.Y}}}";
        }

        /// <summary>
        /// Gets the hash code for this object which can help for quick checks of equality
        /// or when inserting this Rectangle into a hash-based collection such as a Dictionary or Hashtable 
        /// </summary>
        /// <returns>An integer hash used to identify this Rectangle</returns>
        public override int GetHashCode()
        {
            return Position.X ^ Position.Y ^ Size.X ^ Size.Y;
        }

        #endregion

        #region Comparaison

        /// <summary>
        /// Determines whether this Rectangle contains a specified point represented by its x and y-coordinates
        /// </summary>
        /// <param name="x">The x-coordinate of the specified point</param>
        /// <param name="y">The y-coordinate of the specified point</param>
        /// <returns>True if the specified point is contained within this Rectangle; False otherwise</returns>
        public bool Contains(int x, int y)
        {
            return Position.X <= x && x < x + Size.X && Position.Y <= y && y < y + Size.Y;
        }

        /// <summary>
        /// Determines whether this Rectangle contains a specified Vector2
        /// </summary>
        /// <param name="value">The Vector2 to evaluate</param>
        /// <returns>True if the specified Vector2 is contained within this Rectangle; False otherwise</returns>
        public bool Contains(Vector2 value)
        {
            return Position.X <= value.X && value.X < Position.X + Size.X && Position.Y <= value.Y && value.Y < Position.Y + Size.Y;
        }

        /// <summary>
        /// Determines whether this Rectangle entirely contains the specified Rectangle
        /// </summary>
        /// <param name="value">The Rectangle to evaluate</param>
        /// <returns>True if this Rectangle entirely contains the specified Rectangle; False otherwise</returns>
        public bool Contains(Rectangle value)
        {
            return Position.X <= value.Position.X && value.Position.X + value.Size.X <= Position.X + Size.X && Position.Y <= value.Position.Y
                     && value.Position.Y + value.Size.Y <= Position.Y + Size.Y;
        }

        /// <summary>
        /// Changes the position of the Rectangles by the values of the specified Vector2
        /// </summary>
        /// <param name="offsetPoint">The values to adjust the position of the Rectangle by</param>
        public void Offset(Vector2 offsetPoint)
        {
            Vector2 v = new Vector2(Position.X + offsetPoint.X, Position.Y + offsetPoint.Y);
            Position = v.RoundToInt2();
        }

        /// <summary>
        /// Changes the position of the Rectangle by the specified x and y offsets
        /// </summary>
        /// <param name="offsetX">Change in the x-position</param>
        /// <param name="offsetY">Change in the y-position</param>
        public void Offset(int offsetX, int offsetY)
        {
            Position = new int2(Position.X + offsetX, Position.Y + offsetY);
        }

        /// <summary>
        /// Pushes the edges of the Rectangle out by the specified horizontal and vertical values
        /// </summary>
        /// <param name="horizontalValue">Value to push the sides out by</param>
        /// <param name="verticalValue">Value to push the top and bottom out by</param>
        /// <exception cref="OverflowException">Thrown if the new width or height exceed Int32.MaxValue, or new x or y are smaller than int32.MinValue</exception>
        public void Inflate(int horizontalValue, int verticalValue)
        {
            Position = new int2(Position.X - horizontalValue, Position.Y - verticalValue);
            Size = new int2(Size.X + horizontalValue * 2, Size.Y + verticalValue * 2);
        }

        /// <summary>
        /// Determines whether this Rectangle intersects with the specified Rectangle
        /// </summary>
        /// <param name="value">The Rectangle to evaluate</param>
        /// <returns>True if the specified Rectangle intersects with this one; False otherwise</returns>
        public bool Intersects(Rectangle value)
        {
            return value.Left < Right &&
                   Left < value.Right &&
                   value.Top < Bottom &&
                   Top < value.Bottom;
        }

        /// <summary>
        /// Determines whether this Rectangle intersects with the specified Rectangle
        /// </summary>
        /// <param name="value">The Rectangle to evaluate</param>
        /// <param name="result">True if the specified Rectangle intersects with this one; False otherwise</param>
        public void Intersects(ref Rectangle value, out bool result)
        {
            result = value.Left < Right &&
                     Left < value.Right &&
                     value.Top < Bottom &&
                     Top < value.Bottom;
        }

        /// <summary>
        /// Creates a Rectangle defining the area where one Rectangle overlaps with another Rectangle
        /// </summary>
        /// <param name="value1">The first Rectangle to compare</param>
        /// <param name="value2">The second Rectangle to compare</param>
        /// <returns>The area where the two specified Rectangles overlap. If the two Rectangles do not overlap the resulting Rectangle will be Empty</returns>
        public static Rectangle Intersect(Rectangle value1, Rectangle value2)
        {
            Intersect(ref value1, ref value2, out Rectangle rectangle);
            return rectangle;
        }

        /// <summary>
        /// Creates a Rectangle defining the area where one Rectangle overlaps with another Rectangle
        /// </summary>
        /// <param name="value1">The first Rectangle to compare</param>
        /// <param name="value2">The second Rectangle to compare</param>
        /// <param name="result">The area where the two specified Rectangles overlap. If the two Rectangles do not overlap the resulting Rectangle will be Empty</param>
        public static void Intersect(ref Rectangle value1, ref Rectangle value2, out Rectangle result)
        {
            if (value1.Intersects(value2))
            {
                int rightSide = Math.Min(value1.Position.X + value1.Size.X, value2.Position.X + value2.Size.X);
                int leftSide = Math.Max(value1.Position.X, value2.Position.X);
                int topSide = Math.Max(value1.Position.Y, value2.Position.Y);
                int bottomSide = Math.Min(value1.Position.Y + value1.Size.Y, value2.Position.Y + value2.Size.Y);
                result = new Rectangle(leftSide, topSide, rightSide - leftSide, bottomSide - topSide);
            }
            else
            {
                result = new Rectangle(0, 0, 0, 0);
            }
        }

        /// <summary>
        /// Creates a new Rectangle that exactly contains the specified two Rectangles
        /// </summary>
        /// <param name="value1">The first Rectangle to contain</param>
        /// <param name="value2">The second Rectangle to contain</param>
        /// <returns>A new Rectangle that exactly contains the specified two Rectangles</returns>
        public static Rectangle Union(Rectangle value1, Rectangle value2)
        {
            int x = Math.Min(value1.Position.X, value2.Position.X);
            int y = Math.Min(value1.Position.Y, value2.Position.Y);
            return new Rectangle(x, y, Math.Max(value1.Right, value2.Right) - x, Math.Max(value1.Bottom, value2.Bottom) - y);
        }

        /// <summary>
        /// Creates a new <see cref="Rectangle"/> that completely contains two other rectangles.
        /// </summary>
        /// <param name="value1">The first <see cref="Rectangle"/>.</param>
        /// <param name="value2">The second <see cref="Rectangle"/>.</param>
        /// <param name="result">The union of the two rectangles as an output parameter.</param>
        public static void Union(ref Rectangle value1, ref Rectangle value2, out Rectangle result)
        {
            result = Union(value1, value2);
        }
        #endregion

        #endregion
    }
}
