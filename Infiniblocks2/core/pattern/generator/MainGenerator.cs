using System;

namespace InfiniBlocks2
{
	class MainGenerator
	{        
		private int[,] blockPos;
		private int[,] inputArray1;
		private int[,] inputArray2;
		private BlockGenerator blockGen;
		private ColorGenerator colorGen;

		//***Need to convert int array to mabey a struct to hold color/health/type info***
		public MainGenerator()
		{
			blockPos = new int[15, 12];
			inputArray1 = new int[15, 12];
			inputArray2 = new int[15, 12];
			blockGen = new BlockGenerator();
			colorGen = new ColorGenerator();
		}

		public int[,] ResetBlocks()
		{
			int[,] tempArray = new int[15, 12];
			int choice1, choice2;

			choice1 = Game1.RNG.Next(0, 1);
			choice2 = Game1.RNG.Next(0, 2);

			switch (choice1)
			{
			/*ase 0:
                    inputArray1 = blockGen.Perlin();
                    break;*/
			case 0:
				inputArray1 = blockGen.Shapes();
				break;
			case 1:
				inputArray1 = blockGen.Directional();
				break;
			}
			switch (choice2)
			{
			case 0:
				inputArray2 = blockGen.Perlin();
				break;
			case 1:
				inputArray2 = blockGen.Shapes();
				break;
			case 2:
				inputArray2 = blockGen.Directional();
				break;
			}

			choice1 = Game1.RNG.Next(0, 2);
			choice2 = Game1.RNG.Next(0, 2);

			switch (choice1)
			{
			case 0:
				inputArray1 = blockGen.CreateBorders(inputArray1);
				break;
			case 1:
				inputArray1 = blockGen.ReflectOnce(inputArray1);
				break;
			case 2:
				inputArray1 = blockGen.ReflectTwice(inputArray1);
				break;
			}
			switch (choice2)
			{
			case 0:
				inputArray2 = blockGen.CreateBorders(inputArray2);
				break;
			case 1:
				inputArray2 = blockGen.ReflectOnce(inputArray2);
				break;
			case 2:
				inputArray2 = blockGen.ReflectTwice(inputArray2);
				break;
			}

			choice1 = Game1.RNG.Next(0, 2);

			switch(choice1)
			{
			case 0:
				tempArray = AddArrays(inputArray1, inputArray2);
				break;
			case 1:
				inputArray2 = SubtractArrays(inputArray1, inputArray2);
				break;
			case 2:
				tempArray = inputArray1;
				break;
			}

			return tempArray;
		}

		public int[,] ResetColors()
		{
			int[,] tempArray = new int[15, 12];
			int[,] tempArray2 = new int[15, 12];
			int colorMethod;

			colorMethod = Game1.RNG.Next(0, 5);
			switch (colorMethod)
			{
			case 0: case 1:
				tempArray = colorGen.Walk();
				break;
			case 2: case 3: case 4:
				tempArray = colorGen.Stripes();
				break;
			case 5:
				tempArray = colorGen.RandomColor();
				break;
			}

			colorMethod = Game1.RNG.Next(0, 2);
			switch (colorMethod)
			{
			case 0:

				break;
			case 1:
				tempArray = blockGen.ReflectOnce(tempArray);
				break;
			case 2:
				tempArray = blockGen.ReflectTwice(tempArray);
				break;
			}

			colorMethod = Game1.RNG.Next(0, 5);
			switch (colorMethod)
			{
			case 0: case 1:
				tempArray = colorGen.Walk();
				break;
			case 2: case 3: case 4:
				tempArray = colorGen.Stripes();
				break;
			case 5:
				tempArray = colorGen.RandomColor();
				break;
			}

			colorMethod = Game1.RNG.Next(0, 2);
			switch (colorMethod)
			{
			case 1:
				colorMethod = Game1.RNG.Next(0, 1);
				if (colorMethod == 0)
					for (int i = 0; i < 15; i++)
					{
						colorMethod = Game1.RNG.Next(0, 2);
						if (colorMethod == 0)
						{
							for (int j = 0; j < 12; j++)
							{
								tempArray[i, j] = tempArray2[i, j];
							}
						}
					}
				else
				{
					for (int i = 0; i < 12; i++)
					{
						colorMethod = Game1.RNG.Next(0, 2);
						if (colorMethod == 0)
						{
							for (int j = 0; j < 15; j++)
							{
								tempArray[j, i] = tempArray2[j, i];
							}
						}
					}
				}
				break;
			}


			return tempArray;
		}

		public int[,] AddArrays(int[,] tempArray1, int[,] tempArray2)
		{
			int[,] outputArray = new int[15,12];

			for (int i = 0; i < 15; i++)
			{
				for (int j = 0; j < 12; j++)
				{
					outputArray[i, j] = tempArray1[i, j];
				}
			}
			for (int i = 0; i < 15; i++)
			{
				for (int j = 0; j < 12; j++)
				{
					if (tempArray2[i, j] == 1)
					{
						outputArray[i, j] = 1;
					}
				}
			}
			return outputArray;
		}

		public int[,] SubtractArrays(int[,] tempArray1, int[,] tempArray2)
		{
			int[,] outputArray = new int[15, 12];

			for (int i = 0; i < 15; i++)
			{
				for (int j = 0; j < 12; j++)
				{
					outputArray[i, j] = tempArray1[i, j];
				}
			}
			for (int i = 0; i < 15; i++)
			{
				for (int j = 0; j < 12; j++)
				{
					if (tempArray2[i, j] == 0)
					{
						outputArray[i, j] = 0;
					}
				}
			}
			return outputArray;
		}
	}
}

