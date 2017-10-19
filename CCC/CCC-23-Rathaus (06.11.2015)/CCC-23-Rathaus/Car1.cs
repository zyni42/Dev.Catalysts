using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC_23_Rathaus
{
	public class Car1
	{
		static int s_carNum = 0;
		public static void ResetCarNames () { s_carNum = 0; }
		public Car1 () { CarNum = s_carNum++; CarName = CarNum.ToString (); }
		public string CarName = "?";
		public int    CarNum  = -1;
		public int FromSegmet;
		public int ToSegmet;
		public int CurrentSegmet;
		public int NumSteps;
		public bool Used;
		public bool Waiting;
		public bool IsNewOnRoad { get { return CurrentSegmet == FromSegmet; } }
		public bool IsLeaving   { get { return ToSegmet == CurrentSegmet; } }

		public object VisTag;

		public int StartTimeStep;

		public override string ToString ()
		{
			return string.Format ("{0}::[{1}->{2}]<time:{3}>now:{4}(pos={5})"
				, CarName
				, FromSegmet
				, ToSegmet
				, StartTimeStep
				, NumSteps
				, CurrentSegmet
				);
		}
	}
}
