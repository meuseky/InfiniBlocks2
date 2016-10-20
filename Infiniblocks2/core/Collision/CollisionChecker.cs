using System;

namespace InfiniBlocks2
{
	public class CollisionChecker
	{
		// RectangleCollision
		// CircleCollision
		// RectToCircleCollision
		// CircleToRectCollision
		// 

		public CollisionChecker ()
		{
		}

		public bool Intersects(Shape OtherShape)
		{
			switch (OtherShape.GetType()) {
			case Circle:
				return ((other.Center - Center).Length () < (other.Radius - Radius));
				break;
			case Rectangle:
				break;
			}
		}
	}
}