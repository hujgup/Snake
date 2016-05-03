using System;

namespace SnakeGame.UserInterface
{
	/// <summary>
	/// Allows the setting of a boolean value based on the firing of an event.
	/// </summary>
	public abstract class BooleanFlag : Flag<bool>
	{
		private EventHandler _boolSet;
		/// <summary>
		/// Initializes a new instance of the <see cref="SnakeGame.UserInterface.BooleanFlag"/> class.
		/// </summary>
		/// <param name="setter">The delegate that sets the value of this flag.</param>
		public BooleanFlag(Func<bool> setter) : base(setter)
		{
			_boolSet = delegate(object sender, EventArgs e)
			{
				if (Value && StateSetTrue != null)
				{
					StateSetTrue(this, e);
				}
				else if (!Value && StateSetFalse != null)
				{
					StateSetFalse(this, e);
				}
			};
			RenderEvents.LogicTick += _boolSet;
		}
		/// <summary>
		/// Occurs when the Set value changes to true.
		/// </summary>
		public event EventHandler StateSetTrue;
		/// <summary>
		/// Occurs when the Set value changes to false.
		/// </summary>
		public event EventHandler StateSetFalse;
		public override void Dispose()
		{
			RenderEvents.LogicTick -= _boolSet;
		}
	}
}

