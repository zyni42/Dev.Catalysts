using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Addictive_Game
{
	class GameBoardLvl1
	{
		public int Rows;
		public int Cols;
		public int NumUsedFields;
		public Tuple<int,int,int>[] UsedFields;	// position, row, col

		public GameBoardLvl1 (string inputFile)
		{
			var sr = File.OpenText (inputFile);
			var inputFileContent = sr.ReadToEnd ();
			sr.Close ();

			var splitted = inputFileContent.Split (new char [] {' '});
			
			Rows = int.Parse(splitted[0]);
			Cols = int.Parse(splitted[1]);
			NumUsedFields = int.Parse(splitted[2]);
			UsedFields = new Tuple<int,int,int> [NumUsedFields];

			for (int i = 0; i < UsedFields.Length; i++)
			{
				var pos = int.Parse(splitted[i+3]);
				var row = ((pos-1) / Cols) + 1;
				var col = pos - (row-1) * Cols;
				UsedFields[i] = new Tuple<int,int,int> (pos, row, col);
			}
		}

		public string CalculateResult ()
		{
			string result = string.Empty;
			for (int i = 0; i < UsedFields.Length; i++)
			{
				result += UsedFields[i].Item2 + " " + UsedFields[i].Item3 + " ";
			}
			return result.Substring (0,result.Length-1);
		}
	}
}
