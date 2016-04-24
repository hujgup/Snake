using System;
using SwinGameSDK;

namespace SnakeGame.UserInterface
{
	/// <summary>
	/// Allows access to the rendering events used by the game.
	/// </summary>
	public static class RenderEvents
	{
		/// <summary>
		/// Gets the set of delegates set to execute when LogicTick fires.
		/// </summary>
		public static Delegate[] LogicInvokers()
		{
			return LogicTick != null ? LogicTick.GetInvocationList() : new Delegate[0];
		}
		/// <summary>
		/// Gets the set of delegates set to execute when RenderTick fires.
		/// </summary>
		public static Delegate[] RenderInvokers()
		{
			return RenderTick != null ? RenderTick.GetInvocationList() : new Delegate[0];
		}
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

