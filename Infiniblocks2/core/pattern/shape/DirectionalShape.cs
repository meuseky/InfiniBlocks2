using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace InfiniBlocks2
{
	public class DirectionalShape
	{
		//General Variables
		private int[,] normalBlockArray1;
		private int[,] normalBlockArray2;
		private int[,] normalBlockArray3;
		private int[,] solidBlockArray;
		private int[,] diagArray;
		private int count1, count2, count3;

		//Debug
		private SpriteFont dbFont;
		private string dbString;
		private int[] dbArray1;
		private int[,] dbArray2;
		private int[,] dbArray3;
		private bool dbCounter1, dbCounter2;

		//For reflections
		private int[,] tempArray;
		private Texture2D normalTxr;
		private Texture2D solidTxr;
		private Vector2 offset;

		private int tempRand, tempRand1, tempRand2, tempRand3;
		private SeedTypeEnumeration gameSeed;
		private CutDirEnumeration startCut;
		//Keeps track of cuts
		private int[] cuttingEdge;
		//Angle that diagonal cuts at 1-1, 2-1, 3-1
		private int cuttingAngle;
		private Random RNG = new Random();
		//Remaining length of blocks when cutting
		private int edgeLength;
		private int remainingLength;

		//Constructor
		public DirectionalShape()
		{
			//Initialise General Variables
			normalBlockArray1 = new int[15, 12];
			normalBlockArray2 = new int[15, 12];
			normalBlockArray3 = new int[15, 12];

			solidBlockArray = new int[15, 12];
			diagArray = new int[27, 12];
			tempArray = new int[15, 12];
			Array.Clear(diagArray, 0, diagArray.Length);
			offset = new Vector2(50, 50);
		}

		public int[,] Reset()
		{
			/*
            Array.Clear(normalBlockArray1, 0, normalBlockArray1.Length);
            Array.Clear(normalBlockArray2, 0, normalBlockArray2.Length);
            Array.Clear(normalBlockArray3, 0, normalBlockArray3.Length);
            count1 = 0;
            count2 = 0;
            count3 = 0;
            */

			tempRand = RNG.Next(0, 4);

			gameSeed = (SeedTypeEnumeration)tempRand;

			gameSeed = SeedTypeEnumeration.Typical;

			switch(gameSeed)
			{ 
			case SeedTypeEnumeration.Typical:

				do
				{
					Array.Clear(normalBlockArray1, 0, normalBlockArray1.Length);
					Array.Clear(normalBlockArray2, 0, normalBlockArray2.Length);
					Array.Clear(normalBlockArray3, 0, normalBlockArray3.Length);

					count1 = 0;
					count2 = 0;
					count3 = 0;

					TypicalCut(normalBlockArray1);
					TypicalCut(normalBlockArray2);
					TypicalCut(normalBlockArray3);

					for (int i = 0; i < 15; i++)
					{
						for (int j = 0; j < 12; j++)
						{
							if (normalBlockArray1[i, j] == 1)
							{
								count1++;
							}
							if (normalBlockArray2[i, j] == 1)
							{
								count2++;
							}
							if (normalBlockArray3[i, j] == 1)
							{
								count3++;
							}
						}
					}

					if (count1 > count2 && count1 > count3)
					{
						if (count3 > count2)
						{
							SwitchArrays(ref normalBlockArray2, ref normalBlockArray3);
						}
						break;
					}
					else if (count2 > count3)
					{
						SwitchArrays(ref normalBlockArray2, ref normalBlockArray3);

						if (count1 > count3)
						{
							SwitchArrays(ref normalBlockArray2, ref normalBlockArray3);
						}
					}
					else
					{
						SwitchArrays(ref normalBlockArray2, ref normalBlockArray3);

						if (count1 > count2)
						{
							SwitchArrays(ref normalBlockArray2, ref normalBlockArray3);
						}
					}

					int tempSum = 0;

					for (int i = 0; i < 15; i++)
					{
						for (int j = 0; j < 12; j++)
						{
							tempSum = 0;

							tempSum = normalBlockArray1[i, j] - normalBlockArray2[i, j];

							if (tempSum == 0 || tempSum == 1)
							{
								normalBlockArray1[i, j] = tempSum;
							}
						}
					}

					for (int i = 0; i < 15; i++)
					{
						for (int j = 0; j < 12; j++)
						{
							tempSum = 0;

							tempSum = normalBlockArray1[i, j] - normalBlockArray3[i, j];

							if (tempSum == 0 || tempSum == 1)
							{
								normalBlockArray1[i, j] = tempSum;
							}
						}
					}

					count1 = 0;
					for (int i = 0; i < 15; i++)
					{
						for (int j = 0; j < 12; j++)
						{
							if (normalBlockArray1[i, j] == 1)
							{
								count1++;
							}
						}
					}
				} while (count1 > 100 || count1 < 90);
				break;

			case SeedTypeEnumeration.Blocky:

				break;

			case SeedTypeEnumeration.Shape:

				break;

			case SeedTypeEnumeration.Holey:

				break;

			case SeedTypeEnumeration.LemonDiff:

				break;
			}
			return normalBlockArray1;
		}

		public void SwitchArrays(ref int[,] array1, ref int[,] array2)
		{
			int[,] switchTempArray = new int[15, 12];

			for (int i = 0; i < 15; i++)
			{
				for (int j = 0; j < 12; j++)
				{
					switchTempArray[i, j] = array1[i, j];
					array1[i, j] = array2[i, j];
					array2[i, j] = switchTempArray[i, j];
				}
			}

		}

		public void TypicalCut(int[,] blockArray)
		{


			tempRand1 = RNG.Next(0, 4);

			startCut = (CutDirEnumeration)tempRand1;

			switch(startCut)
			{ 
			case CutDirEnumeration.Vertical:
				cuttingEdge = new int[12]; 
				break;

			case CutDirEnumeration.Horizontal:
				cuttingEdge = new int[15];
				break;

			case CutDirEnumeration.LeftDiagonal:
				cuttingAngle = RNG.Next(1,2);
				cuttingEdge = new int[27];
				break;

			case CutDirEnumeration.RightDiagonal:
				cuttingAngle = RNG.Next(1,2);
				cuttingEdge = new int[27];
				break;
			}

			StartCutting();
			TransferCut(normalBlockArray1);
			CreateBorders();



		}


		public void StartCutting()
		{
			int p = 0;

			do
			{
				if (cuttingEdge[p] == 0)
				{
					edgeLength = cuttingEdge.Length - 1;

					GetCutLength(p, edgeLength);

					//Trying to give areas more space

					if (tempRand2 == 1)
					{
						p++;
						tempRand2 = 0;
					}

				}
				p += 3;
			} while (p < cuttingEdge.Length);

			dbArray1 = cuttingEdge;
		}

		public void GetCutLength(int j, int m)
		{
			remainingLength = m - j;

			//Should this be changed to -1
			if (remainingLength > 0)
			{
				tempRand2 = RNG.Next(1,3);


				if (tempRand2 == 1)
				{
					tempRand3 = 6;
					do
					{
						tempRand3 = RNG.Next(1, remainingLength);
					}
					while (tempRand3 > 5);

					for (int k = j; k < j + tempRand3; k++)
					{
						cuttingEdge[k] = 1;
					}
				}
			}
		}

		public void TransferCut(int[,] blockArray)
		{
			switch (startCut)
			{
			case CutDirEnumeration.Vertical:
				for (int i = 0; i < cuttingEdge.Length; i++)
				{
					if (cuttingEdge[i] == 1)
					{

						for (int j = 0; j < 15; j++)
						{
							blockArray[j, i] = 1;
						}

					}

				}
				break;

			case CutDirEnumeration.Horizontal:
				for (int i = 0; i < cuttingEdge.Length; i++)
				{
					if (cuttingEdge[i] == 1)
					{

						for (int j = 0; j < 12; j++)
						{
							blockArray[i, j] = 1;
						}

					}

				}
				break;

			case CutDirEnumeration.LeftDiagonal:
				for (int i = 0; i < cuttingEdge.Length; i++)
				{
					if (cuttingEdge[i] == 1)
					{

						for (int j = 0; j < 12; j++)
						{
							if (j <= i)
							{
								diagArray[i - j, 11 - j] = 1;
							}
						}
						for (int k = 0; k < 15; k++)
						{
							for (int m = 0; m < 12; m++)
							{
								blockArray[k, m] = diagArray[k, m];
							}
						}
					}
				}
				Array.Clear(diagArray, 0, diagArray.Length);
				break;

			case CutDirEnumeration.RightDiagonal:
				//Same as left diagonal but reflected

				for (int i = 0; i < cuttingEdge.Length; i++)
				{
					if (cuttingEdge[i] == 1)
					{

						for (int j = 0; j < 12; j++)
						{
							if (j <= i)
							{
								diagArray[i - j, 11 - j] = 1;
							}
						}
						for (int k = 0; k < 15; k++)
						{
							for (int m = 0; m < 12; m++)
							{
								blockArray[k, m] = diagArray[k, m];
							}
						}
					}
				}

				for (int m = 0; m < 12; m++)
				{
					for (int n = 0; n < 15; n++)
					{
						tempArray[(14 - n), m] = blockArray[n, m];
					}

				}

				for (int m = 0; m < 12; m++)
				{
					for (int n = 0; n < 15; n++)
					{
						blockArray[n, m] = tempArray[n, m];
					}

				}
				//blockArray = tempArray;
				break;
			}
		}

		//Drawing Method
		public void DrawMe(SpriteBatch sb)
		{
			for (int i = 0; i < 15; i++)
			{
				for (int j = 0; j < 12; j++)
				{
					if(normalBlockArray1[i,j] == 1)
					{
						sb.Draw(normalTxr, new Rectangle((int)(offset.X * i), (int)(offset.Y * j), normalTxr.Width, normalTxr.Height), Color.White);
					}
				}
			}

			/*
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    if (solidBlockArray[i, j] == 1)
                    {
                        sb.Draw(solidTxr, new Rectangle((int)(offset.X * i), (int)(offset.Y * j), solidTxr.Width, solidTxr.Height), Color.White);
                    }
                }
            }
            */

			/*
			for (int i = 0; i < dbArray1.Length; i++)
			{
				dbString = dbArray1[i].ToString();
				sb.DrawString(dbFont, dbString, new Vector2(i * 20, 10), Color.Black);
			}
			*/

			//cuttingEdge
			//normalBlockArray

			//sb.DrawString(dbFont, remainingLength.ToString(), new Vector2(20, 50), Color.Black);


			sb.DrawString(dbFont, tempRand.ToString(), new Vector2(0, 40), Color.Black);
			sb.DrawString(dbFont, tempRand1.ToString(), new Vector2(20, 40), Color.Black);
			sb.DrawString(dbFont, tempRand2.ToString(), new Vector2(40, 40), Color.Black);
			sb.DrawString(dbFont, tempRand3.ToString(), new Vector2(60, 40), Color.Black);

			dbString = startCut.ToString();
			sb.DrawString(dbFont, dbString, new Vector2(80, 40), Color.Black);




		}

		public void CreateBorders()
		{
			int[] borderCuts;

			Random RNG = new Random();
			double cutDir;
			int cutNumber, cutType;

			//Choose cut direction 0 = Horizontal
			cutDir = RNG.NextDouble();

			//Choose number of cuts 0 = 1 Cut
			cutNumber = RNG.Next(0, 2);

			if (cutDir > 0.5f)
			{

				switch (cutNumber)
				{
				case 0:
					//Choose placement of cuts
					cutType = RNG.Next(0, 2);

					switch (cutType)
					{
					case 0:
						borderCuts = new int[] { 0 };
						CutBorders(cutDir, borderCuts);
						break;
					case 1:
						borderCuts = new int[] { 5, 6 };
						CutBorders(cutDir, borderCuts);
						break;
					case 2:
						borderCuts = new int[] { 11 };
						CutBorders(cutDir, borderCuts);
						break;
					}

					break;
				case 1:
					cutType = RNG.Next(0, 3);

					switch (cutType)
					{
					case 0:
						borderCuts = new int[] { 0, 11 };
						CutBorders(cutDir, borderCuts);
						break;
					case 1:
						borderCuts = new int[] { 0, 5, 6 };
						CutBorders(cutDir, borderCuts);
						break;
					case 2:
						borderCuts = new int[] { 5, 6, 11 };
						CutBorders(cutDir, borderCuts);
						break;
					case 3:
						borderCuts = new int[] { 3, 8 };
						CutBorders(cutDir, borderCuts);
						break;
					}

					break;
				case 2:
					cutType = 0;

					borderCuts = new int[] { 0, 5, 6, 11 };
					CutBorders(cutDir, borderCuts);
					break;
				}
			}
			else
			{
				switch (cutNumber)
				{
				case 0:
					cutType = RNG.Next(0, 2);

					switch (cutType)
					{
					case 0:
						borderCuts = new int[] { 0 };
						CutBorders(cutDir, borderCuts);
						break;
					case 1:
						borderCuts = new int[] { 7 };
						CutBorders(cutDir, borderCuts);
						break;
					case 2:
						borderCuts = new int[] { 14 };
						CutBorders(cutDir, borderCuts);
						break;
					}

					break;
				case 1:
					cutType = RNG.Next(0, 3);
					switch (cutType)
					{
					case 0:
						borderCuts = new int[] { 0, 14 };
						CutBorders(cutDir, borderCuts);
						break;
					case 1:
						borderCuts = new int[] { 0, 7 };
						CutBorders(cutDir, borderCuts);
						break;
					case 2:
						borderCuts = new int[] { 7, 14 };
						CutBorders(cutDir, borderCuts);
						break;
					case 3:
						borderCuts = new int[] { 4, 10 };
						CutBorders(cutDir, borderCuts);
						break;
					}
					break;
				case 2:
					borderCuts = new int[] { 0, 7, 14 };
					CutBorders(cutDir, borderCuts);
					break;
				}
			}
		}

		public void CutBorders(double direction, int[] cuts)
		{
			if (direction > 0.5f)
			{
				for (int i = 0; i < cuts.Length; i++)
				{
					for (int j = 0; j < 15; j++)
					{
						normalBlockArray1[j, cuts[i]] = 0;
					}
				}
			}
			else
			{
				for (int i = 0; i < cuts.Length; i++)
				{
					for (int j = 0; j < 12; j++)
					{
						normalBlockArray1[cuts[i], j] = 0;
					}
				}
			}
		}
	}
}

