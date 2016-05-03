using System;

namespace SnakeGame
{
	public class MetaController : IDisposable
	{
		private GameController _controller;
		public MetaController()
		{
			_controller = null;
		}
		public Difficulty GameplayDifficulty
		{
			get;
			set;
		}
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

