using System;
using NUnit.Framework;

namespace SnakeGame.GridSystem
{
	/// <summary>
	/// Provides unit tests for the RangeRestriction class.
	/// </summary>
	[TestFixture()]
	public class TestRangeRestriction
	{
		private void TestCorrectVals<T>(T low, T high, bool lowEx, bool highEx) where T : IComparable<T>
		{
			RangeRestriction<T> r = new RangeRestriction<T>(low, high, lowEx, highEx);
			if (low.CompareTo(high) > 0)
			{
				T temp = low;
				low = high;
				high = temp;
			}
			Assert.AreEqual(low, r.LowerBound, "LowerBound must correspond to the lowest passed value out of the 1st and 2nd constructor parameters.");
			Assert.AreEqual(high, r.UpperBound, "Upper must correspond to the lowest passed value out of the 1st and 2nd constructor parameters.");
			Assert.AreEqual(lowEx, r.LowerExclusive, "LowerExclusive must correspond to the 3rd constructor parameter.");
			Assert.AreEqual(highEx, r.UpperExclusive, "UpperExclusive must correspond to the 4rd constructor parameter.");
			if (lowEx)
			{
				Assert.IsFalse(r.InRange(low), "If LowerExclusive is true, the point defined by LowerBound should not itself be in range.");
			}
			else
			{
				Assert.IsTrue(r.InRange(low), "If LowerExclusive is false, the point defined by LowerBound should itself be in range.");
			}
			if (highEx)
			{
				Assert.IsFalse(r.InRange(high), "If UpperExclusive is true, the point defined by UpperBound should not itself be in range.");
			}
			else
			{
				Assert.IsTrue(r.InRange(high), "If UpperExclusive is false, the point defined by UpperBound should itself be in range.");
			}
			RangeRestriction<T> r2 = new RangeRestriction<T>(low, high, lowEx, highEx);
			Assert.AreEqual(r, r2, "RangeRestrictin<T> structural equality must be defined (AssertEquals).");
			Assert.IsTrue(r.Equals(r2), "RangeRestrictin<T> structural equality must be defined (IEquatable Equals).");
			Assert.IsTrue(r == r2, "RangeRestrictin<T> structural equality must be defined (== operator).");
			Assert.IsFalse (r != r2, "RangeRestrictin<T> structural inequality must be defined (!= operator).");
			Assert.IsInstanceOf<IEquatable<RangeRestriction<T>>>(r, "RangeRestriction<T> must implement IEquatable<RangeRestriction<T>>.");
			Assert.AreEqual((r.LowerExclusive ? "(" : "[") + r.LowerBound.ToString() + ", " + r.UpperBound.ToString() + (r.UpperExclusive ? ")" : "]"), r.ToString(), "ToString() must output the correct format.");
		}
		/// <summary>
		/// Tests that RangeRestrictions behave a certain way for any given set of values, and for any IComparable type.
		/// </summary>
		[Test()]
		public void Construction()
		{
			TestCorrectVals<int>(7, 89, false, false);
			TestCorrectVals<int>(7, 89, true, false);
			TestCorrectVals<int>(7, 89, false, true);
			TestCorrectVals<int>(7, 89, true, true);
			TestCorrectVals<int>(84, 3, false, true);
			TestCorrectVals<double>(Math.PI, 5.24, false, false);
			TestCorrectVals<byte>(0, 4, false, true);
		}
		/// <summary>
		/// Checks that certain constructor overloads exist, and that they produce the correct values.
		/// </summary>
		[Test()]
		public void CtorOverloads()
		{
			RangeRestriction<short> r = new RangeRestriction<short>(0,3);
			Assert.AreEqual(0, r.LowerBound, "Two-T overload must set the first parameter as it is set in the main contsructor.");
			Assert.AreEqual(3, r.UpperBound, "Two-T overload must set the second parameter as it is set in the main contsructor.");
			Assert.IsFalse(r.LowerExclusive, "Two-T overload must set LowerExclusive to false.");
			Assert.IsFalse(r.UpperExclusive, "Two-T overload must set UpperExclusive to false.");
		}
	}
}

