using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC41_Sandbox
{
	internal class Program
	{
		// static void Main(string[] args)
		// {
		// 	int timeX, timeY;

		// 	Tuple<string, string> result = DoMovementString(0, 0, 5, 0, out timeX, out timeY);
		// 	Console.WriteLine("0/0 -> 5/0");
		// 	Console.WriteLine($"X({timeX}): {result.Item1}");
		// 	Console.WriteLine($"Y({timeY}): {result.Item2}");
		// 	Console.WriteLine();

		// 	Console.WriteLine("0/0 -> -7/0");
		// 	Tuple<string, string> result2 = DoMovementString(0, 0, -7, 0, out timeX, out timeY);
		// 	Console.WriteLine($"X({timeX}): {result2.Item1}");
		// 	Console.WriteLine($"Y({timeY}): {result2.Item2}");
		// 	Console.WriteLine();

		// 	Console.WriteLine("0/0 -> 0/5");
		// 	Tuple<string, string> result3 = DoMovementString(0, 0, 0, 5, out timeX, out timeY);
		// 	Console.WriteLine($"X({timeX}): {result3.Item1}");
		// 	Console.WriteLine($"Y({timeY}): {result3.Item2}");
		// 	Console.WriteLine();

		// 	Console.WriteLine("0/0 -> 0/-7");
		// 	Tuple<string, string> result4 = DoMovementString(0, 0, 0, -7, out timeX, out timeY);
		// 	Console.WriteLine($"X({timeX}): {result4.Item1}");
		// 	Console.WriteLine($"Y({timeY}): {result4.Item2}");
		// 	Console.WriteLine();

		// 	Console.WriteLine("0/0 -> 5/-7");
		// 	Tuple<string, string> result5 = DoMovementString(0, 0, 5, -7, out timeX, out timeY);
		// 	Console.WriteLine($"X({timeX}): {result5.Item1}");
		// 	Console.WriteLine($"Y({timeY}): {result5.Item2}");
		// 	Console.WriteLine();
		// }

		static Tuple<string, string> DoMovementString(int startX, int startY, int endX, int endY, out int timeX, out int timeY)
		{
			timeX = 0;
			timeY = 0;

			string xMoves = string.Empty;
			string yMoves = string.Empty;

			int currentSpeedStep = 0, currentPos = 0;
			int xDir = 0;
			int yDir = 0;
			int distance = 0;
			int deltaTimeX = 0;
			int deltaTimeY = 0;

			if (startY != endY && startX == endX) // move Y
			{
				yDir = 1;
				distance = endY - startY;
			}
			else if (startX != endX && startY == endY) // move X
			{
				xDir = 1;
				distance = endX - startX;
			}
			else
			{
				throw new InvalidOperationException("*** MEHR ALS EINE RICHTUNG angegeben; nur X oder Y erwartet! ***");
			}

			while (currentSpeedStep + 1 <= (Math.Abs(distance) - currentPos) && currentSpeedStep < 5)
			{
				currentPos++;
				currentSpeedStep++;

				deltaTimeX = Math.Sign(distance) * (6 - currentSpeedStep) * xDir;
				deltaTimeY = Math.Sign(distance) * (6 - currentSpeedStep) * yDir;

				if (xDir == 0)
				{
					for (int i = 0; i < Math.Abs(deltaTimeY); i++)
					{
						xMoves += "0 ";
						timeX++;
					}

					yMoves += $"{deltaTimeY} ";
					timeY += Math.Abs(deltaTimeY);
				}
				else
				{
					for (int i = 0; i < Math.Abs(deltaTimeX); i++)
					{
						yMoves += "0 ";
						timeY++;
					}

					xMoves += $"{deltaTimeX} ";
					timeX += Math.Abs(deltaTimeX);
				}
			}
			while (currentSpeedStep <= (Math.Abs(distance) - currentPos))
			{
				currentPos++;

				deltaTimeX = Math.Sign(distance) * (6 - currentSpeedStep) * xDir;
				deltaTimeY = Math.Sign(distance) * (6 - currentSpeedStep) * yDir;

				if (xDir == 0)
				{
					for (int i = 0; i < Math.Abs(deltaTimeY); i++)
					{
						xMoves += "0 ";
						timeX++;
					}

					yMoves += $"{deltaTimeY} ";
					timeY += Math.Abs(deltaTimeY);
				}
				else
				{
					for (int i = 0; i < Math.Abs(deltaTimeX); i++)
					{
						yMoves += "0 ";
						timeY++;
					}

					xMoves += $"{deltaTimeX} ";
					timeX += Math.Abs(deltaTimeX);
				}
			}
			for (; --currentSpeedStep > 0;)
			{
				deltaTimeX = Math.Sign(distance) * (6 - currentSpeedStep) * xDir;
				deltaTimeY = Math.Sign(distance) * (6 - currentSpeedStep) * yDir;

				if (xDir == 0)
				{
					for (int i = 0; i < Math.Abs(deltaTimeY); i++)
					{
						xMoves += "0 ";
						timeX++;
					}

					yMoves += $"{deltaTimeY} ";
					timeY += Math.Abs(deltaTimeY);
				}
				else
				{
					for (int i = 0; i < Math.Abs(deltaTimeX); i++)
					{
						yMoves += "0 ";
						timeY++;
					}

					xMoves += $"{deltaTimeX} ";
					timeX += Math.Abs(deltaTimeX);
				}
			}

			return new Tuple<string, string>(xMoves.Trim(), yMoves.Trim());
		}

		static double CalcDistance (int startX, int startY, int endX, int endY)
		{
			return Math.Sqrt(Math.Pow(Math.Abs(endX - startX), 2) + Math.Pow(Math.Abs(endY - startY), 2));
		}

		static Tuple<int,int> CalcDirection (int startX, int startY, int endX, int endY)
		{
			return new Tuple<int, int>(endX - startX, endY - startY);
		}

		static void CalcShortestPath (Tuple<int,int> start, Tuple<int,int> obstacle, Tuple<int,int> destination)
		{
			// Check direct connection
			double distance = CalcDistance(start.Item1, start.Item2, destination.Item1, destination.Item2);
			Tuple<int,int> vector = CalcDirection(start.Item1, start.Item2, destination.Item1, destination.Item2);
		}

		static bool CheckCollision (Tuple<int,int> start, Tuple<int,int> vector, double distance, Tuple<int,int> obstacle)
		{
			throw new NotImplementedException();

			Tuple<double, double> deltaVector = new Tuple<double, double>(1, vector.Item2 / vector.Item1);
			//Tuple<Tuple<int, int>, Tuple<int, int>, Tuple<int, int>, Tuple<int, int>> obstacleBounds = new Tuple<Tuple<int, int>, Tuple<int, int>, Tuple<int, int>, Tuple<int, int>>(
			//		new Tuple<int, int> (obstacle.Item1 - 3, obstacle.Item2 + 3),	// top left
			//		new Tuple<int, int> (obstacle.Item1 + 3, obstacle.Item2 + 3),	// top right
			//		new Tuple<int, int> (obstacle.Item1 + 3, obstacle.Item2 - 3),	// bottom right
			//		new Tuple<int, int> (obstacle.Item1 - 3, obstacle.Item2 - 3)	// bottom left
			//	);
			Tuple<int, int, int, int> obstacleBounds = new Tuple<int, int, int, int>(
				obstacle.Item1 - 3, // left
				obstacle.Item2 + 3, // top
				obstacle.Item1 + 3, // right
				obstacle.Item2 - 3	// bottom
				);
			double checkX = start.Item1, checkY = start.Item2;
			for (double delta = 0; delta <= distance; )
			{
				checkX += deltaVector.Item1;
				checkY += deltaVector.Item2;

				if (checkX >=  obstacleBounds.Item1 && checkX <= obstacleBounds.Item3 && checkY >= obstacleBounds.Item4 && checkY <= obstacleBounds.Item2)
				{
					// collision
				}
			}
		}
	}
}
