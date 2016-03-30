using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace BaseStuff
{
    public class CccTest
    {
		#region Parse input
		public static List<string[]> ReadInputFile (string inputFile)
		{
			return ReadInputFileExtended (inputFile, new string [] { "\r", "\n" }, new string [] {" "});
		}

		public static List<string[]> ReadInputFileByComma (string inputFile)
		{
			return ReadInputFileExtended (inputFile, new string [] { "\r", "\n" }, new string [] {","});
		}

		public static List<string[]> ReadInputFileExtended (string inputFile, string[] newLineChars, string[] dataSeperators)
		{
			var sr = File.OpenText (inputFile);
			var inputFileContent = sr.ReadToEnd ();
			sr.Close ();

			var lines = inputFileContent.Split (newLineChars, StringSplitOptions.RemoveEmptyEntries);
			List<string[]> splittedLines = new List<string[]> (lines.Length);
			
			for (int i = 0; i < lines.Length; i++)
				splittedLines.Add (SplitBySeperators (lines[i], dataSeperators));
			
			return splittedLines;
		}

		public static string[] SplitBySpaces (string input)
		{
			return SplitBySeperators (input, new string[] {" "});
		}

		public static string[] SplitBySeperators (string input, string[] seperators)
		{
			return input.Split (seperators, StringSplitOptions.RemoveEmptyEntries);
		}
		#endregion

		#region Create Results
		public static void CreateResultTxtFile (string file, string result)
		{
			try { if (!Directory.Exists (Path.GetDirectoryName(file))) Directory.CreateDirectory (Path.GetDirectoryName(file)); }
			catch { }

			var sw = File.CreateText (file);
			sw.Write (result);
			sw.Close ();
		}

		public static void CreateResultImageFile (string file, Image result)
		{
			if (!Directory.Exists (Path.GetDirectoryName(file))) Directory.CreateDirectory (Path.GetDirectoryName(file));
			result.Save (file, System.Drawing.Imaging.ImageFormat.Bmp);
		}

		/// <summary>
		/// NOTE:  Returned FastBitmap is linked to referenced Bitmap. Use referneced bitmap after drawing with FastBitmap is finished!
		/// USAGE: FastBitmap.LockImage(), FastBitmap.SetPixel(xxx)/.GetPixel (xxx), FastBitmap.UnlockImage()
		/// </summary>
		/// <param name="bitmap"></param>
		/// <returns></returns>
		public static FastBitmap CreateFastBitmap (ref Bitmap bitmap)
		{
			return new FastBitmap (bitmap);
		}
		#endregion

		#region 1-based position & row/column helper
		// NOTE: 1-based positions/rows/cols
		public static void OneBasedPositionToRowAndColumn (int position, int numRows, int numColumns, out int row, out int col)
		{
			row = col = -1;
			if (!IsValidOneBasedPosition (position, numRows, numColumns)) return;
			row = ((position-1) / numColumns) + 1;
			col = position - (row-1) * numColumns;
		}

		// NOTE: 1-based positions/rows/cols
		public static int OneBasedRowAndColumnToPosition (int row, int col, int numRows, int numColumns)
		{
			if (!IsValidOneBasedRowAndColumn (row, col, numRows, numColumns)) return -1;
			return ((row-1) * numColumns) + col;
		}

		// NOTE: 1-based positions/rows/cols
		public static bool IsValidOneBasedPosition (int position, int numRows, int numCols)
		{
			return (position > 0 && position <= (numRows * numCols));
		}

		// NOTE: 1-based positions/rows/cols
		public static bool IsValidOneBasedRowAndColumn (int row, int column, int numRows, int numColumns)
		{
			return (row > 0 && row <= numRows && column > 0 && column <= numColumns);
		}
		#endregion

		#region Output to STDERR
		public static void WriteLineToStandardError (string msg)
		{
			msg += System.Environment.NewLine;
			byte[] bytes = new byte[msg.Length * sizeof(char)];
			System.Buffer.BlockCopy(msg.ToCharArray(), 0, bytes, 0, bytes.Length);
			Console.OpenStandardError ().Write (bytes, 0, bytes.Length);
		}
		public static void WriteCrToStandardError (string msg)
		{
			msg += "\r";
			byte[] bytes = new byte[msg.Length * sizeof(char)];
			System.Buffer.BlockCopy(msg.ToCharArray(), 0, bytes, 0, bytes.Length);
			Console.OpenStandardError ().Write (bytes, 0, bytes.Length);
		}
		#endregion

		#region Conversion helper
		public static float FloatParse (string floatNr)
		{
			return float.Parse (floatNr.Replace (".", ","));
		}
		#endregion
    }
}
