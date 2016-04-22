using System;
using SnakeGame.GridSystem;
using NUnit.Framework;

namespace SnakeGame
{
	[TestFixture()]
	public class TestMovementNode
	{
		private void TestCtor(Grid.Cell c, Direction dir)
		{
			MovementNode m = new MovementNode(c, dir);
			Assert.AreEqual(c, m.Cell);
			Assert.AreEqual(dir, m.MovementDirection);
		}
		[Test()]
		public void Construction()
		{
			Grid g = new Grid(8, 8);
			TestCtor(g[1, 0], Direction.Up);
			TestCtor(g[6, 1], Direction.Left);
			TestCtor(g[2, 4], Direction.Right);
			TestCtor(g[1, 3], Direction.Down);
			TestCtor(g[2, 5], Direction.Right);
		}
	}
}

