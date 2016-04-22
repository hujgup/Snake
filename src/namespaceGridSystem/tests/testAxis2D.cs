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
		private void SurpressUnusedWarning(object o)
		{
		}
		/// <summary>
		/// Checks that all the enumerations exist.
		/// </summary>
		[Test()]
		public void EnumsExist()
		{
			Axis2D a;
			Assert.DoesNotThrow(delegate()
			{
				a = Axis2D.X;
				a = Axis2D.Y;
				SurpressUnusedWarning(a);
			}, "Axis2D.X and Axis2D.Y must both exist.");
		}
	}
}

