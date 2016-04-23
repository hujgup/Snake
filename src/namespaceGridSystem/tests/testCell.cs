using System;
using NUnit.Framework;

namespace SnakeGame.GridSystem
{
	/// <summary>
	/// Provides unit tests for the Cell class.
	/// </summary>
	[TestFixture()]
	public class TestCell
	{
		private void TestCtor(Grid g, int x, int y)
		{
			Cell c = g[x, y];
			Assert.IsTrue(c.IsValid, "A valid cell must not be flagged as invalid.");
			Assert.AreEqual(g, c.Owner, "The cell must be owned by the specified grid.");
			Assert.AreEqual(x, c.X, "The X componet of the cell must correspond to the first value passed to the Grid indexer.");
			Assert.AreEqual(y, c.Y, "The Y componet of the cell must correspond to the second value passed to the Grid indexer.");
			Assert.AreEqual(c.X, c.GetAxisValue(Axis2D.X), "GetAxisValue(Axis2D.X) must return the same value as the X property.");
			Assert.AreEqual(c.Y, c.GetAxisValue(Axis2D.Y), "GetAxisValue(Axis2D.Y) must return the same value as the Y property.");
			Cell c2 = g[x, y];
			Assert.AreEqual(c, c2, "Cell structural equality must be defined (AssertEquals).");
			Assert.IsTrue(c.Equals(c2), "Cell structural equality must be defined (IEquatable Equals).");
			Assert.IsTrue(c == c2, "Cell structural equality must be defined (== operator).");
			Assert.IsFalse(c != c2, "Cell structural inequality must be defined (!= operator).");
			Assert.IsInstanceOf<IEquatable<Cell>>(c, "Cell must implement IEquatable<Cell>.");
			Assert.AreEqual("(" + x.ToString() + ", " + y.ToString() + ")", c.ToString(), "ToString() must output the correct format.");
			Assert.IsTrue(g.IsDefined(c.X, c.Y), "A valid cell must always be defined in a grid whose range contains it (by axis).");
			Assert.IsTrue(g.IsDefined(c), "A valid cell must always be defined in a grid whose range contains it (pass cell).");
			Grid g2 = new Grid(g.Width, g.Height);
			Cell c3 = g2[x, y];
			Assert.AreNotEqual(c, c3, "Two cells with the same dimensions but not the same owner should not be equal.");
			Cell c4 = g[x != 0 ? x - 1 : x + 1, y != 0 ? y - 1 : y + 1];
			Assert.AreNotEqual(c, c4, "Two cells with the same owner but not the same dimensions should not be equal.");
		}
		/// <summary>
		/// Tests that cells behave a certain way for any given set of values.
		/// </summary>
		[Test()]
		public void Cells()
		{
			Grid g = new Grid(10, 10);
			TestCtor(g, 6, 9);
			TestCtor(g, 3, 6);
			TestCtor(g, 7, 4);
		}
		/// <summary>
		/// Tests that the INVALID_CELL constant is an invalid cell.
		/// </summary>
		[Test()]
		public void InvalidCell()
		{
			Assert.IsFalse(Cell.INVALID_CELL.IsValid, "The Cell.INVALID_CELL constant must have its IsValid property set to false.");
		}
	}
}

