using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace InfiniBlocks2
{
	class PowerUp
	{
		#region Variables
		private Vector2 m_pos;
		private Texture2D m_txr;
		private int m_health;
		private Rectangle m_boundingBox;
		private float m_vel;
		private PowerTypeEnumeration m_PowerType;
		private Color powerColor;

		//Animation Variables
		private AnimStateEnumeration m_animState;
		private int m_animCell;
		private float m_frameTimer;

		public Rectangle Rect
		{
			get
			{
				return m_boundingBox;
			}
		}

		public AnimStateEnumeration Anim
		{
			get
			{
				return m_animState;
			}
			set
			{
				m_animState = value;
			}
		}

		public PowerTypeEnumeration Power
		{
			get
			{
				return m_PowerType;
			}
			set
			{
				m_PowerType = value;
			}
		}
		#endregion

		#region Methods
		public PowerUp(Texture2D txr, int xPos, int yPos)
		{
			m_animState = AnimStateEnumeration.Normal;
			m_txr = txr;
			m_pos.X = xPos;
			m_pos.Y = yPos;
			m_boundingBox = new Rectangle((int)m_pos.X, (int)m_pos.Y, 55, 25);
			m_vel = 4;
			m_frameTimer = 0.2f;
			m_animCell = 0;

			//Change depending on**
			int randPower = Game1.RNG.Next(0, 2);

			switch (randPower)
			{
			case 0:
				m_PowerType = PowerTypeEnumeration.Red;
				break;
			case 1:
				m_PowerType = PowerTypeEnumeration.Green;
				break;
			case 2:
				m_PowerType = PowerTypeEnumeration.Blue;
				break;
			default:
				m_PowerType = PowerTypeEnumeration.Red;
				randPower = 0;
				break;
			}

			powerColor = Game1.powerColors[(int)m_PowerType];
		}

		public void UpdateMe()
		{
			m_boundingBox.Y += (int)m_vel;
		}

		public void DrawMe(SpriteBatch sb, GameTime gt)
		{
			switch (m_animState)
			{
			case AnimStateEnumeration.Normal:

				if(m_frameTimer >= 0)
				{
					m_frameTimer -= (float)gt.ElapsedGameTime.TotalSeconds;
				}
				else
				{
					m_frameTimer = 0.2f;
					m_animCell++;
					if(m_animCell > 4)
					{
						m_animCell = 0;
					}
				}

				sb.Draw(m_txr, m_boundingBox, new Rectangle(0, m_animCell * 25, 55, 25), powerColor);

				break;
			case AnimStateEnumeration.Hit:



				break;

			case AnimStateEnumeration.Death:

				break;
			}
		}
		#endregion
	}
}

