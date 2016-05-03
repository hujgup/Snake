using System;
using SnakeGame.GridSystem;
using NUnit.Framework;

namespace SnakeGame.Model
{
	/// <summary>
	/// Provides unit tests for the Direction enum, and its extension methods.
	/// </summary>
	[TestFixture()]
	public class TestDirection
	{
		private void SurpressUnusedWarning(object o)
		{
		}
		/// <summary>
		/// Checks that all the enumerations exist.
		/// </summary>
		[Test()]
		public void EnumsExist()
		{
			Direction d;
			Assert.DoesNotThrow(delegate()
			{
				d = Direction.Up;
				d = Direction.Right;
				d = Direction.Down;
				d = Direction.Left;
				SurpressUnusedWarning(d);
			}, "Direction must contain the following enum values: Up, Right, Down, Left.");
		}
		/// <summary>
		/// Checks that the extension methods are behaving properly.
		/// </summary>
		[Test()]
		public void ExtensionMethods()
		{
			Assert.AreEqual(Direction.Right, Direction.Left.Invert());
			Assert.AreEqual(Direction.Left, Direction.Right.Invert());
			Assert.AreEqual(Direction.Down, Direction.Up.Invert());
			Assert.AreEqual(Direction.Up, Direction.Down.Invert());

			Assert.AreEqual(Axis2D.X, Direction.Right.GetAxis());
			Assert.AreEqual(Axis2D.X, Direction.Left.GetAxis());
			Assert.AreEqual(Axis2D.Y, Direction.Up.GetAxis());
			Assert.AreEqual(Axis2D.Y, Direction.Down.GetAxis());

			Assert.AreEqual(Direction.Left, Direction.Up.RotateAnticlockwise());
			Assert.AreEqual(Direction.Down, Direction.Left.RotateAnticlockwise());
			Assert.AreEqual(Direction.Right, Direction.Down.RotateAnticlockwise());
			Assert.AreEqual(Direction.Up, Direction.Right.RotateAnticlockwise());

			Assert.AreEqual(Direction.Left, Direction.Down.RotateClockwise());
			Assert.AreEqual(Direction.Down, Direction.Right.RotateClockwise());
			Assert.AreEqual(Direction.Right, Direction.Up.RotateClockwise());
			Assert.AreEqual(Direction.Up, Direction.Left.RotateClockwise());

			Assert.AreEqual(Direction.Down.RotateAnticlockwise(), Direction.Down.RotateAnticlockwise(1));
			Assert.AreEqual(Direction.Up.RotateAnticlockwise(), Direction.Up.RotateAnticlockwise(1));
			Assert.AreEqual(Direction.Right.RotateAnticlockwise(), Direction.Right.RotateAnticlockwise(1));
			Assert.AreEqual(Direction.Left.RotateAnticlockwise(), Direction.Left.RotateAnticlockwise(1));

			Assert.AreEqual(Direction.Down.RotateClockwise(), Direction.Down.RotateClockwise(1));
			Assert.AreEqual(Direction.Up.RotateClockwise(), Direction.Up.RotateClockwise(1));
			Assert.AreEqual(Direction.Right.RotateClockwise(), Direction.Right.RotateClockwise(1));
			Assert.AreEqual(Direction.Left.RotateClockwise(), Direction.Left.RotateClockwise(1));

			Assert.AreEqual(Direction.Up.Invert(), Direction.Up.RotateAnticlockwise(2));
			Assert.AreEqual(Direction.Down.Invert(), Direction.Down.RotateAnticlockwise(2));
			Assert.AreEqual(Direction.Left.Invert(), Direction.Left.RotateAnticlockwise(2));
			Assert.AreEqual(Direction.Right.Invert(), Direction.Right.RotateAnticlockwise(2));

			Assert.AreEqual(Direction.Up.Invert(), Direction.Up.RotateClockwise(2));
			Assert.AreEqual(Direction.Down.Invert(), Direction.Down.RotateClockwise(2));
			Assert.AreEqual(Direction.Left.Invert(), Direction.Left.RotateClockwise(2));
			Assert.AreEqual(Direction.Right.Invert(), Direction.Right.RotateClockwise(2));

			Assert.AreEqual(Direction.Right, Direction.Up.RotateAnticlockwise(3));
			Assert.AreEqual(Direction.Up, Direction.Left.RotateAnticlockwise(3));
			Assert.AreEqual(Direction.Left, Direction.Down.RotateAnticlockwise(3));
			Assert.AreEqual(Direction.Down, Direction.Right.RotateAnticlockwise(3));

			Assert.AreEqual(Direction.Left, Direction.Up.RotateClockwise(3));
			Assert.AreEqual(Direction.Down, Direction.Left.RotateClockwise(3));
			Assert.AreEqual(Direction.Right, Direction.Down.RotateClockwise(3));
			Assert.AreEqual(Direction.Up, Direction.Right.RotateClockwise(3));

			Assert.AreEqual(Direction.Up.RotateClockwise(), Direction.Up.RotateAnticlockwise(3));
			Assert.AreEqual(Direction.Right.RotateClockwise(), Direction.Right.RotateAnticlockwise(3));
			Assert.AreEqual(Direction.Down.RotateClockwise(), Direction.Down.RotateAnticlockwise(3));
			Assert.AreEqual(Direction.Left.RotateClockwise(), Direction.Left.RotateAnticlockwise(3));

			Assert.AreEqual(Direction.Up.RotateAnticlockwise(), Direction.Up.RotateClockwise(3));
			Assert.AreEqual(Direction.Right.RotateAnticlockwise(), Direction.Right.RotateClockwise(3));
			Assert.AreEqual(Direction.Down.RotateAnticlockwise(), Direction.Down.RotateClockwise(3));
			Assert.AreEqual(Direction.Left.RotateAnticlockwise(), Direction.Left.RotateClockwise(3));

			Assert.AreEqual(Direction.Up, Direction.Up.RotateAnticlockwise(4));
			Assert.AreEqual(Direction.Down, Direction.Down.RotateAnticlockwise(4));
			Assert.AreEqual(Direction.Left, Direction.Left.RotateAnticlockwise(4));
			Assert.AreEqual(Direction.Right, Direction.Right.RotateAnticlockwise(4));

			Assert.AreEqual(Direction.Up, Direction.Up.RotateClockwise(4));
			Assert.AreEqual(Direction.Down, Direction.Down.RotateClockwise(4));
			Assert.AreEqual(Direction.Left, Direction.Left.RotateClockwise(4));
			Assert.AreEqual(Direction.Right, Direction.Right.RotateClockwise(4));

			Assert.AreEqual(Direction.Up.RotateClockwise(1), Direction.Up.RotateAnticlockwise(-1));
			Assert.AreEqual(Direction.Down.RotateClockwise(1), Direction.Down.RotateAnticlockwise(-1));
			Assert.AreEqual(Direction.Left.RotateClockwise(1), Direction.Left.RotateAnticlockwise(-1));
			Assert.AreEqual(Direction.Right.RotateClockwise(1), Direction.Right.RotateAnticlockwise(-1));

			Assert.AreEqual(Direction.Up.RotateAnticlockwise(1), Direction.Up.RotateClockwise(-1));
			Assert.AreEqual(Direction.Down.RotateAnticlockwise(1), Direction.Down.RotateClockwise(-1));
			Assert.AreEqual(Direction.Left.RotateAnticlockwise(1), Direction.Left.RotateClockwise(-1));
			Assert.AreEqual(Direction.Right.RotateAnticlockwise(1), Direction.Right.RotateClockwise(-1));
		}
	}
}

