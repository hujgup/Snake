using System;

namespace SnakeGame
{
	/// <summary>
	/// Represents the game as a whole.
	/// </summary>
	public class MetaController : IDisposable
	{
		private GameController _controller;
		/// <summary>
		/// Initializes a new instance of the <see cref="SnakeGame.MetaController"/> class.
		/// </summary>
		public MetaController()
		{
			_controller = null;
		}
		/// <summary>
		/// Gets or sets the gameplay difficulty.
		/// </summary>
		public Difficulty GameplayDifficulty
		{
			get;
			set;
		}
		/// <summary>
		/// Swaps out the current controller for another one.
		/// </summary>
		/// <param name="controller">The controller to swap to.</param>
		public void SetController(GameController controller)
		{
			if (_controller != null)
			{
				_controller.Dispose();
			}
/*
			controller.Done += (object sender, StateChangeEventArgs e) =>
			{
				_controller.Dispose();
				_controller = null;
				switch (e.TargetState)
				{
					case GameState.ScoreInput:
						SetController(new ScoreInputController(GameplayDifficulty));
						break;
					default:
						throw new ArgumentException("Given TargetState does not have a MetaController handler.","e.TargetState");
				}
			};
*/
			_controller = controller;
		}
		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="SnakeGame.MetaController"/>. The
		/// <see cref="Dispose"/> method leaves the <see cref="SnakeGame.MetaController"/> in an unusable state. After calling
		/// <see cref="Dispose"/>, you must release all references to the <see cref="SnakeGame.MetaController"/> so the
		/// garbage collector can reclaim the memory that the <see cref="SnakeGame.MetaController"/> was occupying.</remarks>
		public void Dispose()
		{
			try
			{
				if (_controller != null)
				{
					_controller.Dispose();
				}
			}
			catch (ObjectDisposedException) // Due to threading, it's possible that something else disposed _controller before this was called
			{
			}
		}
	}
}

