using System;

namespace SnakeGame
{
	public abstract class GameController : IDisposable
	{
		protected void OnDone(GameState targetState)
		{
			if (Done != null)
			{
				Done(this,new StateChangeEventArgs(targetState));
			}
		}
		public event EventHandler<StateChangeEventArgs> Done;
		public abstract void Dispose();
	}
}

