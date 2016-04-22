﻿using System;
using System.Collections.Generic;
using SnakeGame.GridSystem;
using NUnit.Framework;

namespace SnakeGame
{
	[TestFixture()]
	public class TestFruit
	{
		private void TestConstruction(Grid g, Grid.Cell loc, int value)
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
				Fruit f = new Fruit(g, Grid.Cell.INVALID_CELL, 1);
			});
			Assert.Throws<ArgumentException>(delegate()
			{
				Fruit f = new Fruit(new Grid(18, 24), g[0, 0], 3);
			});
			Assert.Throws<ArgumentOutOfRangeException>(delegate()
			{
				Fruit f = new Fruit(g, new Grid.Cell(g, 128, 128), 7);
			});
		}
		[Test()]
		public void Overloads()
		{
			Grid g = new Grid(8, 8);
			Fruit f = new Fruit(g, 4);
			Assert.IsTrue(f.OccupiedCell.IsValid);
			Assert.IsTrue(g.IsDefined(f.OccupiedCell));

		}
		[Test()]
		public void Randomization()
		{
			Grid g = new Grid(8, 8);
			Fruit f = new Fruit(g, g[5, 7], 4);
			int max = 65536;
			Dictionary<Grid.Cell, int> cells = new Dictionary<Grid.Cell, int>(max);
			{
				Grid.Cell c;
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
				foreach (KeyValuePair<Grid.Cell, int> kvp in cells)
				{
					value = Math.Abs(kvp.Value - expected);
					if (value > errorMargin)
					{
						Assert.Fail("Fruit.RandomizeLocation() randomization not random enough: Expected a value within " + errorMargin.ToString() + " of " + expected.ToString() + ", but was " + value.ToString() + " away (value = " + kvp.Value.ToString() + "). Note that this check may fail due to random chance - try running the tests again.");
					}
				}
			}
			cells.Clear();
			List<Grid.Cell> exclude = new List<Grid.Cell>(16);
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
				Grid.Cell c;
				for (int i = 0; i < max; i++)
				{
					f.RandomizeLocation(exclude);
					c = f.OccupiedCell;
					if (exclude.Contains(c))
					{
						string fail = "Fruit.RandomizeLocation(List<Grid.Cell>) not excluding specified cells: Expected any cell except those in the set {";
						foreach (Grid.Cell eCell in exclude)
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
				foreach (KeyValuePair<Grid.Cell, int> kvp in cells)
				{
					value = Math.Abs(kvp.Value - expected);
					if (value > errorMargin)
					{
						Assert.Fail("Fruit.RandomizeLocation(List<Grid.Cell>) randomization not random enough: Expected a value within " + errorMargin.ToString() + " of " + expected.ToString() + ", but was " + value.ToString() + " away (value = " + kvp.Value.ToString() + "). Note that this check may fail due to random chance - try running the tests again.");
					}
				}
			}
			cells.Clear();
			{
				Grid.Cell c;
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
				foreach (KeyValuePair<Grid.Cell, int> kvp in cells)
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
				Grid.Cell c;
				for (int i = 0; i < max; i++)
				{
					f = new Fruit(g, exclude, 1);
					c = f.OccupiedCell;
					if (exclude.Contains(c))
					{
						string fail = "Fruit.Constructor(Grid, List<Grid.Cell>, int) not excluding specified cells: Expected any cell except those in the set {";
						foreach (Grid.Cell eCell in exclude)
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
				foreach (KeyValuePair<Grid.Cell, int> kvp in cells)
				{
					value = Math.Abs(kvp.Value - expected);
					if (value > errorMargin)
					{
						Assert.Fail("Fruit.Constructor(Grid, List<Grid.Cell>, int) randomization not random enough: Expected a value within " + errorMargin.ToString() + " of " + expected.ToString() + ", but was " + value.ToString() + " away (value = " + kvp.Value.ToString() + "). Note that this check may fail due to random chance - try running the tests again.");
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
	}
}

