using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace SnakeGame
{
	/// <summary>
	/// Provides unit tests for the Difficulty enumeration.
	/// </summary>
	[TestFixture()]
	public class TestDifficulty
	{
		/// <summary>
		/// Tests the Increment extension method.
		/// </summary>
		[Test()]
		public void TestIncrement()
		{
			Assert.AreEqual(Difficulty.Medium, Difficulty.Easy.Increment(), "Calling Increment on Easy should return Medium.");
			Assert.AreEqual(Difficulty.Hard, Difficulty.Medium.Increment(), "Calling Increment on Medium should return Hard.");
			Assert.AreEqual(Difficulty.Hard, Difficulty.Hard.Increment(), "Calling Increment on Hard should return Hard (do not loop around).");
		}
		/// <summary>
		/// Tests the Decrement extension method.
		/// </summary>
		[Test()]
		public void TestDecrement()
		{
			Assert.AreEqual(Difficulty.Medium, Difficulty.Hard.Decrement(), "Calling Decrement on Hard should return Medium.");
			Assert.AreEqual(Difficulty.Easy, Difficulty.Medium.Decrement(), "Calling Decrement on Medium should return Easy.");
			Assert.AreEqual(Difficulty.Easy, Difficulty.Easy.Decrement(), "Calling Decrement on Easy should return Easy (do not loop around).");
		}
		/// <summary>
		/// Tests the GetEnumerator extension method.
		/// </summary>
		[Test()]
		public void TestEnumerator()
		{
			IEnumerator<Difficulty> enumerator = Difficulty.Easy.GetEnumerator();
			Assert.Throws<Exception>(delegate()
			{
				Difficulty d = enumerator.Current;
			}, "Calling Current on an enumerator that has not been moved should throw an exception.");
			Assert.IsTrue(enumerator.MoveNext(), "While there is another difficulty, MoveNext should return true (test 1).");
			Assert.AreEqual(Difficulty.Easy, enumerator.Current, "Calling Current after moving once should return Easy.");
			Assert.IsTrue(enumerator.MoveNext(), "While there is another difficulty, MoveNext should return true (test 2).");
			Assert.AreEqual(Difficulty.Medium, enumerator.Current, "Calling Current after moving once should return Medium.");
			Assert.IsTrue(enumerator.MoveNext(), "While there is another difficulty, MoveNext should return true (test 2).");
			Assert.AreEqual(Difficulty.Hard, enumerator.Current, "Calling Current after moving once should return Hard.");
			Assert.IsFalse(enumerator.MoveNext(), "When all difficulties have been exhausted, MoveNext should return false.");
			Assert.Throws<Exception>(delegate()
			{
				Difficulty d = enumerator.Current;
			}, "Caling Current on an enumerator that has exhausted all difficulties should throw an exception.");
			enumerator.Reset();
			Assert.IsTrue(enumerator.MoveNext(), "Resetting an enumerator should cause MoveNext to once again return true.");
			Assert.AreEqual(Difficulty.Easy, enumerator.Current, "Calling Current after resetting and moving once should return Easy.");
			enumerator.Dispose();

			enumerator = Difficulty.Easy.GetEnumerator();
			enumerator.MoveNext();
			IEnumerator<Difficulty> e2 = Difficulty.Medium.GetEnumerator();
			e2.MoveNext();
			Assert.AreEqual(enumerator.Current, e2.Current, "No matter what enumeration value GetEnumerator is called on, the first element should always be Easy (test 1).");
			e2.Dispose();
			e2 = Difficulty.Hard.GetEnumerator();
			e2.MoveNext();
			Assert.AreEqual(enumerator.Current, e2.Current, "No matter what enumeration value GetEnumerator is called on, the first element should always be Easy (test 2).");
			e2.Dispose();
			enumerator.Dispose();
		}
	}
}

