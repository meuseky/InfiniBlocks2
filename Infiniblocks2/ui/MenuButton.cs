using System;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace InfiniBlocks2
{
	class MenuButtons
	{
		private Texture2D[] m_buttonArt;
		private Rectangle[] m_buttonPos;
		private int selectedButton;
		private bool[] onOrOff;
		private MenuScreenEnumeration m_screen;

		//Keeps track of which textures to use for buttons
		//Due to repeats of textures
		private int[,] txrChoices = { { 0, 1, 2, 3 }, { 4, 5, 6, 11 }, { 10, 10, 10, 11 }, { 7, 8, 9, 11 } };

		public MenuButtons(Texture2D[] txr)
		{
			selectedButton = 0;
			m_buttonArt = txr;
			onOrOff = new bool[4];
			m_buttonPos = new Rectangle[4];

			onOrOff[0] = true;
			onOrOff[1] = false;
			onOrOff[2] = false;
			onOrOff[3] = false;

			//**Need to initialise positions for all rectangles
			//Just use the same four rectangles
			//**Update later
			m_buttonPos[0] = new Rectangle(1070, 265, 225, 75);
			m_buttonPos[1] = new Rectangle(1070, 390, 225, 75);
			m_buttonPos[2] = new Rectangle(1070, 515, 225, 75);
			m_buttonPos[3] = new Rectangle(1070, 640, 225, 75);
		}

		public MenuScreenEnumeration UpdateMe(KeyboardState currKb, KeyboardState oldKb, MouseState currMs, MouseState oldMS, MenuScreenEnumeration menuScreen, ref GameStateEnumeration gameState, ref InPlayState gameInPlay)
		{
			m_screen = menuScreen;

			if (currKb.IsKeyDown(Keys.Up) && oldKb.IsKeyUp(Keys.Up))
			{
//				//Sound.PlayEffect(0);
				selectedButton = (Math.Abs((selectedButton - 1) * 4) + (selectedButton - 1)) % 4;
			}
			if (currKb.IsKeyDown(Keys.Down) && oldKb.IsKeyUp(Keys.Down))
			{
				// //Sound.PlayEffect(0);
				selectedButton = (selectedButton + 1) % 4;
			}

			for (int i = 0; i < 4; i++)
			{
				if (m_buttonPos[i].Contains(currMs.X, currMs.Y))
				{
					selectedButton = i;
				}
			}

			//Disallow movement if on HighScore screen
			if (m_screen == MenuScreenEnumeration.HighScore)
			{
				selectedButton = 3;
			}

			for (int i = 0; i < 4; i++)
			{
				if (selectedButton == i)
				{
					onOrOff[i] = true;
				}
				else
				{
					onOrOff[i] = false;
				}
			}

			//**Add in mouse click event
			if (currKb.IsKeyDown(Keys.Enter) && oldKb.IsKeyUp(Keys.Enter))
			{
//				//Sound.PlayEffect(0);

				switch (m_screen)
				{
				case MenuScreenEnumeration.Main:
					switch (selectedButton)
					{
					case 0:
						m_screen = MenuScreenEnumeration.NewGame;
						break;
					case 1:
						m_screen = MenuScreenEnumeration.HighScore;
						break;
					case 2:
						m_screen = MenuScreenEnumeration.Settings;
						break;
					case 3:
						//Switches Game1 Update to play Outro
						gameState = GameStateEnumeration.Outro;
						break;
					}
					break;

				case MenuScreenEnumeration.NewGame:
					switch (selectedButton)
					{
					case 0:
						//Easy
						gameInPlay.Strength = GameStrengthEnumeration.Easy;
						gameState = GameStateEnumeration.InPlay;
						m_screen = MenuScreenEnumeration.Main;
						break;
					case 1:
						//Medium
						gameInPlay.Strength = GameStrengthEnumeration.Medium;
						gameState = GameStateEnumeration.InPlay;
						m_screen = MenuScreenEnumeration.Main;
						break;
					case 2:
						//Hard
						gameInPlay.Strength = GameStrengthEnumeration.Hard;
						gameState = GameStateEnumeration.InPlay;
						m_screen = MenuScreenEnumeration.Main;
						break;
					case 3:
						m_screen = MenuScreenEnumeration.Main;
						break;
					}
					break;

				case MenuScreenEnumeration.HighScore:
					//Only one choice, back to main menu
					m_screen = MenuScreenEnumeration.Main;
					break;

				case MenuScreenEnumeration.Settings:
					switch (selectedButton)
					{
					case 0:
						//**TBD
						//Full screen
						//Resolution
						//Volume: music & effects
						//reset scores
						break;
					case 1:
//						if (//Sound.SoundOn == true)
//						{
//							//Sound.soundOn = false;
//							//Sound.StopTrack();
//						}
//						else
//						{
//							//Sound.soundOn = true;
//						}
						break;
					case 2:
						ResetFile();
						break;
					case 3:
						m_screen = MenuScreenEnumeration.Main;
						break;
					}

					break;
				}
				selectedButton = 0;
				return m_screen;
			}
			else
			{
				return m_screen;
			}
		}

		public void DrawMe(SpriteBatch sb)
		{
			for (int j = 0; j < 4; j++)
			{
				int animCell;
				if(j == selectedButton)
				{
					animCell = 125;
				}
				else
				{
					animCell = 0;
				}
				//Change m_buttonArt selections depending on current screen
				//Uses txrChoices to set texture used
				sb.Draw(m_buttonArt[txrChoices[(int)m_screen, j]], new Rectangle(m_buttonPos[j].X - 25, m_buttonPos[j].Y -25, 275, 125), new Rectangle(0, animCell, 275, 125), Color.White);
			}
		}

		public void ResetFile()
		{
			//File.Delete("scores.dat");
			//File.Create("scores.dat");
			FileStream scoreFile = new FileStream("scores.dat", FileMode.Open, FileAccess.Write);
			BinaryWriter newWriter = new BinaryWriter(scoreFile);
			newWriter.Write(0);
			newWriter.Flush();
			newWriter.Close();
		}
	}
}

