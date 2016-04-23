using System;

namespace SnakeGame.GridSystem
{
	/// <summary>
	/// Represents a single cell in a grid.
	/// </summary>
	public class Cell : IEquatable<Cell>
	{
		/// <summary>
		/// A cell that is out-of-range of all possible grids.
		/// </summary>
		public static readonly Cell INVALID_CELL = new Cell(null, -1, -1);
		/// <summary>
		/// Initializes a new instance of the <see cref="SnakeGame.GridSystem.Cell"/> class.
		/// </summary>
		/// <param name="owner">The grid this cell belongs to.</param>
		/// <param name="x">The x coordinate of this cell.</param>
		/// <param name="y">The y coordinate of this cell.</param>
		public Cell(Grid owner, int x, int y)
		{
			Owner = owner;
			X = x;
			Y = y;
		}
		/// <summary>
		/// Gets the Grid that this cell belongs to.
		/// </summary>
		public Grid Owner
		{
			get;
			private set;
		}
		/// <summary>
		/// Gets the x value of this cell.
		/// </summary>
		public int X
		{
			get;
			private set;
		}
		/// <summary>
		/// Gets the y value of this cell.
		/// </summary>
		public int Y
		{
			get;
			private set;
		}
		/// <summary>
		/// Gets a value indicating whether this instance is a valid cell.
		/// </summary>
		/// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
		public bool IsValid
		{
			get
			{
				return Owner != null && X >= 0 && Y >= 0;
			}
		}
		/// <summary>
		/// Gets the coordinate corresponding to the given axis.
		/// </summary>
		/// <param name="axis">The axis to get the coordinate of.</param>
		public int GetAxisValue(Axis2D axis)
		{
			return axis == Axis2D.X ? X : Y;
		}
		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current <see cref="SnakeGame.GridSystem.Cell"/>.
		/// </summary>
		public override string ToString()
		{
			return string.Format("({0}, {1})", X, Y);
		}
		/// <summary>
		/// Serves as a hash function for a <see cref="SnakeGame.GridSystem.Cell"/> object.
		/// </summary>
		/// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
		public override int GetHashCode()
		{
			unchecked
			{
				return Owner.GetHashCode() + X.GetHashCode() + Y.GetHashCode();
			}
		}
		/// <summary>
		/// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="SnakeGame.GridSystem.Cell"/>.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="SnakeGame.GridSystem.Cell"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
		/// <see cref="SnakeGame.GridSystem.Cell"/>; otherwise, <c>false</c>.</returns>
		public override bool Equals(object obj)
		{
			return GetType().IsInstanceOfType(obj) ? Equals((Cell)obj) : false;
		}
		/// <summary>
		/// Determines whether the specified <see cref="SnakeGame.GridSystem.Cell"/> is equal to the current <see cref="SnakeGame.GridSystem.Cell"/>.
		/// </summary>
		/// <param name="other">The <see cref="SnakeGame.GridSystem.Cell"/> to compare with the current <see cref="SnakeGame.GridSystem.Cell"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="SnakeGame.GridSystem.Cell"/> is equal to the current
		/// <see cref="SnakeGame.GridSystem.Cell"/>; otherwise, <c>false</c>.</returns>
		public bool Equals(Cell other)
		{
			return Owner == other.Owner && X == other.X && Y == other.Y;
		}
		/// <param name="a">Arg a.</param>
		/// <param name="b">Arg b.</param>
		public static bool operator ==(Cell a, Cell b)
		{
			return a.Equals(b);
		}
		/// <param name="a">Arg a.</param>
		/// <param name="b">Arg b.</param>
		public static bool operator !=(Cell a, Cell b)
		{
			return !(a == b);
		}
	}
}

