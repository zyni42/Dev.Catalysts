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

			//stations = ParseInput4(@"..\..\Levels\level4\level4-1.txt", out journeys, out numFastestHypers);
			//resultList.Add(CalculateDuration4(stations, journeys, numFastestHypers).ToString());

			//stations = ParseInput4(@"..\..\Levels\level4\level4-2.txt", out journeys, out numFastestHypers);
			//resultList.Add(CalculateDuration4(stations, journeys, numFastestHypers).ToString());

			//stations = ParseInput4(@"..\..\Levels\level4\level4-3.txt", out journeys, out numFastestHypers);
			//resultList.Add(CalculateDuration4(stations, journeys, numFastestHypers).ToString());

			//stations = ParseInput4(@"..\..\Levels\level4\level4-4.txt", out journeys, out numFastestHypers);
			//resultList.Add(CalculateDuration4(stations, journeys, numFastestHypers).ToString());

			stations = ParseInput6(@"..\..\Levels\level6\level6-eg.txt", out journeys, out numFastestHypers, out maxHyperDistance);
			//resultList.Add(CalculateDuration6(stations, journeys, numFastestHypers).ToString());

			return resultList.ToArray ();
		}


		//Station [] GenerateNextHyperRoute (long maxHyperDistance, Dictionary<string, Station> stations, Journey[] journeys)
		//{
		//	var myHyperStationsTmp = new Dictionary<string, Station> ();
		//	foreach (var singleJourney in journeys) {
		//		if (!myHyperStationsTmp.ContainsKey (singleJourney.Start.Name))
		//			myHyperStationsTmp.Add (singleJourney.Start.Name, singleJourney.Start);
		//		if (!myHyperStationsTmp.ContainsKey (singleJourney.Stop.Name))
		//			myHyperStationsTmp.Add (singleJourney.Stop.Name, singleJourney.Stop);
		//	}
		//	var myHyperStations = myHyperStationsTmp.Values.ToArray ();

		//	Random c = new Random ();

		//	var 

		//	// random -> so lang
		//	// for i = 0 ... so lang : random -> station ==INTO==> result
		//	// if resullt ZU LANG re-loop

		//	return result

		//	var theIndexes = new int [myHyperStations.Length];
		//	for ( int i = 0 ; i < theIndexes.Length; i++)
		//		theIndexes[i] = i;

			

		//	var rc = new Station[myHyperStations.Length];
		//	for ( int i = 0 ; i < myHyperStations.Length; i++) {
		//		rc[i] = myHyperStations[ theIndexes[i] ];
		//	}
		//	return rc;
		//}


		//public string CalculateDuration6 (Dictionary<string, Station> stations, Journey[] journeys, long numFastestHypers, long maxHyperDistance)
		//{

		//	Station [] singleHyperRoute = null;

		//	while ((singleHyperRoute = GenerateNextHyperRoute (maxHyperDistance, stations, journeys)) != null) {
		//		long foundfasterJourneys = 0;
		//		foreach (var singleJourney in journeys) {
		//			var durationUsingHyper = CalculateDuration5 (
		//				new Station[] { singleJourney.Start, singleJourney.Stop },
		//				singleHyperRoute
		//				);
		//			if (durationUsingHyper < singleJourney.Duration)
		//				foundfasterJourneys++;
		//			if (foundfasterJourneys >= numFastestHypers) {
		//				var rc = string.Format ("{0}", singleHyperRoute.Length);
		//				foreach (var hop in singleHyperRoute) {
		//					rc += string.Format (" {0}", hop.Name);
		//				}
		//				return rc;
		//			}
		//		}
		//	}

		//	return "SEMMERL";
		//}

		
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
