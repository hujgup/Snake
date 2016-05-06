using System;
using NUnit.Framework;
using SwinGameSDK;

namespace SnakeGame.UserInterface
{
	/// <summary>
	/// Provides unit tests for the CellDrawing class.
	/// </summary>
	[TestFixture()]
	public class TestCellDrawing
	{
		/// <summary>
		/// Tests that CellDrawing.COLOR is set to the correct value.
		/// </summary>
		[Test()]
		public void TestColorConst()
		{
			Assert.AreEqual(SwinGame.RGBColor(196, 196, 196), CellDrawing.COLOR, "CellDrawing.COLOR must be 196, 196, 196.");
		}
		/// <summary>
		/// Tests that CellDrawing.GetColor(string) behaves correctly.
		/// </summary>
		[Test()]
		public void TestGetColor()
		{
			Color c = CellDrawing.GetColor("#e00707");
			Assert.AreEqual(SwinGame.RGBColor(224, 7, 7), c, "Lower-case hex characters should be valid input.");
			c = CellDrawing.GetColor("#E00707");
			Assert.AreEqual(SwinGame.RGBColor(224, 7, 7), c, "Upper-case hex characters should be treated as valid input.");
			c = CellDrawing.GetColor("#008141");
			Assert.AreEqual(SwinGame.RGBColor(0, 129, 65), c, "Input with no non-numeric hex characters should be treated as valid.");
			c = CellDrawing.GetColor("#ef00Ac");
			Assert.AreEqual(SwinGame.RGBColor(239, 0, 172), c, "Input with no numeric hex characters should be treated as valid.");
			c = CellDrawing.GetColor("#F00");
			Color compare = CellDrawing.GetColor("#fF0000");
			Assert.AreEqual(compare, c, "Short-hand inputs should treat each place as if it were expanded and duplicated (#F00 == #FF0000).");
			Color invalid = Color.Transparent;
			c = CellDrawing.GetColor("008141");
			Assert.AreEqual(invalid, c, "An otherwise valid color that is not proceeded by # should be treated as invalid.");
			c = CellDrawing.GetColor("hello world");
			Assert.AreEqual(invalid, c, "Random strings should be treated as invalid.");
			c = CellDrawing.GetColor("#r56f6E");
			Assert.AreEqual(invalid, c, "Color code strings that contain characters outside the case-insensitive hexadecimal range should be treated as invalid.");
		}
	}
}

