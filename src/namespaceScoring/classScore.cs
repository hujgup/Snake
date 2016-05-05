using System;

namespace SnakeGame.Scoring
{
	/// <summary>
	/// Represents a score.
	/// </summary>
	public class Score
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SnakeGame.Scoring.Score"/> class.
		/// </summary>
		/// <param name="gameplayDifficulty">The difficulty being played on.</param>
		/// <param name="playerName">The name of the current player.</param>
		/// <param name="value">The initial score.</param>
		public Score(Difficulty gameplayDifficulty,string playerName, int value)
		{
			GameplayDifficulty = gameplayDifficulty;
			PlayerName = playerName;
			Value = value;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="SnakeGame.Scoring.Score"/> class.
		/// </summary>
		/// <param name="gameplayDifficulty">The difficulty being played on.</param>
		/// <param name="value">The initial score.</param>
		public Score(Difficulty gameplayDifficulty, int value) : this(gameplayDifficulty, "", value)
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="SnakeGame.Scoring.Score"/> class.
		/// </summary>
		/// <param name="gameplayDifficulty">The difficulty being played on.</param>
		/// <param name="playerName">The name of the current player.</param>
		public Score(Difficulty gameplayDifficulty, string playerName) : this(gameplayDifficulty, playerName,0)
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="SnakeGame.Scoring.Score"/> class.
		/// </summary>
		/// <param name="gameplayDifficulty">The difficulty being played on.</param>
		public Score(Difficulty gameplayDifficulty) : this(gameplayDifficulty, 0)
		{
		}
		/// <summary>
		/// Gets the difficulty relevant to this score.
		/// </summary>
		public Difficulty GameplayDifficulty
		{
			get;
			private set;
		}
		/// <summary>
		/// Gets or sets the name of the player this score belongs to.
		/// </summary>
		public string PlayerName
		{
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the actual score value.
		/// </summary>
		public int Value
		{
			get;
			set;
		}
	}
}

