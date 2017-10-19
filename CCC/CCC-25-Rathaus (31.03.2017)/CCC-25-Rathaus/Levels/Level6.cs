using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC_25_Rathaus.Levels
{
	class Level6 : Level5
	{
		public new string[] CalculateResult()
		{
			var resultList = new List<string> ();
			Journey[] journeys;
			long numFastestHypers;
			long maxHyperDistance;
			Dictionary<string, Station> stations;

			stations = ParseInput6(@"..\..\Levels\level6\level6-1.txt", out journeys, out numFastestHypers, out maxHyperDistance);
			resultList.Add ("6-1 ... " + CalculateDuration6 (stations, journeys, numFastestHypers, maxHyperDistance).ToString());

			stations = ParseInput6 (@"..\..\Levels\level6\level6-2.txt", out journeys, out numFastestHypers, out maxHyperDistance);
			resultList.Add ("6-2 ... " + CalculateDuration6 (stations, journeys, numFastestHypers, maxHyperDistance).ToString ());

			stations = ParseInput6 (@"..\..\Levels\level6\level6-3.txt", out journeys, out numFastestHypers, out maxHyperDistance);
			resultList.Add ("6-3 ... " + CalculateDuration6 (stations, journeys, numFastestHypers, maxHyperDistance).ToString ());

			stations = ParseInput6 (@"..\..\Levels\level6\level6-4.txt", out journeys, out numFastestHypers, out maxHyperDistance);
			resultList.Add ("6-4 ... " + CalculateDuration6 (stations, journeys, numFastestHypers, maxHyperDistance).ToString());

			stations = ParseInput6 (@"..\..\Levels\level6\level6-eg.txt", out journeys, out numFastestHypers, out maxHyperDistance);
			resultList.Add ("6-eg .. " + CalculateDuration6 (stations, journeys, numFastestHypers, maxHyperDistance).ToString ());

			return resultList.ToArray ();
		}

		public class ParamsForGenerateNextHyperRoute
		{
			public Station [] HyperStations;
			public Random     Random;
			public long       MaxHyperDistance;
			public long       MaxStations;
		}

		Station[] GenerateNextHyperRoute (ParamsForGenerateNextHyperRoute paramValues)
		{
			const int ABS_MAX = 100;
			for (;;) {
				// random -> so lang
				var numberOfStations = 2 + paramValues.Random.Next (Math.Min (paramValues.HyperStations.Length, ABS_MAX) - 1);
				var hyperRouteArr = new Station[numberOfStations];
				var usedSources = new bool[paramValues.HyperStations.Length];
				double totalHyperDistance = 0;
				// for i = 0 ... so lang : random -> station ==INTO==> result
				for (int i = 0; i < numberOfStations; i++) {
					int stationIdx = -1;
					// avoid duplicate use of a station
					while (stationIdx < 0 || usedSources[stationIdx])
						stationIdx = paramValues.Random.Next (paramValues.HyperStations.Length);
					usedSources[stationIdx] = true;
					hyperRouteArr[i] = paramValues.HyperStations[stationIdx];
					if (i > 0) {
						totalHyperDistance += CalculateDistance (hyperRouteArr[i - 1], hyperRouteArr[i]);
						// if result TOO LONG re-loop
						if (totalHyperDistance > paramValues.MaxHyperDistance)
							break;
					}
				}
				// if result TOO LONG re-loop
				if (totalHyperDistance > paramValues.MaxHyperDistance)
					continue;
				return hyperRouteArr;
			}
		}


		public string CalculateDuration6 (Dictionary<string, Station> stations, Journey[] journeys, long numFastestHypers, long maxHyperDistance)
		{
			DateTime starttime = DateTime.Now;
			ParamsForGenerateNextHyperRoute paramValues = null;
			{
				var myHyperStationsTmp = new Dictionary<string, Station> ();
				foreach (var singleJourney in journeys) {
					if (!myHyperStationsTmp.ContainsKey (singleJourney.Start.Name)) myHyperStationsTmp.Add (singleJourney.Start.Name, singleJourney.Start);
					if (!myHyperStationsTmp.ContainsKey (singleJourney.Stop .Name)) myHyperStationsTmp.Add (singleJourney.Stop .Name, singleJourney.Stop );
				}
				paramValues = new ParamsForGenerateNextHyperRoute () {
					MaxHyperDistance = maxHyperDistance,
					HyperStations = myHyperStationsTmp.Values.ToArray (),
					MaxStations = 100,
					Random = new Random ()
				};
			}
			long numIterations = 0;
			Station[] singleHyperRoute = null;
			while ((singleHyperRoute = GenerateNextHyperRoute (paramValues)) != null) {
				numIterations++;
				long foundfasterJourneys = 0;
				foreach (var singleJourney in journeys) {
					var durationUsingHyper = CalculateDuration5 (
						new Station[] { singleJourney.Start, singleJourney.Stop },
						singleHyperRoute
						);
					if (durationUsingHyper < singleJourney.Duration)
						foundfasterJourneys++;
					if (foundfasterJourneys >= numFastestHypers) {
						var rc = string.Format ("{0}", singleHyperRoute.Length);
						foreach (var hop in singleHyperRoute) {
							rc += string.Format (" {0}", hop.Name);
						}
						return
							string.Format ("{0,-16} ({1,5} iterations)    -->    ", DateTime.Now - starttime, numIterations)
							+
							rc;
					}
				}
			}

			return "SEMMERL";
		}


		public Dictionary<string, Station> ParseInput6 (string inputFile, out Journey[] journeys, out long numFastestHypers, out long maxHyperDistance)
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

			var lineNumOfNumOfJourneys = numStations+1;
			var numJourneys = int.Parse (lines[lineNumOfNumOfJourneys][0]);
			var journeyList = new List<Journey> ();
			for (int journeyIdx = 0; journeyIdx < numJourneys; journeyIdx++) {
				int lineNumberInFile = journeyIdx + lineNumOfNumOfJourneys + 1;
				var journey = new Journey ();
				var startStationName = lines[ lineNumberInFile ][0];
				var  stopStationName = lines[ lineNumberInFile ][1];
				journey.Start = stations[startStationName];
				journey.Stop  = stations[ stopStationName];
				journey.Duration = long.Parse (lines[ lineNumberInFile ][2]);
				journeyList.Add (journey);
			}

			var lineNumOfNumFastestHypers = numJourneys + lineNumOfNumOfJourneys + 1;

			journeys = journeyList.ToArray ();
			numFastestHypers = long.Parse(lines [lineNumOfNumFastestHypers][0]);

			maxHyperDistance = long.Parse(lines [lineNumOfNumFastestHypers + 1][0]);

			return stations;
		}
	}
}
