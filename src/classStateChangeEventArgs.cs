using System;

namespace SnakeGame
{
	public class StateChangeEventArgs : EventArgs
	{
		public StateChangeEventArgs(GameState state) : base()
		{
			TargetState = state;
		}
		public GameState TargetState
		{
			get;
			private set;
		}
	}
}

