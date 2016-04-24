using System;
using NUnit.Framework;
using Mirror = MbUnit.Framework.Mirror;

namespace SnakeGame.UserInterface
{
	/// <summary>
	/// Provides unit tests for the ControlsFlag{T} class.
	/// </summary>
	[TestFixture()]
	public class TestControlsFlag
	{
		/// <summary>
		/// Tests the ControlsFlag{T} class.
		/// </summary>
		[Test()]
		public void TestCF()
		{
			ControlsFlag<int> f = new ControlsFlag<int>(delegate()
			{
				return 4;
			});
			Assert.AreEqual(default(int), f.Value, "The initial value of any ControlsFlag<T> must be the same as default(T).");
			EventHandler h = (EventHandler)Mirror.ForObject(f)["Handler"].Value;
			Assert.AreNotEqual(-1, Array.IndexOf(RenderEvents.LogicInvokers(), h), "ControlsFlag<T> objects must, on creation, subscribe to the RenderEvents.LogicTick event.");
			bool scCalled = false;
			f.StateChange += (object sender, EventArgs e) =>
			{
				scCalled = true;
			};
			RenderEvents.OnLogicTick();
			Assert.AreEqual(4, f.Value, "When the subscribed event is fired, the Value property must be set to the value returned by the delegate passed to the constructor (constant set check).");
			Assert.IsTrue(scCalled, "When Value changes, the StateChange event must fire.");
			f.Dispose();
			Assert.AreEqual(-1, Array.IndexOf(RenderEvents.LogicInvokers(), h), "ControlsFlag<int> objects must, on disposal, unsubscribe from the RenderEvents.LogicTick event.");
			double i = 0d;
			ControlsFlag<double> f2 = new ControlsFlag<double>(delegate()
			{
				double res = i;
				i += 0.25;
				return res;
			});
			RenderEvents.OnLogicTick();
			scCalled = false;
			f2.StateChange += (object sender, EventArgs e) =>
			{
				scCalled = true;
			};
			RenderEvents.OnLogicTick();
			Assert.AreEqual(0.25, f2.Value, "When the subscribed event is fired, the Value property must be set to the value returned by the delegate passed to the constructor (variable set check).");
			Assert.IsTrue(scCalled, "When Value changes, the StateChange event must fire.");
			f2.Dispose();
		}
	}
}

