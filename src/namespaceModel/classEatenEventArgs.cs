using System;

namespace SnakeGame.Model
{
	/// <summary>
	/// Event args for when a Fruit is eaten.
	/// </summary>
	public class EatenEventArgs : EventArgs
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SnakeGame.Model.EatenEventArgs"/> class.
		/// </summary>
		/// <param name="eater">The snake that ate the fruit.</param>
		public EatenEventArgs(Snake eater) : base()
		{
			Eater = eater;
		}
		/// <summary>
		/// Gets the snake that ate the fruit.
		/// </summary>
		public Snake Eater
		{
			get;
			private set;
		}
	}
}

