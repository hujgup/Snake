using System;
using System.Threading;
using SnakeGame.Model;
using SnakeGame.GridSystem;
using NUnit.Framework;
using Mirror = MbUnit.Framework.Mirror;

namespace SnakeGame
{
	/// <summary>
	/// Provides unit tests for the FruitEatenHandler class.
	/// </summary>
	[TestFixture()]
	public class TestGameplayController
	{
		/// <summary>
		/// Tests that the fruit moves to a tile that is not occupied once it is consumed.
		/// </summary>
		[Test()]
		public void TestMovement()
		{
			Grid g = new Grid(16, 16);
			for (int i = 0; i < 1024; i++)
			{
				Snake s = new Snake(g, g[8, 8], 2, Direction.Right);
				Fruit f = new Fruit(g, 1);
				s.MovementDirection = Direction.Right;
				Cell c = g[s.Head.Cell.X + 1, s.Head.Cell.Y];
				f.OccupiedCell = c;
				FruitEatenHandler h = new FruitEatenHandler(f, s);
				s.Move();
				h.EvaluateState();
				Assert.AreNotEqual(c, f.OccupiedCell, "After being eaten, a fruit's location must change.");
				foreach (MovementNode mn in s)
				{
					Assert.AreNotEqual(mn.Cell, f.OccupiedCell, "After being eaten, the fruit's new location must not be inside the snake.");
				}
			}
		}
	}
}

