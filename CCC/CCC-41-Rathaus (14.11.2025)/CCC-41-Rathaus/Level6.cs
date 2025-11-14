using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC_41_Rathaus
{
    internal class Level6
    {
        public static void Solve(string[] rawInputData, StreamWriter sw)
        {
            int count = int.Parse(rawInputData[0]);
            for (int i = 1; i < 1 + 2 * count; i += 2)
            {
                string[] lineData = rawInputData[i].Trim().Split(' ');
                int endPosX = int.Parse(lineData[0].Split(',')[0]);
                int endPosY = int.Parse(lineData[0].Split(',')[1]);
                int maxTime = int.Parse(lineData[1]);
                lineData = rawInputData[i + 1].Trim().Split(' ');
                int meteorPosX = int.Parse(lineData[0].Split(',')[0]);
                int meteorPosY = int.Parse(lineData[0].Split(',')[1]);


                var x = GetPaces(new int[] { 0, 0 }, new int[] { 3, 30 }, new int[] { 40,2 });
                Console.WriteLine(x.Vx);
                Console.WriteLine(x.Vy);
                Console.ReadLine();



            }
}

       static int calculateTime(int distance)
        {
            int time = 0;
            if (distance > 0) time += 5;
            if (distance > 1) time += 4;
            if (distance > 2) time += 3;
            if (distance > 3) time += 2;
            if (distance > 4) time += 1;
            if (distance > 5) time += (distance - 5);
            return time;
        }

        public static string AccString(int time, int dis)
        {
            string s = "";
            int actualTime = calculateTime(dis);
            while (actualTime < time) 
            {
                actualTime++;
                s += "0 ";
            }
            for (int i = 0; i < dis && i < 5; i++)
            {
                s += $"{5-i} ";
            }
            for (int i = 0; i < dis - 5; i++)
            {
                s += "1 ";
            }
            return s;
        }
        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        public static string DeAccString(int time, int dis)
        {
            return Reverse(AccString(time, dis));
        }
        public static (string Vx, string Vy) GetPaces(int[] startPos, int[] midPos, int[] endPos)
        {
            int dX1 = midPos[0] - startPos[0];
            int dY1 = midPos[1] - startPos[1];
            int dX2 = endPos[0] - midPos[0];
            int dY2 = endPos[1] - midPos[1];
            int maxTime1 = 0, maxTime2 = 0,minTime1 = 0, minTime2 = 0;
            bool XGreaterY1 = dX1 > dY1;
            bool XGreaterY2 = dX2 > dY2;

            string vX = "", vY = "";

            maxTime1 = Math.Max(calculateTime(dX1), calculateTime(dY1));
            maxTime2 = Math.Max(calculateTime(dX2), calculateTime(dY2));


            vX += AccString(maxTime1, dX1);
            vY += AccString(maxTime1, dY1);

            vX += DeAccString(maxTime2, dX2);
            vY += DeAccString(maxTime2, dY2);

            return (vX, vY);
        }
    }
}
