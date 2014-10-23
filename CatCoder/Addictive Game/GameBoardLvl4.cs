using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Addictive_Game
{
	class GameBoardLvl4 : GameBoardLvl3
	{
		public bool[] DrawnPaths;
		public bool[] DrawnPoints;
		public int NumFields;

		public GameBoardLvl4 (string inputFile)
			: base (inputFile)
		{
			// Initially, all paths are invalid
			DrawnPaths = new bool [NumPaths];
			for (int i = 0; i < DrawnPaths.Length; i++) DrawnPaths[i] = false;

			NumFields = Rows * Cols;
			DrawnPoints = new bool[NumFields];
			for (int i = 0; i < DrawnPoints.Length; i++) DrawnPoints[i] = false;
		}

		public new Image CalculateResult ()
		{
			Bitmap result = new Bitmap (Cols,Rows);
			var fastBmp = new BitmapProcessing.FastBitmap (result);
	
			// Clear image (make background white)
			fastBmp.LockImage ();
			for (int x = 0; x < Cols; x++)
			{
				for (int y = 0; y < Rows; y++)
				{
					fastBmp.SetPixel (x,y,Color.White);
				}
			}
			fastBmp.UnlockImage ();

			// Draw points
			fastBmp.LockImage ();
			for (int i = 0; i < Points.Length; i++)
			{
				fastBmp.SetPixel (Points[i].Column - 1, Points[i].Row - 1,Color.Black);
				DrawnPoints[Points[i].Position - 1] = true;
			}
			fastBmp.UnlockImage ();

			// Draw paths
			for (int i = 0; i < Paths.Length; i++)
			{
				// Start position invalid?
				if (!Paths[i].StartPosition.IsValidPosition ())
				{
					continue;
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
						hasInvalidStep = true;
						break;
					}
					var nextPoint = new GamePoint (nextRow, nextCol, Rows, Cols, -1);

					// Crosses itself?
					for (int k = 0; k < j; k++)
					{
						if (nextPoint.HasCollision (Paths[i].StepPoints[k]))
						{
								hasInvalidStep = true;
								break;
						}
					}
					if (hasInvalidStep) break;

					// Crosses drawn points (points, other lines)
					if (DrawnPoints[nextPoint.Position - 1])
					{
						if (PointsByColor[Paths[i].Color].Count != 2)
							throw new InvalidOperationException (string.Format ("Farbpunkte({0}: {1}", Paths[i].Color, PointsByColor[Paths[i].Color].Count));

						if (
							(nextPoint.HasCollision (PointsByColor[Paths[i].Color][0]) && !nextPoint.HasCollision (Paths[i].StartPosition)) ||
							(nextPoint.HasCollision (PointsByColor[Paths[i].Color][1]) && !nextPoint.HasCollision (Paths[i].StartPosition))
							)
						{
						}
						else
						{
							hasInvalidStep = true;
							break;
						}
					}

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
						HasInvalidEnd = true;
					}
				}

				// Path seems fine
				if (!HasInvalidEnd && !hasInvalidStep)
				{
					DrawnPaths[i] = true;
					fastBmp.LockImage ();
					for (int j = 0; j < Paths[i].StepPoints.Length; j++) {
						fastBmp.SetPixel (Paths[i].StepPoints[j].Column - 1, Paths[i].StepPoints[j].Row - 1, Color.Black);
						DrawnPoints[Paths[i].StepPoints[j].Position - 1] = true;
					}
					fastBmp.UnlockImage ();
				}
			}

			return result;
		}
	}
}
