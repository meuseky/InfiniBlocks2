using System;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace InfiniBlocks2
{
	public class IntroState
	{
		#region Variables
		private bool fadeIn, fadeOut, animating;
		private Texture2D[] m_introArt;
		private bool init;

		//Used in Animation
		private double m_animDelay;
		private int m_currCell;
		private int m_cellOffset;

		//Use for Fade In/Out Effects
		private int m_AlphaValue;
		private int m_FadeIncrement;
		private double m_FadeDelay;
		#endregion

		#region Methods
		public IntroState(Texture2D[] txr)
		{
			m_AlphaValue = 1;
			m_FadeIncrement = 10;
			m_FadeDelay = .04;
			fadeIn = true;
			fadeOut = false;
			animating = false;
			init = true;

			m_animDelay = 0.2f;
			m_currCell = 0;
			m_cellOffset = 225;

			m_introArt = txr;

			CheckFile();
		}

		public void CheckFile()
		{
			if (File.Exists("scores.dat"))
			{

			}
			else
			{
				File.Create("scores.dat");
				FileStream scoreFile = new FileStream("scores.dat", FileMode.Open, FileAccess.Write);
				BinaryWriter newWriter = new BinaryWriter(scoreFile);
				newWriter.Write(0);
				newWriter.Flush();
				newWriter.Close();
			}
		}

		public void UpdateMe(GameTime gt, ref GameStateEnumeration gameState, KeyboardState currKb, KeyboardState oldKb)
		{
			if (currKb.IsKeyDown(Keys.Enter) && oldKb.IsKeyUp(Keys.Enter))
			{
				gameState = GameStateEnumeration.Menu;
			}

			if (init)
			{
				//Sound.StartTrack(5);
				init = false;
			}

			if (fadeIn)
			{
				//Decrement the delay
				m_FadeDelay -= gt.ElapsedGameTime.TotalSeconds;

				//If the Fade delays has dropped below zero, fade in/out a little more.
				if (m_FadeDelay <= 0)
				{
					//Reset the Fade delay
					m_FadeDelay = .04;

					//Increment/Decrement the fade value for the image
					m_AlphaValue += m_FadeIncrement;

					//Stop fade in when fully realised
					if (m_AlphaValue >= 255)
					{
						fadeIn = false;
						animating = true;
						//Switch to lowering for fadeOut
						m_FadeIncrement *= -1;
						m_FadeDelay = .03;
					}
				}
			}

			if (animating)
			{
				if (m_currCell < 10)
				{
					m_animDelay -= gt.ElapsedGameTime.TotalSeconds;

					if (m_animDelay <= 0)
					{
						m_animDelay = 0.2f;
						m_currCell++;
					}
				}
				else
				{
					animating = false;
					fadeOut = true;
				}
			}

			if (fadeOut)
			{
				//Decrement the delay
				m_FadeDelay -= gt.ElapsedGameTime.TotalSeconds;

				//If the Fade delays has dropped below zero, fade in/out a little more.
				if (m_FadeDelay <= 0)
				{
					//Reset the Fade delay
					m_FadeDelay = .03;

					//Increment/Decrement the fade value for the image
					m_AlphaValue += m_FadeIncrement;

					//Stop fade in when fully realised
					if (m_AlphaValue <= 0)
					{
						//Sound.StopTrack();
						gameState = GameStateEnumeration.Menu;
					}
				}
			}
		}

		public void DrawMe(SpriteBatch sb)
		{
			sb.Draw(m_introArt[0], new Rectangle(0, 0, m_introArt[0].Width, m_introArt[0].Height),
				new Color(m_AlphaValue, m_AlphaValue, m_AlphaValue));

			sb.Draw(m_introArt[1], new Rectangle(380, 250, 550, 225),
				new Color(m_AlphaValue, m_AlphaValue, m_AlphaValue));
			if (fadeIn)
			{
				sb.Draw(m_introArt[2], new Rectangle(380, 50 + (int)(m_AlphaValue * 2.5f), 550, 450),
					new Color(m_AlphaValue, m_AlphaValue, m_AlphaValue));
			}
		}
		#endregion
	}
}

