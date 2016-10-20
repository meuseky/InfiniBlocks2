using System;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace InfiniBlocks2
{
	public class MenuState
	{
		private MenuScreenEnumeration menuScreen;

		//0-3 backgrounds
		//4-7 Main Buttons
		//8-10 NewGame Buttons
		//11-13 Settings Buttons
		//14 Empty Button (for HighScore)
		//15 Back Button (in 3 submenus)
		private Texture2D[] m_backArt;
		private Texture2D[] m_buttonArt;
		private SpriteFont m_font;
		private int highScore;
		private bool init;

		private MenuButtons menuButtons;

		public MenuState(Texture2D[] txr, SpriteFont menuFont)
		{
			menuScreen = MenuScreenEnumeration.Main;
			m_font = menuFont;

			m_backArt = new Texture2D[4];
			m_buttonArt = new Texture2D[12];
			init = true;

			//Pass background textures into array
			for (int i = 0; i < 4; i++)
			{

				m_backArt[i] = txr[i];
			}

			//Pass button textures into seperate array
			for (int i = 4; i < 16; i++)
			{
				m_buttonArt[i - 4] = txr[i];
			}

			menuButtons = new MenuButtons(m_buttonArt);
		}

		public void UpdateMe(KeyboardState currKb, KeyboardState oldKb, MouseState currMs, MouseState oldMS, ref GameStateEnumeration gameState, ref InPlayState gameInPlay)
		{
			if (init)
			{
				//Sound.StartTrack(7);
				init = false;
			}

			menuScreen = menuButtons.UpdateMe(currKb, oldKb, currMs, oldMS, menuScreen, ref gameState, ref gameInPlay);

			if (menuScreen == MenuScreenEnumeration.HighScore)
			{
				highScore = GetHighScore();
			}
		}

		public void DrawMe(SpriteBatch sb)
		{

			sb.Draw(m_backArt[(int)menuScreen], new Rectangle(0, 0, m_backArt[(int)menuScreen].Width, m_backArt[(int)menuScreen].Height), Color.White);

			menuButtons.DrawMe(sb);


			switch (menuScreen)
			{
			case MenuScreenEnumeration.Main:

				break;

			case MenuScreenEnumeration.NewGame:

				break;

			case MenuScreenEnumeration.HighScore:

				//highScore = GetHighScore();

				sb.DrawString(m_font, "High Score: " + highScore, new Vector2(265, 193), Color.Gold);

				break;

			case MenuScreenEnumeration.Settings:

				break;
			}

		}

		public int GetHighScore()
		{
			int number;
			FileStream scoreFile = new FileStream("scores.dat", FileMode.Open, FileAccess.Read);
			BinaryReader scoreReader = new BinaryReader(scoreFile);
			number = scoreReader.ReadInt32();
			scoreReader.Close();
			return number;
		}
	}
}

