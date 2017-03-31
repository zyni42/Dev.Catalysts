using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC_25_Rathaus.Levels
{
	class Level2 : Level1
	{
		public new string[] CalculateResult()
		{
			var resultList = new List<string> ();
			Station[] driveRoute;
			Station[] hyperRoute;
			Dictionary<string, Station> stations;

			stations = ParseInput2(@"..\..\Levels\level2\level2-1.txt", out driveRoute, out hyperRoute);
			resultList.Add(CalculateDuration2(driveRoute, hyperRoute).ToString());

			stations = ParseInput2(@"..\..\Levels\level2\level2-2.txt", out driveRoute, out hyperRoute);
			resultList.Add(CalculateDuration2(driveRoute, hyperRoute).ToString());

			stations = ParseInput2(@"..\..\Levels\level2\level2-3.txt", out driveRoute, out hyperRoute);
			resultList.Add(CalculateDuration2(driveRoute, hyperRoute).ToString());

			stations = ParseInput2(@"..\..\Levels\level2\level2-4.txt", out driveRoute, out hyperRoute);
			resultList.Add(CalculateDuration2(driveRoute, hyperRoute).ToString());

			//stations = ParseInput2 (@"..\..\Levels\level2\level2-eg.txt", out driveRoute, out hyperRoute);
			//resultList.Add(CalculateDuration2(driveRoute, hyperRoute).ToString());

			return resultList.ToArray ();
		}

		public Dictionary<string, Station> ParseInput2 (string inputFile, out Station[] driveRoute, out Station[] hyperRoute)
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
			var driveStatNameSTART = lines [numStations+1][0];
			var driveStatNameSTOP  = lines [numStations+1][1];
			var statSTART = stations[driveStatNameSTART];
			var statSTOP  = stations[driveStatNameSTOP ];
			driveRoute = new Station[] { statSTART, statSTOP };

			var hyperStatNameSTART = lines [numStations+2][0];
			var hyperStatNameSTOP  = lines [numStations+2][1];
			statSTART = stations[hyperStatNameSTART];
			statSTOP  = stations[hyperStatNameSTOP ];
			hyperRoute = new Station[] { statSTART, statSTOP };

			return stations;
		}

		public long CalculateDuration2 (Station[] driveStations, Station[] hyperStations)
		{
			Station hy0 = FindNearestHyper (driveStations[0], hyperStations[0], hyperStations[1]);
			Station hy1 = ( hy0 == hyperStations[0]) ? hyperStations[1] : hyperStations[0];

			double driveDurationStart = CalculateDurationDrive (driveStations[0], hy0);
			double hyperDuration = CalculateDurationHyper (hyperStations[0], hyperStations[1]);
			double driveDurationStop = CalculateDurationDrive (hy1, driveStations[1]);
			// rechenen
			// + + + 
			// runden
			var result = Math.Round (driveDurationStart + hyperDuration + driveDurationStop);
			return (long)result;
		}

		public Station FindNearestHyper (Station reference, Station hyperStation1, Station hyperStation2)
		{
			double dist1 = CalculateDistance (reference, hyperStation1);
			double dist2 = CalculateDistance (reference, hyperStation2);
			if (dist1 <= dist2)
				return hyperStation1;
			return hyperStation2;
		}

		public double CalculateDurationDrive (Station stat1, Station stat2)
		{
			double dist = CalculateDistance (stat1, stat2);
			double duration = (dist / 15);
			return duration;
		}

		public double CalculateDurationHyper (Station stat1, Station stat2)
		{
			double dist = CalculateDistance (stat1, stat2);
			double duration = (dist / 250) + 200;
			return duration;
		}

	}
}
