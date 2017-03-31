using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CCC_25_Rathaus.Levels;

namespace CCC_25_Rathaus
{
	class Program
	{
		static void Main(string[] args)
		{
			Level1 lvl1 = new Level1();
			var lvl1Result = lvl1.CalculateResult ();
			//var lvlResult = lvl1Result;

			Level2 lvl2 = new Level2();
			var lvl2Result = lvl2.CalculateResult ();
			//var lvlResult = lvl2Result;

			Level3 lvl3 = new Level3();
			var lvl3Result = lvl3.CalculateResult ();
			//var lvlResult = lvl3Result;

			Level4 lvl4 = new Level4();
			var lvl4Result = lvl4.CalculateResult ();
			//var lvlResult = lvl4Result;

			Level5 lvl5 = new Level5();
			var lvl5Result = lvl5.CalculateResult ();
			//var lvlResult = lvl5Result;

			Level6 lvl6 = new Level6();
			var lvl6Result = lvl6.CalculateResult ();
			var lvlResult = lvl6Result;

			foreach (var line in lvlResult)
			{
				Console.WriteLine (line);
			}

			Console.WriteLine("Press ANY key...");
			Console.ReadKey ();
		}
	}
}
