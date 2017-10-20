using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CCC_27_Rathaus.Levels;

namespace CCC_27_Rathaus
{
	class Program
	{
		static void Main(string[] args)
		{
			//Level1 lvl1 = new Level1();
			//lvl1.CalculateResult(@"..\..\Levels\level1\level1-1.txt");
			//lvl1.CalculateResult(@"..\..\Levels\level1\level1-2.txt");
			//lvl1.CalculateResult(@"..\..\Levels\level1\level1-3.txt");
			//lvl1.CalculateResult(@"..\..\Levels\level1\level1-4.txt");

			//Level2 lvl2 = new Level2();
			//lvl2.CalculateResult(@"..\..\Levels\level2\level2-eg.txt");
			//lvl2.CalculateResult(@"..\..\Levels\level2\level2-1.txt");
			//lvl2.CalculateResult(@"..\..\Levels\level2\level2-2.txt");
			//lvl2.CalculateResult(@"..\..\Levels\level2\level2-3.txt");
			//lvl2.CalculateResult(@"..\..\Levels\level2\level2-4.txt");

			//Level3 lvl3 = new Level3();
			//lvl3.CalculateResult(@"..\..\Levels\level3\level3-eg.txt");
			//lvl3.CalculateResult(@"..\..\Levels\level3\level3-1.txt");
			//lvl3.CalculateResult(@"..\..\Levels\level3\level3-2.txt");
			//lvl3.CalculateResult(@"..\..\Levels\level3\level3-3.txt");
			//lvl3.CalculateResult(@"..\..\Levels\level3\level3-4.txt");

			Level4 lvl4 = new Level4();
			lvl4.CalculateResult(@"..\..\Levels\level4\level4-eg.txt");
			lvl4.CalculateResult(@"..\..\Levels\level4\level4-1.txt");
			lvl4.CalculateResult(@"..\..\Levels\level4\level4-2.txt");
			lvl4.CalculateResult(@"..\..\Levels\level4\level4-3.txt");
			lvl4.CalculateResult(@"..\..\Levels\level4\level4-4.txt");

			Console.ReadKey();
		}
	}
}
