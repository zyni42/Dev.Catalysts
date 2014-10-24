using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC_21_Rathaus
{
	class Level3 : Level2, BaseStuff.ICccLevel<string>
	{
		public Level3() { }

		public class Car3 : Car2
		{
			public float EnergyUsedWh = 0;
			public static float MaxEnginePowerKw = 310;
			public static float PowerConsumersKw = 1;

			public Steering CalculateNextSteeringEnergySave (float wantedDistance, float secondsMax, float maxEnergyUsageWh, int energyMaxThrottle, float energyMaxSpeed)
			{
				var steering = new Steering ();

				// AT GOAL: DO BRAKE
				if (Distance >= wantedDistance)
				{
					steering.Throttle = 0;
					steering.Brake = Steering.MaxBrake;
					return steering;
				}

				// CURRENT LIMIT
				if (this.Speed < (this.LimitCurrent * 0.95) &&
					this.Speed < energyMaxSpeed)
				{
					// speed UP
					steering.Throttle = energyMaxThrottle;
					steering.Brake = 0;
				}
				else
				{
					// speed KEEP
					steering.Throttle = 0;
					steering.Brake = 0;
				}

				// NO NEXT LIMIT
				if (this.LimitDistanceToNext == 0 && this.LimitNext == 0)
					return steering;

				var theRealNextLimit = Math.Min (energyMaxSpeed, this.LimitNext);

				// NEXT LIMIT HIGHER
				if (theRealNextLimit >= this.LimitCurrent)
					return steering;

				// NEXT LIMIT SMALLER
				var wantToGetSlowerKmh = this.Speed - (theRealNextLimit * 0.95);
				var needSecToBrake = wantToGetSlowerKmh / Car2.MaxBreakKmHPerSec;
				var needDistanceToBrakeMETER = needSecToBrake * this.SpeedMperSec;

				// we need to brake LATER
				if (this.LimitDistanceToNext > needDistanceToBrakeMETER)
					return steering;

				// we need to brake NOW
				steering.Throttle = 0;
				steering.Brake = Steering.MaxBrake;

				return steering;
			}
			public Steering CalculateNextSteering (float wantedDistance, float secondsMax, float maxEnergyUsageWh)
			{
				var steering = CalculateNextSteeringEnergySave (wantedDistance, secondsMax, maxEnergyUsageWh, 30, 70);

				var currKW = (steering.Throttle / 100.0f) * MaxEnginePowerKw + PowerConsumersKw;
				var usedKWs = currKW * 0.1f;
				var usedWs  = usedKWs * 1000;
				var usedWh  = usedWs  / 3600;

				this.EnergyUsedWh += usedWh;

				if (this.EnergyUsedWh > maxEnergyUsageWh)
				{
					throw new Exception (string.Format ("energy exhausted: {0} > {1}", this.EnergyUsedWh, maxEnergyUsageWh));
				}

				return steering;
			}

		}

		public new string CalculateResult()
		{
			var result = string.Empty;

			var car = new Car3 ();
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
				{
					var limitData = BaseStuff.CccTest.SplitBySpaces (conLine);
					if (limitData[0] != "speedlimit") throw new Exception ("NOT speedlimit: " + limitData[0]);
					car.LimitCurrent = BaseStuff.CccTest.FloatParse (limitData[1]);
					car.LimitDistanceToNext = BaseStuff.CccTest.FloatParse (limitData[2]);
					car.LimitNext= BaseStuff.CccTest.FloatParse (limitData[3]);
				}

				conLine = Console.ReadLine ();
			//	BaseStuff.CccTest.WriteLineToStandardError ("IN  : " + conLine);
				{
					var updateCommand = BaseStuff.CccTest.SplitBySpaces (conLine);
					if (updateCommand[0] != "update") throw new Exception ("NOT update: " + updateCommand[0]);
				}

				steering = car.CalculateNextSteering (1500, 100, 1000);

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
