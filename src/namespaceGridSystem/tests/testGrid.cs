using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace SnakeGame.GridSystem
{
	/// <summary>
	/// Provides unit tests for the Grid class.
	/// </summary>
	[TestFixture()]
	public class TestGrid
	{
		private void TestValidGrid(int width, int height)
		{
			Grid g = new Grid(width, height);
			Assert.AreEqual(width, g.Width, "Width must correspond to the first constructor argument.");
			Assert.AreEqual(height, g.Height, "Height must correspond to the second constructor argument.");
			RangeRestriction<int> xRestrict = new RangeRestriction<int>(0, width, false, true);
			RangeRestriction<int> yRestrict = new RangeRestriction<int>(0, height, false, true);
			Assert.AreEqual(xRestrict, g.RangeX, "RangeX must be [0, width).");
			Assert.AreEqual(yRestrict, g.RangeY, "RangeY must be [0, height).");
			Assert.AreEqual(g.RangeX, g.GetAxisRange(Axis2D.X), "GetAxisRange(Axis2D.X) must return the same value as RangeX.");
			Assert.AreEqual(g.RangeY, g.GetAxisRange(Axis2D.Y), "GetAxisRange(Axis2D.Y) must return the same value as RangeY.");
			Assert.AreEqual(width * height, g.CellCount, "Total number of cells must be width * height.");
			int x;
			for (int y = 0; y < height; y++)
			{
				for (x = 0; x < width; x++)
				{
					Assert.IsTrue(g.IsDefined(x, y), "All points in range must be defined.");
					Assert.AreNotEqual(Cell.INVALID_CELL, g[x, y], "A point in range must be a valid cell.");
				}
			}
			Assert.IsFalse(g.IsDefined(-1, 0), "Out-of-range points must be undefined (-1, 0).");
			Assert.IsFalse(g[-1, 0].IsValid, "Out-of-range cells must be invalid cells (-1, 0).");
			Assert.IsFalse(g.IsDefined(0, -1), "Out-of-range points must be undefined (0, -1).");
			Assert.IsFalse(g[0, -1].IsValid, "Out-of-range cells must be invalid cells (0, -1).");
			Assert.IsFalse(g.IsDefined(-1, -1), "Out-of-range points must be undefined (-1, -1).");
			Assert.IsFalse(g[-1, -1].IsValid, "Out-of-range cells must be invalid cells (-1, -1).");
			Assert.IsFalse(g.IsDefined(width, 0), "Out-of-range points must be undefined (width, 0).");
			Assert.IsFalse(g[width, 0].IsValid, "Out-of-range cells must be invalid cells (width, 0).");
			Assert.IsFalse(g.IsDefined(0, height), "Out-of-range points must be undefined (0, height).");
			Assert.IsFalse(g[0, height].IsValid, "Out-of-range cells must be invalid cells (0, height).");
			Assert.IsFalse(g.IsDefined(width, height), "Out-of-range points must be undefined (width, height).");
			Assert.IsFalse(g[width, height].IsValid, "Out-of-range cells must be invalid cells (width, height).");
			Assert.IsInstanceOf<IEnumerable<Cell>>(g, "Grid must implement IEnumerable<Cell>.");
		}
		/// <summary>
		/// Tests that grids behave a certian way for any given set of values.
		/// </summary>
		[Test()]
		public void GridSizes()
		{
			TestValidGrid(10, 8);
			TestValidGrid(128, 131);
			TestValidGrid(1, 1);
			Assert.Throws<ArgumentOutOfRangeException>(delegate()
			{
				Grid g = new Grid(0, 0);
			}, "Constructing a grid of 0 width and 0 height must throw an ArgumentOutOfRangeException.");
			Assert.Throws<ArgumentOutOfRangeException>(delegate()
			{
				Grid g = new Grid(-1, 8);
			}, "Constructing a grid of 0 height must throw an ArgumentOutOfRangeException.");
			Assert.Throws<ArgumentOutOfRangeException>(delegate()
			{
				Grid g = new Grid(8, -1);
			}, "Constructing a grid of 0 width must throw an ArgumentOutOfRangeException.");
			Assert.Throws<ArgumentOutOfRangeException>(delegate()
			{
				Grid g = new Grid(-1, -1);
			}, "Constructing a grid of negative width and negative height must throw an ArgumentOutOfRangeException.");
		}
		/// <summary>
		/// Tests that the grid random cell selection is working correctly.
		/// </summary>
		[Test()]
		public void Randomization()
		{
			Grid g = new Grid(16, 16);
			int max = 65536;
			Dictionary<Cell, int> cells = new Dictionary<Cell, int>(max);
			{
				Cell c;
				for (int i = 0; i < max; i++)
				{
					c = g.GetRandomCell();
					if (cells.ContainsKey(c))
					{
						cells[c]++;
					}
					else
					{
						cells.Add(c, 1);
					}
				}
				int expected = 256;
				int errorMargin = 64;
				int value;
				foreach (KeyValuePair<Cell, int> kvp in cells)
				{
					value = Math.Abs(kvp.Value - expected);
					if (value > errorMargin)
					{
						Assert.Fail("Grid.GetRandomCell() randomization not random enough: Expected a value within " + errorMargin.ToString() + " of " + expected.ToString() + ", but was " + value.ToString() + " away (value = " + kvp.Value.ToString() + "). Note that this check may fail due to random chance - try running the tests again.");
					}
				}
			}

			cells.Clear();

			{
				List<Cell> exclude = new List<Cell>(8);
				exclude.Add(g[0, 0]);
				exclude.Add(g[0, 1]);
				exclude.Add(g[15, 6]);
				exclude.Add(g[7, 7]);
				exclude.Add(g[4, 8]);
				exclude.Add(g[4, 8]);
				exclude.Add(g[10, 10]);
				exclude.Add(g[9, 0]);
				Cell c;
				for (int i = 0; i < max; i++)
				{
					c = g.GetRandomCell(exclude);
					if (exclude.Contains(c))
					{
						string fail = "Grid.GetRandomCell(List<Cell>) not excluding specified cells: Expected any cell except those in the set {";
						foreach (Cell eCell in exclude)
						{
							fail += eCell.ToString()+", ";
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
				int expected = 264;
				int errorMargin = 66;
				int value;
				foreach (KeyValuePair<Cell, int> kvp in cells)
				{
					value = Math.Abs(kvp.Value - expected);
					if (value > errorMargin)
					{
						Assert.Fail("Grid.GetRandomCell(List<Cell>) randomization not random enough: Expected a value within " + errorMargin.ToString() + " of " + expected.ToString() + ", but was " + value.ToString() + " away (value = " + kvp.Value.ToString() + "). Note that this check may fail due to random chance - try running the tests again.");
					}
				}
			}

			{
				g = new Grid(1, 1);
				List<Cell> exclude = new List<Cell>(1);
				exclude.Add(g[0, 0]);
				Assert.IsFalse(g.GetRandomCell(exclude).IsValid, "Attempting to get a random cell when all cells in a grid have been excluded should return an invalid cell.");
			}
		}
	}
}

