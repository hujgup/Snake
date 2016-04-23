using System;
using System.Timers;

namespace SnakeGame
{
	/// <summary>
	/// Class for periodically moving a Snake.
	/// </summary>
	public class SnakeMover : IDisposable
	{
		private Snake _snake;
		private Timer _timer;
		/// <summary>
		/// Initializes a new instance of the <see cref="SnakeGame.SnakeMover"/> class.
		/// </summary>
		/// <param name="snake">The snake to move.</param>
		/// <param name="updatesPerSecond">The number of times per second to move the snake.</param>
		public SnakeMover(Snake snake, double updatesPerSecond)
		{
			_snake = snake;
			_timer = new Timer(1000d/updatesPerSecond);
			_timer.Elapsed += (object sender, ElapsedEventArgs e) =>
			{
				_snake.Move();
			};
			_timer.Start();
		}
		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="SnakeGame.SnakeMover"/>. The
		/// <see cref="Dispose"/> method leaves the <see cref="SnakeGame.SnakeMover"/> in an unusable state. After calling
		/// <see cref="Dispose"/>, you must release all references to the <see cref="SnakeGame.SnakeMover"/> so the garbage
		/// collector can reclaim the memory that the <see cref="SnakeGame.SnakeMover"/> was occupying.</remarks>
		public void Dispose()
		{
			_timer.Stop();
			_timer.Dispose();
		}
	}
}

