using System;
using NUnit.Framework;
using Mirror = MbUnit.Framework.Mirror;

namespace SnakeGame.UserInterface
{
	/// <summary>
	/// Provides unit tests for the BooleanControlsFlag class.
	/// </summary>
	[TestFixture()]
	public class TestBooleanControlsFlag
	{
		/// <summary>
		/// Tests the BooleanControlsFlag class.
		/// </summary>
		[Test()]
		public void TestBCF()
		{
			BooleanControlsFlag f = new BooleanControlsFlag(delegate()
			{
				return true;
			});
			Assert.IsFalse(f.Value, "The initial value of any BooleanControlsFlag must be false.");
			EventHandler h = (EventHandler)Mirror.ForObject(f)["Handler"].Value;
			Assert.AreNotEqual(-1, Array.IndexOf(RenderEvents.LogicInvokers(), h), "BooleanControlsFlag objects must, on creation, subscribe to the RenderEvents.LogicTick event.");
			bool scCalled = false;
			f.StateChange += (object sender, EventArgs e) =>
			{
				scCalled = true;
			};
			bool sctCalled = false;
			f.StateSetTrue += (object sender, EventArgs e) =>
			{
				sctCalled = true;
			};
			bool scfCalled = false;
			f.StateSetFalse += (object sender, EventArgs e) =>
			{
				scfCalled = true;
			};
			RenderEvents.OnLogicTick();
			Assert.IsTrue(f.Value, "When the subscribed event is fired, the Value property must be set to the value returned by the delegate passed to the constructor (constant set check 1).");
			Assert.IsTrue(scCalled, "When Value changes, the StateChange event must fire.");
			Assert.IsTrue(sctCalled, "When Value changes from false to true, the StateSetTrue event must fire.");
			Assert.IsFalse(scfCalled, "When Value changes from false to true, the StateSetFalse event must not fire.");
			f.Dispose();
			Assert.AreEqual(-1, Array.IndexOf(RenderEvents.LogicInvokers(), h), "BooleanControlsFlag objects must, on disposal, unsubscribe from the RenderEvents.LogicTick event.");
			int i = 0;
			f = new BooleanControlsFlag(delegate()
			{
				return ++i == 1;
			});
			RenderEvents.OnLogicTick();
			scCalled = false;
			f.StateChange += (object sender, EventArgs e) =>
			{
				scCalled = true;
			};
			sctCalled = false;
			f.StateSetTrue += (object sender, EventArgs e) =>
			{
				sctCalled = true;
			};
			scfCalled = false;
			f.StateSetFalse += (object sender, EventArgs e) =>
			{
				scfCalled = true;
			};
			RenderEvents.OnLogicTick();
			Assert.IsFalse(f.Value, "When the subscribed event is fired, the Value property must be set to the value returned by the delegate passed to the constructor (variable set check).");
			Assert.IsTrue(scCalled, "When Value changes, the StateChange event must fire.");
			Assert.IsFalse(sctCalled, "When Value changes from true to false, the StateSetTrue event must not fire.");
			Assert.IsTrue(scfCalled, "When Value changes from true to false, the StateSetFalse event must fire.");
			f.Dispose();
			f = new BooleanControlsFlag(delegate()
			{
				return false;
			});
			scCalled = false;
			f.StateChange += (object sender, EventArgs e) =>
			{
				scCalled = true;
			};
			sctCalled = false;
			f.StateSetTrue += (object sender, EventArgs e) =>
			{
				sctCalled = true;
			};
			scfCalled = false;
			f.StateSetFalse += (object sender, EventArgs e) =>
			{
				scfCalled = true;
			};
			RenderEvents.OnLogicTick();
			Assert.IsFalse(f.Value, "When the subscribed event is fired, the Value property must be set to the value returned by the delegate passed to the constructor (constant set check 2).");
			Assert.IsFalse(scCalled, "When Value doesn't change, the StateChange event must not fire.");
			Assert.IsFalse(sctCalled, "When Value doesn't change, the StateSetTrue event must not fire.");
			Assert.IsFalse(scfCalled, "When Value doesn't change, the StateSetFalse event must not fire.");
		}
	}
}

