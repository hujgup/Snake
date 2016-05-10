using System;
using SwinGameSDK;

namespace SnakeGame.UserInterface
{
	public class KeyboardLetterFlag : Flag<char>
	{
		public KeyboardLetterFlag() : base(delegate()
		{
			char res = '\0';
			for (char check = 'A'; res == '\0' && check <= 'Z'; check++)
			{
				KeyCode code = GetCode(check);
				if (code != KeyCode.vk_Unknown && SwinGame.KeyTyped(code))
				{
					res = check;
					break;
				}
			}
			if (res == '\0')
			{
				if (SwinGame.KeyDown(KeyCode.vk_BACKSPACE))
				{
					res = '\b';
				}
				else if (res == '\0' && SwinGame.KeyDown(KeyCode.vk_SPACE))
				{
					res = ' ';
				}
			}
			return res;
		})
		{
			RenderEvents.LogicTick += Handler;
		}
		private static KeyCode GetCode(char c)
		{
			KeyCode res = KeyCode.vk_Unknown;
			switch (c)
			{
				case 'A':
				case 'a':
					res = KeyCode.vk_a;
					break;
				case 'B':
				case 'b':
					res = KeyCode.vk_b;
					break;
				case 'C':
				case 'c':
					res = KeyCode.vk_c;
					break;
				case 'D':
				case 'd':
					res = KeyCode.vk_d;
					break;
				case 'E':
				case 'e':
					res = KeyCode.vk_e;
					break;
				case 'F':
				case 'f':
					res = KeyCode.vk_f;
					break;
				case 'G':
				case 'g':
					res = KeyCode.vk_g;
					break;
				case 'H':
				case 'h':
					res = KeyCode.vk_h;
					break;
				case 'I':
				case 'i':
					res = KeyCode.vk_i;
					break;
				case 'J':
				case 'j':
					res = KeyCode.vk_j;
					break;
				case 'K':
				case 'k':
					res = KeyCode.vk_k;
					break;
				case 'L':
				case 'l':
					res = KeyCode.vk_l;
					break;
				case 'M':
				case 'm':
					res = KeyCode.vk_m;
					break;
				case 'N':
				case 'n':
					res = KeyCode.vk_n;
					break;
				case 'O':
				case 'o':
					res = KeyCode.vk_o;
					break;
				case 'P':
				case 'p':
					res = KeyCode.vk_p;
					break;
				case 'Q':
				case 'q':
					res = KeyCode.vk_q;
					break;
				case 'R':
				case 'r':
					res = KeyCode.vk_r;
					break;
				case 'S':
				case 's':
					res = KeyCode.vk_s;
					break;
				case 'T':
				case 't':
					res = KeyCode.vk_t;
					break;
				case 'U':
				case 'u':
					res = KeyCode.vk_u;
					break;
				case 'V':
				case 'v':
					res = KeyCode.vk_v;
					break;
				case 'W':
				case 'w':
					res = KeyCode.vk_w;
					break;
				case 'X':
				case 'x':
					res = KeyCode.vk_x;
					break;
				case 'Y':
				case 'y':
					res = KeyCode.vk_y;
					break;
				case 'Z':
				case 'z':
					res = KeyCode.vk_z;
					break;
			}
			return res;
		}
		public override void Dispose()
		{
			RenderEvents.LogicTick -= Handler;
		}
	}
	public class TextInput : IDisposable
	{
		private KeyboardLetterFlag _letters;
		private System.Timers.Timer _timer;
		private bool _allowRepeats;
		private bool _typedOnce;
		private EventHandler _typer;
		public TextInput()
		{
			_letters = new KeyboardLetterFlag();
			_allowRepeats = false;
			_typedOnce = false;
			_timer = new System.Timers.Timer(512);
			_timer.Elapsed += (object sender, System.Timers.ElapsedEventArgs e) =>
			{
				_allowRepeats = true;
			};
			_letters.StateChange += (object sender, EventArgs e) =>
			{
				_allowRepeats = false;
				_typedOnce = false;
				_timer.Stop();
				_timer.Start();
			};
			_typer = delegate(object sender, EventArgs e)
			{
				if (_letters.Value != '\0')
				{
					bool allow;
					if (!_typedOnce)
					{
						_typedOnce = true;
						allow = true;
					}
					else
					{
						allow = _allowRepeats;
					}
					if (allow)
					{
						if (_letters.Value == '\b')
						{
							if (Text.Length != 0)
							{
								Text = Text.Substring(0, Text.Length - 1);
							}
						}
						else
						{
							Text += _letters.Value;
						}
					}
				}
			};
			RenderEvents.LogicTick += _typer;
		}
		public string Text
		{
			get;
			private set;
		}
		public void Dispose()
		{
			_timer.Stop();
			_timer.Dispose();
			RenderEvents.LogicTick -= _typer;
		}
	}
}

