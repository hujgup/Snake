using System;

namespace SnakeGame.UserInterface
{
	/// <summary>
	/// Allows the setting of a boolean value based on the <see cref="SnakeGame.UserInterface.RenderEvents.LogicTick"/> event. 
	/// </summary>
	public class BooleanControlsFlag : BooleanFlag
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SnakeGame.UserInterface.BooleanControlsFlag"/> class.
		/// </summary>
		/// <param name="setter">The delegate that sets the value of this flag.</param>
		public BooleanControlsFlag(Func<bool> setter) : base(setter)
		{
			RenderEvents.LogicTick += Handler;
		}
		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="SnakeGame.UserInterface.BooleanControlsFlag"/>. The
		/// <see cref="Dispose"/> method leaves the <see cref="SnakeGame.UserInterface.BooleanControlsFlag"/> in an unusable state.
		/// After calling <see cref="Dispose"/>, you must release all references to the
		/// <see cref="SnakeGame.UserInterface.BooleanControlsFlag"/> so the garbage collector can reclaim the memory that the
		/// <see cref="SnakeGame.UserInterface.BooleanControlsFlag"/> was occupying.</remarks>
		public override void Dispose()
		{
			RenderEvents.LogicTick -= Handler;
		}
	}
}

