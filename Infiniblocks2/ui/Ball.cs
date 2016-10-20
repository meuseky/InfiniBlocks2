using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace InfiniBlocks2
{
	class Ball
	{
		#region Variables
		private Vector2 m_pos;
		private Texture2D m_txr;
		//private BoundingSphere m_boundingSphere;
		private Rectangle[] m_boundingBoxes;
		private Vector2 m_vel;
		//Represents overall speed level
		private float m_ballAcc;
		private Rectangle m_screenBounds;
		//private Vector2 m_acc;

		//Animation Variables
		private AnimStateEnumeration m_animState;
		private Rectangle m_animCell;
		private float m_frameTimer;
		private float m_fps;

		private BallStateEnumeration ballState;

		public BallStateEnumeration State
		{
			get
			{
				return ballState;
			}
			set
			{
				ballState = value;
			}
		}

		public Vector2 Pos
		{
			get
			{
				return m_pos;
			}
			set
			{
				m_pos = value;
			}
		}

		public Rectangle[] Rect
		{
			get
			{
				return m_boundingBoxes;
			}
			set
			{
				m_boundingBoxes = value;
			}
		}

		public float VecX
		{
			get
			{
				return m_vel.X;
			}
			set
			{
				m_vel.X = value;
			}
		}

		public float VecY
		{
			get
			{
				return m_vel.Y;
			}
			set
			{
				m_vel.Y = value;
			}
		}

		#endregion

		#region Methods
		public Ball(Texture2D txr, Rectangle screenBounds)
		{
			m_animState = AnimStateEnumeration.Normal;
			ballState = BallStateEnumeration.Stuck;
			m_txr = txr;
			//**Adjust Later
			//Set m_pos as center of Bounding Sphere
			m_pos = new Vector2(578, 475);

			//m_boundingBoxes[0] = new Rectangle((int)m_pos.X, (int)m_pos.Y, m_txr.Width, m_txr.Height);
			m_boundingBoxes = new Rectangle[4];
			m_boundingBoxes[0] = new Rectangle((int)m_pos.X + 3, (int)m_pos.Y, 19, 25);
			m_boundingBoxes[1] = new Rectangle((int)m_pos.X, (int)m_pos.Y + 3, 25, 19);
			m_boundingBoxes[2] = new Rectangle((int)m_pos.X + 3, (int)m_pos.Y + 3, 19, 19);
			m_boundingBoxes[3] = new Rectangle((int)m_pos.X, (int)m_pos.Y, 25, 25);

			//m_boundingBox = new Rectangle((int)m_pos.X, (int)m_pos.Y, 25, 25);
			//m_boundingSphere = new BoundingSphere(new Vector3(m_pos, 0), m_txr.Width / 2);
		}

		public void UpdateMe()
		{

			double hypoTen, ratioX, ratioY;

			hypoTen = Math.Sqrt((m_vel.X * m_vel.X) + (m_vel.Y * m_vel.Y));
			ratioX = m_vel.X / hypoTen;
			ratioY = m_vel.Y / hypoTen;

			if (hypoTen > 10)
			{
				hypoTen *= 0.8;
				m_vel.X = (float)(ratioX * hypoTen);
				m_vel.Y = (float)(ratioY * hypoTen);
			}
			if (hypoTen < 5 && ballState != BallStateEnumeration.Stuck)
			{
				hypoTen *= 1.2;
				m_vel.X = (float)(ratioX * hypoTen);
				m_vel.Y = (float)(ratioY * hypoTen);
			}

			if (m_vel.Y > -0.1f && m_vel.Y < 0.1f)
			{
				m_vel.Y += 0.1f;
			}

			//Add velocity to position
			m_pos.X += m_vel.X;
			m_pos.Y += m_vel.Y;

			//m_boundingBox.X = (int)m_pos.X + 7;
			//m_boundingBox.Y = (int)m_pos.Y;

			m_boundingBoxes[0].X = (int)m_pos.X + 3;
			m_boundingBoxes[0].Y = (int)m_pos.Y;

			m_boundingBoxes[1].X = (int)m_pos.X;
			m_boundingBoxes[1].Y = (int)m_pos.Y + 3;

			m_boundingBoxes[2].X = (int)m_pos.X + 3;
			m_boundingBoxes[2].Y = (int)m_pos.Y + 3;

			m_boundingBoxes[3].X = (int)m_pos.X;
			m_boundingBoxes[3].Y = (int)m_pos.Y;

		}

		public void DrawMe(SpriteBatch sb, GameTime gt)
		{
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
			m_pos.X = 0;
			m_pos.Y = 0;

			m_vel.X = -1;
			m_vel.Y = -1;
		}

		public void NewLevel()
		{

		}
		#endregion
	}
}

