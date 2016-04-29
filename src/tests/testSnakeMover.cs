using System;
using System.Timers;
using SnakeGame.GridSystem;
using NUnit.Framework;

namespace SnakeGame
{
	/// <summary>
	/// Provides unit tests for the SnakeMover class.
	/// </summary>
	[TestFixture()]
	public class TestSnakeMover
	{
		/// <summary>
		/// Tests that the Target property exists and is set properly.
		/// </summary>
		[Test()]
		public void TargetExists()
		{
			Grid g = new Grid(16, 16);
			Snake s = new Snake(g, g[8, 8], 2, Direction.Right);
			SnakeMover sm = new SnakeMover(s, 4);
			Assert.AreEqual(s, sm.Target, "SnakeMover.Target should correspond to the first constructor parameter.");
		}
		/// <summary>
		/// Tests ticking motion.
		/// </summary>
		[Test()]
		public void TickingMotion()
		{
			Grid g = new Grid(16, 16);
			Snake s = new Snake(g, g[8, 8], 2, Direction.Right);
			Timer t = new Timer(1000);
			SnakeMover sm = new SnakeMover(s, 4);
			t.Elapsed += (object sender, ElapsedEventArgs e) => {
				t.Stop();
				sm.Dispose();
				Assert.AreEqual(g[12, 8], s.Head.Cell, "A SnakeMover set to tick 4 times per second should move the snake 4 times per second.");
				Assert.AreEqual(Direction.Right, s.Head.MovementDirection, "A SnakeMover should not by itself change the snake's direction.");
			};
			t.Start();
		}
		/// <summary>
		/// Tests that the SnakeMover events are behaving properly.
		/// </summary>
		[Test()]
		public void Events()
		{
			Grid g = new Grid(16, 16);
			Cell c = g[8, 8];
			Snake s = new Snake(g, c, 2, Direction.Up);
			SnakeMover sm = new SnakeMover(s, 4);
			sm.BeforeMove += (object sender, EventArgs e) =>
			{
				Assert.AreEqual(c, s.Head, "When SnakeMover.BeforeMove fires, the snake's head should be in the position it was before movement.");
			};
			sm.AfterMove += (object sender, EventArgs e) =>
			{
				Assert.AreEqual(g[7, 8], s.Head, "When SnakeMover.AfterMove fires, the snake's head should be in its new position.");
			};
			sm.Dispose();
			s = new Snake(g, g[1, 0], 2, Direction.Left);
			sm = new SnakeMover(s, 4);
			int ticks = 0;
			sm.BeforeMove += (object sender, EventArgs e) =>
			{
				if (++ticks > 2)
				{
					Assert.Fail("SnakeMover.OutOfBounds did not fire at the correct time (too many ticks elapsed).");
				}
			};
			sm.OutOfBounds += (object sender, EventArgs e) =>
			{
				if (ticks < 2)
				{
					Assert.Fail("SnakeMover.OutOfBounds did not fire at the correct time (too early).");
				}
				else
				{
					Assert.Pass();
				}
			};
		}
	}
}

