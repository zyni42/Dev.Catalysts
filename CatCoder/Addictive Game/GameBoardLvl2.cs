using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Addictive_Game
{
	class GameBoardLvl2
	{
		public class GamePoint
		{
			public int Position;
			public int Color;
			public int Row;
			public int Column;
		}

		public int Rows;
		public int Cols;
		public int NumPoints;
		public GamePoint[] Points;
		public GameBoardLvl2 (string inputFile)
		{
			var sr = File.OpenText (inputFile);
			var inputFileContent = sr.ReadToEnd ();
			sr.Close ();

			var splitted = inputFileContent.Split (new char [] {' '});
			
			Rows = int.Parse(splitted[0]);
			Cols = int.Parse(splitted[1]);
			NumPoints = int.Parse(splitted[2]);
			Points = new GamePoint[NumPoints];

			for (int i = 0; i < Points.Length; i++)
			{
				var pos = int.Parse(splitted[3+i*2]);
				var clr = int.Parse(splitted[3+i*2+1]);
				var row = ((pos-1) / Cols) + 1;
				var col = pos - (row-1) * Cols;

				var gp = new GamePoint () {
					Position	= pos,
					Color		= clr,
					Row			= row,
					Column		= col
				};
				Points[i] = gp;
			}
		}

		public string CalculateResult ()
		{
			string result = string.Empty;

			Dictionary<int,List<GamePoint>> gpsByColor = new Dictionary<int,List<GamePoint>> ();
			for (int i = 0; i < Points.Length; i++)
			{
				if (!gpsByColor.ContainsKey (Points[i].Color))
					gpsByColor.Add (Points[i].Color, new List<GamePoint> ());
				gpsByColor[Points[i].Color].Add (Points[i]);
			}
			var ogpsByColor = gpsByColor.OrderBy ((item) => item.Key).ToArray ();

			for (int i = 0; i < ogpsByColor.Length; i++)
			{
				var md = Math.Abs (ogpsByColor[i].Value[0].Row - ogpsByColor[i].Value[1].Row) + Math.Abs (ogpsByColor[i].Value[0].Column - ogpsByColor[i].Value[1].Column);
				result += md + " ";
			}
			
			return result.Substring (0,result.Length-1);
		}}
	}
