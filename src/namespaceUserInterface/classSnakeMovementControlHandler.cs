using System;
using System.Collections.Generic;
using SnakeGame.Model;

namespace SnakeGame.UserInterface
{
	/// <summary>
	/// Class for periodically moving a snake, and changing its direction.
	/// </summary>
	public class SnakeMovementControlHandler : SnakeMover
	{
		private Queue<Direction> _movementQueue;
		private int _currentTick;
		private int _queuedTick;
		/// <summary>
		/// Initializes a new instance of the <see cref="SnakeGame.UserInterface.SnakeMovementControlHandler"/> class.
		/// </summary>
		/// <param name="snake">The snake to move.</param>
		/// <param name="updatesPerSecond">The number of times per second to move the snake.</param>
		public SnakeMovementControlHandler(Snake snake, double updatesPerSecond) : base(snake, updatesPerSecond)
		{
			_movementQueue = new Queue<Direction>();
			_currentTick = 0;
			_queuedTick = -1;
			BeforeMove += (object sender, EventArgs e) =>
			{
				if (_movementQueue.Count != 0)
				{
					Target.MovementDirection = _movementQueue.Dequeue();
				}
				_currentTick++;
			};
		}
		/// <summary>
		/// Adds a specified direction to the end of this movement tick's queue.
		/// </summary>
		/// <param name="dir">The direction to enqueue.</param>
		public void Enqueue(Direction dir)
		{
			if (_currentTick != _queuedTick)
			{
				_movementQueue.Clear();
				_queuedTick = _currentTick;
			}
			_movementQueue.Enqueue(dir);
		}
	}
}

