using System;

namespace SnakeGame
{
	/// <summary>
	/// State change event arguments.
	/// </summary>
	public class StateChangeEventArgs : EventArgs
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SnakeGame.StateChangeEventArgs"/> class.
		/// </summary>
		/// <param name="state">The state being changed to.</param>
		public StateChangeEventArgs(GameState state) : base()
		{
			TargetState = state;
		}
		/// <summary>
		/// Gets the state being changed to.
		/// </summary>
		public GameState TargetState
		{
			get;
			private set;
		}
	}
}

