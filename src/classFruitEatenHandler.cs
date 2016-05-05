using System;
using SnakeGame.Model;

namespace SnakeGame
{
	/// <summary>
	/// Handles the eating of a given fruit by a given snake.
	/// </summary>
	public class FruitEatenHandler
	{
		private Fruit _objective;
		private Snake _player;
		/// <summary>
		/// Initializes a new instance of the <see cref="SnakeGame.FruitEatenHandler"/> class.
		/// </summary>
		/// <param name="objective">The fruit that can be eaten.</param>
		/// <param name="player">The player that can eat the fruit.</param>
		public FruitEatenHandler(Fruit objective, Snake player)
		{
			_objective = objective;
			_player = player;
			_objective.Eaten += (object sender, EatenEventArgs e) =>
			{
				_objective.RandomizeLocation(_player.OccupiedCells);
			};
		}
		/// <summary>
		/// Checks whether the snake should eat the fruit.
		/// </summary>
		public void EvaluateState()
		{
			if (_player.Head.Cell == _objective.OccupiedCell)
			{
				_objective.Eat(_player);
			}
}
	}
}

