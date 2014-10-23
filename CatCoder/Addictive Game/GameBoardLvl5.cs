using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Addictive_Game
{
	class GameBoardLvl5  : GameBoardLvl3
	{
		public class TestPointStatus
		{
			public Tuple<int,int>[] ColorStates;		// Color = 1...n; Status = 1 (connected), 2 (connectable), 3 (not connectable)

			public string ToResult ()
			{
				var result = string.Empty;
				var orderedColorStates = ColorStates.OrderBy ((item) => item.Item1).ToArray ();
				foreach (var colorState in orderedColorStates)
				{
					result += colorState.Item2 + " ";
				}
				return result.Substring (0,result.Length-1);
			}
		}

		public class TestFieldStatus
		{
			public bool[] UsedFields;
		}

		public class CheckedGamePoint : GamePoint
		{
			bool CheckedNorth = false;
			bool CheckedEast = false;
			bool CheckedSouth = false;
			bool CheckedWest = false;

			public CheckedGamePoint (GamePoint gamePoint)
			{
				Color = gamePoint.Color;
				NumRows = gamePoint.NumRows;
				NumColumns = gamePoint.NumColumns;
				Row = gamePoint.Row;
				Column = gamePoint.Column;
				Position = gamePoint.Position;
			}

			public GamePath.Step NextPossibleDirection ()
			{
				if			(!CheckedEast)		return GamePath.AllowedStepDirections['E'];
				else if		(!CheckedSouth)		return GamePath.AllowedStepDirections['S'];
				else if		(!CheckedWest)		return GamePath.AllowedStepDirections['W'];
				else if		(!CheckedNorth)		return GamePath.AllowedStepDirections['N'];
				else							return null;
			}
		}

		GameBoardLvl3[] Tests;
		TestPointStatus[] TestPointStates;
		TestFieldStatus[] TestFieldStates;
		int[] TestNumFields;

		public GameBoardLvl5 (string inputFile)
		{
			var sr = File.OpenText (inputFile);
			var inputFileContent = sr.ReadToEnd ();
			sr.Close ();

			var lines = inputFileContent.Split (new string [] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
			var splitted = lines[0].Split (new char [] {' '});

			var numTests = int.Parse(splitted[0]);
			Tests = new GameBoardLvl3[numTests];
			TestPointStates = new TestPointStatus[numTests];
			TestFieldStates = new TestFieldStatus[numTests];
			TestNumFields = new int[numTests];

			var splittedTests = new string[splitted.Length - 1];
			Array.Copy (splitted, 1, splittedTests, 0, splittedTests.Length);

			int offsetNextTest = 0;
			for (int i = 0; i < Tests.Length; i++)
			{
				var nextSplittedTests = new string[splittedTests.Length - offsetNextTest];
				Array.Copy (splittedTests, offsetNextTest, nextSplittedTests, 0, nextSplittedTests.Length);
				splittedTests = nextSplittedTests;

				Tests[i] = new GameBoardLvl3 ();
				offsetNextTest = Tests[i].ParseInput (splittedTests);

				TestNumFields[i] = Tests[i].Rows * Tests[i].Cols;

				TestPointStates[i] = new TestPointStatus () {
					ColorStates = new Tuple<int,int> [Tests[i].PointsByColor.Count]
				};

				TestFieldStates[i] = new TestFieldStatus () {
					UsedFields = new bool[Tests[i].Rows * Tests[i].Cols]
				};
				
				foreach (var point in Tests[i].Points)
					TestFieldStates[i].UsedFields[point.Position-1] = true;

				foreach (var path in Tests[i].Paths)
				{
					foreach (var point in path.StepPoints)
					{
						TestFieldStates[i].UsedFields[point.Position-1] = true;
					}
				}
			}
		}

		bool _CheckNextStepToFinish (int stepCounter, GameBoardLvl3 map, TestFieldStatus usedFields, TestFieldStatus checkedFields, GamePoint currentStep, GamePoint endPoint, out bool isConnectable)
		{
			isConnectable = false;
			stepCounter++;

			// Mark current step as checked
			checkedFields.UsedFields[currentStep.Position - 1] = true;

			// Are we there?
			if (currentStep.HasCollision(endPoint))
			{
				checkedFields.UsedFields[currentStep.Position - 1] = false;		// Reset current step to unchecked
				
				isConnectable = true;
				return true;
			}

			// Try to navigate to next step in this order: E, S, W, N
			var nextStep = new GamePoint (currentStep.Row, currentStep.Column + 1, map.Rows, map.Cols, -1);	// E
			var isValidPosition = (nextStep.Position != -1);
			var isUsedField = (isValidPosition && usedFields.UsedFields[nextStep.Position - 1]) ? (nextStep.HasCollision (endPoint) ? false : true) : false;
			var isCheckedField = (isValidPosition && checkedFields.UsedFields [nextStep.Position - 1]);
			if (isValidPosition && !isUsedField && !isCheckedField && _CheckNextStepToFinish (stepCounter, map, usedFields, checkedFields, nextStep, endPoint, out isConnectable)) return true;
			else
			{
				nextStep = new GamePoint (currentStep.Row + 1, currentStep.Column, map.Rows, map.Cols, -1);	// S
				isValidPosition = (nextStep.Position != -1);
				isUsedField = (isValidPosition && usedFields.UsedFields[nextStep.Position - 1]) ? (nextStep.HasCollision (endPoint) ? false : true) : false;
				isCheckedField = (isValidPosition && checkedFields.UsedFields [nextStep.Position - 1]);
				if (isValidPosition && !isUsedField && !isCheckedField && _CheckNextStepToFinish (stepCounter, map, usedFields, checkedFields, nextStep, endPoint, out isConnectable)) return true;
				else
				{
					nextStep = new GamePoint (currentStep.Row, currentStep.Column - 1, map.Rows, map.Cols, -1);	// W
					isValidPosition = (nextStep.Position != -1);
					isUsedField = (isValidPosition && usedFields.UsedFields[nextStep.Position - 1]) ? (nextStep.HasCollision (endPoint) ? false : true) : false;
					isCheckedField = (isValidPosition && checkedFields.UsedFields [nextStep.Position - 1]);
					if (isValidPosition && !isUsedField && !isCheckedField && _CheckNextStepToFinish (stepCounter, map, usedFields, checkedFields, nextStep, endPoint, out isConnectable)) return true;
					{
						nextStep = new GamePoint (currentStep.Row - 1, currentStep.Column, map.Rows, map.Cols, -1);	// N
						isValidPosition = (nextStep.Position != -1);
						isUsedField = (isValidPosition && usedFields.UsedFields[nextStep.Position - 1]) ? (nextStep.HasCollision (endPoint) ? false : true) : false;
						isCheckedField = (isValidPosition && checkedFields.UsedFields [nextStep.Position - 1]);
						if (isValidPosition && !isUsedField && !isCheckedField && _CheckNextStepToFinish (stepCounter, map, usedFields, checkedFields, nextStep, endPoint, out isConnectable)) return true;
					}
				}
			}

			checkedFields.UsedFields[currentStep.Position - 1] = false;		// Reset current step to unchecked
			stepCounter--;
			return false;
		}

		string _CalculateResultRecursive ()
		{
			string result = string.Empty;
			
			// Check tests
			for (int i = 0; i < Tests.Length; i++)
			{
				// Check point states per color
				foreach (var colorKey in Tests[i].PointsByColor.Keys)
				{
					var color = Tests[i].PointsByColor[colorKey][0].Color;
					var startPoint = Tests[i].PointsByColor[colorKey][0];
					var endPoint = Tests[i].PointsByColor[colorKey][1];

					// Check if points are already connected (have a path definition)
					var isConnected = false;
					foreach (var path in Tests[i].Paths)
						if (path.Color == color) {
							TestPointStates[i].ColorStates[color-1] = new Tuple<int,int> (color, 1);	// connected
							isConnected = true;
							break;
							}

					var isConnectable = false;
					if (!isConnected)
					{
						// Check if points are connectable
						var stepCounter = 0;
						var checkedFields = new TestFieldStatus () {
							UsedFields = new bool[Tests[i].Rows * Tests[i].Cols]
						};
						_CheckNextStepToFinish (stepCounter, Tests[i], TestFieldStates[i], checkedFields, startPoint, endPoint, out isConnectable);
						if (isConnectable)	TestPointStates[i].ColorStates[color-1] = new Tuple<int,int> (color, 2);	// connectable
						else				TestPointStates[i].ColorStates[color-1] = new Tuple<int,int> (color, 3);	// not connectable
					}
				}
				result += TestPointStates[i].ToResult () + " ";
			}
			
			return result.Substring (0,result.Length-1);
		}

		string _CalculateResultLoops ()
		{
			string result = string.Empty;

			// Check tests
			for (int i = 0; i < Tests.Length; i++)
			{
				// Check point states per color
				foreach (var colorKey in Tests[i].PointsByColor.Keys)
				{
					var color = Tests[i].PointsByColor[colorKey][0].Color;
					var startPoint = new CheckedGamePoint (Tests[i].PointsByColor[colorKey][0]);
					var endPoint = new CheckedGamePoint (Tests[i].PointsByColor[colorKey][1]);

					// Check if points are already connected (have a path definition)
					var isConnected = false;
					foreach (var path in Tests[i].Paths)
						if (path.Color == color) {
							TestPointStates[i].ColorStates[color-1] = new Tuple<int,int> (color, 1);	// connected
							isConnected = true;
							break;
							}

					var isConnectable = false;
					if (!isConnected)
					{
						// Check if points are connectable
						var stepsToEnd = new Stack<int> (TestNumFields[i]);							// position stack
						var checkedFields = new Dictionary<int,CheckedGamePoint> (TestNumFields[i]);	// position key
						var rows = Tests[i].Rows;
						var cols = Tests[i].Cols;
						CheckedGamePoint nextField;
						while (!isConnectable)
						{
							var lastField = checkedFields[stepsToEnd.Peek()];
							var nextStepDir = lastField.NextPossibleDirection ();
							if (nextStepDir != null)	// step forward
							{
								nextField = new CheckedGamePoint (new GamePoint (lastField.Row + nextStepDir.RowDirection, lastField.Column + nextStepDir.ColDirection, rows, cols, -1));
							}
							else						// step backward
							{

							}







#error CHECK EACH FIELD AND STORE STATUS IN CHECKEDFIELDS!!!
							break;
						}
						if (isConnectable)	TestPointStates[i].ColorStates[color-1] = new Tuple<int,int> (color, 2);	// connectable
						else				TestPointStates[i].ColorStates[color-1] = new Tuple<int,int> (color, 3);	// not connectable
					}
				}
				result += TestPointStates[i].ToResult () + " ";
			}

			return result.Substring (0,result.Length-1);
		}

		public new string CalculateResult ()
		{
			return _CalculateResultRecursive ();
		}
	}
}
