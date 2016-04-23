using System;

namespace SnakeGame.GridSystem
{
	/// <summary>
	/// Provides methods for enforcing range constraints.
	/// </summary>
	public class RangeRestriction<T> : IEquatable<RangeRestriction<T>> where T : IComparable<T>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SnakeGame.GridSystem.RangeRestriction{T}"/> class.
		/// </summary>
		/// <param name="lowerBound">The lower bound of this range.</param>
		/// <param name="upperBound">The upper bound of this range.</param>
		/// <param name="lowerExclusive">If set to <c>true</c>, the lower bound does not include itself.</param>
		/// <param name="upperExclusive">If set to <c>true</c>, the upper bound does not include itself.</param>
		public RangeRestriction(T lowerBound, T upperBound, bool lowerExclusive, bool upperExclusive)
		{
			if (lowerBound.CompareTo(upperBound) > 0)
			{
				T temp = lowerBound;
				lowerBound = upperBound;
				upperBound = temp;
			}
			LowerBound = lowerBound;
			UpperBound = upperBound;
			LowerExclusive = lowerExclusive;
			UpperExclusive = upperExclusive;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="SnakeGame.GridSystem.RangeRestriction{T}"/> class.
		/// </summary>
		/// <param name="lowerBound">The lower bound of this range.</param>
		/// <param name="upperBound">The upper bound of this range.</param>
		public RangeRestriction(T lowerBound, T upperBound) : this(lowerBound, upperBound, false, false)
		{
		}
		/// <summary>
		/// Gets the lower edge of the range.
		/// </summary>
		public T LowerBound
		{
			get;
			private set;
		}
		/// <summary>
		/// Gets the upper edge of the range.
		/// </summary>
		public T UpperBound
		{
			get;
			private set;
		}
		/// <summary>
		/// Gets a value indicating whether the lower bound of this range should itself not be in the range.
		/// </summary>
		public bool LowerExclusive
		{
			get;
			private set;
		}
		/// <summary>
		/// Gets a value indicating whether the upper bound of this range should itself not be in the range.
		/// </summary>
		public bool UpperExclusive
		{
			get;
			private set;
		}
		/// <summary>
		/// Checks whether a given value is in the range specified by this instance.
		/// </summary>
		/// <returns><c>true</c> if the value was in range, <c>false</c> otherwise.</returns>
		/// <param name="value">The value to check.</param>
		public bool InRange(T value)
		{
			int cmpLower = value.CompareTo(LowerBound);
			bool res = LowerExclusive ? cmpLower > 0 : cmpLower >= 0;
			if (res)
			{
				int cmpUpper = value.CompareTo(UpperBound);
				res = UpperExclusive ? cmpUpper < 0 : cmpUpper <= 0;
			}
			return res;
		}
		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current <see cref="SnakeGame.GridSystem.RangeRestriction{T}"/>.
		/// </summary>
		public override string ToString()
		{
			return	string.Format((LowerExclusive ? "(" : "[") + "{0}, {1}" + (UpperExclusive ? ")" : "]"), LowerBound, UpperBound);
		}
		/// <summary>
		/// Serves as a hash function for a <see cref="SnakeGame.GridSystem.RangeRestriction{T}"/> object.
		/// </summary>
		/// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
		public override int GetHashCode()
		{
			unchecked
			{
				return LowerBound.GetHashCode() + UpperBound.GetHashCode() + LowerExclusive.GetHashCode() + UpperExclusive.GetHashCode();
			}
		}
		/// <summary>
		/// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="SnakeGame.GridSystem.RangeRestriction{T}"/>.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="SnakeGame.GridSystem.RangeRestriction{T}"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
		/// <see cref="SnakeGame.GridSystem.RangeRestriction{T}"/>; otherwise, <c>false</c>.</returns>
		public override bool Equals(object obj)
		{
			return GetType().IsInstanceOfType(obj) ? Equals((RangeRestriction<T>)obj) : false;
		}
		/// <summary>
		/// Determines whether the specified <see cref="SnakeGame.GridSystem.RangeRestriction{T}"/> is equal to the current <see cref="SnakeGame.GridSystem.RangeRestriction{T}"/>.
		/// </summary>
		/// <param name="other">The <see cref="SnakeGame.GridSystem.RangeRestriction{T}"/> to compare with the current <see cref="SnakeGame.GridSystem.RangeRestriction{T}"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="SnakeGame.GridSystem.RangeRestriction{T}"/> is equal to the current
		/// <see cref="SnakeGame.GridSystem.RangeRestriction{T}"/>; otherwise, <c>false</c>.</returns>
		public bool Equals(RangeRestriction<T> other)
		{
			return LowerBound.CompareTo(other.LowerBound) == 0 && UpperBound.CompareTo(other.UpperBound) == 0 && LowerExclusive == other.LowerExclusive && UpperExclusive == other.UpperExclusive;
		}
		/// <param name="a">First arg.</param>
		/// <param name="b">Second arg.</param>
		public static bool operator ==(RangeRestriction<T> a, RangeRestriction<T> b)
		{
			return a.Equals(b);
		}
		/// <param name="a">First arg.</param>
		/// <param name="b">Second arg.</param>
		public static bool operator !=(RangeRestriction<T> a, RangeRestriction<T> b)
		{
			return !(a == b);
		}
	}
}

