using System;
using NUnit.Framework;

namespace SnakeGame.GridSystem
{
	/// <summary>
	/// Provides unit tests for the Grid class, and the Grid.Cell class.
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
					Assert.AreNotEqual(Grid.Cell.INVALID_CELL, g[x, y], "A point in range must be a valid cell.");
				}
			}
			// TODO: Change indexer AreNotEqual checks to "does not throw exception" checks (and AreEqual to "does throw")
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
		}
		private void TestCell(Grid g, int x, int y)
		{
			Grid.Cell c = g[x, y];
			Assert.IsTrue(c.IsValid, "A valid cell must not be flagged as invalid.");
			Assert.AreEqual(x, c.X, "The X componet of the cell must correspond to the first value passed to the Grid indexer.");
			Assert.AreEqual(y, c.Y, "The Y componet of the cell must correspond to the second value passed to the Grid indexer.");
			Assert.AreEqual(c.X, c.GetAxisValue(Axis2D.X), "GetAxisValue(Axis2D.X) must return the same value as the X property.");
			Assert.AreEqual(c.Y, c.GetAxisValue(Axis2D.Y), "GetAxisValue(Axis2D.Y) must return the same value as the Y property.");
			Grid.Cell c2 = g[x, y];
			Assert.AreEqual(c, c2, "Grid.Cell structural equality must be defined (AssertEquals).");
			Assert.IsTrue(c.Equals(c2), "Grid.Cell structural equality must be defined (IEquatable Equals).");
			Assert.IsTrue(c == c2, "Grid.Cell structural equality must be defined (== operator).");
			Assert.IsFalse(c != c2, "Grid.Cell structural inequality must be defined (!= operator).");
			Assert.IsInstanceOf<IEquatable<Grid.Cell>>(c, "Grid.Cell must implement IEquatable<GridCell>.");
			Assert.AreEqual("(" + x.ToString() + ", " + y.ToString() + ")", c.ToString(), "ToString() must output the correct format.");
			Assert.IsTrue(g.IsDefined(c.X, c.Y), "A valid cell must always be defined in a grid whose range contains it (by axis).");
			Assert.IsTrue(g.IsDefined(c), "A valid cell must always be defined in a grid whose range contains it (pass cell).");
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
			Assert.Throws<ArgumentOutOfRangeException>(delegate() {
				Grid g = new Grid(0, 0);
			}, "Constructing a grid of 0 width and 0 height must throw an ArgumentOutOfRangeException.");
			Assert.Throws<ArgumentOutOfRangeException>(delegate() {
				Grid g = new Grid(-1, 8);
			}, "Constructing a grid of 0 height must throw an ArgumentOutOfRangeException.");
			Assert.Throws<ArgumentOutOfRangeException>(delegate() {
				Grid g = new Grid(8, -1);
			}, "Constructing a grid of 0 width must throw an ArgumentOutOfRangeException.");
			Assert.Throws<ArgumentOutOfRangeException>(delegate() {
				Grid g = new Grid(-1, -1);
			}, "Constructing a grid of negative width and negative height must throw an ArgumentOutOfRangeException.");
		}
		/// <summary>
		/// Tests that cells behave a certain way for any given set of values.
		/// </summary>
		[Test()]
		public void Cells()
		{
			Grid g = new Grid(10, 10);
			TestCell(g, 6, 9);
			TestCell(g, 3, 6);
			TestCell(g, 7, 4);
		}
		/// <summary>
		/// Tests that the INVALID_CELL constant is an invalid cell.
		/// </summary>
		[Test()]
		public void InvalidCell()
		{
			Assert.IsFalse(Grid.Cell.INVALID_CELL.IsValid, "The Grid.Cell.INVALID_CELL constant must have its IsValid property set to false.");
		}
	}
}

