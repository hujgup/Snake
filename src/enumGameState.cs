using System;

namespace SnakeGame
{
	/// <summary>
	/// Describes the high-level state of the game.
	/// </summary>
	public enum GameState
	{
		/// <summary>
		/// Represents the game being on the main menu screen.
		/// </summary>
		MainMenu,
		/// <summary>
		/// Represents the game being on the setup menu screen.
		/// </summary>
		SetupMenu,
		/// <summary>
		/// Represents the game being on the gameplay screen.
		/// </summary>
		Gameplay,
		/// <summary>
		/// Represents the game being on the game over/score input screen.
		/// </summary>
		ScoreInput,
		/// <summary>
		/// Represents the game being on the highscore view screen for the easy difficulty.
		/// </summary>
		ScoreViewEasy,
		/// <summary>
		/// Represents the game being on the highscore view screen for the medium difficulty.
		/// </summary>
		ScoreViewMedium,
		/// <summary>
		/// Represents the game being on the highscore view screen for the hard difficulty.
		/// </summary>
		ScoreViewHard
	}
}

