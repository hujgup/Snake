using System;
using SnakeGame.Scoring;
using SnakeGame.UserInterface;
using SwinGameSDK;

namespace SnakeGame
{
	public class ScoreInputController : GameController
	{
		private BooleanControlsFlag _up;
		private BooleanControlsFlag _down;
		private BooleanControlsFlag _select;
		private EventHandler _renderer;
		private Score _score;
		private TextInput _input;
		public ScoreInputController(Score s)
		{
			_score = s;
			_input = new TextInput();

			_select = new BooleanControlsFlag(delegate()
			{
				return SwinGame.KeyTyped(KeyCode.vk_RETURN);
			});
			_select.StateSetTrue += (object sender, EventArgs e) =>
			{
				if (_input.Text.Length != 0)
				{
					_score.PlayerName = _input.Text;
					ScoreIO io = new ScoreIO();
					io.AddScore(_score);
					io.Write();
				}
				//OnDone(new MainMenuController());
			};
			_renderer = delegate(object sender, EventArgs e)
			{
				Color selected = CellDrawing.GetColor("#4ac925");
				SwinGame.DrawText("Please input your name to submit your score:", CellDrawing.COLOR, 16, 128);
				SwinGame.DrawText(_input.Text, selected, 96, 140);
			};
			RenderEvents.RenderTick += _renderer;
		}
		public override void Dispose()
		{
			_select.Dispose();
			_input.Dispose();
			RenderEvents.RenderTick -= _renderer;
		}
	}
}

