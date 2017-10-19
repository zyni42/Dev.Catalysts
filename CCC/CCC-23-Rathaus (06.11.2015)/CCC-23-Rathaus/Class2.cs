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
		static bool asciiOutput = true;
		public Class2 (string fileName)
		{
			lines = CccTest.ReadInputFileByComma (inFileName = fileName);
		}
		public void CalculateResult ()
		{
			CalculateResult (null);
		}
		public bool CalculateResult (Vis.CCC23RathausView viewer)
		{
			var line = lines [0];
			var countSegments = int.Parse (line[0]);

			line = lines [1];
			var countCars = int.Parse (line[0]);

			var cars = new Car1 [countCars];
			var road = new Road (countSegments);


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

			if (viewer != null) {
				viewer.Init (cars, road, inFileName);
			}

			int width = 1 + (int)Math.Log10 (cars.Length - 1);
			string carFmtString = "{0," + width.ToString () + "}|";
			string emtpyString  = string.Format (carFmtString, string.Empty);

			bool pauseLastTime = false;
			bool singleStep = false;
			bool pause = false;
			bool quit  = false;

			for (int currStep = 0; !quit; currStep++) {

				if (pause && !singleStep) currStep--;

				for (int q = 0; q < cars.Length; q++) {
					cars[q].Used = false;
				}

				int carsActive = 0;
				if (viewer != null) {
					Vis.CCC23RathausView.Operation op = Vis.CCC23RathausView.Operation.Nix;
					while (Console.KeyAvailable) {
						var keyInfo = Console.ReadKey (true);
						bool ok = false;
						switch (keyInfo.KeyChar) {
							case '+' : op = op | Vis.CCC23RathausView.Operation.SpeedPlus ; ok = true; break;
							case '-' : op = op | Vis.CCC23RathausView.Operation.SpeedMinus; ok = true; break;
						}
						switch (keyInfo.Key) {
							case ConsoleKey.Spacebar: pause       = !pause      ; singleStep = false; continue;
							case ConsoleKey.Enter   : pause       = true        ; singleStep = true ; continue;
							case ConsoleKey.Escape  : quit        = true        ;                     continue;
							case ConsoleKey.A       : asciiOutput = !asciiOutput;                     continue;
						}
					}
					viewer.ShowData (road, currStep, op);
				}

				bool pauseChanged = pauseLastTime != pause;
				pauseLastTime = pause;
				if (pause) {
					System.Threading.Thread.Sleep (50);
					if (pauseChanged) {
						Console.Write ("*** PAUSE *** [Esc]...quit  [Space]...continue  [Enter]...single-step  [A]...toggle-ascii                                                                                     \r");
					}
					if (!singleStep)
						continue;
					singleStep = false;
				}
				if (pauseChanged) {
					// NOP
				}

				if (asciiOutput) {
					Console.Write ("{0,3} : |", currStep);
					for (int s = 0; s < road.CountSegments; s++) {
						if (road [s] == null) {
							Console.Write (emtpyString);
						}
						else {
							Console.Write (carFmtString, road[s].CarName);
						}
					}
				}

				if (true)
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

				for (int s = 0; s < road.CountSegments; s++) {
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
									if (s < road.CountSegments && road [s + 1] == null) {
										// fahren
										road[s  ] = null;
										road[s+1] = car;
										car.CurrentSegmet = s+1;
										car.Waiting = false;
										carsActive++;
									}
									else {
										// warten
										car.Waiting = true;
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

				if (asciiOutput)
					Console.WriteLine ();
				if (carsActive == 0)
					break;

				Console.Write ("{0,-13} [Esc]...quit  [Space]...{1,-8}  [Enter]...single-step  [A]...toggle-ascii  [+]...speed-up  [-]...speed-down                                     \r"
					, pause ? "*** STEP ****" : string.Empty
					, pause ? "continue" : "pause");
			}

			if (quit) {
				Console.WriteLine ();
				Console.WriteLine ("********* QUIT by operator *********");
				Console.WriteLine ();
			}
			else {
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
			return quit;
		}
	}
}
