using System;
using System.Collections.Generic;

namespace SnakeGame.Scoring
{
	/// <summary>
	/// Represents a set of scores.
	/// </summary>
	public class ScoreCollection
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SnakeGame.Scoring.ScoreCollection"/> class.
		/// </summary>
		/// <param name="scores">The current scores.</param>
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
		/// <summary>
		/// Initializes a new instance of the <see cref="SnakeGame.Scoring.ScoreCollection"/> class.
		/// </summary>
		public ScoreCollection() : this(new List<Score>())
		{
		}
		/// <summary>
		/// Gets every single score this set contains.
		/// </summary>
		public List<Score> AllScores
		{
			get;
			private set;
		}
		/// <summary>
		/// Gets all the scores contained in this set that were gained on Easy difficulty.
		/// </summary>
		public List<Score> EasyScores
		{
			get;
			private set;
		}
		/// <summary>
		/// Gets all the scores contained in this set that were gained on Medium difficulty.
		/// </summary>
		public List<Score> MediumScores
		{
			get;
			private set;
		}
		/// <summary>
		/// Gets all the scores contained in this set that were gained on Hard difficulty.
		/// </summary>
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
		/// <summary>
		/// Adds a new score to the set.
		/// </summary>
		/// <param name="s">The score to add.</param>
		public void Add(Score s)
		{
			AllScores.Add(s);
			FilterScores(s.GameplayDifficulty).Add(s);
		}
		/// <summary>
		/// Returns a set of scores meeting the specified criteria.
		/// </summary>
		/// <param name="playerName">Only get this player's scores.</param>
		/// <param name="difficulty">Only get scores for this difficulty level.</param>
		/// <param name="greaterThanOrEqualTo">Only get scores whose value is &gt;= this number.</param>
		public List<Score> FilterScores(string playerName, Difficulty difficulty, int greaterThanOrEqualTo)
		{
			return FilterActual(FilterScores(difficulty), playerName, greaterThanOrEqualTo);
		}
		/// <summary>
		/// Returns a set of scores meeting the specified criteria.
		/// </summary>
		/// <param name="playerName">Only get this player's scores.</param>
		/// <param name="difficulty">Only get scores for this difficulty level.</param>
		public List<Score> FilterScores(string playerName, Difficulty difficulty)
		{
			return FilterScores(playerName, difficulty, 0);
		}
		/// <summary>
		/// Returns a set of scores meeting the specified criteria.
		/// </summary>
		/// <param name="difficulty">Only get scores for this difficulty level.</param>
		/// <param name="greaterThanOrEqualTo">Only get scores whose value is &gt;= this number.</param>
		public List<Score> FilterScores(Difficulty difficulty, int greaterThanOrEqualTo)
		{
			return FilterScores(null, difficulty, greaterThanOrEqualTo);
		}
		/// <summary>
		/// Returns a set of scores meeting the specified criteria.
		/// </summary>
		/// <param name="playerName">Only get this player's scores.</param>
		/// <param name="greaterThanOrEqualTo">Only get scores whose value is &gt;= this number.</param>
		public List<Score> FilterScores(string playerName, int greaterThanOrEqualTo)
		{
			return FilterActual(AllScores, playerName, greaterThanOrEqualTo);
		}
		/// <summary>
		/// Returns a set of scores meeting the specified criteria.
		/// </summary>
		/// <param name="playerName">Only get this player's scores.</param>
		public List<Score> FilterScores(string playerName)
		{
			return FilterScores(playerName, 0);
		}
		/// <summary>
		/// Returns a set of scores meeting the specified criteria.
		/// </summary>
		/// <param name="difficulty">Only get scores for this difficulty level.</param>
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
		/// <summary>
		/// Returns a set of scores meeting the specified criteria.
		/// </summary>
		/// <param name="greaterThanOrEqualTo">Only get scores whose value is &gt;= this number.</param>
		public List<Score> FilterScores(int greaterThanOrEqualTo)
		{
			return FilterScores(null, greaterThanOrEqualTo);
		}
	}
}

