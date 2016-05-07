using System;
using System.Timers;
using System.Collections.Generic;
using SnakeGame.GridSystem;
using NUnit.Framework;

namespace SnakeGame.Model
{
	/// <summary>
	/// Provides unit tests for the Fruit class.
	/// </summary>
	[TestFixture()]
	public class TestFruit
	{
		private void TestConstruction(Grid g, Cell loc, int value)
		{
			Fruit f = new Fruit(g, loc, value);
			Assert.AreEqual(g, f.PlayArea);
			Assert.AreEqual(loc, f.OccupiedCell);
			Assert.AreEqual(value, f.Value);
			Assert.IsTrue(g.IsDefined(f.OccupiedCell));
		}
		private void TestConstruction(Grid g, int value)
		{
			Fruit f = new Fruit(g, value);
			Assert.AreEqual(g, f.PlayArea);
			Assert.AreEqual(value, f.Value);
			Assert.IsTrue(g.IsDefined(f.OccupiedCell));
			Assert.IsTrue(f.OccupiedCell.IsValid);
		}
		/// <summary>
		/// Tests fruit construction.
		/// </summary>
		[Test()]
		public void Construction()
		{
			Grid g = new Grid(16, 16);
			TestConstruction(g, g[5, 8], 8);
			TestConstruction(g, g[7, 7], 1);
			TestConstruction(g, g[1, 8], 5);
			TestConstruction(g, g[0, 10], 3);
			TestConstruction(g, g[12, 4], -2);
			Assert.Throws<ArgumentNullException>(delegate()
			{
				Fruit f = new Fruit(null, g[0, 0], 8);
			});
			Assert.Throws<ArgumentException>(delegate()
			{
				Fruit f = new Fruit(g, Cell.INVALID_CELL, 1);
			});
			Assert.Throws<ArgumentException>(delegate()
			{
				Fruit f = new Fruit(new Grid(18, 24), g[0, 0], 3);
			});
			Assert.Throws<ArgumentOutOfRangeException>(delegate()
			{
				Fruit f = new Fruit(g, new Cell(g, 128, 128), 7);
			});
		}
		/// <summary>
		/// Tests fruit constructor overloads.
		/// </summary>
		[Test()]
		public void Overloads()
		{
			Grid g = new Grid(8, 8);
			Fruit f = new Fruit(g, 4);
			Assert.IsTrue(f.OccupiedCell.IsValid);
			Assert.IsTrue(g.IsDefined(f.OccupiedCell));

		}
		/// <summary>
		/// Tests fruit location randomization.
		/// </summary>
		[Test()]
		public void Randomization()
		{
			Grid g = new Grid(8, 8);
			Fruit f = new Fruit(g, g[5, 7], 4);
			int max = 65536;
			Dictionary<Cell, int> cells = new Dictionary<Cell, int>(max);
			{
				Cell c;
				for (int i = 0; i < max; i++)
				{
					f.RandomizeLocation();
					c = f.OccupiedCell;
					if (cells.ContainsKey(c))
					{
						cells[c]++;
					}
					else
					{
						cells.Add(c, 1);
					}
				}
				int expected = 1024;
				int errorMargin = 256;
				int value;
				foreach (KeyValuePair<Cell, int> kvp in cells)
				{
					value = Math.Abs(kvp.Value - expected);
					if (value > errorMargin)
					{
						Assert.Fail("Fruit.RandomizeLocation() randomization not random enough: Expected a value within " + errorMargin.ToString() + " of " + expected.ToString() + ", but was " + value.ToString() + " away (value = " + kvp.Value.ToString() + "). Note that this check may fail due to random chance - try running the tests again.");
					}
				}
			}
			cells.Clear();
			List<Cell> exclude = new List<Cell>(16);
			exclude.Add(g[0, 10]);
			exclude.Add(g[6, 2]);
			exclude.Add(g[0, 5]);
			exclude.Add(g[2, 6]);
			exclude.Add(g[9, 1]);
			exclude.Add(g[4, 2]);
			exclude.Add(g[1, 4]);
			exclude.Add(g[3, 5]);
			exclude.Add(g[5, 5]);
			exclude.Add(g[6, 5]);
			exclude.Add(g[7, 5]);
			exclude.Add(g[1, 5]);
			exclude.Add(g[2, 5]);
			exclude.Add(g[0, 0]);
			exclude.Add(g[1, 0]);
			exclude.Add(g[2, 0]);
			{
				Cell c;
				for (int i = 0; i < max; i++)
				{
					f.RandomizeLocation(exclude);
					c = f.OccupiedCell;
					if (exclude.Contains(c))
					{
						string fail = "Fruit.RandomizeLocation(List<Cell>) not excluding specified cells: Expected any cell except those in the set {";
						foreach (Cell eCell in exclude)
						{
							fail += eCell.ToString() + ", ";
						}
						fail = fail.Substring(0, fail.Length - 2) + "}, but was " + c.ToString() + ".";
						Assert.Fail(fail);
					}
					else if (cells.ContainsKey(c))
					{
						cells[c]++;
					}
					else
					{
						cells.Add(c, 1);
					}
				}
				int expected = 1365;
				int errorMargin = 341;
				int value;
				foreach (KeyValuePair<Cell, int> kvp in cells)
				{
					value = Math.Abs(kvp.Value - expected);
					if (value > errorMargin)
					{
						Assert.Fail("Fruit.RandomizeLocation(List<Cell>) randomization not random enough: Expected a value within " + errorMargin.ToString() + " of " + expected.ToString() + ", but was " + value.ToString() + " away (value = " + kvp.Value.ToString() + "). Note that this check may fail due to random chance - try running the tests again.");
					}
				}
			}
			cells.Clear();
			{
				Cell c;
				for (int i = 0; i < max; i++)
				{
					f = new Fruit(g, 1);
					c = f.OccupiedCell;
					if (cells.ContainsKey(c))
					{
						cells[c]++;
					}
					else
					{
						cells.Add(c, 1);
					}
				}
				int expected = 1024;
				int errorMargin = 256;
				int value;
				foreach (KeyValuePair<Cell, int> kvp in cells)
				{
					value = Math.Abs(kvp.Value - expected);
					if (value > errorMargin)
					{
						Assert.Fail("Fruit.Constructor(Grid, int) location randomization not random enough: Expected a value within " + errorMargin.ToString() + " of " + expected.ToString() + ", but was " + value.ToString() + " away (value = " + kvp.Value.ToString() + "). Note that this check may fail due to random chance - try running the tests again.");
					}
				}
			}
			cells.Clear();
			{
				Cell c;
				for (int i = 0; i < max; i++)
				{
					f = new Fruit(g, exclude, 1);
					c = f.OccupiedCell;
					if (exclude.Contains(c))
					{
						string fail = "Fruit.Constructor(Grid, List<Cell>, int) not excluding specified cells: Expected any cell except those in the set {";
						foreach (Cell eCell in exclude)
						{
							fail += eCell.ToString() + ", ";
						}
						fail = fail.Substring(0, fail.Length - 2) + "}, but was " + c.ToString() + ".";
						Assert.Fail(fail);
					}
					else if (cells.ContainsKey(c))
					{
						cells[c]++;
					}
					else
					{
						cells.Add(c, 1);
					}
				}
				int expected = 1365;
				int errorMargin = 341;
				int value;
				foreach (KeyValuePair<Cell, int> kvp in cells)
				{
					value = Math.Abs(kvp.Value - expected);
					if (value > errorMargin)
					{
						Assert.Fail("Fruit.Constructor(Grid, List<Cell>, int) randomization not random enough: Expected a value within " + errorMargin.ToString() + " of " + expected.ToString() + ", but was " + value.ToString() + " away (value = " + kvp.Value.ToString() + "). Note that this check may fail due to random chance - try running the tests again.");
					}
				}
			}
			{
				g = new Grid(1, 1);
				f = new Fruit(g, 1);
				exclude.Clear();
				exclude.Add(g[0, 0]);
				f.RandomizeLocation(exclude);
				Assert.IsFalse(f.OccupiedCell.IsValid, "Attempting to randomize location when all cells in a grid have been excluded should return an invalid cell.");
			}
		}
		/// <summary>
		/// Tests fruit eating.
		/// </summary>
		[Test()]
		public void TestEat()
		{
			Grid g = new Grid(16, 16);
			Snake s = new Snake(g, g[8, 8], 2, Direction.Up);
			Fruit f = new Fruit(g, g[0, 0], 1);
			Timer t = new Timer(1000);
			f.Eaten += (object sender, EatenEventArgs e) =>
			{
				t.Stop();
				t.Dispose();
				Assert.AreEqual(s, e.Eater, "The argument passed to EatenEventArgs must be the snake that ate the fruit.");
				Assert.Pass();
			};
			t.Elapsed += (object sender, ElapsedEventArgs e) =>
			{
				Assert.Fail("Fruit.Eaten event did not fire.");
			};
			t.Start();
			f.Eat(s);
		}
        /// <summary>
        /// Tests snake length when fruit eaten.
        /// </summary>
        [Test()]
        public void TestSnakeLengthWhenEaten()
        {
            Grid g = new Grid(16, 16);
            Snake s = new Snake(g, g[8, 8], 2, Direction.Up);
            Fruit f = new Fruit(g, g[0, 0], 1);

            f.Eaten += (object sender, EatenEventArgs e) =>
            {
                Console.WriteLine("1");
                Assert.AreEqual(3, s.Length, "Snakes length should increase by one as it consumes a fruit");
            };


            Console.WriteLine("2");
            f.Eat(s);

        }
	}
}

