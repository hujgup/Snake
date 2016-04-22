using System;
using System.Collections.Generic;
using SnakeGame.GridSystem;
using SwinGameSDK;

namespace SnakeGame.Graphics
{
	// TODO: Just dumping all the graphics and control stuff in here for now to get *something* working - better solution later.
	public static class CellDrawing
	{
		private static readonly Color _CELL_COLOR = SwinGame.RGBColor(196, 196, 196);
		public static void Draw(int xOffset, int yOffset, int size, Grid.Cell c)
		{
			SwinGame.FillRectangle(_CELL_COLOR, xOffset + c.X*size, yOffset + c.Y*size, size, size);
		}
	}
	public static class MainClass
	{
		public static void Main(string[] args)
		{
			Grid playArea = new Grid(32, 32);
			Snake snake = new Snake(playArea, playArea[16, 16], 5, Direction.Right);
			Fruit objective = new Fruit(playArea, snake.OccupiedCells, 1);

			RenderEvents.LogicTick += (object sender, EventArgs e) =>
			{
				if (SwinGame.KeyDown(KeyCode.vk_w))
				{
					snake.MovementDirection = Direction.Up;
				}
				else if (SwinGame.KeyDown(KeyCode.vk_a))
				{
					snake.MovementDirection = Direction.Left;
				}
				else if (SwinGame.KeyDown(KeyCode.vk_s))
				{
					snake.MovementDirection = Direction.Down;
				}
				else if (SwinGame.KeyDown(KeyCode.vk_d))
				{
					snake.MovementDirection = Direction.Right;
				}
			};
			RenderEvents.RenderTick += (object sender, EventArgs e) =>
			{
				int x;
				int y;
				for (y = -1, x = -1; x <= playArea.Width; x++)
				{
					CellDrawing.Draw(8, 8, 8, new Grid.Cell(playArea, x, y));
					CellDrawing.Draw(8, 8, 8, new Grid.Cell(playArea, x, playArea.Height));
				}
				for (y = 0, x = -1; y < playArea.Height; y++)
				{
					CellDrawing.Draw(8, 8, 8, new Grid.Cell(playArea, x, y));
					CellDrawing.Draw(8, 8, 8, new Grid.Cell(playArea, playArea.Width, y));
				}
				foreach (MovementNode node in snake)
				{
					CellDrawing.Draw(8, 8, 8, node.Cell);
				}
				CellDrawing.Draw(8, 8, 8, objective.OccupiedCell);
			};

			SwinGame.OpenGraphicsWindow("Snake", 272, 272);
			Color background = SwinGame.RGBColor(32, 32, 32);
			SnakeMover mover = new SnakeMover(snake, 5d);
			while (!SwinGame.WindowCloseRequested())
			{
				SwinGame.ProcessEvents();
				RenderEvents.OnLogicTick();
				SwinGame.ClearScreen(background);
				RenderEvents.OnRenderTick();
				SwinGame.RefreshScreen(60);
			}
			mover.Dispose();
		}
	}
}

