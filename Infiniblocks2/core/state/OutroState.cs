using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace InfiniBlocks2
{
	public class OutroState
	{
		private bool fadeIn, fadeOut, animating;
		private Texture2D[] m_outroArt;

		//Used in Animation
		private double m_animDelay;
		private int m_currCell;
		private int m_cellOffset;

		//Use for Fade In/Out Effects
		private int m_AlphaValue;
		private int m_FadeIncrement;
		private double m_FadeDelay;
		private bool init;
		private int txrOffset;

		public OutroState(Texture2D[] txr)
		{
			m_AlphaValue = 1;
			m_FadeIncrement = 15;
			m_FadeDelay = .02;
			fadeIn = true;
			fadeOut = false;
			animating = false;
			init = true;

			m_animDelay = 0.2f;
			m_currCell = 0;
			m_cellOffset = 225;
			txrOffset = 0;

			m_outroArt = txr;
		}

		public void UpdateMe(GameTime gt, ref GameStateEnumeration gameState)
		{
			if (init)
			{
				//Sound.StartTrack(6);
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
					m_FadeDelay = .02;

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

			txrOffset += 13;

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
					m_FadeDelay = .035;

					//Increment/Decrement the fade value for the image
					m_AlphaValue += m_FadeIncrement;

					//Stop fade in when fully realised
					if (m_AlphaValue <= 0)
					{
						//Sound.StopTrack();
						gameState = GameStateEnumeration.Exit;
					}
				}
			}
		}

		public void DrawMe(SpriteBatch sb)
		{
			sb.Draw(m_outroArt[0], new Rectangle(0, 0, 1366, 768), new Rectangle(0, 1280 - txrOffset, m_outroArt[0].Width, 768),
				new Color(m_AlphaValue, m_AlphaValue, m_AlphaValue));
		}
	}
}

