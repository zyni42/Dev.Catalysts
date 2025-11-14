using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace CCC_41_Rathaus
{
    internal class Level
    {
        public static void Solve1(string[] rawInputData, StreamWriter sw)
        {
            int count = int.Parse(rawInputData[0]);
            for (int i = 1; i < 1 + count; i++)
            {
                string[] lineData = rawInputData[i].Trim().Split(' ');
                int sum = 0;
                foreach (string line in lineData)
                {
                    sum += int.Parse(line);
                }
                sw.WriteLine(sum);
            }
        }
        public static void Solve2(string[] rawInputData, StreamWriter sw)
        {
            int count = int.Parse(rawInputData[0]);
            for (int i = 1; i < 1 + count; i++)
            {
                string[] lineData = rawInputData[i].Trim().Split(' ');
                int time = 0;
                int pos = 0;
                foreach (string line in lineData)
                {
                    if (int.Parse(line) == 0)
                    {
                        time++;
                    }
                    else
                    {
                        pos += Math.Sign(int.Parse(line));
                        time += Math.Abs(int.Parse(line));
                    }

                }
                sw.WriteLine(pos + " " + time);
            }
        }
        public static void Solve3(string[] rawInputData, StreamWriter sw)
        {
            int count = int.Parse(rawInputData[0]);
            for (int i = 1; i < 1 + count; i++)
            {
                string[] lineData = rawInputData[i].Trim().Split(' ');
                int endPos = int.Parse(lineData[0]);
                int maxTime = int.Parse(lineData[1]);

                int currentSpeedStep = 0, currentPos = 0;

                sw.Write($"0 ");

                while (currentSpeedStep + 1 <= (Math.Abs(endPos) - currentPos) && currentSpeedStep < 5)
                {
                    currentPos++;
                    currentSpeedStep++;
                    sw.Write($"{Math.Sign(endPos) * (6 - currentSpeedStep)} ");
                }
                while (currentSpeedStep <= (Math.Abs(endPos) - currentPos))
                {
                    currentPos++;
                    sw.Write($"{Math.Sign(endPos) * (6 - currentSpeedStep)} ");
                }
                for (; --currentSpeedStep > 0;)
                {
                    sw.Write($"{Math.Sign(endPos) * (6 - currentSpeedStep)} ");
                }

                sw.WriteLine($"0");

            }
        }
        public static void Solve4(string[] rawInputData, StreamWriter sw)
        {
            int count = int.Parse(rawInputData[0]);
            for (int i = 1; i < 1 + count; i++)
            {
                string[] lineData = rawInputData[i].Trim().Split(' ');
                int endPosX = int.Parse(lineData[0].Split(',')[0]);
                int endPosY = int.Parse(lineData[0].Split(',')[1]);
                int maxTime = int.Parse(lineData[1]);

                int currentSpeedStep = 0, currentPos = 0;

                sw.Write($"0 ");

                while (currentSpeedStep + 1 <= (Math.Abs(endPosX) - currentPos) && currentSpeedStep < 5)
                {
                    currentPos++;
                    currentSpeedStep++;
                    sw.Write($"{Math.Sign(endPosX) * (6 - currentSpeedStep)} ");
                }
                while (currentSpeedStep <= (Math.Abs(endPosX) - currentPos))
                {
                    currentPos++;
                    sw.Write($"{Math.Sign(endPosX) * (6 - currentSpeedStep)} ");
                }
                for (; --currentSpeedStep > 0;)
                {
                    sw.Write($"{Math.Sign(endPosX) * (6 - currentSpeedStep)} ");
                }

                sw.WriteLine($"0");
                currentSpeedStep = 0;
                currentPos = 0;
                sw.Write($"0 ");

                while (currentSpeedStep + 1 <= (Math.Abs(endPosY) - currentPos) && currentSpeedStep < 5)
                {
                    currentPos++;
                    currentSpeedStep++;
                    sw.Write($"{Math.Sign(endPosY) * (6 - currentSpeedStep)} ");
                }
                while (currentSpeedStep <= (Math.Abs(endPosY) - currentPos))
                {
                    currentPos++;
                    sw.Write($"{Math.Sign(endPosY) * (6 - currentSpeedStep)} ");
                }
                for (; --currentSpeedStep > 0;)
                {
                    sw.Write($"{Math.Sign(endPosY) * (6 - currentSpeedStep)} ");
                }

                sw.WriteLine($"0");
                sw.WriteLine();

            }
        }
        public static void Solve5(string[] rawInputData, StreamWriter sw)
        {
            int count = int.Parse(rawInputData[0]);
            for (int i = 1; i < 1 + 2*count; i += 2)
            {
                if (i == 1975) ;
                string[] lineData = rawInputData[i].Trim().Split(' ');
                int endPosX = int.Parse(lineData[0].Split(',')[0]);
                int endPosY = int.Parse(lineData[0].Split(',')[1]);
                int maxTime = int.Parse(lineData[1]);
                lineData = rawInputData[i + 1].Trim().Split(' ');
                int meteorPosX = int.Parse(lineData[0].Split(',')[0]);
                int meteorPosY = int.Parse(lineData[0].Split(',')[1]);

                PathFindingAndWrite(maxTime, endPosX, endPosY, meteorPosX, meteorPosY, sw);

            }
        }
        static int PathFindingAndWrite(int maxTime, int endX, int endY, int meteorX, int meteorY, StreamWriter sw)
        {
            int[] meteorPos = { meteorX, meteorY };
            int[] destPos = { endX, endY };
            int[] myPos = { 0, 0 }, nextMove;
            int directionX= 0, directionY = 0;
            List<int[]> Path = new List<int[]>() { myPos };
            List<int[]> possibleMoves = new List<int[]>();
            while (myPos[0] != endX || myPos[1] != endY)
            {
                possibleMoves.Clear();
                int[] nowMove = new int[] { myPos[0] - 1, myPos[1],0 };
                nowMove[2] = Grade(nowMove, myPos, destPos, directionX, directionY);
                if (!isInPerimeter(nowMove, meteorPos) && !Path.Any(move => move[0] == nowMove[0] && move[1] == nowMove[1])) possibleMoves.Add(nowMove);
                nowMove = new int[] { myPos[0] + 1, myPos[1],  0};
                nowMove[2] = Grade(nowMove, myPos, destPos, directionX, directionY);
                if (!isInPerimeter(nowMove, meteorPos) && !Path.Any(move => move[0] == nowMove[0] && move[1] == nowMove[1])) possibleMoves.Add(nowMove);
                nowMove = new int[] { myPos[0], myPos[1] - 1 ,0};
                nowMove[2] = Grade(nowMove, myPos, destPos, directionX, directionY);
                if (!isInPerimeter(nowMove, meteorPos) && !Path.Any(move => move[0] == nowMove[0] && move[1] == nowMove[1])) possibleMoves.Add(nowMove);
                nowMove = new int[] { myPos[0], myPos[1] + 1 ,0};
                nowMove[2] = Grade(nowMove, myPos, destPos, directionX, directionY);
                if (!isInPerimeter(nowMove, meteorPos) && !Path.Any(move => move[0] == nowMove[0] && move[1] == nowMove[1])) possibleMoves.Add(nowMove);

                possibleMoves = possibleMoves.OrderByDescending(move => move[2]).ThenBy(move => Math.Abs(destPos[1]-move[1])).ThenBy(move => destPos[1] - move[1]).ToList();
                nextMove = possibleMoves.FirstOrDefault();

                directionX = nextMove[0] - myPos[0];
                directionY = nextMove[1] - myPos[1];
                Path.Add(nextMove);
                myPos = Path.Last();
            }
            List<int[,]> Lines = new List<int[,]>();
            int y = Path[1][1] - Path[0][1], curY;
            int x = Path[1][0] - Path[0][0], curX;
            myPos = Path.FirstOrDefault();
            for (int i = 2; i < Path.Count; i++)
            {
                curY = Path[i][1] - Path[i - 1][1];
                curX = Path[i][0] - Path[i - 1][0];
                if (curX != x || curY != y)
                {
                    Lines.Add(new int[,] { { myPos[0], myPos[1] }, { Path[i - 1][0], Path[i - 1][1] } });
                    myPos = Path[i - 1];
                    x = curX;
                    y = curY;
                }
                if (i == Path.Count - 1)
                {
                    Lines.Add(new int[,] { { myPos[0], myPos[1] }, { Path[i][0], Path[i][1] } });
                }
                

            }
            Console.WriteLine($"Count: {Path.Count} , Lines: {Lines.Count}");
            string vX = "0 ", vY = "0 ";
            int totalTime = 2;
            foreach (int[,] line in Lines)
            {
                var a = DoMovementString(line[0, 0], line[0, 1], line[1, 0], line[1, 1], out int timeX, out int timeY);
                totalTime += Math.Max(timeX, timeY);
                vX += a.Item1.Trim() + " ";
                vY += a.Item2.Trim() + " ";
            }

            sw.WriteLine(vX + "0");
            sw.WriteLine(vY + "0");
            if (totalTime > maxTime) throw new Exception();
            return 0;
        }
        static bool isInPerimeter(int[] pos, int[] meteorPos)
        {
            for (int x = meteorPos[0] - 2; x <= meteorPos[0] + 2; x++)
            {
                for (int y = meteorPos[1] - 2; y <= meteorPos[1] + 2; y++)
                {
                    if (pos[0] == x && pos[1] == y) return true;
                }
            }
            return false;
        }
        static int Grade(int[] pos, int[] prev, int[] dest, int dirX, int dirY)
        {
            int value = 0, myDirX, myDirY;
            if (Math.Abs(dest[1] - pos[1]) + Math.Abs(dest[0] - pos[0]) < Math.Abs(dest[1] - prev[1]) + Math.Abs(dest[0] - prev[0])) value+=2;
            else if (Math.Abs(dest[1] - pos[1]) + Math.Abs(dest[0] - pos[0]) > Math.Abs(dest[1] - prev[1]) + Math.Abs(dest[0] - prev[0])) value-=2;
            
            myDirX = pos[0] - prev[0];
            myDirY = pos[1] - prev[1];
            if (myDirY == dirY && myDirX == dirX) value++;
            return value;
        }






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






	}
}
