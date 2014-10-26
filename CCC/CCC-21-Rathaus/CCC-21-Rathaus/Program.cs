using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC_21_Rathaus
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
			string [] lvlResult;
			var cl = args.Length > 0 ? args[0] : "0";
			var clInt = int.Parse (cl);
			BaseStuff.CccTest.WriteLineToStandardError (string.Format ("command line : {0} ==> {1}", cl, clInt));
			switch (clInt) {
				case 1: lvlResult = DoLevel1 (); break;
				case 2: lvlResult = DoLevel2 (); break;
				case 3: lvlResult = DoLevel3 (); break;
				case 4: lvlResult = DoLevel4 (); break;
				case 5: lvlResult = DoLevel5 (); break;
				case 6: lvlResult = DoLevel6 (); break;
//				case 7: lvlResult = DoLevel7 (); break;
				default: throw new Exception (string.Format ("unsupported LEVEL [ {0} ==> {1} ]", cl, clInt));
			}
		}

		static string[] DoLevel1 ()
		{
			var lvl1Results = new List<string> ();

			Level1 lvl1;
			string result;
			//string lvlFile;
			//string extLvl = ".txt";

			// Input #1
			//lvlFile = Level1File + "0";
			lvl1 = new Level1 (/*InputDir + lvlFile + ExtIn*/);
			result = lvl1.CalculateResult ();
			lvl1Results.Add (result);
			//BaseStuff.CccTest.CreateResultTxtFile (OutputDir + lvlFile + ExtOut + extLvl, result);

			// Input #2
			// ...
			// Input #n

			return lvl1Results.ToArray ();
		}
		static string[] DoLevel2 ()
		{
			var lvl2Results = new List<string> ();

			Level2 lvl2;
			string result;
			//string lvlFile;
			//string extLvl = ".txt";

			// Input #1
			//lvlFile = Level1File + "0";
			lvl2 = new Level2 (/*InputDir + lvlFile + ExtIn*/);
			result = lvl2.CalculateResult ();
			lvl2Results.Add (result);
			//BaseStuff.CccTest.CreateResultTxtFile (OutputDir + lvlFile + ExtOut + extLvl, result);

			// Input #2
			// ...
			// Input #n

			return lvl2Results.ToArray ();
		}
		static string[] DoLevel3 ()
		{
			var lvl3Results = new List<string> ();

			Level3 lvl3;
			string result;

			// Input #1
			lvl3 = new Level3 ();
			result = lvl3.CalculateResult ();
			lvl3Results.Add (result);

			return lvl3Results.ToArray ();
		}
		static string[] DoLevel4 ()
		{
			var lvl4Results = new List<string> ();

			Level4 lvl4;
			string result;

			// Input #1
			lvl4 = new Level4 ();
			result = lvl4.CalculateResult ();
			lvl4Results.Add (result);

			return lvl4Results.ToArray ();
		}
		static string[] DoLevel5 ()
		{
			var lvl5Results = new List<string> ();

			Level5 lvl5;
			string result;

			// Input #1
			lvl5 = new Level5 ();
			result = lvl5.CalculateResult ();
			lvl5Results.Add (result);

			return lvl5Results.ToArray ();
		}
		static string[] DoLevel6 ()
		{
			var lvl6Results = new List<string> ();

			Level6 lvl6;
			string result;

			// Input #1
			lvl6 = new Level6 ();
			result = lvl6.CalculateResult ();
			lvl6Results.Add (result);

			return lvl6Results.ToArray ();
		}
	}
}
