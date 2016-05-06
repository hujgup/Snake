using System;
using System.Collections.Generic;

namespace SnakeGame
{
	/// <summary>
	/// Represents gameplay difficulty.
	/// </summary>
	public enum Difficulty
	{
		/// <summary>
		/// The easiest difficulty.
		/// </summary>
		Easy = 5,
		/// <summary>
		/// The average difficulty.
		/// </summary>
		Medium = 13,
		/// <summary>
		/// The hardest difficulty.
		/// </summary>
		Hard = 19,
	}
	/// <summary>
	/// Provides extension methods for the Difficulty enumeration.
	/// </summary>
	public static class DifficultyExtensions
	{
		private class DifficultyEnumerator : IEnumerator<Difficulty>
		{
			private bool _unsetBefore;
			private bool _unsetAfter;
			private Difficulty _current;
			public DifficultyEnumerator()
			{
				_current = Difficulty.Easy;
				Reset();
			}
			public Difficulty Current
			{
				get
				{
					if (_unsetBefore || _unsetAfter)
					{
						throw new Exception("Current is unset.");
					}
					return _current;
				}
			}
			object System.Collections.IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}
			public bool MoveNext()
			{
				bool res = _unsetBefore;
				if (res)
				{
					_unsetBefore = false;
					_current = Difficulty.Easy;
				}
				else
				{
					res = _current != Difficulty.Hard;
					if (res)
					{
						res = !_unsetAfter;
						if (res)
						{
							res = _current != Difficulty.Hard;
							if (res)
							{
								_current = _current.Increment();
							}
						}
					}
					else
					{
						_unsetAfter = true;
					}
				}
				return res;
			}
			public void Reset()
			{
				_unsetBefore = true;
				_unsetAfter = false;
			}
			public void Dispose()
			{
			}
		}
		/// <summary>
		/// Gets the next highest difficulty level.
		/// </summary>
		/// <param name="d">The current difficulty level.</param>
		public static Difficulty Increment(this Difficulty d)
		{
			Difficulty res = d;
			switch (d)
			{
				case Difficulty.Easy:
					res = Difficulty.Medium;
					break;
				case Difficulty.Medium:
					res = Difficulty.Hard;
					break;
				default:
					break;
			}
			return res;
		}
		/// <summary>
		/// Gets the next lowest difficulty level.
		/// </summary>
		/// <param name="d">The current difficulty level.</param>
		public static Difficulty Decrement(this Difficulty d)
		{
			Difficulty res = d;
			switch (d)
			{
				case Difficulty.Hard:
					res = Difficulty.Medium;
					break;
				case Difficulty.Medium:
					res = Difficulty.Easy;
					break;
				default:
					break;
			}
			return res;
		}
		/// <summary>
		/// Gets an enumerator over all Difficulties.
		/// </summary>
		/// <param name="d">Placeholder.</param>
		public static IEnumerator<Difficulty> GetEnumerator(this Difficulty d)
		{
			return new DifficultyEnumerator();
		}
	}
}

