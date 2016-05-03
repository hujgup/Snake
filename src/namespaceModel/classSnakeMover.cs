using System;
using System.Timers;
using KeyCode = SwinGameSDK.KeyCode;

namespace SnakeGame.Model
{
	/// <summary>
	/// Class for periodically moving a Snake.
	/// </summary>
	public class SnakeMover : IDisposable
	{
		private Snake _snake;
		private Timer _timer;
		/// <summary>
		/// Initializes a new instance of the <see cref="SnakeGame.Model.SnakeMover"/> class.
		/// </summary>
		/// <param name="snake">The snake to move.</param>
		/// <param name="updatesPerSecond">The number of times per second to move the snake.</param>
		public SnakeMover(Snake snake, double updatesPerSecond)
		{
			_snake = snake;
			_timer = new Timer(1000d/updatesPerSecond);
			_timer.Elapsed += (object sender, ElapsedEventArgs e) =>
			{
				if (BeforeMove != null)
				{
					BeforeMove(this, EventArgs.Empty);
				}
				_snake.Move();
				if (AfterMove != null)
				{
					AfterMove(this, EventArgs.Empty);
				}
			};
			_timer.Start();
		}
		/// <summary>
		/// Gets the Snake that this instance moves.
		/// </summary>
		public Snake Target
		{
			get
			{
				return _snake;
			}
		}
		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="SnakeGame.Model.SnakeMover"/>. The
		/// <see cref="Dispose"/> method leaves the <see cref="SnakeGame.Model.SnakeMover"/> in an unusable state. After calling
		/// <see cref="Dispose"/>, you must release all references to the <see cref="SnakeGame.Model.SnakeMover"/> so the garbage
		/// collector can reclaim the memory that the <see cref="SnakeGame.Model.SnakeMover"/> was occupying.</remarks>
		public void Dispose()
		{
			_timer.Stop();
			_timer.Dispose();
		}
		/// <summary>
		/// Fires just before the snake is moved.
		/// </summary>
		public event EventHandler BeforeMove;
		/// <summary>
		/// Fires just after the snake is moved.
		/// </summary>
		public event EventHandler AfterMove;
	}
}

