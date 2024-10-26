using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CCC_40_Rathaus
{
    internal class Level5
    {
        public string CalculateResult(string levelFile)
        {
            int lineNr = 0;
            List<string> inputLines = new List<string>();
            foreach (string line in File.ReadLines(levelFile))
            {
                lineNr++;
                if (lineNr == 1) continue;
                inputLines.Add(line);
            }

            List<string[]> rooms = new List<string[]>();
            foreach (string line in inputLines)
            {
                rooms.Add(IDPlacement(line));
            }

            StringBuilder stbResult = new StringBuilder(rooms.Count);
            foreach (string[] roomIds in rooms)
            {
                foreach (string roomIdLine in roomIds)
                {
                    stbResult.AppendLine(roomIdLine);
                }
                stbResult.AppendLine();
            }
            return stbResult.ToString();
        }

        string[] IDPlacement(string input)
        {
            string[] values = input.Split(' ');
            int x = int.Parse(values[0]), y = int.Parse(values[1]), desks = int.Parse(values[2]), currentId = 1, currentdesks = 0;
            int[,] boardAccess = new int[x, y];
            int[,] boardState = new int[x, y];
            int[][] priority = new int[][] { new int[] { 0, 0 }, new int[] { -1, -1 } }; //x,y
            while (currentdesks < desks)
            {
                
                #region Priorities
                int[] cp = new int[2];
                int usedP;
                if (priority[1][0] == -1)
                {
                    cp = priority[0];
                    usedP = 0;
                }
                else if (priority[0][0] == -1)
                {
                    cp = priority[1];
                    usedP = 1;
                }
                else if (priority[0][0] < priority[1][0])
                {
                    cp = priority[0];
                    usedP = 0;
                }
                else if (priority[0][0] > priority[1][0])
                {
                    cp = priority[1];
                    usedP = 1;
                }
                else if (priority[0][1] < priority[1][1])
                {
                    cp = priority[0];
                    usedP = 0;
                }
                else if (priority[0][1] > priority[1][1])
                {
                    cp = priority[1];
                    usedP = 1;
                }
                else
                {
                    cp = priority[0];
                    usedP = 0;
                }
                #endregion

                if (cp[1] + 1 < y && cp[0] + 2 < x) //RECHTS
                {

                    if (cp[1] + 3 < y)
                    {
                        priority[usedP] = new int[] { cp[0], cp[1] + 3 };
                        boardState[cp[0], cp[1]] = 1;
                        boardState[cp[0], cp[1] + 1] = 1;
                        currentdesks++;
                        while (true)
                        {
                            cp[0] += 2;
                            if (cp[0] + 2 < x || cp[0] + 1 == x)
                            {
                                boardState[cp[0], cp[1]] = 1;
                                boardState[cp[0], cp[1] + 1] = 1;
                                currentdesks++;
                            }
                            else
                            {
                                if (priority[(usedP + 1) % 2][0] == -1)
                                {
                                    priority[1][0] = cp[0];
                                    priority[1][1] = cp[1];
                                }
                                break;
                            }
                        }
                    }
                    else if (cp[1] + 2 == y)
                    {
                        priority[usedP] = new int[] { -1, -1 };
                        boardState[cp[0], cp[1]] = 1;
                        boardState[cp[0], cp[1] + 1] = 1;
                        currentdesks++;
                        while (true)
                        {
                            cp[0] += 2;
                            if (cp[0] + 2 < x)
                            {
                                boardState[cp[0], cp[1]] = 1;
                                boardState[cp[0], cp[1] + 1] = 1;
                                currentdesks++;
                            }
                            else break;
                        }
                    }
                    else if (cp[0] + 1 < x) //UNTEN
                    {

                        if (cp[0] + 3 < x)
                        {
                            priority[usedP] = new int[] { cp[0] + 3, cp[1] };
                            boardState[cp[0], cp[1]] = 1;
                            boardState[cp[0] + 1, cp[1]] = 1;
                            currentdesks++;
                            while (true)
                            {
                                cp[1] += 2;
                                if (cp[1]< y)
                                {
                                    boardState[cp[0], cp[1]] = 1;
                                    boardState[cp[0] + 1, cp[1]] = 1;
                                    currentdesks++;
                                }
                                else
                                {
                                    if (priority[(usedP + 1) % 2][0] == -1)
                                    {
                                        priority[1][0] = cp[0];
                                        priority[1][1] = cp[1];
                                    }
                                    break;
                                }
                            }
                        }
                        else if (cp[0] + 2 <= x)
                        {
                            priority[usedP] = new int[] { -1, -1 };
                            boardState[cp[0], cp[1]] = 1;
                            boardState[cp[0] + 1, cp[1]] = 1;
                            currentdesks++;
                            while (true)
                            {
                                cp[1] += 2;
                                if (cp[1] < y && boardState[cp[0], cp[1]] != 1)
                                {
                                    boardState[cp[0], cp[1]] = 1;
                                    boardState[cp[0] + 1, cp[1]] = 1;
                                    currentdesks++;
                                }
                                else break;
                            }
                        }
                    }
                }
                else if (cp[0] + 1 < x ) //UNTEN
                {

                    if (cp[0] + 3 < x)
                    {
                        priority[usedP] = new int[] { cp[0]+3, cp[1]};
                        boardState[cp[0], cp[1]] = 1;
                        boardState[cp[0]+1, cp[1]] = 1;
                        currentdesks++;
                        while (true)
                        {
                            cp[1] += 2;
                            if (cp[1] < y)
                            {
                                boardState[cp[0], cp[1]] = 1;
                                boardState[cp[0] + 1, cp[1]] = 1;
                                currentdesks++;
                            }
                            else
                            {
                                if (priority[(usedP+1)%2][0] == -1)
                                {
                                    priority[1][0] = cp[0];
                                    priority[1][1] = cp[1];
                                }
                                break;
                            }
                        }
                    }
                    else if (cp[0] + 2 <= x)
                    {
                        priority[usedP] = new int[] { -1, -1 };
                        boardState[cp[0], cp[1]] = 1;
                        boardState[cp[0] + 1, cp[1]] = 1;
                        currentdesks++;
                        while (true)
                        {
                            cp[1] += 2;
                            if (cp[1] < y && boardState[cp[0],cp[1]] != 1)
                            {
                                boardState[cp[0], cp[1]] = 1;
                                boardState[cp[0] + 1, cp[1] ] = 1;
                                currentdesks++;
                            }
                            else break;
                        }
                    }
                }
                Console.WriteLine(currentdesks);
                Console.ReadKey(true);

            }
            string[] output = new string[y];
            for (int myY = 0; myY < y; myY++)
            {
                for (int myX = 0; myX < x; myX++)
                {
                    output[myY] += boardState[myX,myY] == 1 ? "X" : ".";
                }
            }
            
            return output;
        }

   //     bool FillDesks(bool richtung, bool ausrichtung, int startX, int startY, int maxX, int maxY, out Tuple<int,int> nextPriority)
   //     {
			//nextPriority = null;
			//if (startX >= maxX || startY >= maxY) return false;

			//if (richtung)   // rechts
   //         {
   //             if (ausrichtung)    // vertikal
   //             {
   //                 if (startY + 1 >= maxY) return FillDesks (richtung, !ausrichtung, startX, startY, maxX, maxY, out nextPriority);

   //             }
   //             else    // horizontal
   //             {

   //             }
   //         }
   //         else // runter
   //         {
   //             if (ausrichtung)    // vertikal
   //             {

   //             }
   //             else    // horizontal
   //             {

   //             }
   //         }
   //     }
    }
}
