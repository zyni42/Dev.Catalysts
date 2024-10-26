using BaseStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CCC_40_Rathaus
{
	internal class Level1
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

			var numRoomDesks = MaximumDesks (inputLines.ToArray());

			StringBuilder stbResult = new StringBuilder(numRoomDesks.Length);
			foreach (int numDesks in numRoomDesks)
			{
				stbResult.AppendLine($"{numDesks}");
			}
			return stbResult.ToString();
		}

		int[] MaximumDesks(string[] d)
		{
			int[] outputArray = new int[d.Length];
			for (int i= 0; i < d.Length; i++)
			{
				string[] currentvalues = d[i].Split(' ');
				outputArray[i] = int.Parse(currentvalues[0]) / 3 * int.Parse(currentvalues[1]);
			}
			return outputArray;
		}
	}
}
