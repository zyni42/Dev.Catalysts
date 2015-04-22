using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC_22_Linz
{
	class Level1 : BaseStuff.ICccLevel
	{
		public Level1() { }

		public void CalculateResult()
		{
			// *** CREATE HELPER OBJECTS ***
			//var car = new Car ();
			//var steering = new Steering ();

			// *** READ CONSOLE INPUTS ***
			BaseStuff.CccTest.WriteLineToStandardError (string.Format ("running : {0}", this));
			for (;;)
			{
				// *** INPUT #1 ***
				var conLine = Console.ReadLine ();
				if (string.IsNullOrEmpty (conLine)) {
					BaseStuff.CccTest.WriteLineToStandardError ("TERMINATE for empty input");
					break;
				}
				{
					//var speedData = BaseStuff.CccTest.SplitBySpaces (conLine);
					//if (speedData[0] != "speed") throw new Exception ("NOT speed: " + speedData[0]);
					//car.Speed = BaseStuff.CccTest.FloatParse (speedData[1]);
				}

				// *** INPUT #2 ***
				conLine = Console.ReadLine ();
				{
					//var distData = BaseStuff.CccTest.SplitBySpaces (conLine);
					//if (distData[0] != "distance") throw new Exception ("NOT distance: " + distData[0]);
					//car.Distance = BaseStuff.CccTest.FloatParse (distData[1]);
				}

				// *** CALCULATION ***
				//steering = car.CalculateNextSteering (500);

				// *** RESULT CONSOLE OUTPUT ***
				//var throttle = "throttle " + steering.Throttle.ToString ();
				//Console.WriteLine (throttle);
				//var brake = "brake " + steering.Brake.ToString ();
				//Console.WriteLine (brake);
			}
		}
	}
}
