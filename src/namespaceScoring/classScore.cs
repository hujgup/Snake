using System;

namespace SnakeGame.Scoring
{
	public class Score
	{
		public Score(Difficulty gameplayDifficulty,string playerName, int value)
		{
			GameplayDifficulty = gameplayDifficulty;
			PlayerName = playerName;
			Value = value;
		}
		public Score(Difficulty gameplayDifficulty, int value) : this(gameplayDifficulty, "", value)
		{
		}
		public Score(Difficulty gameplayDifficulty, string playerName) : this(gameplayDifficulty, playerName,0)
		{
		}
		public Score(Difficulty gameplayDifficulty) : this(gameplayDifficulty, 0)
		{
		}
		public Difficulty GameplayDifficulty
		{
			get;
			private set;
		}
		public string PlayerName
		{
			get;
			set;
		}
		public int Value
		{
			get;
			set;
		}
	}
}

