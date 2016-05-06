using System;
using System.Collections.Generic;
using SnakeGame.UserInterface;
using SwinGameSDK;

namespace SnakeGame
{
	/// <summary>
	/// Controls the flow of the game during gameplay setup.
	/// </summary>
	public class SetupMenuController : GameController
	{
		private BooleanControlsFlag _up;
		private BooleanControlsFlag _down;
		private BooleanControlsFlag _select;
		private Difficulty _difficulty;
		private EventHandler _renderer;
		/// <summary>
		/// Initializes a new instance of the <see cref="SnakeGame.SetupMenuController"/> class.
		/// </summary>
		public SetupMenuController()
		{
			_difficulty = Difficulty.Medium;
			_up = new BooleanControlsFlag(delegate()
			{
				return SwinGame.KeyTyped(KeyCode.vk_UP) || SwinGame.KeyTyped(KeyCode.vk_w);
			});
			_up.StateSetTrue += (object sender, EventArgs e) =>
			{
				switch (_difficulty)
				{
					case Difficulty.Easy:
						_difficulty = Difficulty.Hard;
						break;
					default:
						_difficulty = _difficulty.Decrement();
						break;
				}
			};
			_down = new BooleanControlsFlag(delegate()
			{
				return SwinGame.KeyTyped(KeyCode.vk_DOWN) || SwinGame.KeyTyped(KeyCode.vk_s);
			});
			_down.StateSetTrue += (object sender, EventArgs e) =>
			{
				switch (_difficulty)
				{
					case Difficulty.Hard:
						_difficulty = Difficulty.Easy;
						break;
					default:
						_difficulty = _difficulty.Increment();
						break;
				}
			};
			_select = new BooleanControlsFlag(delegate()
			{
				return SwinGame.KeyTyped(KeyCode.vk_SPACE) || SwinGame.KeyTyped(KeyCode.vk_RETURN);
			});
			_select.StateSetTrue += (object sender, EventArgs e) =>
			{
				OnDone(new GameplayController(_difficulty));
			};
			_renderer = delegate(object sender, EventArgs e)
			{
				Color selected = CellDrawing.GetColor("#4ac925");
				SwinGame.DrawText("Choose a difficulty level:", CellDrawing.COLOR, 12, 12);
				IEnumerator<Difficulty> enumerator = _difficulty.GetEnumerator();
				int y = 24;
				int increment = 12;
				while (enumerator.MoveNext())
				{
					SwinGame.DrawText(enumerator.Current.ToString(), enumerator.Current == _difficulty ? selected : CellDrawing.COLOR, 12, y);
					y += increment;
				}
			};
			RenderEvents.RenderTick += _renderer;
		}
		/// <summary>
		/// Releases all resource used by the <see cref="SnakeGame.SetupMenuController"/> object.
		/// </summary>
		/// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="SnakeGame.SetupMenuController"/>. The
		/// <see cref="Dispose"/> method leaves the <see cref="SnakeGame.SetupMenuController"/> in an unusable state. After
		/// calling <see cref="Dispose"/>, you must release all references to the <see cref="SnakeGame.SetupMenuController"/>
		/// so the garbage collector can reclaim the memory that the <see cref="SnakeGame.SetupMenuController"/> was occupying.</remarks>
		public override void Dispose()
		{
			_up.Dispose();
			_down.Dispose();
			_select.Dispose();
			RenderEvents.RenderTick -= _renderer;
		}
	}
}

