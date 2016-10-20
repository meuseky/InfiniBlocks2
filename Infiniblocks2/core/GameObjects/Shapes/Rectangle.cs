using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace InfiniBlocks2
{
	public class Rect : Shape
	{
		public Vector2 Origin { get; set; }
		public Vector2 Size { get; set; }

		public CollisionChecker collChecker;

		// origin is dist from UL of screen to UL of rect
		// size is dist from origin to BR of rect
		public Rect (Vector2 origin, Vector2 size)
		{
		}

		public bool Contains(Vector2 point)
		{
			return true; // ((point - Center).Length() <= Radius);
		}

		public bool Intersects(Shape other)
		{
			return true; //collChecker(, other)
		}
	}
}

