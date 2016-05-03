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
			StateChange += _boolSet;
		}
		/// <summary>
		/// Occurs when the Set value changes to true.
		/// </summary>
		public event EventHandler StateSetTrue;
		/// <summary>
		/// Occurs when the Set value changes to false.
		/// </summary>
		public event EventHandler StateSetFalse;
		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="SnakeGame.UserInterface.BooleanFlag"/>. The
		/// <see cref="Dispose"/> method leaves the <see cref="SnakeGame.UserInterface.BooleanFlag"/> in an unusable state.
		/// After calling <see cref="Dispose"/>, you must release all references to the
		/// <see cref="SnakeGame.UserInterface.BooleanFlag"/> so the garbage collector can reclaim the memory that the
		/// <see cref="SnakeGame.UserInterface.BooleanFlag"/> was occupying.</remarks>
		public override void Dispose()
		{
			StateChange -= _boolSet;
		}
	}
}

