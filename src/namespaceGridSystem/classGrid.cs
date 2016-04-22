using System;
using System.Collections.Generic;

namespace SnakeGame.GridSystem
{
	/// <summary>
	/// Represents a grid of cells.
	/// </summary>
	public class Grid
	{
		/// <summary>
		/// Represents a single cell in a grid.
		/// </summary>
		public struct Cell : IEquatable<Cell>
		{
			/// <summary>
			/// A cell that is out-of-range of all possible grids.
			/// </summary>
			public static readonly Cell INVALID_CELL = new Cell(-1, -1);
			/// <summary>
			/// Initializes a new instance of the <see cref="Snake.GridSystem.Grid+Cell"/> struct.
			/// </summary>
			/// <param name="x">The x coordinate of this cell.</param>
			/// <param name="y">The y coordinate of this cell.</param>
			public Cell(int x, int y) : this()
			{
				X = x;
				Y = y;
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
					return X >= 0 && Y >= 0;
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
			/// Returns a <see cref="System.String"/> that represents the current <see cref="Snake.GridSystem.Grid+Cell"/>.
			/// </summary>
			public override string ToString()
			{
				return string.Format("({0}, {1})", X, Y);
			}
			/// <summary>
			/// Serves as a hash function for a <see cref="Snake.GridSystem.Grid+Cell"/> object.
			/// </summary>
			/// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
			public override int GetHashCode()
			{
				unchecked
				{
					return X.GetHashCode() + Y.GetHashCode();
				}
			}
			/// <summary>
			/// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="Snake.GridSystem.Grid+Cell"/>.
			/// </summary>
			/// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="Snake.GridSystem.Grid+Cell"/>.</param>
			/// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
			/// <see cref="Snake.GridSystem.Grid+Cell"/>; otherwise, <c>false</c>.</returns>
			public override bool Equals(object obj)
			{
				return GetType().IsInstanceOfType(obj) ? Equals((Cell)obj) : false;
			}
			/// <summary>
			/// Determines whether the specified <see cref="Snake.GridSystem.Grid+Cell"/> is equal to the current <see cref="Snake.GridSystem.Grid+Cell"/>.
			/// </summary>
			/// <param name="other">The <see cref="Snake.GridSystem.Grid+Cell"/> to compare with the current <see cref="Snake.GridSystem.Grid+Cell"/>.</param>
			/// <returns><c>true</c> if the specified <see cref="Snake.GridSystem.Grid+Cell"/> is equal to the current
			/// <see cref="Snake.GridSystem.Grid+Cell"/>; otherwise, <c>false</c>.</returns>
			public bool Equals(Cell other)
			{
				return X == other.X && Y == other.Y;
			}
			public static bool operator ==(Cell a, Cell b)
			{
				return a.Equals(b);
			}
			public static bool operator !=(Cell a, Cell b)
			{
				return !(a == b);
			}
		}

		private List<List<Cell>> _cells;
		/// <summary>
		/// Initializes a new instance of the <see cref="Snake.GridSystem.Grid"/> class.
		/// </summary>
		/// <param name="width">The grid's width.</param>
		/// <param name="height">The grid's height.</param>
		public Grid(int width, int height)
		{
			if (width <= 0)
			{
				throw new ArgumentOutOfRangeException("width", width, "Width must be a positive number.");
			}
			if (height <= 0)
			{
				throw new ArgumentOutOfRangeException("height", height, "Height must be a positive number.");
			}
			_cells = new List<List<Cell>>(height);
			List<Cell> row;
			int x;
			for (int y = 0; y < height; y++)
			{
				row = new List<Cell>(width);
				for (x = 0; x < width; x++)
				{
					row.Add(new Cell(x, y));
				}
				_cells.Add(row);
			}
			Width = width;
			Height = height;
			RangeX = new RangeRestriction<int>(0, width, false, true);
			RangeY = new RangeRestriction<int>(0, height, false, true);
		}
		/// <summary>
		/// Gets the <see cref="Snake.GridSystem.Grid+Cell"/> with the specified coordinates.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public Cell this[int x, int y]
		{
			get
			{
/*
				if (!CheckWidth(x))
				{
					throw new IndexOutOfRangeException("X component of indexer (first argument) must be in the range " + FormatRange(0, Width) + ".");
				}
				if (!CheckHeight(y))
				{
					throw new IndexOutOfRangeException("Y component of indexer (second argument) must be in the range " + FormatRange(0, Height) + ".");
				}
*/
				Cell res = Cell.INVALID_CELL;
				if (RangeX.InRange(x) && RangeY.InRange(y))
				{
					res = _cells[y][x];
				}
/*
				if (res == Cell.INVALID_CELL)
				{
					throw new DataMisalignedException("A valid value was not create during object construction.");
				}
*/
				return res;
			}
		}
		/// <summary>
		/// Gets the width of this grid.
		/// </summary>
		public int Width
		{
			get;
			private set;
		}
		/// <summary>
		/// Gets the height of this grid.
		/// </summary>
		public int Height
		{
			get;
			private set;
		}
		/// <summary>
		/// Gets the range of valid values along the X axis.
		/// </summary>
		public RangeRestriction<int> RangeX
		{
			get;
			private set;
		}
		/// <summary>
		/// Gets the range of valid values along the Y axis.
		/// </summary>
		public RangeRestriction<int> RangeY
		{
			get;
			private set;
		}
		/// <summary>
		/// Gets the total number of cells in this grid.
		/// </summary>
		public int CellCount
		{
			get
			{
				return Width * Height;
			}
		}
		private string FormatRange(int lowerBound, int upperBound)
		{
			return "[" + lowerBound.ToString() + ", " + upperBound.ToString() + "]";
		}
		/// <summary>
		/// Determines whether the specified coordinate is within this grid's range.
		/// </summary>
		/// <returns><c>true</c> if the coordinate is in range; otherwise, <c>false</c>.</returns>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public bool IsDefined(int x, int y)
		{
			return RangeX.InRange(x) && RangeY.InRange(y);
		}
		/// <summary>
		/// Determines whether the specified coordinate is within this grid's range.
		/// </summary>
		/// <returns><c>true</c> if the coordinate is in range; otherwise, <c>false</c>.</returns>
		/// <param name="c">the cell to check.</param>
		public bool IsDefined(Grid.Cell c)
		{
			return IsDefined(c.X, c.Y);
		}
		/// <summary>
		/// Gets the range of valid values along the specified axis.
		/// </summary>
		/// <param name="axis">The axis to get the range of.</param>
		public RangeRestriction<int> GetAxisRange(Axis2D axis)
		{
			return axis == Axis2D.X ? RangeX : RangeY;
		}
	}
}

