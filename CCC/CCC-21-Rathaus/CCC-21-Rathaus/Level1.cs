using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC_21_Rathaus
{
	class Level1 : BaseStuff.ICccLevel<string>
	{
		public Level1() { }

		public class Car
		{
			public float Speed;
			public float Distance;
			public float Time;

			public Steering CalculateNextSteering (float wantedDistance)
			{
				var steering = new Steering ();
				if (Distance < wantedDistance)
				{
					steering.Throttle = Steering.MaxThrottle;
					steering.Brake = 0;
				}
				else
				{
					steering.Throttle = 0;
					steering.Brake = Steering.MaxBrake;
				}
				return steering;
			}
		}
		public class Steering
		{
			public int Throttle;
			public int Brake;

			public static int MaxThrottle = 100;
			public static int MaxBrake = 100;
		}

		public string CalculateResult()
		{
			var result = string.Empty;

			var car = new Car ();
			var steering = new Steering ();

			for (;;)
			{
				var conLine = Console.ReadLine ();
				{
					var speedData = BaseStuff.CccTest.SplitBySpaces (conLine);
					if (speedData[0] != "speed") throw new Exception ("NOT speed: " + speedData[0]);
					car.Speed = BaseStuff.CccTest.FloatParse (speedData[1]);
				}

				conLine = Console.ReadLine ();
				{
					var distData = BaseStuff.CccTest.SplitBySpaces (conLine);
					if (distData[0] != "distance") throw new Exception ("NOT distance: " + distData[0]);
					car.Distance = BaseStuff.CccTest.FloatParse (distData[1]);
				}

				conLine = Console.ReadLine ();
				{
					var timeData = BaseStuff.CccTest.SplitBySpaces (conLine);
					if (timeData[0] != "time") throw new Exception ("NOT time: " + timeData[0]);
					car.Time = BaseStuff.CccTest.FloatParse (timeData[1]);
				}

				conLine = Console.ReadLine ();
			//	BaseStuff.CccTest.WriteLineToStandardError ("IN  : " + conLine);
				{
					var updateCommand = BaseStuff.CccTest.SplitBySpaces (conLine);
					if (updateCommand[0] != "update") throw new Exception ("NOT update: " + updateCommand[0]);
				}

				steering = car.CalculateNextSteering (500);

			//	steering.Throttle = 100;
			//	steering.Brake = 1;

				var throttle = "throttle " + steering.Throttle.ToString ();
				Console.WriteLine (throttle);
				var brake = "brake " + steering.Brake.ToString ();
				Console.WriteLine (brake);
			}


			// Calculate result
			result += "something" + " ";

			return result.Substring (0,result.Length-1);
		}
	}
}
