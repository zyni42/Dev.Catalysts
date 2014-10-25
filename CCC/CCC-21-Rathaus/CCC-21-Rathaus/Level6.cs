using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC_21_Rathaus
{
	class Level6 : Level2, BaseStuff.ICccLevel<string>
	{
		public enum TrLightState
		{
			Green,
			Yellow,
			Red,
			RedYellow
		}
		public class TrafficLight
		{
			public float	Distance;
			public float	RemainingTime;
			public TrLightState State = TrLightState.Green;
		}
		public class NextCar
		{
			public float	Distance;
			public float	Speed;
		}
		
		public class Car6 : Car2
		{
			public TrafficLight TrafficLight;
			public NextCar NextCar;
			public Car6() { TrafficLight = new TrafficLight (); NextCar = new NextCar (); }

			public Steering CalculateNextSteeringTl6 (float wantedDistance, float secondsMax)
			{
				//if (this.NextCar.Distance != 0 || this.NextCar.Speed != 0)
				//{
				//	if (this.LimitDistanceToNext == 0 && this.LimitNext == 0)
				//	{
				//		//BaseStuff.CccTest.WriteLineToStandardError ("A");
				//		this.LimitDistanceToNext = this.NextCar.Distance;
				//		this.LimitNext = this.NextCar.Speed;
				//	}
				//	else
				//	{
				//		if (this.NextCar.Distance < this.LimitDistanceToNext)
				//		{
				//			//BaseStuff.CccTest.WriteLineToStandardError ("B");	
				//			this.LimitDistanceToNext = this.NextCar.Distance;
				//			this.LimitNext = this.NextCar.Speed;
				//		}
				//	}

				//}

				var steering = base.CalculateNextSteering (wantedDistance, secondsMax);

				if (this.NextCar.Distance != 0 || this.NextCar.Speed != 0)
				{

					{
						if (this.LimitDistanceToNext == 0 && this.LimitNext == 0)
						{
							//BaseStuff.CccTest.WriteLineToStandardError ("A");
							this.LimitDistanceToNext = this.NextCar.Distance;
							this.LimitNext = this.NextCar.Speed;
						}
						else
						{
							if (this.NextCar.Distance < this.LimitDistanceToNext)
							{
								//BaseStuff.CccTest.WriteLineToStandardError ("B");	
								this.LimitDistanceToNext = this.NextCar.Distance;
								this.LimitNext = this.NextCar.Speed;
							}
						}

					}

					// NEXT LIMIT HIGHER
					if (this.NextCar.Speed >= this.LimitCurrent || this.Speed == 0) {
						// NOP
					}
					else {

						// NEXT LIMIT SMALLER
						var wantToGetSlowerKmh = this.Speed - (this.NextCar.Speed * 0.95);
						var needSecToBrake = wantToGetSlowerKmh / Car2.MaxBreakKmHPerSec;
						var needDistanceToBrakeMETER = needSecToBrake * this.SpeedMperSec;

						// we need to brake LATER
						if (this.NextCar.Distance > 10 && this.NextCar.Distance > needDistanceToBrakeMETER)
							return steering;

						// we need to brake NOW
						steering.Throttle = 0;
						steering.Brake = Steering.MaxBrake;
					}
				}
				// if we DO BRAKE : no more action here
				if (steering.Throttle == 0 && steering.Brake == Steering.MaxBrake)
					return steering;

				// NO TL
				if (this.TrafficLight.Distance == 0) {
				//	BaseStuff.CccTest.WriteLineToStandardError ("no TL");
					return steering;
				}

				// when do we get to the light?
				var timeUntilTlSec =
					(this.SpeedMperSec == 0)
					? 0
					: this.TrafficLight.Distance / this.SpeedMperSec;
				// what is TL state ater that time?
				if (this.TrafficLight.State == TrLightState.Green)
				{
					// GREEN
					// is it STILL GREEN ater timeUntilTlSec?
					if (timeUntilTlSec < this.TrafficLight.RemainingTime)
					{
						// YEAH! just DRIVE ON
						return steering;
					}
				}
				else
				{
					// NOT GREEN

					if (this.TrafficLight.Distance <= 5) {
						steering.Throttle = 0;
						steering.Brake = Steering.MaxBrake;
						return steering;
					}


					// is it THEN GREEN ater timeUntilTlSec?
					if (this.TrafficLight.RemainingTime < timeUntilTlSec)
					{
						// YEAH! just DRIVE ON
						steering.Throttle = 0;
						return steering;
					}
				}

				//if (this.TrafficLight.Distance <= 2) {
				//	steering.Throttle = 0;
				//	steering.Brake = Steering.MaxBrake;
				//	return steering;
				//}

				// TL will be non-grteen when we get there
				{
					var wantToGetSlowerKmh = this.Speed * 1.05f;
					var needSecToBrake = wantToGetSlowerKmh / Car2.MaxBreakKmHPerSec;
					var needDistanceToBrakeMETER = needSecToBrake * this.SpeedMperSec;

					// we need to brake LATER
					if (this.TrafficLight.Distance > 5 && this.TrafficLight.Distance > needDistanceToBrakeMETER) {
						return steering;
					}
				}

				// we need to brake NOW
				steering.Throttle = 0;
				steering.Brake = Steering.MaxBrake;

				return steering;
			}
		}

		public Level6() { }

		public new string CalculateResult()
		{
			var result = string.Empty;

			var car = new Car6 ();
			var steering = new Steering ();

			for (;;)
			{
				var conLine = Console.ReadLine ();
				if (string.IsNullOrEmpty (conLine)) {
					BaseStuff.CccTest.WriteLineToStandardError ("TERMIANTE for empty input");
					break;
				}
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
				{
					var trafficlightData = BaseStuff.CccTest.SplitBySpaces (conLine);
					if (trafficlightData[0] != "trafficlight") throw new Exception ("NOT trafficlight: " + trafficlightData[0]);
					car.TrafficLight.Distance = BaseStuff.CccTest.FloatParse (trafficlightData[1]);
					if (car.TrafficLight.Distance != 0) {
						car.TrafficLight.State =  (TrLightState)(Enum.Parse (typeof (TrLightState), trafficlightData[2]));
						car.TrafficLight.RemainingTime = BaseStuff.CccTest.FloatParse (trafficlightData[3]);
					}
				}

				conLine = Console.ReadLine ();
				{
					var nextCarData = BaseStuff.CccTest.SplitBySpaces (conLine);
					if (nextCarData[0] != "nextcar") throw new Exception ("NOT nextcar: " + nextCarData[0]);
					car.NextCar.Distance = BaseStuff.CccTest.FloatParse (nextCarData[1]);
					car.NextCar.Speed = BaseStuff.CccTest.FloatParse (nextCarData[2]);
				}

				conLine = Console.ReadLine ();
			//	BaseStuff.CccTest.WriteLineToStandardError ("IN  : " + conLine);
				{
					var updateCommand = BaseStuff.CccTest.SplitBySpaces (conLine);
					if (updateCommand[0] != "update") throw new Exception ("NOT update: " + updateCommand[0]);
				}

				steering = car.CalculateNextSteeringTl6 (1500, 100);

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
