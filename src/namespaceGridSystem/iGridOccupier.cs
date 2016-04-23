using System;
using System.Collections.Generic;

namespace SnakeGame.GridSystem
{
	/// <summary>
	/// Represents an entity that can occupy multiple cells.
	/// </summary>
	public interface IGridOccupier
	{
		/// <summary>
		/// Gets the cells that this entity occupies.
		/// </summary>
		IEnumerable<Cell> OccupiedCells
		{
			get;
		}
	}
}

