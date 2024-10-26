using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseStuff;

namespace CCC_40_Rathaus
{
	internal class Program
	{
		static void Main(string[] args)
		{
			// C:\Users\User42\Downloads\CCC-Levels
			//CccTest.
			const string levelRootPath = @"C:\Users\User42\Downloads\CCC-Levels\";

			//const int level = 1;
			//for (int subLevel = 1; subLevel <= 5; subLevel++)
			//{
			//	Level1 level1 = new Level1();
			//	string lvl1Path = $"{levelRootPath}Level{level}\\";
			//	var lvlResult = level1.CalculateResult($"{lvl1Path}level{level}_{subLevel}.in");
			//	File.WriteAllText ($"{lvl1Path}level{level}_{subLevel}.out", lvlResult);
			//}

			//const int level = 2;
			//for (int subLevel = 1; subLevel <= 5; subLevel++)
			//{
			//	Level2 level2 = new Level2();
			//	string lvlPath = $"{levelRootPath}Level{level}\\";
			//	var lvlResult = level2.CalculateResult($"{lvlPath}level{level}_{subLevel}.in");
			//	File.WriteAllText ($"{lvlPath}level{level}_{subLevel}.out", lvlResult);
			//}

			//const int level = 3;
			//for (int subLevel = 1; subLevel <= 5; subLevel++)
			//{
			//	Level3 level3 = new Level3();
			//	string lvlPath = $"{levelRootPath}Level{level}\\";
			//	var lvlResult = level3.CalculateResult($"{lvlPath}level{level}_{subLevel}.in");
			//	File.WriteAllText($"{lvlPath}level{level}_{subLevel}.out", lvlResult);
			//}

			//const int level = 4;
			//for (int subLevel = 1; subLevel <= 5; subLevel++)
			//{
			//	var level4 = new Level4();
			//	string lvlPath = $"{levelRootPath}Level{level}\\";
			//	var lvlResult = level4.CalculateResult($"{lvlPath}level{level}_{subLevel}.in");
			//	File.WriteAllText($"{lvlPath}level{level}_{subLevel}.out", lvlResult);
			//}

			const int level = 5;
			for (int subLevel = 1; subLevel <= 5; subLevel++)
			{
				var level5 = new Level5();
				string lvlPath = $"{levelRootPath}Level{level}\\";
				var lvlResult = level5.CalculateResult($"{lvlPath}level{level}_{subLevel}.in");
				File.WriteAllText($"{lvlPath}level{level}_{subLevel}.out", lvlResult);
			}
		}
	}
}
