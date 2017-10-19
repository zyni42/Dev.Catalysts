using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC_25_Rathaus.Levels
{
	class Level1 : BaseStuff.ICccLevel<string[]>
	{
		public class Station
		{
			public string Name;
			public double X;
			public double Y;
		}


		public string[] CalculateResult()
		{
			var resultList = new List<string> ();
			Station[] route;
			Dictionary<string, Station> stations;
			
			stations = ParseInput(@"..\..\Levels\level1\level1-1.txt", out route);
			resultList.Add (CalculateDuration (route[0], route[1]).ToString ());

			stations = ParseInput(@"..\..\Levels\level1\level1-2.txt", out route);
			resultList.Add (CalculateDuration (route[0], route[1]).ToString ());

			stations = ParseInput(@"..\..\Levels\level1\level1-3.txt", out route);
			resultList.Add (CalculateDuration (route[0], route[1]).ToString ());

			stations = ParseInput(@"..\..\Levels\level1\level1-4.txt", out route);
			resultList.Add (CalculateDuration (route[0], route[1]).ToString ());

			//stations = ParseInput(@"..\..\Levels\level1\level1-eg.txt", out route);
			//resultList.Add (CalculateDuration (route[0], route[1]).ToString ());

			return resultList.ToArray ();
		}

		public long CalculateDuration (Station stat1, Station stat2)
		{
			double dist = CalculateDistance (stat1, stat2);
			double duration = (dist / 250) + 200;
			return (long)Math.Round (duration, 0);
		}


		public double CalculateDistance (Station stat1, Station stat2)
		{
			double dX = Math.Abs ( stat1.X - stat2.X );
			double dY = Math.Abs ( stat1.Y - stat2.Y );
			double dist = Math.Sqrt ( Math.Pow ( dX , 2 ) + Math.Pow ( dY , 2 ) ) ;
			return dist;
		}

		public Dictionary<string, Station> ParseInput (string inputFile, out Station[] route)
		{
			Dictionary<string, Station> stations = new Dictionary<string, Station> ();

			var lines = BaseStuff.CccTest.ReadInputFile (inputFile);
			var numStations = int.Parse (lines[0][0]);
			for (int statIdx = 0; statIdx < numStations; statIdx++) {
				int lineNumberInFile = statIdx + 1 ;
				var stat = new Station ();
				stat.Name = lines[ lineNumberInFile ][0];
				stat.X    = double.Parse (lines[ lineNumberInFile ][1]);
				stat.Y    = double.Parse (lines[ lineNumberInFile ][2]);
				stations.Add(stat.Name, stat);
			}
			var statNameSTART = lines [numStations+1][0];
			var statNameSTOP  = lines [numStations+1][1];

			var statSTART = stations[statNameSTART];
			var statSTOP  = stations[statNameSTOP ];
			route = new Station[] { statSTART, statSTOP };
			return stations;
		}
	}
}
