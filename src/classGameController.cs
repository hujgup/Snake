using System;

namespace SnakeGame
{
	/// <summary>
	/// Represents any object used to control overall flow.
	/// </summary>
	public abstract class GameController : IDisposable
	{
		/// <summary>
		/// Raises the done event.
		/// </summary>
		/// <param name="targetState">The state that should be swapped to.</param>
		protected void OnDone(GameState targetState)
		{
			if (Done != null)
			{
				Done(this,new StateChangeEventArgs(targetState));
			}
		}
		/// <summary>
		/// Occurs when the game state changes away from that which is provided by this controller.
		/// </summary>
		public event EventHandler<StateChangeEventArgs> Done;
		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="SnakeGame.GameController"/>. The
		/// <see cref="Dispose"/> method leaves the <see cref="SnakeGame.GameController"/> in an unusable state. After calling
		/// <see cref="Dispose"/>, you must release all references to the <see cref="SnakeGame.GameController"/> so the
		/// garbage collector can reclaim the memory that the <see cref="SnakeGame.GameController"/> was occupying.</remarks>
		public abstract void Dispose();
	}
}

