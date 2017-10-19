using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC_25_Rathaus.Levels
{
	class Level4 : Level3
	{
		public new string[] CalculateResult()
		{
			var resultList = new List<string> ();
			Journey[] journeys;
			long numFastestHypers;
			Dictionary<string, Station> stations;

			stations = ParseInput4(@"..\..\Levels\level4\level4-1.txt", out journeys, out numFastestHypers);
			resultList.Add(CalculateDuration4(stations, journeys, numFastestHypers).ToString());

			stations = ParseInput4(@"..\..\Levels\level4\level4-2.txt", out journeys, out numFastestHypers);
			resultList.Add(CalculateDuration4(stations, journeys, numFastestHypers).ToString());

			stations = ParseInput4(@"..\..\Levels\level4\level4-3.txt", out journeys, out numFastestHypers);
			resultList.Add(CalculateDuration4(stations, journeys, numFastestHypers).ToString());

			stations = ParseInput4(@"..\..\Levels\level4\level4-4.txt", out journeys, out numFastestHypers);
			resultList.Add(CalculateDuration4(stations, journeys, numFastestHypers).ToString());

			//stations = ParseInput4(@"..\..\Levels\level4\level4-eg.txt", out journeys, out numFastestHypers);
			//resultList.Add(CalculateDuration4(stations, journeys, numFastestHypers).ToString());

			return resultList.ToArray ();
		}
		
		public string CalculateDuration4 (Dictionary<string, Station> stations, Journey[] journeys, long numFastestHypers)
		{
			var stationArray = stations.Values.ToArray ();
			for (int from = 0; from < stationArray.Length; from++) {
				for (int to = 0; to < stationArray.Length; to++) {
					if (from == to)
						continue;
					long foundfasterJourneys = 0;
					foreach (var singleJourney in journeys) {
						var durationUsingHyper = CalculateDuration2 (
							new Station[] { singleJourney.Start, singleJourney.Stop },
							new Station[] { stationArray[from], stationArray[to] }
							);
						if (durationUsingHyper < singleJourney.Duration)
							foundfasterJourneys++;
						if (foundfasterJourneys >= numFastestHypers) {
							return string.Format ("{0} {1}", stationArray[from].Name, stationArray[to].Name);
						}
					}
				}
			}
			return "SEMMERL";
		}

		public Dictionary<string, Station> ParseInput4 (string inputFile, out Journey[] journeys, out long numFastestHypers)
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

			return stations;
		}
	}
}
