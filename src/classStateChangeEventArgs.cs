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
		/// <param name="switchTo">The GameController to switch to.</param>
		public StateChangeEventArgs(GameController switchTo) : base()
		{
			Target = switchTo;
		}
		/// <summary>
		/// Gets the target GameController.
		/// </summary>
		public GameController Target
		{
			get;
			private set;
		}
	}
}

