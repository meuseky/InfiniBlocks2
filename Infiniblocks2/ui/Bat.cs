using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace InfiniBlocks2
{
	class Bat
	{
		#region Variables
		private Vector2 m_pos;
		private Texture2D m_txr;
		private Rectangle m_boundingBox;
		private Rectangle m_screenBounds;
		private float m_velL, m_velR;
		//private float m_acc;

		//Animation Variables
		private AnimStateEnumeration m_animState;
		private Rectangle m_animCell;
		private float m_frameTimer;
		private float m_fps;

		public Rectangle Rect
		{
			get
			{
				return m_boundingBox;
			}
		}

		public float VelL
		{
			get
			{
				return m_velL;
			}
		}

		public float VelR
		{
			get
			{
				return m_velR;
			}
		}

		public float Vel
		{
			get
			{
				return m_velR - m_velL;
			}
		}
		#endregion

		#region Methods
		public Bat(Texture2D txr, Rectangle screenBounds)
		{
			m_animState = AnimStateEnumeration.Normal;
			m_velL = 0;
			m_velR = 0;
			m_screenBounds = screenBounds;
			//Initialise Position
			m_pos = new Vector2(540, 665);
			m_boundingBox = new Rectangle((int)m_pos.X, (int)m_pos.Y, 100, 25);
			m_txr = txr;
		}

		public void UpdateMe(KeyboardState currKb)
		{
			//Add in check for intersection with powerups

			//Adjust velocity based on input.
			if (currKb.IsKeyDown(Keys.A) || currKb.IsKeyDown(Keys.Left))
			{
				m_velL += 3;
				if (m_velL > 14)
				{
					m_velL = 14;
				}
			}
			else
			{
				m_velL -= 2;
				if (m_velL < 0)
				{
					m_velL = 0;
				}
			}

			if (currKb.IsKeyDown(Keys.D) || currKb.IsKeyDown(Keys.Right))
			{
				m_velR += 3;
				if (m_velR > 14)
				{
					m_velR = 14;
				}
			}
			else
			{
				m_velR -= 2;
				if (m_velR < 0)
				{
					m_velR = 0;
				}
			}

			//Adjust screen position from velocity
			m_pos.X += m_velR;
			m_pos.X -= m_velL;

			//Adjust collision sphere
			m_boundingBox.X = (int)m_pos.X;
			m_boundingBox.Y = (int)m_pos.Y;

			//Make sure sphere is in bounds
			//**Put in adjusted screen bounds, maybe use a rectangle
			if (m_pos.X < m_screenBounds.X)
			{
				m_pos.X = m_screenBounds.X;
				m_velL = 0;
			}
			if (m_pos.X + m_txr.Width > m_screenBounds.X + m_screenBounds.Width)
			{
				m_pos.X = m_screenBounds.X + m_screenBounds.Width - m_txr.Width;
				m_velR = 0;
			}
		}

		public void DrawMe(SpriteBatch sb, GameTime gt)
		{
			//Change to suit powerup states too..
			switch (m_animState)
			{
			case AnimStateEnumeration.Normal:

				sb.Draw(m_txr, new Rectangle((int)m_pos.X, (int)m_pos.Y, m_txr.Width, m_txr.Height), Color.White);

				break;
			case AnimStateEnumeration.Hit:

				break;

			case AnimStateEnumeration.Death:

				break;
			}
		}

		public void Reset()
		{
			m_pos.X = 540;
			m_pos.X = 665;

			m_velL = 0;
			m_velR = 0;

			m_boundingBox.X = (int)m_pos.X;
			m_boundingBox.Y = (int)m_pos.Y;
		}

		#endregion
	}

}

