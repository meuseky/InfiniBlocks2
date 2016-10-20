using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace InfiniBlocks2
{
	class BlockGenerator
	{
		public BlockGenerator()
		{

		}

		public int[,] Perlin()
		{
			int tileCount;
			double tempAmount;
			int[,] tileHolder = new int[15, 12];
			double[,] doubleHolder = new double[15, 12];
			double[,] neighborArrays = new double[15, 12];

			//do
			//{
			tileCount = 0;
			Array.Clear(tileHolder, 0, tileHolder.Length);
			Array.Clear(doubleHolder, 0, doubleHolder.Length);
			Array.Clear(neighborArrays, 0, neighborArrays.Length);

			for (int k = 0; k < 5; k++)
			{
				for (int i = 0; i < 15; i++)
				{
					for (int j = 0; j < 12; j++)
					{
						doubleHolder[i, j] = Game1.RNG.NextDouble();
					}
				}
			}

			//Create array adding a fifth to neighboring array elements
			for (int i = 0; i < 15; i++)
			{
				for (int j = 0; j < 12; j++)
				{
					tempAmount = doubleHolder[i, j];
					tempAmount /= 2;

					if (i > 0)
					{
						neighborArrays[i - 1, j] += tempAmount;
					}
					if (i < 14)
					{
						neighborArrays[i + 1, j] += tempAmount;
					}
					if (j > 0)
					{
						neighborArrays[i, j - 1] += tempAmount;
					}
					if (j < 11)
					{
						neighborArrays[i, j + 1] += tempAmount;
					}
				}
			}

			//Add neighborArray to the Original Array
			for (int i = 0; i < 15; i++)
			{
				for (int j = 0; j < 12; j++)
				{
					doubleHolder[i, j] += neighborArrays[i, j];
					doubleHolder[i, j] /= 5;
				}
			}

			//Set tile existance to one if over threshold
			for (int i = 0; i < 15; i++)
			{
				for (int j = 0; j < 12; j++)
				{
					if (doubleHolder[i, j] > 0.30f)
					{
						tileHolder[i, j] = 1;
						tileCount += 1;
					}
					else
					{
						tileHolder[i, j] = 0;
					}
				}
			}

			//} while (tileCount < 80 || tileCount > 140);

			return tileHolder;
		}

		public int[,] Shapes()
		{
			int[,] blockArray = new int[15, 12];
			int[,] largerArray = new int[25, 22];

			//Random RNG = new Random();
			Vector2 shapeOffset = new Vector2();
			Array.Clear(blockArray, 0, blockArray.Length);
			Array.Clear(largerArray, 0, largerArray.Length);
			List<SimpleShape> simpShap = new List<SimpleShape>();

			for (int i = 0; i < 20; i++)
			{
				simpShap.Add(new SimpleShape());
			}

			//Keep track of amount of field filled
			double percentNeeded;
			double percentFilled = 0;
			int blockCount;
			int xVal = 0;
			int yVal = 0;
			int cycleCount = 0;

			//**Implement code below maybe
			//percentNeeded = RNG.NextDouble();
			percentNeeded = 0.6f;

			do
			{
				//Create a simple shape to place in field

				//**Put in some exception handling

				//Pick a random location where the shape will fit on the screen
				shapeOffset.X = Game1.RNG.Next(2, 14);
				shapeOffset.Y = Game1.RNG.Next(2, 11);

				/*
                //**Put vector values into numbers, shaving off too large or too small values
				//**Try to find a method to shorten this code
				//Check if shape intersects pre-existing shapes
				xVal = (int)shapeOffset.X + (int)simpShap[0].Centre.X;
				yVal = (int)shapeOffset.Y + (int)simpShap[0].Centre.Y - simpShap[0].Space[0];
				if (xVal > 0 && xVal < 14)
				{
					if (yVal > 0 && yVal < 11)
					{
						if (largerArray[xVal, yVal] == 1)
						{
							continue;
						}
					}
					else
					{
						continue;
					}
				}
				else
				{
					continue;
				}

				xVal = (int)shapeOffset.X + (int)simpShap[0].Centre.X + simpShap[0].Space[1];
				yVal = (int)shapeOffset.Y + (int)simpShap[0].Centre.Y - simpShap[0].Space[1];
				if (xVal > 0 && xVal < 14)
				{
					if (yVal > 0 && yVal < 11)
					{
						if (largerArray[xVal, yVal] == 1)
						{
							continue;
						}
					}
					else
					{
						continue;
					}
				}
				else
				{
					continue;
				}

				xVal = (int)shapeOffset.X + (int)simpShap[0].Centre.X + simpShap[0].Space[2];
				yVal = (int)shapeOffset.Y + (int)simpShap[0].Centre.Y;
				if (xVal > 0 && xVal < 14)
				{
					if (yVal > 0 && yVal < 11)
					{
						if (largerArray[xVal, yVal] == 1)
						{
							continue;
						}
					}
					else
					{
						continue;
					}
				}
				else
				{
					continue;
				}

				xVal = (int)shapeOffset.X + (int)simpShap[0].Centre.X + simpShap[0].Space[3];
				yVal = (int)shapeOffset.Y + (int)simpShap[0].Centre.Y + simpShap[0].Space[3];
				if (xVal > 0 && xVal < 14)
				{
					if (yVal > 0 && yVal < 11)
					{
						if (largerArray[xVal, yVal] == 1)
						{
							continue;
						}
					}
					else
					{
						continue;
					}
				}
				else
				{
					continue;
				}

				xVal = (int)shapeOffset.X + (int)simpShap[0].Centre.X;
				yVal = (int)shapeOffset.Y + (int)simpShap[0].Centre.Y + simpShap[0].Space[4];
				if (xVal > 0 && xVal < 14)
				{
					if (yVal > 0 && yVal < 11)
					{
						if (largerArray[xVal, yVal] == 1)
						{
							continue;
						}
					}
					else
					{
						continue;
					}
				}
				else
				{
					continue;
				}

				xVal = (int)shapeOffset.X + (int)simpShap[0].Centre.X - simpShap[0].Space[5];
				yVal = (int)shapeOffset.Y + (int)simpShap[0].Centre.Y + simpShap[0].Space[5];
				if (xVal > 0 && xVal < 14)
				{
					if (yVal > 0 && yVal < 11)
					{
						if (largerArray[xVal, yVal] == 1)
						{
							continue;
						}
					}
					else
					{
						continue;
					}
				}
				else
				{
					continue;
				}

				xVal = (int)shapeOffset.X + (int)simpShap[0].Centre.X - simpShap[0].Space[6];
				yVal = (int)shapeOffset.Y + (int)simpShap[0].Centre.Y;
				if (xVal > 0 && xVal < 14)
				{
					if (yVal > 0 && yVal < 11)
					{
						if (largerArray[xVal, yVal] == 1)
						{
							continue;
						}
					}
					else
					{
						continue;
					}
				}
				else
				{
					continue;
				}

				xVal = (int)shapeOffset.X + (int)simpShap[0].Centre.X - simpShap[0].Space[7];
				yVal = (int)shapeOffset.Y + (int)simpShap[0].Centre.Y - simpShap[0].Space[7];
				if (xVal > 0 && xVal < 14)
				{
					if (yVal > 0 && yVal < 11)
					{
						if (largerArray[xVal, yVal] == 1)
						{
							continue;
						}
					}
					else
					{
						continue;
					}
				}
				else
				{
					continue;
				}
				*/

				//**fix this bloody code.. look at blockArray... shapeoffset.. x
				//Place tiles into designated area
				for (int i = 0; i < 5; i++)
				{
					for (int j = 0; j < 5; j++)
					{
						if (simpShap[cycleCount].Tiles[i, j] == 1)
						{
							largerArray[(int)shapeOffset.X + i, (int)shapeOffset.Y + j] = 1;
						}
					}
				}

				blockCount = 0;
				for (int i = 0; i < 15; i++)
				{
					for (int j = 0; j < 12; j++)
					{
						if (largerArray[i, j] == 1)
						{
							blockCount++;
						}
					}
				}
				cycleCount++;
				//percentFilled = (float)(blockCount / 180);
			} while (cycleCount < 8);
			//} while (percentFilled < percentNeeded);
			//**Needed? simpShap.Clear();

			for (int i = 0; i < 15; i++)
			{
				for (int j = 0; j < 12; j++)
				{
					blockArray[i, j] = largerArray[i + 3, j + 2];
				}
			}

			return blockArray;
		}

		public int[,] Directional()
		{
			int[,] tempArray = new int[15, 12];
			DirectionalShape dirBlocks = new DirectionalShape();

			tempArray = dirBlocks.Reset();
			return tempArray;
		}

		public int[,] CreateBorders(int[,] input)
		{
			int[,] tempArray = input;

			int[] borderCuts;

			double cutDir;
			int cutNumber, cutType;

			//Choose cut direction 0 = Horizontal
			cutDir = Game1.RNG.NextDouble();

			//Choose number of cuts 0 = 1 Cut
			cutNumber = Game1.RNG.Next(0, 2);

			if (cutDir > 0.5f)
			{

				switch (cutNumber)
				{
				case 0:
					//Choose placement of cuts
					cutType = Game1.RNG.Next(0, 2);

					switch (cutType)
					{
					case 0:
						borderCuts = new int[] { 0 };
						tempArray = CutBorders(cutDir, borderCuts, tempArray);
						break;
					case 1:
						borderCuts = new int[] { 5, 6 };
						tempArray = CutBorders(cutDir, borderCuts, tempArray);
						break;
					case 2:
						borderCuts = new int[] { 11 };
						tempArray = CutBorders(cutDir, borderCuts, tempArray);
						break;
					}

					break;
				case 1:
					cutType = Game1.RNG.Next(0, 3);

					switch (cutType)
					{
					case 0:
						borderCuts = new int[] { 0, 11 };
						tempArray = CutBorders(cutDir, borderCuts, tempArray);
						break;
					case 1:
						borderCuts = new int[] { 0, 5, 6 };
						tempArray = CutBorders(cutDir, borderCuts, tempArray);
						break;
					case 2:
						borderCuts = new int[] { 5, 6, 11 };
						tempArray = CutBorders(cutDir, borderCuts, tempArray);
						break;
					case 3:
						borderCuts = new int[] { 3, 8 };
						tempArray = CutBorders(cutDir, borderCuts, tempArray);
						break;
					}

					break;
				case 2:
					cutType = 0;

					borderCuts = new int[] { 0, 5, 6, 11 };
					tempArray = CutBorders(cutDir, borderCuts, tempArray);
					break;
				}
			}
			else
			{
				switch (cutNumber)
				{
				case 0:
					cutType = Game1.RNG.Next(0, 2);

					switch (cutType)
					{
					case 0:
						borderCuts = new int[] { 0 };
						tempArray = CutBorders(cutDir, borderCuts, tempArray);
						break;
					case 1:
						borderCuts = new int[] { 7 };
						tempArray = CutBorders(cutDir, borderCuts, tempArray);
						break;
					case 2:
						borderCuts = new int[] { 14 };
						tempArray = CutBorders(cutDir, borderCuts, tempArray);
						break;
					}

					break;
				case 1:
					cutType = Game1.RNG.Next(0, 3);
					switch (cutType)
					{
					case 0:
						borderCuts = new int[] { 0, 14 };
						tempArray = CutBorders(cutDir, borderCuts, tempArray);
						break;
					case 1:
						borderCuts = new int[] { 0, 7 };
						tempArray = CutBorders(cutDir, borderCuts, tempArray);
						break;
					case 2:
						borderCuts = new int[] { 7, 14 };
						tempArray = CutBorders(cutDir, borderCuts, tempArray);
						break;
					case 3:
						borderCuts = new int[] { 4, 10 };
						tempArray = CutBorders(cutDir, borderCuts, tempArray);
						break;
					}
					break;
				case 2:
					borderCuts = new int[] { 0, 7, 14 };
					tempArray = CutBorders(cutDir, borderCuts, tempArray);
					break;
				}
			}
			return tempArray;
		}

		public int[,] CutBorders(double direction, int[] cuts, int[,] origArray)
		{


			if (direction > 0.5f)
			{
				for (int i = 0; i < cuts.Length; i++)
				{
					for (int j = 0; j < 15; j++)
					{
						origArray[j, cuts[i]] = 0;
					}
				}
			}
			else
			{
				for (int i = 0; i < cuts.Length; i++)
				{
					for (int j = 0; j < 12; j++)
					{
						origArray[cuts[i], j] = 0;
					}
				}
			}
			return origArray;
		}

		public int[,] ReflectOnce(int[,] input)
		{
			double tempRand1, tempRand2;
			tempRand1 = Game1.RNG.NextDouble();
			tempRand2 = Game1.RNG.NextDouble();

			if (tempRand1 > 0.5f)
			{
				for (int i = 0; i < 7; i++)
				{
					for (int j = 0; j < 12; j++)
					{
						if (tempRand2 > 0.5f)
						{
							input[14 - i, j] = input[i, j];
						}
						else
						{
							input[i, j] = input[14 - i, j];
						}
					}
				}
			}
			else
			{
				for (int i = 0; i < 14; i++)
				{
					for (int j = 0; j < 6; j++)
					{
						if (tempRand2 > 0.5f)
						{
							input[i, 11 - j] = input[i, j];
						}
						else
						{
							input[i, j] = input[i, 11 - j];
						}
					}
				}
			}
			return input;
		}

		public int[,] ReflectTwice(int[,] input)
		{
			double tempRand;
			tempRand = Game1.RNG.NextDouble();

			if (tempRand > 0.5f)
			{
				for (int i = 5; i < 10; i++)
				{
					for (int j = 0; j < 12; j++)
					{
						input[9 - i, j] = input[i, j];
						input[14 - (i - 5), j] = input[i, j];
					}
				}
			}
			else
			{
				for (int i = 0; i < 14; i++)
				{
					for (int j = 4; j < 8; j++)
					{
						input[i, 7 - j] = input[i, j];
						input[i, 11 - (j - 4)] = input[i, j];
					}
				}
			}
			return input;
		}
		/*
        public int[,] method(int[,] input)
        {

        }
        */
	}
}

