using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CCC_22_Linz
{
	class Program
	{
		static void Main(string[] args)
		{
			//System.Windows.Forms.MessageBox.Show ("Waiting for Debugger...");

			var cl = args.Length > 0 ? args[0] : "0";
			var clInt = int.Parse (cl);
			BaseStuff.CccTest.WriteLineToStandardError (string.Format ("command line : {0} ==> {1}", cl, clInt));
			switch (clInt) {
				case 1: DoLevel1 (); break;
				case 2: DoLevel2 (); break;
				case 3: DoLevel3 (); break;
				//case 4: DoLevel4 (); break;
				//case 5: DoLevel5 (); break;
				//case 6: DoLevel6 (); break;
				//case 7: DoLevel7 (); break;
				default: throw new Exception (string.Format ("unsupported LEVEL [ {0} ==> {1} ]", cl, clInt));
			}
		}

		static void DoLevel1 ()
		{
			Level1 lvl1 = new Level1 ();
			lvl1.CalculateResult ();
		}

		static void DoLevel2 ()
		{
			Level2 lvl2 = new Level2 ();
			lvl2.CalculateResult ();
		}
		static void DoLevel3 ()
		{
			Level3 lvl3 = new Level3 ();
			lvl3.CalculateResult ();
		}

		static void DoLevel4 ()
		{
			Level4 lvl4 = new Level4 ();
			lvl4.CalculateResult ();
		}

		static void DoLevel5 ()
		{
			Level5 lvl5 = new Level5 ();
			lvl5.CalculateResult ();
		}

		static void DoLevel6 ()
		{
			Level6 lvl6 = new Level6 ();
			lvl6.CalculateResult ();
		}

		static void DoLevel7 ()
		{
			Level7 lvl7 = new Level7 ();
			lvl7.CalculateResult ();
		}

	}
}
