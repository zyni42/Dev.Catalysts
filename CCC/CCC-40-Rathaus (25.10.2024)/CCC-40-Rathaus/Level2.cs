using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CCC_40_Rathaus
{
	internal class Level2
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
				for (int j = 0; j < x/3; j++)
				{
					output[i] += $"{currentId} {currentId} {currentId}"+(j== x-1?"":" ");
					currentId++;
				}
			}
			return output;
		}
	}
}
