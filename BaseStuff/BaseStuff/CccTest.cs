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
		public static List<string[]> ReadInputFile (string inputFile)
		{
			var sr = File.OpenText (inputFile);
			var inputFileContent = sr.ReadToEnd ();
			sr.Close ();

			var lines = inputFileContent.Split (new string [] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
			List<string[]> splittedLines = new List<string[]> (lines.Length);
			
			for (int i = 0; i < lines.Length; i++)
				splittedLines.Add (lines[i].Split (new char [] {' '}));
			
			return splittedLines;
		}

		public static void CreateResultTxtFile (string file, string result)
		{
			if (!Directory.Exists (Path.GetDirectoryName(file))) Directory.CreateDirectory (Path.GetDirectoryName(file));

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
    }
}
