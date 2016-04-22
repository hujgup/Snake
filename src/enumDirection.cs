using System;
using SnakeGame.GridSystem;

namespace SnakeGame
{
	/// <summary>
	/// Represents an orthogonal direction.
	/// </summary>
	public enum Direction
	{
		Up,
		Right,
		Down,
		Left
	}
	/// <summary>
	/// Provides extension methods for the Direction enumeration.
	/// </summary>
	public static class DirectionExtensions
	{
		/// <summary>
		/// Gets the increment value for movement in this direction.
		/// </summary>
		/// <param name="dir">The direction being moved along.</param>
		public static int Sign(this Direction dir)
		{
			return (dir == Direction.Up || dir == Direction.Left) ? -1 : 1;
		}
		/// <summary>
		/// Gets the axis for movement in this direction.
		/// </summary>
		/// <param name="dir">The direction being moved along.</param>
		public static Axis2D GetAxis(this Direction dir)
		{
			return (dir == Direction.Up || dir == Direction.Down) ? Axis2D.Y : Axis2D.X;
		}
		/// <summary>
		/// Gets the direction opposite this direction.
		/// </summary>
		/// <param name="dir">the direction to invert.</param>
		public static Direction Invert(this Direction dir)
		{
			Direction res;
			switch (dir)
			{
				case SnakeGame.Direction.Down:
					res = Direction.Up;
					break;
				case Direction.Left:
					res = Direction.Right;
					break;
				case Direction.Right:
					res = Direction.Left;
					break;
				default: // Direction.Up
					res = Direction.Down;
					break;
			}
			return res;
		}
		/// <summary>
		/// Gets the direction 90 degrees anticlockwise from this direction.
		/// </summary>
		/// <param name="dir">The direction to rotate.</param>
		public static Direction RotateAnticlockwise(this Direction dir)
		{
			Direction res;
			switch (dir)
			{
				case SnakeGame.Direction.Down:
					res = Direction.Right;
					break;
				case Direction.Left:
					res = Direction.Down;
					break;
				case Direction.Right:
					res = Direction.Up;
					break;
				default: // Direction.Up
					res = Direction.Left;
					break;
			}
			return res;
		}
		/// <summary>
		/// Gets the direction 90 degrees clockwise from this direction.
		/// </summary>
		/// <param name="dir">The direction to rotate.</param>
		public static Direction RotateClockwise(this Direction dir)
		{
			Direction res;
			switch (dir)
			{
				case SnakeGame.Direction.Down:
					res = Direction.Left;
					break;
				case Direction.Left:
					res = Direction.Up;
					break;
				case Direction.Right:
					res = Direction.Down;
					break;
				default: // Direction.Up
					res = Direction.Right;
					break;
			}
			return res;
		}
	}
}

