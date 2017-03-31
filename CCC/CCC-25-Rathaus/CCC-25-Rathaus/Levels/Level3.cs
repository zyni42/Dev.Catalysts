using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC_25_Rathaus.Levels
{
	class Level3 : Level2
	{

		public class Journey
		{
			public Station Start;
			public Station Stop;
			public long Duration;
		}

		public new string[] CalculateResult()
		{
			var resultList = new List<string> ();
			Journey[] journeys;
			Station[] hyperRoute;
			Dictionary<string, Station> stations;

			stations = ParseInput3(@"..\..\Levels\level3\level3-1.txt", out journeys, out hyperRoute);
			resultList.Add(CalculateDuration3 (journeys, hyperRoute).ToString());

			stations = ParseInput3(@"..\..\Levels\level3\level3-2.txt", out journeys, out hyperRoute);
			resultList.Add(CalculateDuration3 (journeys, hyperRoute).ToString());

			stations = ParseInput3(@"..\..\Levels\level3\level3-3.txt", out journeys, out hyperRoute);
			resultList.Add(CalculateDuration3 (journeys, hyperRoute).ToString());

			stations = ParseInput3(@"..\..\Levels\level3\level3-4.txt", out journeys, out hyperRoute);
			resultList.Add(CalculateDuration3 (journeys, hyperRoute).ToString());

			//stations = ParseInput3(@"..\..\Levels\level3\level3-eg.txt", out journeys, out hyperRoute);
			//resultList.Add(CalculateDuration3 (journeys, hyperRoute).ToString());

			return resultList.ToArray ();
		}

		public long CalculateDuration3 (Journey[] journeys, Station[] hyperStations)
		{
			long numberOfFasterHyperJourneys = 0;
			foreach (var singleJourney in journeys) {
				var driveStations = new Station[] { singleJourney.Start, singleJourney.Stop };
				var journeyDurationWithHyper = CalculateDuration2 (driveStations, hyperStations);
				if (journeyDurationWithHyper <= singleJourney.Duration)
					numberOfFasterHyperJourneys++;
			}
			return numberOfFasterHyperJourneys;
		}

		public Dictionary<string, Station> ParseInput3 (string inputFile, out Journey[] journeys, out Station[] hyperRoute)
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

			var lineNumOfhyperRoute = numJourneys + lineNumOfNumOfJourneys + 1;
			var hyperStatNameSTART = lines [lineNumOfhyperRoute][0];
			var hyperStatNameSTOP  = lines [lineNumOfhyperRoute][1];
			var statSTART = stations[hyperStatNameSTART];
			var statSTOP  = stations[hyperStatNameSTOP ];

			journeys = journeyList.ToArray ();
			hyperRoute = new Station[] { statSTART, statSTOP };

			return stations;
		}
	}
}
