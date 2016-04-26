using System;
using SnakeGame.GridSystem;
using NUnit.Framework;

namespace SnakeGame.UserInterface
{
	/// <summary>
	/// Provides unit tests for the SnakeMovementControlHandler class.
	/// </summary>
	[TestFixture()]
	public class TestSnakeMovementControlHandler
	{
		/// <summary>
		/// Tests that the inheritance tree is correct.
		/// </summary>
		[Test()]
		public void TestInheritance()
		{
			Grid g = new Grid(16, 16);
			Snake s = new Snake(g, g[8, 8], 2, Direction.Right);
			SnakeMovementControlHandler smch = new SnakeMovementControlHandler(s, 4);
			Assert.IsInstanceOf<SnakeMover>(smch, "SnakeMovementControlHandler must inherit from SnakeMover.");
		}
		/// <summary>
		/// Tests that items all queued up on the same tick all get executed in the correct order.
		/// </summary>
		[Test()]
		public void TestQueueOneTick()
		{
			// test that things are added to the queue, that the queue executes in the correct order, and that if something is queued on the next tick, the previous queue is cleared
			Grid g = new Grid(16, 16);
			Snake s = new Snake(g, g[8, 8], 2, Direction.Right);
			SnakeMovementControlHandler smch = new SnakeMovementControlHandler(s, 1);
			smch.Enqueue(Direction.Left);
			smch.Enqueue(Direction.Up);
			smch.Enqueue(Direction.Right);
			int ticks = 0;
			smch.AfterMove += (object sender, EventArgs e) =>
			{
				if (ticks == 0)
				{
					Assert.AreEqual(Direction.Left, s.MovementDirection);
				}
				else if (ticks == 1)
				{
					Assert.AreEqual(Direction.Up, s.MovementDirection);
				}
				else if (ticks == 2)
				{
					Assert.AreEqual(Direction.Right, s.MovementDirection);
					smch.Dispose();
					Assert.Pass();
				}
				ticks++;
			};
		}
		/// <summary>
		/// Tests that queueing an item up on a different tick then the previous items were queued overrides the prior queue with the new input.
		/// </summary>
		[Test()]
		public void TestQueueMultipleTick()
		{
			Grid g = new Grid(16, 16);
			Snake s = new Snake(g, g[8, 8], 2, Direction.Right);
			SnakeMovementControlHandler smch = new SnakeMovementControlHandler(s, 1);
			smch.Enqueue(Direction.Left);
			smch.Enqueue(Direction.Up);
			int ticks = 0;
			smch.AfterMove += (object sender, EventArgs e) =>
			{
				if (ticks == 0)
				{
					Assert.AreEqual(Direction.Left, s.MovementDirection);
					smch.Enqueue(Direction.Right);
				}
				else if (ticks == 1)
				{
					Assert.AreEqual(Direction.Right, s.MovementDirection);
					smch.Dispose();
					Assert.Pass();
				}
				ticks++;
			};
		}
	}
}

