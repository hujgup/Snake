using System;
using SnakeGame.Model;
using SnakeGame.GridSystem;
using SnakeGame.UserInterface;
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
			_playArea = new Grid(32, 32);
			_player = new Snake(_playArea, _playArea[16, 16], 5, Direction.Right);
			_objective = new Fruit(_playArea, _player.OccupiedCells, 3);
			_objective.Eaten += (object sender, EventArgs e) =>
			{
				_objective.RandomizeLocation(_player.OccupiedCells);
			};
			_mover = new SnakeMovementControlHandler(_player, (int)difficulty);
			_mover.OutOfBounds += (object sender, EventArgs e) =>
			{
				OnDone(GameState.ScoreInput);
			};
			_mover.AfterMove += (object sender, EventArgs e) =>
			{
				if (_player.Head.Cell == _objective.OccupiedCell)
				{
					_objective.Eat(_player);
				}
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

