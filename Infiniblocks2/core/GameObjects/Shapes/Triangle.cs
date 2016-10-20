using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace InfiniBlocks2
{
	public class Triangle
	{
		Vector2 vertex1;
		Vector2 vertex2;
		Vector2 vertex3;

		public Triangle (Vector2 pos1, Vector2 pos2, Vector2 pos3)
		{
			vertex1 = pos1;
			vertex2 = pos2;
			vertex3 = pos3;
		}
	}
}

