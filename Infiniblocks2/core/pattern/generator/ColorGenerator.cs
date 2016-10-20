using System;

namespace InfiniBlocks2
{
	class ColorGenerator
	{
		public ColorGenerator()
		{

		}

		public int[,] Walk()
		{
			int[,] tempArray = new int[15, 12];
			Array.Clear(tempArray, 0, tempArray.Length);
			int randX, randY, randColor, randDir;

			for (int k = 0; k < 100; k++)
			{
				//Choose a random starting position
				randX = Game1.RNG.Next(0, 14);
				randY = Game1.RNG.Next(0, 11);
				//Only set to pick the first six colors
				randColor = Game1.RNG.Next(0, 11);

				tempArray[randX, randY] = randColor;

				//Move coloring tile set number of times
				for (int i = 0; i < 25; i++)
				{
					randDir = Game1.RNG.Next(0, 3);
					switch (randDir)
					{
					case 0:
						if (randY == 0)
						{
							//break;
						}
						else
						{
							randY--;
						}
						break;
					case 1:
						if (randX == 14)
						{
							//break;
						}
						else
						{
							randX++;
						}
						break;
					case 2:
						if (randY == 11)
						{
							//break;
						}
						else
						{
							randY++;
						}
						break;
					case 3:
						if (randX == 0)
						{
							//break;
						}
						else
						{
							randX--;
						}
						break;
					}
					tempArray[randX, randY] = randColor;
				}
			}
			return tempArray;
		}

		public int[,] Stripes()
		{
			int[,] tempArray = new int[15, 12];

			double HorizOrVert;
			int cols, rows;
			int colCount, colColor;

			HorizOrVert = Game1.RNG.NextDouble();
			if (HorizOrVert > 0.5f)
			{
				cols = 15;
				rows = 12;
			}
			else
			{
				cols = 12;
				rows = 15;
			}

			colCount = 0;
			colColor = 0;

			for (int i = 0; i < cols; i++)
			{
				if (colCount == 0)
				{
					colCount = Game1.RNG.Next(1, 3);
					colColor = Game1.RNG.Next(0, 11);
				}
				for (int j = 0; j < rows; j++)
				{
					if (HorizOrVert > 0.5f)
					{
						tempArray[i, j] = colColor;
					}
					else
					{
						tempArray[j, i] = colColor;
					}
				}
				colCount--;
			}

			return tempArray;
		}

		public int[,] RandomColor()
		{
			int[,] tempArray = new int[15, 12];
			int tempRand;

			for (int i = 0; i < 15; i++)
			{
				for (int j = 0; j < 12; j++)
				{
					tempRand = Game1.RNG.Next(0, 11);
					tempArray[i, j] = tempRand;
				}
			}

			return tempArray;
		}
	}
}

