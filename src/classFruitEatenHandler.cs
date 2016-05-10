using System;
using SnakeGame.Scoring;
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
        private Score _score;
		/// <summary>
		/// Initializes a new instance of the <see cref="SnakeGame.FruitEatenHandler"/> class.
		/// </summary>
		/// <param name="objective">The fruit that can be eaten.</param>
		/// <param name="player">The player that can eat the fruit.</param>
        /// <param name="score">The score to update when fruit eaten.</param>
        public FruitEatenHandler(Fruit objective, Snake player,Score score)
		{
			_objective = objective;
			_player = player;
            _score=score;
			_objective.Eaten += (object sender, EatenEventArgs e) =>
			{
				_objective.RandomizeLocation(_player.OccupiedCells);
                _score.Value+=_objective.Value;
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

