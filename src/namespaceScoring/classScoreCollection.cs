using System;
using System.Collections.Generic;

namespace SnakeGame.Scoring
{
	public class ScoreCollection
	{
		public ScoreCollection(List<Score> scores)
		{
			scores.Sort(delegate(Score a, Score b) {
				return b.Value.CompareTo(a.Value);
			});
			AllScores = scores;
			EasyScores = new List<Score>();
			MediumScores = new List<Score>();
			HardScores = new List<Score>();
			foreach (Score s in scores)
			{
				FilterScores(s.GameplayDifficulty).Add(s);
			}
		}
		public List<Score> AllScores
		{
			get;
			private set;
		}
		public List<Score> EasyScores
		{
			get;
			private set;
		}
		public List<Score> MediumScores
		{
			get;
			private set;
		}
		public List<Score> HardScores
		{
			get;
			private set;
		}
		private List<Score> FilterActual(List<Score> toConsider, string playerName, int gteq)
		{
			List<Score> res = new List<Score>();
			bool playerNull = playerName == null;
			foreach (Score s in toConsider)
			{
				if ((playerNull || playerName == s.PlayerName) && s.Value >= gteq)
				{
					res.Add(s);
				}
			}
			return res;
		}
		public void Add(Score s)
		{
			AllScores.Add(s);
			FilterScores(s.GameplayDifficulty).Add(s);
		}
		public List<Score> FilterScores(string playerName, Difficulty difficulty, int greaterThanOrEqualTo)
		{
			return FilterActual(FilterScores(difficulty), playerName, greaterThanOrEqualTo);
		}
		public List<Score> FilterScores(string playerName, Difficulty difficulty)
		{
			return FilterScores(playerName, difficulty, 0);
		}
		public List<Score> FilterScores(Difficulty difficulty, int greaterThanOrEqualTo)
		{
			return FilterScores(null, difficulty, greaterThanOrEqualTo);
		}
		public List<Score> FilterScores(string playerName, int greaterThanOrEqualTo)
		{
			return FilterActual(AllScores, playerName, greaterThanOrEqualTo);
		}
		public List<Score> FilterScores(string playerName)
		{
			return FilterScores(playerName, 0);
		}
		public List<Score> FilterScores(Difficulty difficulty)
		{
			List<Score> res;
			switch (difficulty)
			{
				case Difficulty.Easy:
					res = EasyScores;
					break;
				case Difficulty.Medium:
					res = MediumScores;
					break;
				case Difficulty.Hard:
					res = HardScores;
					break;
				default:
					throw new ArgumentException("Difficulty is undefined","difficulty");
			}
			return res;
		}
		public List<Score> FilterScores(int greaterThanOrEqualTo)
		{
			return FilterScores(null, greaterThanOrEqualTo);
		}
	}
}

