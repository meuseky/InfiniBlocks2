using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InfiniBlocks2
{
	class Block
	{
		#region Variables
		private Vector2 m_pos;
		private Texture2D m_txr;
		private int m_health;
		private Rectangle m_boundingBox;
		private BlockTypeEnumeration m_blockType;
		private Color blockColor, animColor;

		//Animation Variables
		private AnimStateEnumeration m_animState;
		private Rectangle m_animCell;
		private double m_frameTimer;
		private float m_fps;

		public Rectangle Rect
		{
			get
			{
				return m_boundingBox;
			}
		}

		public int Health
		{
			get
			{
				return m_health;
			}
			set
			{
				m_health = value;
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

		public double Timer
		{
			get
			{
				return m_frameTimer;
			}
			set
			{
				m_frameTimer = value;
			}
		}

		public BlockTypeEnumeration Type
		{
			get
			{
				return m_blockType;
			}
		}

		#endregion

		#region Methods
		//, Color color, BlockType blockType
		public Block(Texture2D txr, int xPos, int yPos, int currColor)
		{
			m_animState = AnimStateEnumeration.Normal;
			m_txr = txr;
			m_pos.X = (xPos * 55) + 170;
			m_pos.Y = (yPos * 35) + 34;

			//Three bounding boxes for greater accuracy.
			m_boundingBox = new Rectangle((int)m_pos.X, (int)m_pos.Y, m_txr.Width, m_txr.Height);

			blockColor = Game1.blockColors[currColor];
			animColor = blockColor;

			//Change below later
			m_health = 2;

		}

		public void UpdateMe(GameTime gt)
		{
			//Run animation if hit
			//Only switch state if solid
			//Update score if death

			//
			if (m_animState == AnimStateEnumeration.Hit)
			{

				m_frameTimer -= gt.ElapsedGameTime.TotalSeconds;
				ResetAnimColor(m_frameTimer);
				if (m_frameTimer <= 0)
				{
					m_animState = AnimStateEnumeration.Normal;
				}
			}
			/*
            if (m_health <= 0)
            {
                m_animState = AnimStateEnumeration.Death;

            }
            */

		}

		public void DrawMe(SpriteBatch sb, GameTime gt)
		{
			switch (m_animState)
			{
			case AnimStateEnumeration.Normal:
				//**Change Color later

				sb.Draw(m_txr, new Rectangle((int)m_pos.X, (int)m_pos.Y, m_txr.Width, m_txr.Height), blockColor);

				break;
			case AnimStateEnumeration.Hit:

				sb.Draw(m_txr, new Rectangle((int)m_pos.X, (int)m_pos.Y, m_txr.Width, m_txr.Height), animColor);

				break;

			case AnimStateEnumeration.Death:

				break;
			}
		}

		public void ResetAnimColor(double timer)
		{
			double tempR, tempG, tempB;

			tempR = blockColor.R + (1024 * timer);
			if (tempR > 254)
			{
				tempR = 254;
			}
			tempG = blockColor.G + (1024 * timer);
			if (tempG > 254)
			{
				tempG = 254;
			}
			tempB = blockColor.B + (1024 * timer);
			if (tempB > 254)
			{
				tempB = 254;
			}

			animColor.R = (byte)(tempR);
			animColor.G = (byte)(tempG);
			animColor.B = (byte)(tempB);
		}

		public void NewLevel()
		{

		}
		#endregion
	}
}

