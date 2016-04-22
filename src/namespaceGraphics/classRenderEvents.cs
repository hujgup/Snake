using System;
using SwinGameSDK;

namespace SnakeGame.Graphics
{
	/// <summary>
	/// Allows access to the rendering events used by the game.
	/// </summary>
	public static class RenderEvents
	{
		public static void OnLogicTick()
		{
			if (LogicTick != null)
			{
				LogicTick(null, EventArgs.Empty);
			}
		}
		public static void OnRenderTick()
		{
			if (RenderTick != null)
			{
				RenderTick(null, EventArgs.Empty);
			}
		}
		public static event EventHandler LogicTick;
		public static event EventHandler RenderTick;
	}
}

