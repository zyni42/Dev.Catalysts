using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CCC_22_Linz
{
	class Level5 : Level3, BaseStuff.ICccLevel
	{
		public Level5(bool talk) {
			HotDoc5.Talk = talk;
		}

		new public void CalculateResult()
		{
			string conLine ;
			// *** READ CONSOLE INPUTS ***
			BaseStuff.CccTest.WriteLineToStandardError (string.Format ("running : {0}", this));
			Console.WriteLine ("GET_NUMBER");
			var rodNumberTxt = Console.ReadLine ().Split (new string [] {" "}, StringSplitOptions.RemoveEmptyEntries);
			var rodNumberX = int.Parse (rodNumberTxt[0]);
			var rodNumberY = int.Parse (rodNumberTxt[1]);

			// *** CREATE HELPER OBJECTS ***
			var hotDoc = new HotDoc5 (rodNumberX, rodNumberY);

#if true
			hotDoc.MoveAny (200,  40);
//			MessageBox.Show ("öj");
			hotDoc.Circle (144);
#else
			hotDoc.MoveAny (400, 200);
			hotDoc.MoveAny (100, 100);
			hotDoc.MoveAny (200,  50);
			hotDoc.MoveAny (100, 150);
#endif

			
			BaseStuff.CccTest.WriteLineToStandardError (string.Format ("SEND :: {0}", "EXIT"));
			Console.WriteLine ("EXIT");
			conLine = Console.ReadLine ();
			BaseStuff.CccTest.WriteLineToStandardError (string.Format ("RECV :: {0}", conLine));

		}

		public class HotDoc5 : Level4.HotDoc4
		{
			public static bool Talk = false;
			public HotDoc5 (int numRodsX, int numRodsY) : base (numRodsX, numRodsY)
			{
			}

			public void Circle (/*float cX, float cY, */int numSteps)
			{
				bool talk = Talk;
				string conLine;
#if false
				{
					for (int rX = 0; rX < _rX; rX++) {
						for (int rY = 0; rY < _rY; rY++) {
							this.Rods[rX,rY].Lifted = true;
							this.Rods[rX,rY].DeviationX = 0;
							this.Rods[rX,rY].DeviationY = 0;
						}
					}
				}
#endif

				for (int step = 0; step < numSteps; step ++)
				{
					//Console.WriteLine ("GET_POSITION");
					//var posTxts = Console.ReadLine ().Split (new string [] {" "}, StringSplitOptions.RemoveEmptyEntries);
					//var posX = float.Parse (posTxts[0]);
					//var posY = float.Parse (posTxts[1]);

					//BaseStuff.CccTest.WriteLineToStandardError (string.Format ("POS  :: {0} {1}", posY, posY));

					//int stillToGoX = (int)((int)(toX) - posX);
					//int stillToGoY = (int)((int)(toY) - posY);

					//if (stillToGoX > 0) stillToGoX = Math.Min (stillToGoX, 5); else stillToGoX = Math.Max (stillToGoX, -5);
					//if (stillToGoY > 0) stillToGoY = Math.Min (stillToGoY, 5); else stillToGoY = Math.Max (stillToGoY, -5);

					//if (stillToGoX == 0 && stillToGoY == 0)
					//	break;

					if (step == 5) {
						int i = step * 9;
					}

					Console.WriteLine ("GET_SHAPE");
					var shapeString = Console.ReadLine ();
					if (string.IsNullOrEmpty (shapeString)) {
						BaseStuff.CccTest.WriteLineToStandardError (string.Format ("CIRCLE   : EMPTY INPUT -> QUIT"));
						return;
					}
					var shapeTxt = shapeString.Split (new string [] {" "}, StringSplitOptions.RemoveEmptyEntries);
					BaseStuff.CccTest.WriteLineToStandardError (string.Format ("CIRCLE   : SHAPESTRING [{0}]", shapeString));
					int [] sX = new int [4];
					int [] sY = new int [4];
					int maxX = int.MinValue;
					int minX = int.MaxValue;
					int maxY = int.MinValue;
					int minY = int.MaxValue;
					for (int i = 0; i < sX.Length; i++) {
						sX[i] = (int)float.Parse (shapeTxt[2*i+0].Replace (".",","));
						sY[i] = (int)float.Parse (shapeTxt[2*i+1].Replace (".",","));
						maxX = Math.Max (sX[i], maxX);	minX = Math.Min (sX[i], minX);
						maxY = Math.Max (sY[i], maxY);	minY = Math.Min (sY[i], minY);
					}
					minX = Math.Max (minX, 0);
					minY = Math.Max (minY, 0);
					maxX = Math.Min (maxX, _rX * 10);
					maxY = Math.Min (maxY, _rY * 10);
					BaseStuff.CccTest.WriteLineToStandardError (string.Format ("CIRCLE   : coords  : X [{0,3}..{1,3}], Y [{2,3}...{3,3}]", minX, maxX, minY, maxY));

					minX /= 10;
//					maxX = (maxX + 9) / 10;
					maxX = (maxX + 19) / 10;
//					maxX = (maxX     ) / 10;

					minY /= 10;
//					maxY = (maxY + 9) / 10;
					maxY = (maxY + 19) / 10;
//					maxY = (maxY     ) / 10;

					BaseStuff.CccTest.WriteLineToStandardError (string.Format ("CIRCLE   : offsets : X [{0,3}..{1,3}], Y [{2,3}...{3,3}]", minX, maxX, minY, maxY));

					const int factor = 5;

					double angleOfMovement = ((2 * Math.PI) / numSteps) * step;
					double moveMentX = Math.Cos (angleOfMovement);
					double moveMentY = Math.Sin (angleOfMovement);

					int stillToGoX = (int)(moveMentX * factor);
					int stillToGoY = (int)(moveMentY * factor);

#if false
					minX = 0;
					minY = 0;
					maxX = _rX;
					maxY = _rY;
#endif
					
					for (int iteration = 0; iteration < 5; iteration++) {
						switch (iteration) {
							// defiere SOLL
							case 0: {
								for (int rX = 0; rX < _rX; rX++) {
									for (int rY = 0; rY < _rY; rY++) {
										if (rX < minX || rX > maxX ||
											rY < minY || rY > maxY) {
											this.Rods[rX,rY].Lifted = false;
											this.Rods[rX,rY].DeviationX = 0;
											this.Rods[rX,rY].DeviationY = 0;
										}
										else {
											if (this.Rods[rX,rY].GroupA) {
												this.Rods[rX,rY].Lifted = false;
											}
											else {
												this.Rods[rX,rY].Lifted = true;
											}
										}
									}
								}
								break;
							}

							case 1: {
							//	for (int rX = 0; rX < _rX; rX++) {
							//		for (int rY = 0; rY < _rY; rY++) {
								for (int rX = minX; rX < maxX; rX++) {
									for (int rY = minY; rY < maxY; rY++) {
										if (this.Rods[rX,rY].GroupB) {
											this.Rods[rX,rY].DeviationX = stillToGoX;
											this.Rods[rX,rY].DeviationY = stillToGoY;
										}
									}
								}
								break;
							}

							case 2: {
							//	for (int rX = 0; rX < _rX; rX++) {
							//		for (int rY = 0; rY < _rY; rY++) {
								for (int rX = minX; rX < maxX; rX++) {
									for (int rY = minY; rY < maxY; rY++) {
										if (this.Rods[rX,rY].GroupA) {
											this.Rods[rX,rY].Lifted = true;
										}
									}
								}
								break;
							}

							case 3: {
							//	for (int rX = 0; rX < _rX; rX++) {
							//		for (int rY = 0; rY < _rY; rY++) {
								for (int rX = minX; rX < maxX; rX++) {
									for (int rY = minY; rY < maxY; rY++) {
										if (this.Rods[rX,rY].GroupB) {
											this.Rods[rX,rY].Lifted = false;
										}
									}
								}
								break;
							}

							case 4: {
							//	for (int rX = 0; rX < _rX; rX++) {
							//		for (int rY = 0; rY < _rY; rY++) {
								for (int rX = minX; rX < maxX; rX++) {
									for (int rY = minY; rY < maxY; rY++) {
										if (this.Rods[rX,rY].GroupB) {
											this.Rods[rX,rY].Lifted = true;
											this.Rods[rX,rY].DeviationX = 0;
											this.Rods[rX,rY].DeviationY = 0;
										}
									}
								}
								break;
							}

							//case 5: {
							////	for (int rX = 0; rX < _rX; rX++) {
							////		for (int rY = 0; rY < _rY; rY++) {
							//	for (int rX = minX; rX < maxX; rX++) {
							//		for (int rY = minY; rY < maxY; rY++) {
							//			this.Rods[rX,rY].Lifted = false;
							//			this.Rods[rX,rY].DeviationX = 0;
							//			this.Rods[rX,rY].DeviationY = 0;
							//		}
							//	}
							//	break;
							//}
						}
						string docString;
						var command = this.GenerateCommand (out docString);
						if (talk) BaseStuff.CccTest.WriteLineToStandardError (string.Format ("CIRCLE   : SEND :: step {0}, iter {1}", step, iteration));
						if (string.IsNullOrEmpty (command)) {
							// NOP
							if (talk) BaseStuff.CccTest.WriteLineToStandardError (string.Format ("CIRCLE   : SEND :: command : ** empty **"));
						}
						else {
							Console.WriteLine (command);
							if (talk) BaseStuff.CccTest.WriteLineToStandardError (string.Format ("CIRCLE   : SEND :: command : {0}", docString));

							conLine = Console.ReadLine ();
							if (talk) BaseStuff.CccTest.WriteLineToStandardError (string.Format ("CIRCLE   : RECV :: {0}", conLine));
						}
						if (talk) MessageBox.Show (string.Format ("STEP {0} iteration {1}", step, iteration));

					}
					if (talk) MessageBox.Show (string.Format ("STEP {0} completed", step));


				//	System.Windows.Forms.MessageBox.Show (string.Format ("done : {0}", iteration));

				}
			}
		}
	}
}
