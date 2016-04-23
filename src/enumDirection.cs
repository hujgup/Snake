using System;
using SnakeGame.GridSystem;

namespace SnakeGame
{
	/// <summary>
	/// Represents an orthogonal direction.
	/// </summary>
	public enum Direction
	{
		/// <summary>
		/// The up direction.
		/// </summary>
		Up,
		/// <summary>
		/// The right direction.
		/// </summary>
		Right,
		/// <summary>
		/// The down direction.
		/// </summary>
		Down,
		/// <summary>
		/// The left direction.
		/// </summary>
		Left
	}
	/// <summary>
	/// Provides extension methods for the Direction enumeration.
	/// </summary>
	public static class DirectionExtensions
	{
		/// <summary>
		/// Gets the increment value for movement in this direction, according to top-left = (0, 0) systems.
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
		/// Gets the direction 90*steps degrees anticlockwise from this direction.
		/// </summary>
		/// <param name="dir">The direction to rotate.</param>
		/// <param name="steps">The number of times to rotate.</param> 
		public static Direction RotateAnticlockwise(this Direction dir, int steps)
		{
			Direction res;
			steps %= 4;
			if (steps == 0)
			{
				res = dir;
			}
			else
			{
				int absSteps = Math.Abs(steps);
				if (absSteps == 2)
				{
					res = dir.Invert();
				}
				else
				{
					if (absSteps == 1)
					{
						res = dir.RotateAnticlockwise();
					}
					else
					{
						res = dir.RotateClockwise();
					}
					if (steps < 0)
					{
						res = res.Invert();
					}
				}
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
		/// Gets the direction 90*steps degrees clockwise from this direction.
		/// </summary>
		/// <param name="dir">The direction to rotate.</param>
		/// <param name="steps">The number of times to rotate.</param> 
		public static Direction RotateClockwise(this Direction dir, int steps)
		{
			return dir.RotateAnticlockwise(-steps);
		}
		/// <summary>
		/// Gets the direction 90 degrees clockwise from this direction.
		/// </summary>
		/// <param name="dir">The direction to rotate.</param>
		public static Direction RotateClockwise(this Direction dir)
		{
			return dir.RotateAnticlockwise().Invert();
		}
	}
}

