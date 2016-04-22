using System;
using NUnit.Framework;

namespace SnakeGame.GridSystem
{
	/// <summary>
	/// Provides unit tests for the Axis2D enumeration.
	/// </summary>
	[TestFixture()]
	public class TestAxis2D
	{
		/// <summary>
		/// Checks that all the enumerations exist.
		/// </summary>
		[Test()]
		public void EnumsExist()
		{
			Assert.DoesNotThrow(delegate() {
				Axis2D a = Axis2D.X;
				a = Axis2D.Y;
			}, "Axis2D.X and Axis2D.Y must both exist.");
		}
	}
}

