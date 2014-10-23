using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Autonomous_Car_Warmup
{
	class Program
	{
		const string InputDir = @"in\";
		const string OutputDir = @"out\";
		const string ExtIn = ".in";
		const string ExtOut = ".out";
		const string Level1File = "level1-";

		static void Main(string[] args)
		{
			var lvl1Result = DoLevel1 ();
		}



		static string[] DoLevel1 ()
		{
			var lvl1Results = new List<string> ();

			

			Level1 lvl1;
			string result;
			string lvlFile;
			string extLvl = ".txt";

			// Input #1
			lvlFile = Level1File + "0";
			lvl1 = new Level1 (/*InputDir + lvlFile + ExtIn*/);
			result = lvl1.CalculateResult ();
			lvl1Results.Add (result);
		//	BaseStuff.CccTest.CreateResultTxtFile (OutputDir + lvlFile + ExtOut + extLvl, result);

			// Input #2
			// ...
			// Input #n

			return lvl1Results.ToArray ();
		}
	}
}
