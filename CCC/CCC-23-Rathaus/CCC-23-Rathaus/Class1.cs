using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BaseStuff;

namespace CCC_23_Rathaus
{
	public class Class1 : ICccLevel
	{
		List<string[]> lines;
		string inFileName;
		public Class1 (string fileName)
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
				//for (int c = 0; c < cars.Length; c++) {
				//	var car = cars[c];
				//	if (car.CurrentSegmet == car.ToSegmet) {
				//		car.NumSteps++;
				//		road[car.CurrentSegmet] = null;
				//		car.CurrentSegmet = -2;
				//	}
				//	else if (car.CurrentSegmet == -1) {
				//		if (                      road[car.FromSegmet  ] != null) continue;
				//		if (car.FromSegmet > 0 && road[car.FromSegmet-1] != null) continue;
				//		car.CurrentSegmet = car.FromSegmet;
				//		car.NumSteps++;
				//		road[car.CurrentSegmet] = car;
				//	}
				//}


				for (int q = 0; q < cars.Length; q++) {
					cars[q].Used = false;
				}

				int carsActive = 0;
				Console.Write ("|");
				for (int s = 0; s < road.Length; s++) {
					if (road [s] == null) {
						Console.Write ("  |");
					}
					else {
						Console.Write ("{0,2}|", road[s].CarName);
					}
				}

				{
					for (int c = 0; c < cars.Length; c++) {
						var car = cars[c];
						if (car.Used) continue;
						if (car.CurrentSegmet == -1) {
							car.Used = true;
							car.NumSteps ++;

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
					//if (road[s  ] == null) {
					////	if (s == 0 || road [s - 1] == null) {
					//		// auto neu einsetzen
					//		for (int c = 0; c < cars.Length; c++) {
					//			if (c == 1) {
					//				int t = 00;
					//			}

					//			var car = cars[c];
					//			if (!car.Used) {
					//				if (car.CurrentSegmet == -1 && car.FromSegmet == s) {
					//					car.NumSteps++;
					//					car.Used = true;
					//					if (s == 0 || road [s - 1] == null) {
					//						car.CurrentSegmet = s;
					//						road[s] = car;
					//						carsActive++;
					//					}
					//					break;
					//				}
					//			}
					//		}
					////	}
					//}
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
