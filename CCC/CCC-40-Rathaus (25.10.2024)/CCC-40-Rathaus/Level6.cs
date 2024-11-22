using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CCC_40_Rathaus
{
    internal class Level6
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

        bool IsFree(int[,] boardstate, int[] startpos, int dirX, int dirY)
        {
            for (int x = 0; x < dirX; x++)
            {
                for (int y = 0; y < dirY; y++)
                {
                    if (startpos[1] + y >= boardstate.GetLength(1) || startpos[0] + x >= boardstate.GetLength(0) || startpos[0] + x < 0 || startpos[1] + y < 0)
                    {
                        continue;
                    }
                    else if (boardstate[startpos[0] + x, startpos[1] + y] != 0) return false;
                }
            }
            return true;
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
                else if (priority[1][0] < priority[0][0])
                {
                    cp = priority[0];
                    usedP = 0;
                }
                else if (priority[1][0] > priority[0][0])
                {
                    cp = priority[1];
                    usedP = 1;
                }
                else if (priority[1][1] < priority[0][1])
                {
                    cp = priority[0];
                    usedP = 0;
                }
                else if (priority[1][1] > priority[0][1])
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



                //ein baustein geht sich stehend aus
                if (cp[1] + 1 < y && cp[0] < x) //RECHTS
                {
                    //zahlt sich aus ist noch genug platz
                    if (cp[1] + 3 < y && cp[0] + 2 < x)
                    {
                        priority[usedP] = new int[] { cp[0], cp[1] + 3 };
                        boardState[cp[0], cp[1]] = 1;
                        boardState[cp[0], cp[1] + 1] = 1;
                        currentdesks++;
                        while (true)
                        {
                            cp[0] += 2;
                            if ((cp[0] + 2 < x || cp[0] + 1 == x) && IsFree(boardState, new int[] { cp[0] - 1, cp[1] - 1 }, 3, 4))
                            {
                                boardState[cp[0], cp[1]] = 1;
                                boardState[cp[0], cp[1] + 1] = 1;
                                currentdesks++;
                            }
                            else
                            {
                                if (priority[(usedP + 1) % 2][0] == -1 && cp[0] < x && cp[1] < y && IsFree(boardState, new int[] { cp[0] - 1, cp[1] - 1 }, 4, 4))
                                {
                                    priority[(usedP + 1) % 2][0] = cp[0];
                                    priority[(usedP + 1) % 2][1] = cp[1];
                                }
                                break;
                            }
                        }
                    }
                    //ein tisch geht sich genau stehend aus oder es ist die WALL
                    else if (cp[1] + 2 == y || cp[0] + 1 == x)
                    {
                        priority[usedP] = new int[] { -1, -1 };
                        boardState[cp[0], cp[1]] = 1;
                        boardState[cp[0], cp[1] + 1] = 1;
                        currentdesks++;
                        while (true)
                        {
                            cp[0] += 2;
                            if (cp[0] < x && IsFree(boardState, new int[] { cp[0] - 1, cp[1] - 1 }, 3, 4))
                            {
                                boardState[cp[0], cp[1]] = 1;
                                boardState[cp[0], cp[1] + 1] = 1;
                                currentdesks++;
                            }
                            else break;
                        }
                    }

                    else if (cp[0] + 1 < x && cp[1] < y && IsFree(boardState, new int[] { cp[0] - 1, cp[1] - 1 }, 4, 3))//UNTEN
                    {
                        //priority lässt sich neu platzieren
                        if (cp[0] + 3 < x)
                        {
                            if (IsFree(boardState, new int[] { cp[0] + 3 - 1, cp[1] - 1 }, 3, 3)) priority[usedP] = new int[] { cp[0] + 3, cp[1] };
                            else if (IsFree(boardState, new int[] { cp[0] + 3 - 1, cp[1] + 1 - 1 }, 3, 3)) priority[usedP] = new int[] { cp[0] + 3, cp[1] + 1 };
                            boardState[cp[0], cp[1]] = 1;
                            boardState[cp[0] + 1, cp[1]] = 1;
                            currentdesks++;
                            while (true)
                            {
                                cp[1] += 2;
                                if ((cp[1] + 2 < y || cp[1] + 1 == y) && IsFree(boardState, new int[] { cp[0] - 1, cp[1] - 1 }, 4, 3))
                                {
                                    boardState[cp[0], cp[1]] = 1;
                                    boardState[cp[0] + 1, cp[1]] = 1;
                                    currentdesks++;
                                }
                                else break;

                            }
                        }
                        //ansonsten einfach auffüllen
                        else
                        {
                            priority[usedP] = new int[] { -1, -1 };
                            boardState[cp[0], cp[1]] = 1;
                            boardState[cp[0] + 1, cp[1]] = 1;
                            currentdesks++;
                            while (true)
                            {
                                cp[1] += 2;
                                if ((cp[1] + 2 < y || cp[1] + 1 == y) && IsFree(boardState, new int[] { cp[0] - 1, cp[1] - 1 }, 4, 3))
                                {
                                    boardState[cp[0], cp[1]] = 1;
                                    boardState[cp[0] + 1, cp[1]] = 1;
                                    currentdesks++;
                                }
                                else break;
                            }
                        }
                    }
                    else
                    {
                        if (IsFree(boardState, new int[] { cp[0] + 2 - 1, cp[1] - 1 }, 3, 3)) priority[usedP] = new int[] { cp[0] + 2, cp[1] };
                        else if (IsFree(boardState, new int[] { cp[0] + 2 - 1, cp[1] + 1 - 1 }, 3, 3)) priority[usedP] = new int[] { cp[0] + 2, cp[1] + 1 };
                        boardState[cp[0], cp[1]] = 1;
                        boardState[cp[0], cp[1] + 1] = 1;
                        currentdesks++;
                    }
                }
                else if (cp[0] + 1 < x && cp[1] < y && IsFree(boardState, new int[] { cp[0] - 1, cp[1] - 1 }, 4, 3)) //UNTEN
                {
                    //priority lässt sich neu plazieren
                    if (cp[0] + 3 < x)
                    {
                        priority[usedP] = new int[] { cp[0] + 3, cp[1] };
                        boardState[cp[0], cp[1]] = 1;
                        boardState[cp[0] + 1, cp[1]] = 1;
                        currentdesks++;
                        while (true)
                        {
                            cp[1] += 2;
                            if (cp[1] < y && IsFree(boardState, new int[] { cp[0] - 1, cp[1] - 1 }, 4, 3))
                            {
                                boardState[cp[0], cp[1]] = 1;
                                boardState[cp[0] + 1, cp[1]] = 1;
                                currentdesks++;
                            }
                            else break;

                        }
                    }
                    //ansonsten einfach auffüllen
                    else
                    {
                        priority[usedP] = new int[] { -1, -1 };
                        boardState[cp[0], cp[1]] = 1;
                        boardState[cp[0] + 1, cp[1]] = 1;
                        currentdesks++;
                        while (true)
                        {
                            cp[1] += 2;
                            if (cp[1] < y && IsFree(boardState, new int[] { cp[0] - 1, cp[1] - 1 }, 4, 3))
                            {
                                boardState[cp[0], cp[1]] = 1;
                                boardState[cp[0] + 1, cp[1]] = 1;
                                currentdesks++;
                            }
                            else break;
                        }
                    }
                }
                //Flach nach links warum auch immer
                else if (cp[0] < x && cp[1] < y && IsFree(boardState, new int[] { cp[0] - 2, cp[1] - 1 }, 4, 3))
                {
                    boardState[cp[0], cp[1]] = 1;
                    boardState[cp[0] - 1, cp[1]] = 1;
                    currentdesks++;
                }
                //stehend nach oben warum auch immer
                else if (cp[0] < x && cp[1] < y && IsFree(boardState, new int[] { cp[0] - 1, cp[1] - 2 }, 3, 4))
                {
                    boardState[cp[0], cp[1]] = 1;
                    boardState[cp[0], cp[1] - 1] = 1;
                    currentdesks++;
                }
                //Console.WriteLine($"\n{currentdesks}\n1 - X:{priority[0][0]} Y:{priority[0][1]}\n2 - X:{priority[1][0]} Y:{priority[1][1]}");
                //for (int drawY = 0; drawY < y; drawY++)
                //{
                //    for (int drawX = 0; (drawX < x); drawX++) 
                //    {
                //        if ((priority[0][0] == drawX && priority[0][1] == drawY) || (priority[1][0] == drawX && priority[1][1] == drawY))
                //        {
                //            Console.ForegroundColor = ConsoleColor.Red;
                //            Console.Write(boardState[drawX, drawY] == 1 ? "X" : "#");
                //            Console.ResetColor();
                //        }
                //        else Console.Write(boardState[drawX, drawY] == 1 ? "X" : ".");

                //    }
                //    Console.WriteLine();
                //}


            }
            string[] output = new string[y];
            for (int myY = 0; myY < y; myY++)
            {
                for (int myX = 0; myX < x; myX++)
                {
                    //Console.Write(boardState[myX, myY] == 1 ? "X" : ".");
                    output[myY] += boardState[myX, myY] == 1 ? "X" : ".";

                }
                //Console.WriteLine();
            }
            //Console.ReadLine();
            return output;
        }
    }
}
