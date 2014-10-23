using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace Addictive_Game
{
	class Program
	{
		const string InputDir = @"in\";
		const string OutputDir = @"out\";
		const string ExtIn = ".in";
		const string ExtOut = ".out";
		const string Level1File = "level1-";
		const string Level2File = "level2-";
		const string Level3File = "level3-";
		const string Level4File = "level4-";
		const string Level5File = "level5-";

		static void Main(string[] args)
		{
			//var lvl1Result = Level1 ();
			//var lvl2Result = Level2 ();
			//var lvl3Result = Level3 ();
			//var lvl4Result = Level4 ();
			var lvl5Result = Level5 ();
		}

		static string[] Level1 ()
		{
			var lvl1Results = new string[4];

			const string lvl10 = Level1File + "0";
			var gbLvl10 = new GameBoardLvl1 (InputDir + lvl10 + ExtIn);
			lvl1Results[0] = gbLvl10.CalculateResult ();
			CreateResultFile (OutputDir + lvl10 + ExtOut, lvl1Results[0]);

			const string lvl11 = Level1File + "1";
			var gbLvl11 = new GameBoardLvl1 (InputDir + lvl11 + ExtIn);
			lvl1Results[1] = gbLvl11.CalculateResult ();
			CreateResultFile (OutputDir + lvl11 + ExtOut, lvl1Results[1]);

			const string lvl12 = Level1File + "2";
			var gbLvl12 = new GameBoardLvl1 (InputDir + lvl12 + ExtIn);
			lvl1Results[2] = gbLvl12.CalculateResult ();
			CreateResultFile (OutputDir + lvl12 + ExtOut, lvl1Results[2]);

			const string lvl13 = Level1File + "3";
			var gbLvl13 = new GameBoardLvl1 (InputDir + lvl13 + ExtIn);
			lvl1Results[3] = gbLvl13.CalculateResult ();
			CreateResultFile (OutputDir + lvl13 + ExtOut, lvl1Results[3]);

			return lvl1Results;
		}

		static string[] Level2 ()
		{
			var lvl2Results = new string[4];

			const string lvl20 = Level2File + "0";
			var gbLvl20 = new GameBoardLvl2 (InputDir + lvl20 + ExtIn);
			lvl2Results[0] = gbLvl20.CalculateResult ();
			CreateResultFile (OutputDir + lvl20 + ExtOut, lvl2Results[0]);

			const string lvl21 = Level2File + "1";
			var gbLvl21 = new GameBoardLvl2 (InputDir + lvl21 + ExtIn);
			lvl2Results[1] = gbLvl21.CalculateResult ();
			CreateResultFile (OutputDir + lvl21 + ExtOut, lvl2Results[1]);

			const string lvl22 = Level2File + "2";
			var gbLvl22 = new GameBoardLvl2 (InputDir + lvl22 + ExtIn);
			lvl2Results[2] = gbLvl22.CalculateResult ();
			CreateResultFile (OutputDir + lvl22 + ExtOut, lvl2Results[2]);

			const string lvl23 = Level2File + "3";
			var gbLvl23 = new GameBoardLvl2 (InputDir + lvl23 + ExtIn);
			lvl2Results[3] = gbLvl23.CalculateResult ();
			CreateResultFile (OutputDir + lvl23 + ExtOut, lvl2Results[3]);

			return lvl2Results;
		}

		static string[] Level3 ()
		{
			var lvl3Results = new List<string> ();
			GameBoardLvl3 gbLvl3;
			string result;
			string lvlFile;

			lvlFile = Level3File + "0";
			gbLvl3 = new GameBoardLvl3 (InputDir + lvlFile + ExtIn);
			result = gbLvl3.CalculateResult ();
			lvl3Results.Add (result);
			CreateResultFile (OutputDir + lvlFile + ExtOut, result);

			lvlFile = Level3File + "01";
			gbLvl3 = new GameBoardLvl3 (InputDir + lvlFile + ExtIn);
			result = gbLvl3.CalculateResult ();
			lvl3Results.Add (result);
			CreateResultFile (OutputDir + lvlFile + ExtOut, result);

			lvlFile = Level3File + "1";
			gbLvl3 = new GameBoardLvl3 (InputDir + lvlFile + ExtIn);
			result = gbLvl3.CalculateResult ();
			lvl3Results.Add (result);
			CreateResultFile (OutputDir + lvlFile + ExtOut, result);

			lvlFile = Level3File + "02";
			gbLvl3 = new GameBoardLvl3 (InputDir + lvlFile + ExtIn);
			result = gbLvl3.CalculateResult ();
			lvl3Results.Add (result);
			CreateResultFile (OutputDir + lvlFile + ExtOut, result);

			lvlFile = Level3File + "2";
			gbLvl3 = new GameBoardLvl3 (InputDir + lvlFile + ExtIn);
			result = gbLvl3.CalculateResult ();
			lvl3Results.Add (result);
			CreateResultFile (OutputDir + lvlFile + ExtOut, result);

			lvlFile = Level3File + "03";
			gbLvl3 = new GameBoardLvl3 (InputDir + lvlFile + ExtIn);
			result = gbLvl3.CalculateResult ();
			lvl3Results.Add (result);
			CreateResultFile (OutputDir + lvlFile + ExtOut, result);

			lvlFile = Level3File + "3";
			gbLvl3 = new GameBoardLvl3 (InputDir + lvlFile + ExtIn);
			result = gbLvl3.CalculateResult ();
			lvl3Results.Add (result);
			CreateResultFile (OutputDir + lvlFile + ExtOut, result);

			lvlFile = Level3File + "04";
			gbLvl3 = new GameBoardLvl3 (InputDir + lvlFile + ExtIn);
			result = gbLvl3.CalculateResult ();
			lvl3Results.Add (result);
			CreateResultFile (OutputDir + lvlFile + ExtOut, result);

			lvlFile = Level3File + "4";
			gbLvl3 = new GameBoardLvl3 (InputDir + lvlFile + ExtIn);
			result = gbLvl3.CalculateResult ();
			lvl3Results.Add (result);
			CreateResultFile (OutputDir + lvlFile + ExtOut, result);

			lvlFile = Level3File + "5";
			gbLvl3 = new GameBoardLvl3 (InputDir + lvlFile + ExtIn);
			result = gbLvl3.CalculateResult ();
			lvl3Results.Add (result);
			CreateResultFile (OutputDir + lvlFile + ExtOut, result);

			lvlFile = Level3File + "6";
			gbLvl3 = new GameBoardLvl3 (InputDir + lvlFile + ExtIn);
			result = gbLvl3.CalculateResult ();
			lvl3Results.Add (result);
			CreateResultFile (OutputDir + lvlFile + ExtOut, result);

			lvlFile = Level3File + "7";
			gbLvl3 = new GameBoardLvl3 (InputDir + lvlFile + ExtIn);
			result = gbLvl3.CalculateResult ();
			lvl3Results.Add (result);
			CreateResultFile (OutputDir + lvlFile + ExtOut, result);

			return lvl3Results.ToArray ();
		}

		static Image[] Level4 ()
		{
			var lvl4Results = new List<Image> ();

			GameBoardLvl4 gbLvl4;
			Image result;
			string lvlFile;
			string extLvl4 = ".bmp";

			lvlFile = Level4File + "0";
			gbLvl4 = new GameBoardLvl4 (InputDir + lvlFile + ExtIn);
			result = gbLvl4.CalculateResult ();
			lvl4Results.Add (result);
			CreateResultFile (OutputDir + lvlFile + ExtOut + extLvl4, result);

			lvlFile = Level4File + "1";
			gbLvl4 = new GameBoardLvl4 (InputDir + lvlFile + ExtIn);
			result = gbLvl4.CalculateResult ();
			lvl4Results.Add (result);
			CreateResultFile (OutputDir + lvlFile + ExtOut + extLvl4, result);

			lvlFile = Level4File + "2";
			gbLvl4 = new GameBoardLvl4 (InputDir + lvlFile + ExtIn);
			result = gbLvl4.CalculateResult ();
			lvl4Results.Add (result);
			CreateResultFile (OutputDir + lvlFile + ExtOut + extLvl4, result);

			lvlFile = Level4File + "3";
			gbLvl4 = new GameBoardLvl4 (InputDir + lvlFile + ExtIn);
			result = gbLvl4.CalculateResult ();
			lvl4Results.Add (result);
			CreateResultFile (OutputDir + lvlFile + ExtOut + extLvl4, result);

			lvlFile = Level4File + "4";
			gbLvl4 = new GameBoardLvl4 (InputDir + lvlFile + ExtIn);
			result = gbLvl4.CalculateResult ();
			lvl4Results.Add (result);
			CreateResultFile (OutputDir + lvlFile + ExtOut + extLvl4, result);

			lvlFile = Level4File + "5";
			gbLvl4 = new GameBoardLvl4 (InputDir + lvlFile + ExtIn);
			result = gbLvl4.CalculateResult ();
			lvl4Results.Add (result);
			CreateResultFile (OutputDir + lvlFile + ExtOut + extLvl4, result);

			return lvl4Results.ToArray ();
		}

		static string[] Level5 ()
		{
			var lvl5Results = new List<string> ();
			GameBoardLvl5 gbLvl5;
			string result;
			string lvlFile;

			lvlFile = Level5File + "0";
			gbLvl5 = new GameBoardLvl5 (InputDir + lvlFile + ExtIn);
			result = gbLvl5.CalculateResult ();
			lvl5Results.Add (result);
			CreateResultFile (OutputDir + lvlFile + ExtOut, result);
			
			lvlFile = Level5File + "1";
			gbLvl5 = new GameBoardLvl5 (InputDir + lvlFile + ExtIn);
			result = gbLvl5.CalculateResult ();
			lvl5Results.Add (result);
			CreateResultFile (OutputDir + lvlFile + ExtOut, result);
			
			lvlFile = Level5File + "2";
			gbLvl5 = new GameBoardLvl5 (InputDir + lvlFile + ExtIn);
			result = gbLvl5.CalculateResult ();
			lvl5Results.Add (result);
			CreateResultFile (OutputDir + lvlFile + ExtOut, result);
			
			lvlFile = Level5File + "3";
			gbLvl5 = new GameBoardLvl5 (InputDir + lvlFile + ExtIn);
			result = gbLvl5.CalculateResult ();
			lvl5Results.Add (result);
			CreateResultFile (OutputDir + lvlFile + ExtOut, result);
			
			lvlFile = Level5File + "4";
			gbLvl5 = new GameBoardLvl5 (InputDir + lvlFile + ExtIn);
			result = gbLvl5.CalculateResult ();
			lvl5Results.Add (result);
			CreateResultFile (OutputDir + lvlFile + ExtOut, result);
			

			
			return lvl5Results.ToArray ();
		}
		
		static void CreateResultFile (string file, string result)
		{
			if (!Directory.Exists (Path.GetDirectoryName(file))) Directory.CreateDirectory (Path.GetDirectoryName(file));
			var sw = File.CreateText (file);
			sw.Write (result);
			sw.Close ();
		}
		static void CreateResultFile (string file, Image result)
		{
			if (!Directory.Exists (Path.GetDirectoryName(file))) Directory.CreateDirectory (Path.GetDirectoryName(file));
			result.Save (file, System.Drawing.Imaging.ImageFormat.Bmp);
		}
	}
}
