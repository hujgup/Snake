using System;
using SnakeGame.Model;
using SnakeGame.GridSystem;
using SnakeGame.UserInterface;
using SnakeGame.Scoring;
using SwinGameSDK;


namespace SnakeGame
{
	/// <summary>
	/// Controls the flow of the game during gameplay segments.
	/// </summary>
	public class GameplayController : GameController
	{
		private Grid _playArea;
		private Snake _player;
		private Fruit _objective;
        private Score _score;
		private FruitEatenHandler _handler;
		private SnakeMovementControlHandler _mover;
		private BooleanControlsFlag _up;
		private BooleanControlsFlag _down;
		private BooleanControlsFlag _left;
		private BooleanControlsFlag _right;
		private EventHandler _renderer;
		/// <summary>
		/// Initializes a new instance of the <see cref="SnakeGame.GameplayController"/> class.
		/// </summary>
		/// <param name="difficulty">The difficulty level to play at.</param>
		public GameplayController(Difficulty difficulty)
		{
            _score = new Score(difficulty);            
			_playArea = new Grid(32, 32);
			_player = new Snake(_playArea, _playArea[16, 16], 5, Direction.Right);
			_objective = new Fruit(_playArea, _player.OccupiedCells, 3);
            _handler = new FruitEatenHandler(_objective, _player,_score);
			_mover = new SnakeMovementControlHandler(_player, (int)difficulty);
			_mover.OutOfBounds += (object sender, EventArgs e) =>
			{
				string finalScore = "Final score: " + _score.Value;
				Color textColor = CellDrawing.GetColor("#e00707");
				EventHandler gameOverText = delegate(object sender2, EventArgs e2)
				{
					SwinGame.DrawText("GAME OVER", textColor, 96, 128);
					SwinGame.DrawText(finalScore, textColor, 96, 140);
				};
				var gameOverTimeout = new System.Timers.Timer(2048);
				gameOverTimeout.Elapsed += (object sender2, System.Timers.ElapsedEventArgs e2) =>
				{
					gameOverTimeout.Stop();
					gameOverTimeout.Dispose();
					RenderEvents.RenderTick -= gameOverText;
					OnDone(new ScoreInputController(_score));
				};
				gameOverTimeout.Start();
				RenderEvents.RenderTick += gameOverText;
			};
			_mover.AfterMove += (object sender, EventArgs e) =>
			{
				_handler.EvaluateState();
			};

			_up = new BooleanControlsFlag(delegate()
			{
				return SwinGame.KeyDown(KeyCode.vk_w) || SwinGame.KeyDown(KeyCode.vk_UP);
			});
			_up.StateSetTrue += (object sender, EventArgs e) =>
			{
				_mover.Enqueue(Direction.Up);
			};
			_left = new BooleanControlsFlag(delegate()
			{
				return SwinGame.KeyDown(KeyCode.vk_a) || SwinGame.KeyDown(KeyCode.vk_LEFT);
			});
			_left.StateSetTrue += (object sender, EventArgs e) =>
			{
				_mover.Enqueue(Direction.Left);
			};
			_down = new BooleanControlsFlag(delegate()
			{
				return SwinGame.KeyDown(KeyCode.vk_s) || SwinGame.KeyDown(KeyCode.vk_DOWN);
			});
			_down.StateSetTrue += (object sender, EventArgs e) =>
			{
				_mover.Enqueue(Direction.Down);
			};
			_right = new BooleanControlsFlag(delegate()
			{
				return SwinGame.KeyDown(KeyCode.vk_d) || SwinGame.KeyDown(KeyCode.vk_RIGHT);
			});
			_right.StateSetTrue += (object sender, EventArgs e) =>
			{
				_mover.Enqueue(Direction.Right);
			};

			Color scoreColor = CellDrawing.GetColor("#008282");
			_renderer = delegate(object sender, EventArgs e)
			{
				int offset = 1;
				int x;
				int y;
				for (y = -1, x = -1; x <= _playArea.Width; x++)
				{
					CellDrawing.Draw(offset, offset, new Cell(_playArea, x, y));
					CellDrawing.Draw(offset, offset, new Cell(_playArea, x, _playArea.Height));
				}
				for (y = 0, x = -1; y < _playArea.Height; y++)
				{
					CellDrawing.Draw(offset, offset, new Cell(_playArea, x, y));
					CellDrawing.Draw(offset, offset, new Cell(_playArea, _playArea.Width, y));
				}
				foreach (MovementNode node in _player)
				{
					CellDrawing.Draw(offset, offset, node.Cell);
				}
				CellDrawing.Draw(offset, offset, _objective.OccupiedCell);
				SwinGame.DrawText("Score: " + _score.Value, scoreColor, 12, 2);
			};
			RenderEvents.RenderTick += _renderer;
		}
		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="SnakeGame.GameplayController"/>. The
		/// <see cref="Dispose"/> method leaves the <see cref="SnakeGame.GameplayController"/> in an unusable state. After
		/// calling <see cref="Dispose"/>, you must release all references to the <see cref="SnakeGame.GameplayController"/>
		/// so the garbage collector can reclaim the memory that the <see cref="SnakeGame.GameplayController"/> was occupying.</remarks>
		public override void Dispose()
		{
			_mover.Dispose();
			_up.Dispose();
			_down.Dispose();
			_left.Dispose();
			_right.Dispose();
			RenderEvents.RenderTick -= _renderer;
		}
	}
}

