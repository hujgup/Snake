using System;
using System.Text.RegularExpressions;
using SnakeGame.GridSystem;
using SwinGameSDK;

namespace SnakeGame.UserInterface
{
	/// <summary>
	/// Allows simple cell drawing.
	/// </summary>
	public static class CellDrawing
	{
		private static readonly Regex _VALID_CODE = new Regex("^#([0-9a-f]{3}|[0-9a-f]{6})$",RegexOptions.IgnoreCase);
		/// <summary>
		/// The color of a cell.
		/// </summary>
		public static readonly Color COLOR = SwinGame.RGBColor(196, 196, 196);
		public const int CELL_SIZE = 12;
		private static byte HexCharLookup(char digit)
		{
			// For some reason .NET doesn't have this built-in, unless you cast to an Int32 first
			byte res;
			switch (digit)
			{
				case '0':
					res = 0;
					break;
				case '1':
					res = 1;
					break;
				case '2':
					res = 2;
					break;
				case '3':
					res = 3;
					break;
				case '4':
					res = 4;
					break;
				case '5':
					res = 5;
					break;
				case '6':
					res = 6;
					break;
				case '7':
					res = 7;
					break;
				case '8':
					res = 8;
					break;
				case '9':
					res = 9;
					break;
				case 'A':
				case 'a':
					res = 10;
					break;
				case 'B':
				case 'b':
					res = 11;
					break;
				case 'C':
				case 'c':
					res = 12;
					break;
				case 'D':
				case 'd':
					res = 13;
					break;
				case 'E':
				case 'e':
					res = 14;
					break;
				case 'F':
				case 'f':
					res = 15;
					break;
				default:
					throw new ArgumentOutOfRangeException("digit","Passed char must be a valid hexadecimal digit (0-9 A-F a-f).");
			}
			return res;
		}
		private static byte LoadShorthand(char digit)
		{
			byte d = HexCharLookup(digit);
			return (byte)((d << 4) | d);
		}
		private static byte LoadLonghand(char leadDigit,char trailingDigit)
		{
			byte d1 = HexCharLookup(leadDigit);
			byte d2 = HexCharLookup(trailingDigit);
			return (byte)((d1 << 4) | d2);
		}
		/// <summary>
		/// Draws a specified cell.
		/// </summary>
		/// <param name="xOffset">The number of cells to the right of X = 0 to re-zero at.</param>
		/// <param name="yOffset">The number of cells to the right of Y = 0 to re-zero at.</param>
		/// <param name="c">The cell to draw.</param>
		public static void Draw(int xOffset, int yOffset, Cell c)
		{
			SwinGame.FillRectangle(COLOR, CELL_SIZE*(xOffset + c.X), CELL_SIZE*(yOffset + c.Y), CELL_SIZE, CELL_SIZE);
		}
		/// <summary>
		/// Generates a SwinGame Color based on the given color code.
		/// </summary>
		/// <param name="code">The color to generate.</param>
		public static Color GetColor(string code)
		{
			Color res;
			Match m = _VALID_CODE.Match(code);
			if (m.Success)
			{
				bool shorthand = code.Length == 4;
				byte r = shorthand ? LoadShorthand(code[1]) : LoadLonghand(code[1], code[2]);
				byte g = shorthand ? LoadShorthand(code[2]) : LoadLonghand(code[3], code[4]);
				byte b = shorthand ? LoadShorthand(code[3]) : LoadLonghand(code[5], code[6]);
				byte a = 255;
				res = Color.FromArgb((a << 24) + (r << 16) + (g << 8) + b);
			}
			else
			{
				res = Color.Transparent;
			}
			return res;
		}
	}
}

