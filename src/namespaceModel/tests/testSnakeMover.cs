using System;
using System.Timers;
using SnakeGame.GridSystem;
using NUnit.Framework;

namespace SnakeGame.Model
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
	}
}

