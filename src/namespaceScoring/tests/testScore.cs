using System;
using System.Threading;
using SnakeGame.Model;
using SnakeGame.GridSystem;
using NUnit.Framework;
using Mirror = MbUnit.Framework.Mirror;
using SnakeGame.Scoring;
namespace SnakeGame
{
    
    /// <summary>
    /// Test score.
    /// </summary>
    [TestFixture()]
    public class testScore
    {
        
        /// <summary>
        /// Tests the score creation.
        /// </summary>
        [Test()]
        public void TestScoreCreation([Values(Difficulty.Easy,Difficulty.Hard,Difficulty.Medium)] Difficulty diff)
        {
            
            Score s = new Score(diff);
            Assert.AreEqual(0, s.Value);
        }
        /// <summary>
        /// Tests the score when fruit eaten.
        /// </summary>
        /// <param name="fruitVal">Fruit value.</param>
        [Test()]
        public void TestScoreWhenFruitEaten([Values(2,3,4,5)]int fruitVal)
        {
            Grid g = new Grid(16, 16);
            Score sv = new Score(Difficulty.Easy);
            Snake s = new Snake(g, g[8, 8], 2, Direction.Right);
            Fruit f = new Fruit(g, fruitVal);
            s.MovementDirection = Direction.Right;
            Cell c = g[s.Head.Cell.X + 1, s.Head.Cell.Y];
            f.OccupiedCell = c;
            FruitEatenHandler h = new FruitEatenHandler(f, s,sv);
            s.Move();
            h.EvaluateState();
            Assert.AreEqual(fruitVal, sv.Value, "Once the snake eats fruit the score should increase by "+fruitVal);
        }
    }
}

