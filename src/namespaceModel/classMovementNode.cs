using System;
using SnakeGame.GridSystem;

namespace SnakeGame.Model
{
	/// <summary>
	/// Represents a cell that was moved to.
	/// </summary>
	public class MovementNode
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SnakeGame.MovementNode"/> class.
		/// </summary>
		/// <param name="cell">The cell moved to.</param>
		/// <param name="dir">The direction of travel at the time of movement.</param>
		public MovementNode(Cell cell, Direction dir)
		{
			Cell = cell;
			MovementDirection = dir;
		}
		/// <summary>
		/// Gets the cell that was moved to.
		/// </summary>
		public Cell Cell
		{
			get;
			private set;
		}
		/// <summary>
		/// Gets the direction of travel at the time that the cell was moved to.
		/// </summary>
		public Direction MovementDirection
		{
			get;
			private set;
		}
	}
}

