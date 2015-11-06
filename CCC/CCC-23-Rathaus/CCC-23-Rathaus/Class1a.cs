using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BaseStuff;

namespace CCC_23_Rathaus
{
	public class Class1a : ICccLevel
	{
		List<string[]> lines;
		string inFileName;
		public Class1a (string fileName)
		{
			lines = CccTest.ReadInputFileByComma (inFileName = fileName);
		}
		public void CalculateResult ()
		{
			var line = lines [0];
			var countSegments = int.Parse (line[0]);

			line = lines [1];
			var countCars = int.Parse (line[0]);

			var cars = new Car1 [countCars];
			var road = new Car1 [countSegments];

			for (int i = 0; i < countCars; i++) {
				int offset = i + 2;
				line = lines[offset];
				var car = new Car1 () {
						FromSegmet = int.Parse (line[0]) - 1,
						ToSegmet = int.Parse (line[1]) - 1,
						CurrentSegmet = -1,
						NumSteps = 0
					};
				cars[i] = car;
			}

			for (;;) {
				var carsActive = 0;
				for (int c = 0; c < cars.Length; c++) {
					var car = cars[c];
					if (car.CurrentSegmet != -2) {
						carsActive++;
						car.NumSteps++;
					}
					if (car.CurrentSegmet == car.ToSegmet) {
						// heraus
						road[car.CurrentSegmet] = null;
						car.CurrentSegmet = -2;
					}
					else if (car.CurrentSegmet == -1) {
						// hinein
						if (                      road[car.FromSegmet  ] != null) continue;
						if (car.FromSegmet > 0 && road[car.FromSegmet-1] != null) continue;
						car.CurrentSegmet = car.FromSegmet;
						road[car.CurrentSegmet] = car;
					}
					else if (car.CurrentSegmet == -2) {
						// draussen == nop
					}
					else {
						// fahren, wenn möglich
						// if (car.CurrentSegmet >= road.Length) continue;
					//	try { 
							if (road [car.CurrentSegmet + 1] != null)
								continue;
					//	}
					//	catch {
					//		Console.WriteLine ();
					//	}
						road[car.CurrentSegmet    ] = null;
						road[car.CurrentSegmet + 1] = car;
						car.CurrentSegmet++;
					}
				}

				Console.Write ("|");
				for (int s = 0; s < road.Length; s++) {
					if (road [s] == null) {
						Console.Write ("  |");
					}
					else {
						Console.Write ("{0,2}|", road[s].CarName);
					}
				}

				Console.WriteLine ();
				if (carsActive == 0)
					break;
			}

			var resultString = string.Empty;
			for (int c = 0; c < cars.Length; c++) {
				var car = cars[c];
				resultString += string.Format ("{0},", car.NumSteps);
			}
			resultString = resultString.Substring (0, resultString.Length - 1);

			var outfilename = inFileName + "1.out";
			CccTest.CreateResultTxtFile (outfilename, resultString);
			Console.WriteLine (outfilename);
		}
	}
}
