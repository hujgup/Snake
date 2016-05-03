using System;
using System.Collections.Generic;
using SnakeGame.Model;
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
		/// <summary>
		/// The entry point of the program, where the program control starts and ends.
		/// </summary>
		/// <param name="args">The command-line arguments.</param>
		public static void Main(string[] args)
		{
			SwinGame.OpenGraphicsWindow("Snake", 272, 272);
			Color background = SwinGame.RGBColor(32, 32, 32);

			MetaController controller = new MetaController();
			controller.SetController(new GameplayController(Difficulty.Medium));

			while (!SwinGame.WindowCloseRequested())
			{
				SwinGame.ProcessEvents();
				RenderEvents.OnLogicTick();
				SwinGame.ClearScreen(background);
				RenderEvents.OnRenderTick();
				SwinGame.RefreshScreen(60);
			}

			controller.Dispose();
		}
	}
}

