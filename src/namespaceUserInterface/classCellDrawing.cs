using System;
using SnakeGame.GridSystem;
using SwinGameSDK;

namespace SnakeGame.UserInterface
{
	/// <summary>
	/// Allows simple cell drawing.
	/// </summary>
	public static class CellDrawing
	{
		private const int _CELL_SIZE = 8;
		private static readonly Color _CELL_COLOR = SwinGame.RGBColor(196, 196, 196);
		/// <summary>
		/// Draws a specified cell.
		/// </summary>
		/// <param name="xOffset">The number of cells to the right of X = 0 to re-zero at.</param>
		/// <param name="yOffset">The number of cells to the right of Y = 0 to re-zero at.</param>
		/// <param name="c">The cell to draw.</param>
		public static void Draw(int xOffset, int yOffset, Cell c)
		{
			SwinGame.FillRectangle(_CELL_COLOR, _CELL_SIZE*(xOffset + c.X), _CELL_SIZE*(yOffset + c.Y), _CELL_SIZE, _CELL_SIZE);
		}
	}
}

