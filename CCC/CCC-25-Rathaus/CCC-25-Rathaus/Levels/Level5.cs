using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC_25_Rathaus.Levels
{
	class Level5 : Level4
	{
		public new string[] CalculateResult()
		{
			var resultList = new List<string> ();
			Station[] journey;
			Station[] hyperRoute;
			Dictionary<string, Station> stations;

			stations = ParseInput5(@"..\..\Levels\level5\level5-1.txt", out journey, out hyperRoute);
			resultList.Add(CalculateDuration5(journey, hyperRoute).ToString());

			stations = ParseInput5(@"..\..\Levels\level5\level5-2.txt", out journey, out hyperRoute);
			resultList.Add(CalculateDuration5(journey, hyperRoute).ToString());

			stations = ParseInput5(@"..\..\Levels\level5\level5-3.txt", out journey, out hyperRoute);
			resultList.Add(CalculateDuration5(journey, hyperRoute).ToString());

			stations = ParseInput5(@"..\..\Levels\level5\level5-4.txt", out journey, out hyperRoute);
			resultList.Add(CalculateDuration5(journey, hyperRoute).ToString());

			//stations = ParseInput5(@"..\..\Levels\level5\level5-eg.txt", out journey, out hyperRoute);
			//resultList.Add(CalculateDuration5(journey, hyperRoute).ToString());

			return resultList.ToArray ();
		}

		public long CalculateDuration5(Station[] journey, Station[] hyperRoute)
		{
			int hyperStartIndex = 0, hyperStopIndex = 0;

			double minDistanceForJourneyStart = int.MaxValue;
			//long   bestIndexForJourneyStart = 0;
			double minDistanceForJourneyStop  = int.MaxValue;
			//long   bestIndexForJourneyStop  = 0;

			Station journeyStart = journey[0];
			Station journeyStop  = journey[1];

			for (int idx = 0; idx < hyperRoute.Length; idx++) {
				double distanceForJourneyStart = CalculateDistance (journeyStart, hyperRoute[idx]);
				if (distanceForJourneyStart < minDistanceForJourneyStart) {
					minDistanceForJourneyStart = distanceForJourneyStart;
					hyperStartIndex = idx;
				}
				double distanceForJourneyStop  = CalculateDistance (journeyStop , hyperRoute[idx]);
				if (distanceForJourneyStop  < minDistanceForJourneyStop ) {
					minDistanceForJourneyStop  = distanceForJourneyStop ;
					hyperStopIndex  = idx;
				}
			}

			double hyperTimeTotal = 0;
			int idxFrom = Math.Min (hyperStartIndex, hyperStopIndex);
			int idxTo   = Math.Max (hyperStartIndex, hyperStopIndex);
			for (int idx = idxFrom; idx < idxTo; idx++) {
				double thisHyperDuration = CalculateDurationDouble (hyperRoute[idx], hyperRoute[idx+1]);
				hyperTimeTotal += thisHyperDuration;
			}
			double driveTimeStart = CalculateDurationDrive (journeyStart, hyperRoute[hyperStartIndex]);
			double driveTimeStop  = CalculateDurationDrive (hyperRoute[hyperStopIndex], journeyStop);

			double journeyTimeOverall = driveTimeStart + driveTimeStop + hyperTimeTotal;
			return (long) Math.Round (journeyTimeOverall, 0);
		}

		public double CalculateDurationDouble (Station stat1, Station stat2)
		{
			double dist = CalculateDistance (stat1, stat2);
			double duration = (dist / 250) + 200;
			return duration;
		}


		public Dictionary<string, Station> ParseInput5 (string inputFile, out Station[] journey, out Station[] hyperRoute)
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
			journey = new Station[] { statSTART, statSTOP };

			long numHyperRouteStations = long.Parse(lines[numStations+2][0]);
			List<Station> hyperRouteList = new List<Station> ();
			for (int idxRouteStation=1; idxRouteStation <= numHyperRouteStations; idxRouteStation++)
			{
				hyperRouteList.Add(stations[lines[numStations+2][idxRouteStation]]);
			}
			hyperRoute = hyperRouteList.ToArray();

			return stations;
		}
	}
}
