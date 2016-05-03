using System;
using System.Collections.Generic;
using SnakeGame.GridSystem;
using NUnit.Framework;

namespace SnakeGame.Model
{
	/// <summary>
	/// Provides unit tests for the Snake class.
	/// </summary>
	[TestFixture()]
	public class TestSnake
	{
		private void MovementTest(Grid g, Cell c, int length, Direction dir)
		{
			Snake s = new Snake(g, c, length, dir);
			Cell check;
			int increment = dir.Invert().Sign();
			bool xAxis = dir.GetAxis() == Axis2D.X;
			int i = 0;
			int getNum = length - 2;
			List<MovementNode> frontNodes = new List<MovementNode>(getNum);
			foreach (MovementNode n in s)
			{
				if (i++ < getNum)
				{
					frontNodes.Add(n);
				}
				else
				{
					break;
				}
			}

			s.Move();
			check = xAxis ? g[c.X - increment, c.Y] : g[c.X, c.Y - increment];
			Assert.AreEqual(check, s.Head.Cell, "Moving the snake should change the position of the head according to the direction of travel.");
			Direction d2 = dir.RotateClockwise();
			s.MovementDirection = d2;
			Assert.AreEqual(d2, s.MovementDirection, "Setting MovementDirection should update the MovementDirection property.");
			Assert.AreNotEqual(d2, s.Head.MovementDirection, "Setting the movement direction should not change existing MovementNodes.");
			s.Move();
			Assert.AreEqual(d2, s.Head.MovementDirection, "Moving the snake after setting the direction should set the new head's MovementDirection property to the new value.");
			{
				IEnumerator<MovementNode> eMain = s.GetEnumerator();
				eMain.MoveNext();
				eMain.MoveNext();
				foreach (MovementNode n in frontNodes)
				{
					Assert.IsTrue(eMain.MoveNext(), "Snake enumerators and enumerable properties must have exactly Length entries at all times (post-movement true check).");
					Assert.AreEqual(n, eMain.Current, "Moving the snake n times should shift enumerable entry positions exactly n slots down, discarding overflow.");
				}
				Assert.IsFalse(eMain.MoveNext(), "Snake enumerators and enumerable properties must have exactly Length entries at all times (post-movement false check).");
				eMain.Reset();
				{
					IEnumerator<MovementNode> eTail = s.Tail.GetEnumerator();
					IEnumerator<Cell> eCells = s.OccupiedCells.GetEnumerator();
					while (eMain.MoveNext())
					{
						Assert.IsTrue(eTail.MoveNext(), "Snake.Tail.GetEnumerator() must have exactly as many elements as Snake.GetEnumerator() (post-movement true check).");
						Assert.IsTrue(eCells.MoveNext(), "Snake.OccupiedCells.GetEnumerator() must have exactly as many elements as Snake.GetEnumerator() (post-movement true check).");
						Assert.AreEqual(eMain.Current.MovementDirection, eTail.Current.MovementDirection, "Each MovementDirection entry in Snake.GetEnumerator() must match the MovementDirection entry in Snake.Tail.GetEnumerator().");
						Assert.AreEqual(eMain.Current.Cell, eTail.Current.Cell, "Each Cell entry in Snake.GetEnumerator() must match the Cell entry in Snake.Tail.GetEnumerator().");
						Assert.AreEqual(eMain.Current.Cell, eCells.Current, "Each Cell entry in Snake.GetEnumerator() must match the entry in Snake.OccupiedCells.GetEnumerator().");
					}
					Assert.IsFalse(eTail.MoveNext(), "Snake.Tail.GetEnumerator() must have exactly as many elements as Snake.GetEnumerator() (post-movement false check).");
					Assert.IsFalse(eCells.MoveNext(), "Snake.OccupiedCells.GetEnumerator() must have exactly as many elements as Snake.GetEnumerator() (post-movement false check).");
				}
			}
			List<MovementNode> mainNodes = new List<MovementNode>(s);
			List<MovementNode> tailNodes = new List<MovementNode>(s.Tail);
			List<Cell> cells = new List<Cell>(s.OccupiedCells);
			Assert.AreEqual(s.Length, mainNodes.Count, "The number of nodes in the snake's tail should match one-to-one with the snake's length at all times (main nodes post-movement check).");
			Assert.AreEqual(s.Length, tailNodes.Count, "The number of nodes in the snake's tail should match one-to-one with the snake's length at all times (tail nodes post-movement check).");
			Assert.AreEqual(s.Length, cells.Count, "The number of nodes in the snake's tail should match one-to-one with the snake's length at all times (cells post-movement check).");
		}
		private void LengthChangeTest(Grid g, Cell c, int length, Direction dir)
		{
			Snake s = new Snake(g, c, length, dir);
			MovementNode oldHead = s.Head;
			MovementNode oldEnd = s.End;
			s.Length = s.Length;
			Assert.AreEqual(oldHead, s.Head, "Settin Snake.Length to the same value as it currently has should change nothing (head check).");
			Assert.AreEqual(oldEnd, s.End, "Settin Snake.Length to the same value as it currently has should change nothing (end check).");
			int oldLength = s.Length;
			int newLength = oldLength + 5;
			s.Length = newLength;
			Assert.AreEqual(newLength, s.Length, "Setting the Snake.Length property should update the Snake.Length getter (increment).");
			Assert.AreEqual(oldHead, s.Head, "Setting the Snake.Length property should not change the snake's head (increment).");
			Assert.AreNotEqual(oldEnd, s.End, "Setting the Snake.Length property to any value except the value it already had should change Snake.End (increment).");
			List<MovementNode> nodes = new List<MovementNode>(s.Tail);
			Assert.AreEqual(newLength, nodes.Count, "The number of MovementNodes in Snake.Tail must match one-to-one with the snake's length at all times (length increase check).");
			int offset = 1;
			int increment = oldEnd.MovementDirection.Invert().Sign();
			bool xAxis = dir.GetAxis() == Axis2D.X;
			Cell check;
			for (int i = oldLength; i < newLength; i++, offset++)
			{
				Assert.AreEqual(oldEnd.MovementDirection, nodes[i].MovementDirection, "Increasing Snake.Length should create new MovementNodes in the same direction as Snake.End.MovementDirection.");
				check = xAxis ? g[oldEnd.Cell.X + offset * increment, oldEnd.Cell.Y] : g[oldEnd.Cell.X, oldEnd.Cell.Y + offset * increment];
				Assert.AreEqual(check, nodes[i].Cell, "Increasing Snake.Length should create new MovementNodes with a position off by one of the previous MovementNode, in accordance with MovementDirection.");
			}
			oldEnd = s.End;
			newLength -= 2;
			s.Length = newLength;
			Assert.AreEqual(newLength, s.Length, "Setting the Snake.Length property should update the Snake.Length getter (decrement).");
			Assert.AreEqual(oldHead, s.Head, "Setting the Snake.Length property should not change the snake's head (decrement).");
			Assert.AreNotEqual(oldEnd, s.End, "Setting the Snake.Length property to any value except the value it already had should change Snake.End (decrement).");
			List<MovementNode> newNodes = new List<MovementNode>(s.Tail);
			Assert.AreEqual(newLength, newNodes.Count, "The number of MovementNodes in Snake.Tail must match one-to-one with the snake's length at all times (length decrease check).");
			Assert.IsFalse(newNodes.Contains(nodes[nodes.Count - 1]), "Decreasing the snake's length by n should remove n nodes from the end of the tail (count - 1).");
			Assert.IsFalse(newNodes.Contains(nodes[nodes.Count - 2]), "Decreasing the snake's length by n should remove n nodes from the end of the tail (count - 2).");
		}
		private void CtorTest(Grid g, Cell c, int length, Direction dir)
		{
			Snake s = new Snake(g, c, length, dir);
			Assert.AreEqual(g, s.PlayArea, "The first constructor parameter must set Snake.PlayArea.");
			Assert.AreEqual(c, s.Head.Cell, "The second constructor parameter must set Snake.Head.Cell.");
			Assert.AreEqual(dir, s.Head.MovementDirection, "The fourth constructor parameter must set Snake.Head.MovementDirection.");
			Assert.AreEqual(length, s.Length, "The third constructor parameter must set Snake.Length.");
			Assert.AreEqual(dir, s.MovementDirection, "The fourth constructor parameter must set Snake.MovementDirection.");
			int increment = dir.Invert().Sign();
			bool xAxis = dir.GetAxis() == Axis2D.X;
			int i = 0;
			int getNum = length - 2;
			Cell check;
			{
				IEnumerator<MovementNode> eMain = s.GetEnumerator();
				IEnumerator<MovementNode> eTail = s.Tail.GetEnumerator();
				IEnumerator<Cell> eCells = s.OccupiedCells.GetEnumerator();
				while (eMain.MoveNext())
				{
					Assert.IsTrue(eTail.MoveNext(), "Snake.Tail.GetEnumerator() must have exactly as many elements as Snake.GetEnumerator() (construct true check).");
					Assert.IsTrue(eCells.MoveNext(), "Snake.OccupiedCells.GetEnumerator() must have exactly as many elements as Snake.GetEnumerator() (construct true check).");
					Assert.AreEqual(dir, eMain.Current.MovementDirection, "All initial MovementNodes must have the same MovementDirection as set in the constructor (main enumerator).");
					Assert.AreEqual(dir, eTail.Current.MovementDirection, "All initial MovementNodes must have the same MovementDirection as set in the constructor (tail enumerator).");
					if (i == 0)
					{
						check = c;
					}
					else if (xAxis)
					{
						check = new Cell(g, c.X + increment * i, c.Y);
					}
					else
					{
						check = new Cell(g, c.X, c.Y + increment * i);
					}
					Assert.AreEqual(check, eMain.Current.Cell, "Snake.GetEnumerator() cells must correspond to specified constraints when Snake is newly constructed.");
					Assert.AreEqual(check, eTail.Current.Cell, "Snake.Tail.GetEnumerator() cells must correspond to specified constraints when Snake is newly constructed.");
					Assert.AreEqual(check, eCells.Current, "Snake.OccupiedCells.GetEnumerator() cells must correspond to specified constraints when Snake is newly constructed.");
					i++;
				}
				Assert.IsFalse(eTail.MoveNext(), "Snake.Tail.GetEnumerator() must have exactly as many elements as Snake.GetEnumerator() (construct false check).");
				Assert.IsFalse(eCells.MoveNext(), "Snake.OccupiedCells.GetEnumerator() must have exactly as many elements as Snake.GetEnumerator() (construct false check).");
			}
			List<MovementNode> mainNodes = new List<MovementNode>(s);
			List<MovementNode> tailNodes = new List<MovementNode>(s.Tail);
			List<Cell> cells = new List<Cell>(s.OccupiedCells);
			Assert.AreEqual(s.Length, mainNodes.Count, "The number of nodes in the snake's tail should match one-to-one with the snake's length at all times (main nodes constructor check).");
			Assert.AreEqual(s.Length, tailNodes.Count, "The number of nodes in the snake's tail should match one-to-one with the snake's length at all times (tail nodes constructor check).");
			Assert.AreEqual(s.Length, cells.Count, "The number of nodes in the snake's tail should match one-to-one with the snake's length at all times (cells constructor check).");
		}
		private void CheckBounds(Grid g, Direction dir)
		{
			Snake s = new Snake(g, g[8, 8], 2, dir);
			for (int i = 0; i < 8; i++)
			{
				s.Move();
				Assert.IsFalse(s.OutOfBounds, "Snake.OutOfBounds must be false while the snake is in a valid cell that is not at the grid width/height or larger, depending on the axis being checked (direction movement checks).");
			}
			s.Move();
			Assert.IsTrue(s.OutOfBounds, "Moving the snake outside the grid in any direction should cause Snake.OutOfBounds to be true (direction movement checks).");
		}
		/// <summary>
		/// Tests snake construction.
		/// </summary>
		[Test()]
		public void Construction()
		{
			Grid g = new Grid(16, 16);
			CtorTest(g, g[8, 8], 5, Direction.Down);
			CtorTest(g, g[4, 9], 2, Direction.Up);
			CtorTest(g, g[15, 0], 7, Direction.Up);
			CtorTest(g, g[3, 11], 3, Direction.Left);
			CtorTest(g, g[12, 11], 4, Direction.Right);
			Snake s;
			Assert.Throws<ArgumentNullException>(delegate()
			{
				s = new Snake(null, g[8, 9], 3, Direction.Left);
			});
			Assert.Throws<ArgumentException>(delegate()
			{
				s = new Snake(g, Cell.INVALID_CELL, 3, Direction.Left);
			});
			Assert.Throws<ArgumentException>(delegate()
			{
				s = new Snake(g, new Cell(null, 0 ,0), 3, Direction.Left);
			});
			Assert.Throws<ArgumentOutOfRangeException>(delegate()
			{
				s = new Snake(g, new Cell(g, 20, 20), 3, Direction.Left);
			});
			Assert.Throws<ArgumentOutOfRangeException>(delegate()
			{
				s = new Snake(g, g[0, 0], 1, Direction.Left);
			});
		}
		/// <summary>
		/// Tests snake movement.
		/// </summary>
		[Test()]
		public void Movement()
		{
			Grid g = new Grid(16, 16);
			MovementTest(g, g[6, 15], 6, Direction.Down);
			MovementTest(g, g[4, 7], 2, Direction.Up);
			MovementTest(g, g[14, 12], 4, Direction.Right);
			MovementTest(g, g[2, 3], 2, Direction.Left);
			MovementTest(g, g[0, 4], 3, Direction.Up);
			Snake s = new Snake(g, g[8, 8], 4, Direction.Right);
			for (int i = 0; i < 8; i++)
			{
				s.Move();
			}
			Assert.IsFalse(s.Head.Cell.IsValid);
		}
		/// <summary>
		/// Tests setting Snake.Length.
		/// </summary>
		[Test()]
		public void LengthSet()
		{
			Grid g = new Grid(16, 16);
			LengthChangeTest(g, g[5, 8], 3, Direction.Up);
			LengthChangeTest(g, g[7, 5], 4, Direction.Down);
			LengthChangeTest(g, g[1, 7], 5, Direction.Left);
			LengthChangeTest(g, g[9, 2], 6, Direction.Right);
			LengthChangeTest(g, g[8, 9], 7, Direction.Up);
			Snake s = new Snake(g, g[8, 8], 3, Direction.Down);
			Assert.Throws<ArgumentOutOfRangeException>(delegate()
			{
				s.Length = 1;
			});
		}
		/// <summary>
		/// Tests the OutOfBounds property.
		/// </summary>
		[Test()]
		public void TestBounds()
		{
			Grid g = new Grid(17, 17);
			CheckBounds(g, Direction.Up);
			CheckBounds(g, Direction.Down);
			CheckBounds(g, Direction.Right);
			CheckBounds(g, Direction.Left);
			Snake s = new Snake(g, g[0, 0], 2, Direction.Left);
			Assert.IsFalse(s.OutOfBounds, "Snake.OutOfBounds must be false while the snake is in a valid cell that is not at the grid width/height or larger, depending on the axis being checked (start 0,0 check).");
			s.Move();
			Assert.IsTrue(s.OutOfBounds, "Moving the snake outside the grid in any direction should cause Snake.OutOfBounds to be true (start 0,0 check).");
		}
	}
}

