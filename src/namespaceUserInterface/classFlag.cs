using System;

namespace SnakeGame.UserInterface
{
	/// <summary>
	/// Allows the setting of a boolean value based on the firing of an event.
	/// </summary>
	public abstract class Flag
	{
		private EventHandler _handler;
		/// <summary>
		/// Initializes a new instance of the <see cref="SnakeGame.UserInterface.Flag"/> class.
		/// </summary>
		/// <param name="setter">The delegate that sets the value of this flag.</param>
		public Flag(Func<bool> setter)
		{
			Set = false;
			_handler = delegate(object sender, EventArgs e)
			{
				bool previous = Set;
				Set = setter.Invoke();
				if (previous != Set)
				{
					if (Set && StateSetTrue != null)
					{
						StateSetTrue(this, e);
					}
					else if (!Set && StateSetFalse != null)
					{
						StateSetFalse(this, e);
					}
					if (StateChange != null)
					{
						StateChange(this, e);
					}
				}
			};
		}
		/// <summary>
		/// The EventHandler that should be used when subscribing to a given event.
		/// </summary>
		protected EventHandler Handler
		{
			get
			{
				return _handler;
			}
		}
		/// <summary>
		/// Gets a value indicating whether this <see cref="SnakeGame.UserInterface.Flag"/> is set.
		/// </summary>
		public bool Set
		{
			get;
			private set;
		}
		/// <summary>
		/// Occurs when the Set value changes.
		/// </summary>
		public event EventHandler StateChange;
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
		/// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="SnakeGame.UserInterface.Flag"/>. The
		/// <see cref="Dispose"/> method leaves the <see cref="SnakeGame.UserInterface.Flag"/> in an unusable state. After
		/// calling <see cref="Dispose"/>, you must release all references to the <see cref="SnakeGame.UserInterface.Flag"/>
		/// so the garbage collector can reclaim the memory that the <see cref="SnakeGame.UserInterface.Flag"/> was occupying.</remarks>
		public abstract void Dispose();
		/// <summary>
		/// Returns the string value of the Set property.
		/// </summary>
		public override string ToString()
		{
			return Set.ToString();
		}
	}
}

