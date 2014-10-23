using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Addictive_Game
{
	class GameBoardLvl3
	{
		public class GamePoint
		{
			public int Position;
			public int Color;
			public int Row;
			public int Column;
			public int NumRows;
			public int NumColumns;

			public GamePoint() {}

			public GamePoint (int position, int numRows, int numColumns, int color)
			{
				Position = position;
				Color = color;
				NumRows = numRows;
				NumColumns = numColumns;
				PositionToRowAndColumn (position, numRows, numColumns, out Row, out Column);
			}
			public GamePoint (int row, int column, int numRows, int numColumns, int color)
			{
				Position = RowAndColumnToPosition (row, column, numRows, numColumns);
				Color = color;
				Row = row;
				Column = column;
				NumRows = numRows;
				NumColumns = numColumns;
			}

			public static void PositionToRowAndColumn (int position, int numRows, int numColumns, out int row, out int col)
			{
				row = col = -1;
				if (!IsValidPosition (position, numRows, numColumns)) return;
				row = ((position-1) / numColumns) + 1;
				col = position - (row-1) * numColumns;
			}

			public static int RowAndColumnToPosition (int row, int col, int numRows, int numColumns)
			{
				if (!IsValidRowAndColumn (row, col, numRows, numColumns)) return -1;
				return ((row-1) * numColumns) + col;
			}

			public static bool IsValidPosition (int position, int numRows, int numCols)
			{
				return (position > 0 && position <= (numRows * numCols));
			}
			public bool IsValidPosition ()
			{
				return (Position > 0 && Position<= (NumRows * NumColumns));
			}

			public static bool IsValidRowAndColumn (int row, int column, int numRows, int numColumns)
			{
				return (row > 0 && row <= numRows && column > 0 && column <= numColumns);
			}
			public bool IsValidRowAndColumn ()
			{
				return (Row > 0 && Row <= NumRows && Column > 0 && Column <= NumColumns);
			}
			public bool HasCollision (GamePoint otherPoint)
			{
				return (Position == otherPoint.Position || (Row == otherPoint.Row && Column == otherPoint.Column));
			}
		}

		public class GamePath
		{
			public class Step
			{
				public char CharDirection;
				public int RowDirection;
				public int ColDirection;
			}
			public static Dictionary<char,Step> AllowedStepDirections = new Dictionary<char,Step> () {
				{ 'N', new Step () { CharDirection='N', RowDirection=-1, ColDirection=0 } },
				{ 'E', new Step () { CharDirection='E', RowDirection=0, ColDirection=1 } },
				{ 'S', new Step () { CharDirection='S', RowDirection=1, ColDirection=0 } },
				{ 'W', new Step () { CharDirection='W', RowDirection=0, ColDirection=-1 } }
			};
			public int Color;
			public GamePoint StartPosition;
			public int Length;
			public Step[] Steps;
			public GamePoint[] StepPoints;
		}

		public int Rows;
		public int Cols;
		public int NumPoints;
		public GamePoint[] Points;
		public Dictionary<int,List<GamePoint>> PointsByColor;
		public int NumPaths;
		public GamePath[] Paths;

		public GameBoardLvl3() {}

		public GameBoardLvl3 (string inputFile)
		{
			var sr = File.OpenText (inputFile);
			var inputFileContent = sr.ReadToEnd ();
			sr.Close ();

			var lines = inputFileContent.Split (new string [] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
			var splitted = lines[0].Split (new char [] {' '});

			ParseInput (splitted);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="splittedInputData"></param>
		/// <returns>Offset of next test</returns>
		public int ParseInput (string[] splittedInputData)
		{
			Rows = int.Parse(splittedInputData[0]);
			Cols = int.Parse(splittedInputData[1]);

			int offsetNumPoints = 2;
			int offsetPoints = offsetNumPoints + 1;
			NumPoints = int.Parse(splittedInputData[offsetNumPoints]);
			Points = new GamePoint[NumPoints];
			PointsByColor = new Dictionary<int,List<GamePoint>> ();
			for (int i = 0; i < Points.Length; i++)
			{
				var pos = int.Parse(splittedInputData[offsetPoints+i*2]);
				var clr = int.Parse(splittedInputData[offsetPoints+i*2+1]);
				Points[i] = new GamePoint (pos,Rows,Cols,clr);

				if (!PointsByColor.ContainsKey (Points[i].Color))
					PointsByColor.Add (Points[i].Color, new List<GamePoint> ());
				PointsByColor[Points[i].Color].Add (Points[i]);
			}

			int offsetNumPaths = offsetPoints + NumPoints * 2;
			int offsetFirstPath = offsetNumPaths + 1;
			int offsetCurrPath = offsetFirstPath;
			NumPaths = int.Parse (splittedInputData[offsetNumPaths]);
			Paths = new GamePath [NumPaths];
			for (int i = 0; i < Paths.Length; i++)
			{
				var clr = int.Parse(splittedInputData[offsetCurrPath]);
				var startPos = int.Parse(splittedInputData[offsetCurrPath+1]);
				var len = int.Parse(splittedInputData[offsetCurrPath+2]);

				Paths[i] = new GamePath () {
					Color = clr,
					StartPosition = new GamePoint (startPos, Rows, Cols, clr),
					Length = len,
					Steps = new GamePath.Step [len],
					StepPoints = new GamePoint [len]
				};

				var lastStepPoint = Paths[i].StartPosition;
				for (int j = 0; j < Paths[i].Steps.Length; j++)
				{
					var dir = char.Parse (splittedInputData[offsetCurrPath+3+j]);
					Paths[i].Steps[j] = new GamePath.Step () {
						CharDirection = dir,
						RowDirection = GamePath.AllowedStepDirections[dir].RowDirection,
						ColDirection = GamePath.AllowedStepDirections[dir].ColDirection
					};
					
					var row = lastStepPoint.Row + Paths[i].Steps[j].RowDirection;
					var col = lastStepPoint.Column + Paths[i].Steps[j].ColDirection;
					Paths[i].StepPoints[j] = new GamePoint (row, col, Rows, Cols, clr);
					lastStepPoint = Paths[i].StepPoints[j];
				}

				offsetCurrPath += 3 + Paths[i].Length;
			}

			return offsetCurrPath;
		}

		public string CalculateResult ()
		{
			string result = string.Empty;

			// Check paths
			for (int i = 0; i < Paths.Length; i++)
			{
				// Start position invalid?
				if (!Paths[i].StartPosition.IsValidPosition ())
				{
					result += "-1 " + Paths[i].Steps.Length + "\r\n";
				}

				// Step invalid?
				bool hasInvalidStep = false;
				GamePoint lastPoint = Paths[i].StartPosition;
				for (int j = 0; j < Paths[i].Length; j++)
				{
					var nextRow = lastPoint.Row + Paths[i].Steps[j].RowDirection;
					var nextCol = lastPoint.Column + Paths[i].Steps[j].ColDirection;

					// Out of bounds?
					if (!GamePoint.IsValidRowAndColumn (nextRow, nextCol, Rows, Cols))
					{
						result += "-1 " + (j+1) + "\r\n";
						hasInvalidStep = true;
						break;
					}
					var nextPoint = new GamePoint (nextRow, nextCol, Rows, Cols, -1);

					// Touches point of different color?
					foreach (var color in PointsByColor.Keys)
					{
						if (color == Paths[i].Color) continue;
						foreach (var point in PointsByColor[color])
						{
							if (nextPoint.HasCollision (point) && nextPoint.Color != point.Color)
							{
								result += "-1 " + (j+1) + "\r\n";
								hasInvalidStep = true;
								break;
							}
						}
					}
					if (hasInvalidStep) break;

					// Crosses itself?
					for (int k = 0; k < j; k++)
					{
						if (nextPoint.HasCollision (Paths[i].StepPoints[k]))
						{
								result += "-1 " + (j+1) + "\r\n";
								hasInvalidStep = true;
								break;
						}
					}
					if (hasInvalidStep) break;
					lastPoint = nextPoint;
				}
				bool HasInvalidEnd = false;
				if (!hasInvalidStep)
				{
					// Does not end in corresponding place?
					GamePoint secondPoint = null;
					if (PointsByColor.ContainsKey (Paths[i].Color) && PointsByColor[Paths[i].Color].Count > 0)
					{
						foreach (var point in PointsByColor[Paths[i].Color])
						{
							if (!point.HasCollision (Paths[i].StartPosition))
							{
								secondPoint = point;
								break;
							}
						}
					}
					if (secondPoint == null) throw new InvalidOperationException (string.Format ("No second point for path {0} with color {1}?", i, Paths[i].Color));
					if (!lastPoint.HasCollision (secondPoint))
					{
						result += "-1 " + Paths[i].Length + "\r\n";
						HasInvalidEnd = true;
					}
				}

				// Path seems fine
				if (!HasInvalidEnd && !hasInvalidStep)
					result += "1 " + Paths[i].Length + "\r\n";
			}

			return result;
		}
	}
}
