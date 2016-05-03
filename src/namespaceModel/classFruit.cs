using System;
using System.Collections.Generic;
using SnakeGame.GridSystem;

namespace SnakeGame.Model
{
	/// <summary>
	/// Represents an edible fruit.
	/// </summary>
	public class Fruit : IGridSingletonOccupier
	{
		private Cell _location;
		private void CtorCheckGrid(Grid playArea)
		{
			if (playArea == null)
			{
				throw new ArgumentNullException("playArea", "playArea cannot be null.");
			}
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="SnakeGame.Model.Fruit"/> class.
		/// </summary>
		/// <param name="playArea">The play area this fruit belongs to.</param>
		/// <param name="location">The location to spawn the fruit at.</param> 
		/// <param name="value">The amount to grow an eater of this fruit by when it is eaten.</param>
		public Fruit(Grid playArea, Cell location, int value)
		{
			CtorCheckGrid(playArea);
			if (!location.IsValid)
			{
				throw new ArgumentException("Location must be valid.", "location");
			}
			if (location.Owner != playArea)
			{
				throw new ArgumentException("Location must be owned by the specified Grid.", "location");
			}
			if (!playArea.IsDefined(location))
			{
				throw new ArgumentOutOfRangeException("Location must be within the specified Grid.", "location");
			}
			PlayArea = playArea;
			Value = value;
			OccupiedCell = location;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="SnakeGame.Model.Fruit"/> class.
		/// </summary>
		/// <param name="playArea">The play area this fruit belongs to.</param>
		/// <param name="exclude">The set of points this fruit should not initially spawn at.</param>
		/// <param name="value">The amount to grow an eater of this fruit by when it is eaten.</param>
		public Fruit(Grid playArea, List<Cell> exclude, int value)
		{
			CtorCheckGrid(playArea);
			PlayArea = playArea;
			Value = value;
			RandomizeLocation(exclude);
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="SnakeGame.Model.Fruit"/> class.
		/// </summary>
		/// <param name="playArea">The play area this fruit belongs to.</param>
		/// <param name="exclude">The set of points this fruit should not initially spawn at.</param>
		/// <param name="value">The amount to grow an eater of this fruit by when it is eaten.</param>
		public Fruit(Grid playArea, IEnumerable<Cell> exclude, int value)
		{
			CtorCheckGrid(playArea);
			PlayArea = playArea;
			Value = value;
			RandomizeLocation(exclude);
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="SnakeGame.Model.Fruit"/> class.
		/// </summary>
		/// <param name="playArea">The play area this fruit belongs to.</param>
		/// <param name="value">The amount to grow an eater of this fruit by when it is eaten.</param>
		public Fruit(Grid playArea, int value)
		{
			CtorCheckGrid(playArea);
			PlayArea = playArea;
			Value = value;
			RandomizeLocation();
		}
		/// <summary>
		/// Gets the area that this fruit may appear in.
		/// </summary>
		public Grid PlayArea
		{
			get;
			private set;
		}
		/// <summary>
		/// Gets the cell occupied by this fruit.
		/// </summary>
		public Cell OccupiedCell
		{
			get
			{
				return _location;
			}
			set
			{
				if (!value.IsValid)
				{
					throw new ArgumentException("Cannot set OccupiedCell to an invalid cell.","value");
				}
				if (PlayArea != value.Owner)
				{
					throw new ArgumentException("Cannot set OccupiedCell to a cell with a different owner than PlayArea.","value");
				}
				if (!PlayArea.IsDefined(value))
				{
					throw new ArgumentOutOfRangeException("value","Location must be within PlayArea.");
				}
				_location = value;
			}
		}
		/// <summary>
		/// Gets or sets the growth amount when this fruit is eaten.
		/// </summary>
		public int Value
		{
			get;
			set;
		}
		/// <summary>
		/// Fires when this fruit is eaten.
		/// </summary>
		public event EventHandler Eaten;
		/// <summary>
		/// Randomizes the location of this fruit.
		/// </summary>
		/// <param name="exclude">The set of points that this fruit should not appear at.</param>
		public void RandomizeLocation(List<Cell> exclude)
		{
			_location = PlayArea.GetRandomCell(exclude);
		}
		/// <summary>
		/// Randomizes the location of this fruit.
		/// </summary>
		/// <param name="exclude">The set of points that this fruit should not appear at.</param>
		public void RandomizeLocation(IEnumerable<Cell> exclude)
		{
			_location = PlayArea.GetRandomCell(exclude);
		}
		/// <summary>
		/// Randomizes the location of this fruit.
		/// </summary>
		public void RandomizeLocation()
		{
			_location = PlayArea.GetRandomCell();
		}
		/// <summary>
		/// Causes the specified snake to eat this fruit.
		/// </summary>
		/// <param name="eater">The snake eating this fruit.</param>
		public void Eat(Snake eater)
		{
			if (Eaten != null)
			{
				Eaten(this, EventArgs.Empty);
			}
		}
	}
}

