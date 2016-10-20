using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace InfiniBlocks2
{
	class SimpleShape
	{
		//Number from 0-14
		int shapeChoice;
		//Number 0-3 corresponding to 0, 90, 180, 270.
		int shapeRotation;

		int[,] choiceTiles;
		Vector2 choiceCentre;
		int[] choiceSpace;

		//Setup some properties for ease of use
		public int[,] Tiles
		{
			get
			{
				if (choiceTiles == null)
					choiceTiles = new int[5, 5];

				return choiceTiles;
			}
			set
			{
				choiceTiles = value;
			}
		}

		public Vector2 Centre
		{
			get
			{
				if (choiceCentre == null)
					choiceCentre = new Vector2();

				return choiceCentre;
			}
			set
			{
				choiceCentre = value;
			}
		}

		public int[] Space
		{
			get
			{
				if (choiceSpace == null)
					choiceSpace = new int[8];

				return choiceSpace;
			}
			set
			{
				choiceSpace = value;
			}
		}

		//Array keeps track of shape tiles
		int[, ,] shapeTiles = {
			//Circles
			{{0,1,0,0,0},{1,1,1,0,0},{0,1,0,0,0},{0,0,0,0,0},{0,0,0,0,0}},
			{{0,1,1,0,0},{1,1,1,1,0},{1,1,1,1,0},{0,1,1,0,0},{0,0,0,0,0}},
			{{0,0,1,0,0},{0,1,1,1,0},{1,1,1,1,1},{0,1,1,1,0},{0,0,1,0,0}},
			//Squares
			{{1,1,0,0,0},{1,1,0,0,0},{0,0,0,0,0},{0,0,0,0,0},{0,0,0,0,0}},
			{{1,1,1,0,0},{1,1,1,0,0},{1,1,1,0,0},{0,0,0,0,0},{0,0,0,0,0}},
			{{1,1,1,1,0},{1,1,1,1,0},{1,1,1,1,0},{1,1,1,1,0},{0,0,0,0,0}},
			//Right Triangles
			{{1,1,0,0,0},{0,1,0,0,0},{0,0,0,0,0},{0,0,0,0,0},{0,0,0,0,0}},
			{{1,1,1,0,0},{0,1,1,0,0},{0,0,1,0,0},{0,0,0,0,0},{0,0,0,0,0}},
			{{1,1,1,1,0},{0,1,1,1,0},{0,0,1,1,0},{0,0,0,1,0},{0,0,0,0,0}},
			//Isosceles Triangles
			{{0,1,0,0,0},{1,1,0,0,0},{0,1,0,0,0},{0,0,0,0,0},{0,0,0,0,0}},
			{{0,1,0,0,0},{1,1,0,0,0},{1,1,0,0,0},{0,1,0,0,0},{0,0,0,0,0}},
			{{0,0,1,0,0},{0,1,1,0,0},{1,1,1,0,0},{0,1,1,0,0},{0,0,1,0,0}},
			//Miscilaneous Shapes
			{{1,0,0,0,0},{1,0,0,0,0},{1,0,0,0,0},{1,0,0,0,0},{0,0,0,0,0}},
			{{0,1,0,0,0},{1,1,0,0,0},{1,0,0,0,0},{0,0,0,0,0},{0,0,0,0,0}},
			{{1,1,0,0,0},{1,0,0,0,0},{1,0,0,0,0},{1,0,0,0,0},{0,0,0,0,0}}
		};

		//Keeps track of 'Centre of shapes'
		Vector2[] shapeCentre = {
			//Circles
			new Vector2(1,1), new Vector2(1,1), new Vector2(2,2),
			//Squares
			new Vector2(0,0), new Vector2(1,1), new Vector2(1,1),
			//Right Triangles
			new Vector2(0,1), new Vector2(0,2), new Vector2(0,3),
			//Isosceles Triangles
			new Vector2(1,1), new Vector2(1,1), new Vector2(2,2),
			//Miscilaneous Shapes
			new Vector2(0,0), new Vector2(1,1), new Vector2(0,0)};

		//Amount of Space, from centre, in 8 directions: Up, Right, Down, Left, UR, DR, DL, UL
		int[,] shapeSpace = {
			//Circles
			{1,1,1,1,0,0,0,0},
			{1,2,2,1,1,1,1,0},
			{2,2,2,2,1,1,1,1},
			//Squares
			{0,1,1,0,0,1,0,0},
			{1,1,1,1,1,1,1,1},
			{1,2,2,1,1,2,1,1},
			//Right Triangles
			{1,1,0,0,0,0,0,0},
			{2,2,0,0,1,0,0,0},
			{3,3,0,0,1,0,0,0},
			//Isosceles Triangles
			{1,1,0,1,0,0,0,0},
			{1,2,0,1,1,0,0,0},
			{2,2,0,2,1,0,0,1},
			//Miscilaneous Shapes
			{0,3,0,0,0,0,0,0},
			{1,0,0,1,1,0,0,0},
			{0,3,1,0,0,0,0,0}
		};

		public SimpleShape()
		{
			//Chooose shape
			shapeChoice = Game1.RNG.Next(0, 14);
			shapeRotation = Game1.RNG.Next(0, 3);

			//Place shape info into seperate arrays
			choiceTiles = new int[5, 5];
			choiceSpace = new int[8];

			//Tile Info
			for (int i = 0; i < 5; i++)
			{
				for (int j = 0; j < 5; j++)
				{
					choiceTiles[i, j] = shapeTiles[shapeChoice, i, j];
				}
			}
			choiceTiles = RotateTiles(shapeRotation, choiceTiles);

			//Centre Info
			choiceCentre = shapeCentre[shapeChoice];
			choiceCentre = RotateCentre(shapeRotation, choiceCentre);

			//Space Info
			for (int i = 0; i < 8; i++)
			{
				choiceSpace[i] = shapeSpace[shapeChoice, i];
			}
			choiceSpace = RotateSpace(shapeRotation, choiceSpace);
		}

		public int[,] RotateTiles(int m_Rotation, int[,] m_Tiles)
		{
			//If the shape isn't rotated, exit out of method
			if (m_Rotation == 0)
			{
				return m_Tiles;
			}

			//Setup an array to hold rotated elements
			int[,] tempTileArray = new int[5, 5];
			//Set array to all zeros, so I just need to plug in the ones.
			Array.Clear(tempTileArray, 0, tempTileArray.Length);

			//Vectors to toss around data in
			List<Vector2> vectorList = new List<Vector2>();
			Vector2 tempVector = new Vector2();
			Vector2 extraVector = new Vector2();

			//List of all tile positions
			for (int i = 0; i < 5; i++)
			{
				for (int j = 0; j < 5; j++)
				{
					if (m_Tiles[i, j] == 1)
					{
						vectorList.Add(new Vector2(i, j));
					}
				}
			}

			//Moving origin position of vectors to allow rotation
			for (int i = 0; i < vectorList.Count; i++)
			{
				tempVector = vectorList[i];

				tempVector.X -= 2;
				tempVector.Y -= 2;

				vectorList[i] = tempVector;
			}

			//Rotating vectors dependingly about origin
			switch (m_Rotation)
			{
			case 0:

				break;
			case 1:
				for (int i = 0; i < vectorList.Count; i++)
				{
					tempVector = vectorList[i];

					extraVector.X = -tempVector.Y;
					extraVector.Y = tempVector.X;

					vectorList[i] = extraVector;
				}
				break;
			case 2:
				for (int i = 0; i < vectorList.Count; i++)
				{
					tempVector = vectorList[i];

					extraVector.X = -tempVector.X;
					extraVector.Y = -tempVector.Y;

					vectorList[i] = extraVector;
				}
				break;
			case 3:
				for (int i = 0; i < vectorList.Count; i++)
				{
					tempVector = vectorList[i];

					extraVector.X = tempVector.Y;
					extraVector.Y = -tempVector.X;

					vectorList[i] = extraVector;
				}
				break;
			}

			//Moving origin back to original position
			for (int i = 0; i < vectorList.Count; i++)
			{
				tempVector = vectorList[i];

				tempVector.X += 2;
				tempVector.Y += 2;

				vectorList[i] = tempVector;
			}

			for (int i = 0; i < vectorList.Count; i++)
			{
				tempVector = vectorList[i];

				tempTileArray[(int)tempVector.X, (int)tempVector.Y] = 1;
			}

			return tempTileArray;
		}

		public Vector2 RotateCentre(int m_Rotation, Vector2 m_Centre)
		{
			if (m_Rotation == 0)
			{
				return m_Centre;
			}

			//Setup vectors to handle the transition
			Vector2 tempCentre = new Vector2();
			Vector2 extraCentre = new Vector2();

			tempCentre = m_Centre;

			//Adjust origin
			tempCentre.X -= 2;
			tempCentre.Y -= 2;

			//Rotating vectors dependingly about origin
			switch (m_Rotation)
			{
			case 0:

				break;
			case 1:
				extraCentre.X = -tempCentre.Y;
				extraCentre.Y = tempCentre.X;
				break;
			case 2:
				extraCentre.X = -tempCentre.X;
				extraCentre.Y = -tempCentre.Y;
				break;
			case 3:
				extraCentre.X = tempCentre.Y;
				extraCentre.Y = -tempCentre.X;
				break;
			}

			//Return to original origin
			extraCentre.X += 2;
			extraCentre.Y += 2;

			return extraCentre;
		}

		public int[] RotateSpace(int m_Rotation, int[] m_Space)
		{
			if (m_Rotation == 0)
			{
				return m_Space;
			}

			//Array to temporarily hold rotated values
			int[] tempSpace = new int[8];
			//Rotation amount
			int rotAmount = 0;
			int modNum;
			int finalMod;

			switch (m_Rotation)
			{
			case 0:

				break;
			case 1:
				rotAmount = -2;
				break;
			case 2:
				rotAmount = -4;
				break;
			case 3:
				rotAmount = -6;
				break;
			}

			for (int i = 0; i < 8; i++)
			{
				modNum = i + rotAmount;
				finalMod = ((modNum % 8) + 8) % 8;

				tempSpace[i] = m_Space[finalMod];
			}

			return tempSpace;
		}
	}
}

