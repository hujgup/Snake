using System;
using System.Collections.Generic;
using SnakeGame.GridSystem;
using SnakeGame.UserInterface;
using SwinGameSDK;

namespace SnakeGame
{
	/// <summary>
	/// Main class.
	/// </summary>
	public static class MainClass
	{
		// TODO: Just dumping all the rendering and control stuff in here for now to get *something* working - better solution later.
		/// <summary>
		/// The entry point of the program, where the program control starts and ends.
		/// </summary>
		/// <param name="args">The command-line arguments.</param>
		public static void Main(string[] args)
		{
			SwinGame.OpenGraphicsWindow("Snake", 272, 272);
			Color background = SwinGame.RGBColor(32, 32, 32);
			Grid playArea = new Grid(32, 32);
			Snake snake = new Snake(playArea, playArea[16, 16], 5, Direction.Right);
			Fruit objective = new Fruit(playArea, snake.OccupiedCells, 1);
			SnakeMovementControlHandler mover = new SnakeMovementControlHandler(snake, 5d);

			BooleanControlsFlag up = new BooleanControlsFlag(delegate()
			{
				return SwinGame.KeyDown(KeyCode.vk_w) || SwinGame.KeyDown(KeyCode.vk_UP);
			});
			up.StateSetTrue += (object sender, EventArgs e) =>
			{
				mover.Enqueue(Direction.Up);
			};
			BooleanControlsFlag left = new BooleanControlsFlag(delegate()
			{
				return SwinGame.KeyDown(KeyCode.vk_a) || SwinGame.KeyDown(KeyCode.vk_LEFT);
			});
			left.StateSetTrue += (object sender, EventArgs e) =>
			{
				mover.Enqueue(snake.MovementDirection = Direction.Left);
			};
			BooleanControlsFlag down = new BooleanControlsFlag(delegate()
			{
				return SwinGame.KeyDown(KeyCode.vk_s) || SwinGame.KeyDown(KeyCode.vk_DOWN);
			});
			down.StateSetTrue += (object sender, EventArgs e) =>
			{
				mover.Enqueue(snake.MovementDirection = Direction.Down);
			};
			BooleanControlsFlag right = new BooleanControlsFlag(delegate()
			{
				return SwinGame.KeyDown(KeyCode.vk_d) || SwinGame.KeyDown(KeyCode.vk_RIGHT);
			});
			right.StateSetTrue += (object sender, EventArgs e) =>
			{
				mover.Enqueue(snake.MovementDirection = Direction.Right);
			};

			RenderEvents.RenderTick += (object sender, EventArgs e) =>
			{
				int offset = 1;
				int x;
				int y;
				for (y = -1, x = -1; x <= playArea.Width; x++)
				{
					CellDrawing.Draw(offset, offset, new Cell(playArea, x, y));
					CellDrawing.Draw(offset, offset, new Cell(playArea, x, playArea.Height));
				}
				for (y = 0, x = -1; y < playArea.Height; y++)
				{
					CellDrawing.Draw(offset, offset, new Cell(playArea, x, y));
					CellDrawing.Draw(offset, offset, new Cell(playArea, playArea.Width, y));
				}
				foreach (MovementNode node in snake)
				{
					CellDrawing.Draw(offset, offset, node.Cell);
				}
				CellDrawing.Draw(offset, offset, objective.OccupiedCell);
			};

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

