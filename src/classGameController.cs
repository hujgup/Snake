using System;
using SnakeGame.Model;
using SnakeGame.GridSystem;
using SnakeGame.UserInterface;
using SwinGameSDK;

namespace SnakeGame
{
/*
	public enum GameState
	{
		MainMenu,
		SettingsMenu,
		Gameplay,
		ScoreInput,
		ScoreView
	}
*/
	public enum Difficulty
	{
		Easy = 5,
		Medium = 13,
		Hard = 19,
	}
	public class GameplayController : IDisposable
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
		public GameplayController(Difficulty difficulty)
		{
			_playArea = new Grid(32, 32);
			_player = new Snake(_playArea, _playArea[16, 16], 5, Direction.Right);
			_objective = new Fruit(_playArea, _player.OccupiedCells, 1);
			_mover = new SnakeMovementControlHandler(_player, (int)difficulty);

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
		public void Dispose()
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

