using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SnakeGame.GridSystem;

namespace SnakeGame
{
	/// <summary>
	/// Represents a Snake.
	/// </summary>
	public class Snake : IGridOccupier, IEnumerable<MovementNode>
	{
		private const string _LENGTH_ERROR = "Length must be a positive number.";
		private LinkedList<MovementNode> _history;
		/// <summary>
		/// Initializes a new instance of the <see cref="SnakeGame.Snake"/> class.
		/// </summary>
		/// <param name="playArea">A grid defining the area of play.</param>
		/// <param name="startingCell">The cell to start the game at.</param>
		/// <param name="initialLength">The initial length of the snake's tail.</param>
		/// <param name="tailDirection">The direction that the snake's initial motion should be towards.</param>
		public Snake(Grid playArea, Grid.Cell startingCell, int initialLength, Direction initialDirection)
		{
			if (initialLength <= 0)
			{
				throw new ArgumentOutOfRangeException("initialLength", _LENGTH_ERROR);
			}
			PlayArea = playArea;
			MovementDirection = initialDirection;
			_history = new LinkedList<MovementNode>();
			_history.AddFirst(new MovementNode(startingCell, initialDirection));
			Length = initialLength;
		}
		/// <summary>
		/// Gets the area of play.
		/// </summary>
		public Grid PlayArea
		{
			get;
			private set;
		}
		/// <summary>
		/// Gets or sets the current direction of motion.
		/// </summary>
		public Direction MovementDirection
		{
			get;
			set;
		}
		/// <summary>
		/// Gets the head of the snake.
		/// </summary>
		public Grid.Cell Head
		{
			get
			{
				return _history.First.Value.Cell;
			}
		}
		/// <summary>
		/// Gets or sets the snake's tail length.
		/// </summary>
		public int Length
		{
			get
			{
				return _history.Count;
			}
			set
			{
				if (value != Length)
				{
					if (value < Length)
					{
						if (value < 0)
						{
							throw new ArgumentOutOfRangeException("value", _LENGTH_ERROR);
						}
						int difference = Length - value;
						for (int i = 0; i < difference; i++)
						{
							_history.RemoveLast();
						}
					}
					else
					{
						MovementNode node = _history.Last.Value;
						int difference = value - Length;
						int cellX;
						int cellY;
						bool changeX = node.MovementDirection.GetAxis() == Axis2D.X;
						int increment = node.MovementDirection.Invert().Sign();
						for (int i = 0; i < difference; i++)
						{
							if (changeX)
							{
								cellX = node.Cell.X + increment;
								cellY = node.Cell.Y;
							}
							else
							{
								cellX = node.Cell.X;
								cellY = node.Cell.Y + increment;
							}
							_history.AddLast(new MovementNode(PlayArea[cellX, cellY], node.MovementDirection));
						}
					}
				}
			}
		}
		/// <summary>
		/// Gets the tail of the snake, in order, from its head.
		/// </summary>
		public IEnumerable<MovementNode> Tail
		{
			get
			{
				return new LinkedList<MovementNode>(_history); // Protecting against casting to LinkedList giving access to data.
			}
		}
		/// <summary>
		/// Gets the set of cells occupied by the snake.
		/// </summary>
		IEnumerable<Grid.Cell> IGridOccupier.OccupiedCells
		{
			get
			{
				List<Grid.Cell> res = new List<Grid.Cell>(_history.Count);
				foreach (MovementNode node in _history)
				{
					res.Add(node.Cell);
				}
				return res;
			}
		}
		/// <summary>
		/// Moves the snake one cell in the direction specified by MovementDirection.
		/// </summary>
		public void Move()
		{
			int x = Head.X;
			int y = Head.Y;
			if (MovementDirection.GetAxis() == Axis2D.X)
			{
				x += MovementDirection.Sign();
			}
			else
			{
				y += MovementDirection.Sign();
			}
			_history.AddFirst(new MovementNode(PlayArea[x, y], MovementDirection));
			_history.RemoveLast();
		}
		/// <summary>
		/// Returns an IEnumerator for the current instance.
		/// </summary>
		public IEnumerator<MovementNode> GetEnumerator()
		{
			return Tail.GetEnumerator();
		}
		/// <summary>
		/// Returns an IEnumerator for the current instance.
		/// </summary>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}

