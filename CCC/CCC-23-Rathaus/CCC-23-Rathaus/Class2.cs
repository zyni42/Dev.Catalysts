using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BaseStuff;

namespace CCC_23_Rathaus
{
	public class Class2 : ICccLevel
	{
		List<string[]> lines;
		string inFileName;
		public Class2 (string fileName)
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
						StartTimeStep = int.Parse (line[2]),
						CurrentSegmet = -1,
						NumSteps = 0
					};
				cars[i] = car;
			}

			int width = 1 + (int)Math.Log10 (cars.Length);
			string carFmtString = "{0," + width.ToString () + "}|";
			string emtpyString  = string.Format (carFmtString, string.Empty);

			for (int currStep = 0; ; currStep++) {

				for (int q = 0; q < cars.Length; q++) {
					cars[q].Used = false;
				}

				int carsActive = 0;
				Console.Write ("{0,3} : |", currStep);
				for (int s = 0; s < road.Length; s++) {
					if (road [s] == null) {
						Console.Write (emtpyString);
					}
					else {
						Console.Write (carFmtString, road[s].CarName);
					}
				}

				{
					// einsetzen in die road
					for (int c = 0; c < cars.Length; c++) {
						var car = cars[c];
						if (car.Used) continue;
						if (car.CurrentSegmet == -1) {
							car.Used = true;
							car.NumSteps ++;

							if (car.NumSteps < car.StartTimeStep) {
								carsActive ++;
								continue;
							}

							var target = road[car.FromSegmet];
							if (target != null) continue;
						//	if (car.FromSegmet > 0 && road [car.FromSegmet - 1] != null) continue;

							car.CurrentSegmet = car.FromSegmet;
							road[car.FromSegmet] = car;
							carsActive ++;
						}
					}
				}

				for (int s = 0; s < road.Length; s++) {
					if (road[s  ] == null) {
					}
					else {
						var car = road[s];
						if (!car.Used) {
							car.NumSteps++;
							car.Used = true;
							if (car.CurrentSegmet == car.ToSegmet) {
								// auto am ziel, herausnehmen
								car.CurrentSegmet = -2;
								road[s] = null;
							}
							else {
								// darf ich schon fahren ? (start-zeit)
								carsActive++;
								if (car.NumSteps >= car.StartTimeStep) {
									// versuche, zu fahren
									if (s < road.Length && road [s + 1] == null) {
										// fahren
										road[s  ] = null;
										road[s+1] = car;
										car.CurrentSegmet = s+1;
										carsActive++;
									}
								}
							}
						}
					}
				}

				for (int q = 0; q < cars.Length; q++) {
					// nicht verwendete WARTEN
					if (!cars [q].Used && cars[q].CurrentSegmet != -2) {
						// eber nur, wenn nicht schon -2 == draussen
						cars [q].NumSteps++;
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
