using System;
using SwinGameSDK;

namespace SnakeGame.Graphics
{
	/// <summary>
	/// Allows access to the rendering events used by the game.
	/// </summary>
	public static class RenderEvents
	{
		/// <summary>
		/// Fires the LogicTick event.
		/// </summary>
		public static void OnLogicTick()
		{
			if (LogicTick != null)
			{
				LogicTick(null, EventArgs.Empty);
			}
		}
		/// <summary>
		/// Fires the RenderTick event.
		/// </summary>
		public static void OnRenderTick()
		{
			if (RenderTick != null)
			{
				RenderTick(null, EventArgs.Empty);
			}
		}
		/// <summary>
		/// Fires when game logic should execute.
		/// </summary>
		public static event EventHandler LogicTick;
		/// <summary>
		/// Fires when rendering should occur.
		/// </summary>
		public static event EventHandler RenderTick;
	}
}

