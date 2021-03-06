﻿using System;
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
			int size = CellDrawing.CELL_SIZE*34;
			SwinGame.OpenGraphicsWindow("Snake", size, size);
			Color background = SwinGame.RGBColor(32, 32, 32);

			MetaController controller = new MetaController();
			controller.SetController(new SetupMenuController());

			while (!SwinGame.WindowCloseRequested())
			{
				SwinGame.ProcessEvents();
				RenderEvents.OnLogicTick();
				SwinGame.ClearScreen(background);
				RenderEvents.OnRenderTick();
				SwinGame.RefreshScreen(60);
			}
		}
	}
}

