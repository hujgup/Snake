using System;
using System.Collections.Generic;
using SnakeGame.GridSystem;

namespace SnakeGame
{
	/// <summary>
	/// Represents an edible fruit.
	/// </summary>
	public class Fruit : IGridSingletonOccupier
	{
		private Grid.Cell _location;
		/// <summary>
		/// Initializes a new instance of the <see cref="SnakeGame.Fruit"/> class.
		/// </summary>
		/// <param name="playArea">The play area this fruit belongs to.</param>
		/// <param name="location">The location to spawn the fruit at.</param> 
		/// <param name="value">The amount to grow an eater of this fruit by when it is eaten.</param>
		public Fruit(Grid playArea, Grid.Cell location, int value)
		{
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
		/// Initializes a new instance of the <see cref="SnakeGame.Fruit"/> class.
		/// </summary>
		/// <param name="playArea">The play area this fruit belongs to.</param>
		/// <param name="exclude">The set of points this fruit should not initially spawn at.</param>
		/// <param name="value">The amount to grow an eater of this fruit by when it is eaten.</param>
		public Fruit(Grid playArea, List<Grid.Cell> exclude, int value)
		{
			PlayArea = playArea;
			Value = value;
			RandomizeLocation(exclude);
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="SnakeGame.Fruit"/> class.
		/// </summary>
		/// <param name="playArea">The play area this fruit belongs to.</param>
		/// <param name="exclude">The set of points this fruit should not initially spawn at.</param>
		/// <param name="value">The amount to grow an eater of this fruit by when it is eaten.</param>
		public Fruit(Grid playArea, IEnumerable<Grid.Cell> exclude, int value)
		{
			PlayArea = playArea;
			Value = value;
			RandomizeLocation(exclude);
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="SnakeGame.Fruit"/> class.
		/// </summary>
		/// <param name="playArea">The play area this fruit belongs to.</param>
		/// <param name="value">The amount to grow an eater of this fruit by when it is eaten.</param>
		public Fruit(Grid playArea, int value)
		{
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
		public Grid.Cell OccupiedCell
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
		/// Randomizes the location of this fruit.
		/// </summary>
		/// <param name="exclude">The set of points that this fruit should not appear at.</param>
		public void RandomizeLocation(List<Grid.Cell> exclude)
		{
			_location = PlayArea.GetRandomCell(exclude);
		}
		/// <summary>
		/// Randomizes the location of this fruit.
		/// </summary>
		/// <param name="exclude">The set of points that this fruit should not appear at.</param>
		public void RandomizeLocation(IEnumerable<Grid.Cell> exclude)
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
		/// Causes this fruit to be eaten.
		/// </summary>
		/// <param name="eater">The entity eating this fruit.</param>
		public void Eat(Snake eater)
		{
			// TODO
		}
	}
}

