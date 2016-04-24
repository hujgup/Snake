using System;

namespace SnakeGame.UserInterface
{
	/// <summary>
	/// Allows the setting of a value based on the <see cref="SnakeGame.UserInterface.RenderEvents.LogicTick"/> event. 
	/// </summary>
	public class ControlsFlag<T> : Flag<T>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SnakeGame.UserInterface.ControlsFlag{T}"/> class.
		/// </summary>
		/// <param name="setter">The delegate that sets the value of this flag.</param>
		public ControlsFlag(Func<T> setter) : base(setter)
		{
			RenderEvents.LogicTick += Handler;
		}
		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="SnakeGame.UserInterface.ControlsFlag{T}"/>.
		/// The <see cref="Dispose"/> method leaves the <see cref="SnakeGame.UserInterface.ControlsFlag{T}"/> in an unusable
		/// state. After calling <see cref="Dispose"/>, you must release all references to the
		/// <see cref="SnakeGame.UserInterface.ControlsFlag{T}"/> so the garbage collector can reclaim the memory that the
		/// <see cref="SnakeGame.UserInterface.ControlsFlag{T}"/> was occupying.</remarks>
		public override void Dispose()
		{
			RenderEvents.LogicTick -= Handler;
		}
	}
}

