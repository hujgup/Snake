using System;

namespace SnakeGame.GridSystem
{
	/// <summary>
	/// Represents an entity that can occupy a single cell.
	/// </summary>
	public interface IGridSingletonOccupier
	{
		/// <summary>
		/// Gets the cell occupied by this entity.
		/// </summary>
		Cell OccupiedCell
		{
			get;
		}
	}
}

