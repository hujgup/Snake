using System;

namespace SnakeGame.UserInterface
{
	/// <summary>
	/// Allows the setting of a value based on the firing of an event.
	/// </summary>
	public abstract class Flag<T>
	{
		private EventHandler _handler;
		/// <summary>
		/// Initializes a new instance of the <see cref="SnakeGame.UserInterface.Flag{T}"/> class.
		/// </summary>
		/// <param name="setter">The delegate that sets the value of this flag.</param>
		public Flag(Func<T> setter)
		{
			Value = default(T);
			_handler = delegate(object sender, EventArgs e)
			{
				T previous = Value;
				Value = setter.Invoke();
				if (!previous.Equals(Value))
				{
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
		/// Gets the value set by this Flag's delegate.
		/// </summary>
		public T Value
		{
			get;
			private set;
		}
		/// <summary>
		/// Occurs when the Set value changes.
		/// </summary>
		public event EventHandler StateChange;
		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="SnakeGame.UserInterface.Flag{T}"/>. The
		/// <see cref="Dispose"/> method leaves the <see cref="SnakeGame.UserInterface.Flag{T}"/> in an unusable state. After
		/// calling <see cref="Dispose"/>, you must release all references to the <see cref="SnakeGame.UserInterface.Flag{T}"/>
		/// so the garbage collector can reclaim the memory that the <see cref="SnakeGame.UserInterface.Flag{T}"/> was occupying.</remarks>
		public abstract void Dispose();
		/// <summary>
		/// Returns the string value of the Set property.
		/// </summary>
		public override string ToString()
		{
			return Value.ToString();
		}
	}
}

