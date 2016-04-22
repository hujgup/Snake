using System;
using SnakeGame.GridSystem;
using NUnit.Framework;

namespace SnakeGame
{
	[TestFixture()]
	public class TestFruit
	{
		private void TestConstruction(Grid g, int value)
		{
			Fruit f = new Fruit(g, value);
			Assert.AreEqual(g, f.PlayArea);
			Assert.AreEqual(value, f.Value);
			Assert.IsTrue(g.IsDefined(f.OccupiedCell));
			Assert.IsTrue(f.OccupiedCell.IsValid);
		}
		[Test()]
		public void Construction()
		{
			Grid g = new Grid(16, 16);
			TestConstruction(g, 5);
		}
	}
}

