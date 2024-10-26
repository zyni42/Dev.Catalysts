using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC_40_Rathaus
{
	internal class Level4
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
            int x = int.Parse(values[0]), y = int.Parse(values[1]), tables = int.Parse(values[2]), currentId = 1;
            string[] output = new string[y];
            for (int i = 0; i < y; i++)
            {
                if (i + 2 < y)
                {
                    for (int j = 0; j < x; j++)
                    {
                        for (int cell = 0; cell < 3; cell++)
                        {
                            output[i + cell] += "X"/*$"{currentId}" + (j == x - 1 ? "" : " ")*/;
                        }
                        if(j+1<x)
                        {
                            for (int cell = 0; cell < 3; cell++)
                            {
                                output[i + cell] += $"." /*+ (j == x - 1 ? "" : " ")*/;
                            }
                            j++;
                        }
                        currentId++;
                    }
                    i += 2;
                    if(i+ 1 < y)
                    {
                        output[i+1] += new string('.', x);
                        i ++;
                    }
                    
                }
                else
                {
                    for (int j = 0; j < x; j++)
                    {
                        if (j + 2 < x)
                        {

                            output[i] += "XXX"/*$"{currentId} {currentId} {currentId}" + (j == x - 1 ? "" : " ")*/;
                            currentId++;
                            j += 2;
                            if (j+1<x)
                            {
                                output[i] += $"."/* + (j == x - 1 ? "" : " ")*/;
                                j++;
                            }
                        }
                        else
                        {
                            output[i] += "."/* + (j == x - 1 ? "" : " ")*/;

                        }
                    }
                    if (i + 1 < y)
                    {
                        output[i + 1] += new string('.', x);
                        i++;
                    }
                }

            }
            return output;
        }
    }
}
