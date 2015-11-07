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
		public Car1 () { CarName = s_carNum++; }
		public int CarName = -1;
		public int FromSegmet;
		public int ToSegmet;
		public int CurrentSegmet;
		public int NumSteps;
		public bool Used;

		public int StartTimeStep;

		public override string ToString ()
		{
			return CarName.ToString ();
		}
	}
}
