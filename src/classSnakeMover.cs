using System;
using System.Timers;

namespace SnakeGame
{
	public class SnakeMover : IDisposable
	{
		private Snake _snake;
		private Timer _timer;
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
		public void Dispose()
		{
			_timer.Stop();
			_timer.Dispose();
		}
	}
}

