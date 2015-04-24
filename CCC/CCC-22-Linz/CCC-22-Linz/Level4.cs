using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC_22_Linz
{
	class Level4 : Level3, BaseStuff.ICccLevel
	{
		public Level4() { }

		public class Rod4
		{
			public void Store ()
			{
				Old_deviationX = DeviationX;
				Old_deviationY = DeviationY;
				Old_lifted    = Lifted;
			}
			public bool Old_lifted = true;
			public int  Old_deviationY = 0;
			public int  Old_deviationX = 0;

			public bool Lifted;
			public int DeviationX;
			public int DeviationY;
		}

		public class HotDoc4
		{
			public HotDoc4 (int numRodsX, int numRodsY)
			{
				_rX = numRodsX;
				_rY = numRodsY;
				Rods = new Rod4[numRodsX][numRodsY];
				for (int rX = 0; rX < numRodsX; rX++) {
					for (int rY = 0; rY < numRodsY; rY++) {
						Rods[rX][rY] = new Rod4 ();
					}
				}
			}
			public int _rX, _rY;
			public Rod4 [][] Rods;

			public string GenerateCommand ()
			{
				StringBuilder stb = new StringBuilder ();
				for (int rX = 0; rX < numRodsX; rX++) {
					for (int rY = 0; rY < numRodsY; rY++) {
						bool mustLift     =  Rods[rX][rY].Lifted;
						bool mustDeviateX = (Rods[rX][rY].DeviationX != Rods[rX][rY].Old_deviationX);
						bool mustDeviateY = (Rods[rX][rY].DeviationY != Rods[rX][rY].Old_deviationY);

						if (mustDeviateX || mustDeviateY || mustLift) {
							var l = string.Format (" {0} {1} {2} {3}", rX+1, rY+1, Rods[rX][rY].DeviationX, Rods[rX][rY].DeviationY);
							stb.Append (l);
						}
						Rods[rX][rY].Store ();
					}
				}
				if (stb.Length > 0)
					stb.Insert (0, "MOVE");

				return stb.ToString ();
			}


			public void MoveAny (float toX, float toY)
			{
				string conLine;
				for (int iteration = 0; ; iteration ++)
				{
					Console.WriteLine ("GET_POSITION");
					var posTxts = Console.ReadLine ().Split (" ", StringSplitOptions.RemoveEmptyEntries);
					var posX = float.Parse (posTxts[0]);
					var posY = float.Parse (posTxts[1]);

					BaseStuff.CccTest.WriteLineToStandardError (string.Format ("POS  :: {0} {1}", posY, posY));

					int stillToGoX = (int)((int)(toX) - posX);
					int stillToGoY = (int)((int)(toY) - posY);

					if (stillToGoX > 0) stillToGoX = Math.Min (stillToGoX, 5); else stillToGoX = Math.Max (stillToGoX, -5);
					if (stillToGoY > 0) stillToGoY = Math.Min (stillToGoY, 5); else stillToGoY = Math.Max (stillToGoY, -5);

					if (stillToGoX == 0 && stillToGoY == 0)
						break;

					switch (iteration % 5) {
						// defiere SOLL
						case 0: {
							for (int r = 1; r <= this.Rods.Length; r++) {
								if ((r % 2) == 0) {
									this.Rods[r-1].Lifted = false;
								}
							}
							break;
						}

						case 1: {
							for (int r = 1; r <= this.Rods.Length; r++) {
								if ((r % 2) == 1) {
									this.Rods[r-1].Deviation = stillToGo;
								}
							}
							break;
						}

						case 2: {
							for (int r = 1; r <= this.Rods.Length; r++) {
								if ((r % 2) == 0) {
									this.Rods[r-1].Lifted = true;
								}
							}
							break;
						}

						case 3: {
							for (int r = 1; r <= this.Rods.Length; r++) {
								if ((r % 2) == 1) {
									this.Rods[r-1].Lifted = false;
								}
							}
							break;
						}

						case 4: {
							for (int r = 1; r <= this.Rods.Length; r++) {
								if ((r % 2) == 1) {
									this.Rods[r-1].Lifted = true;
									this.Rods[r-1].Deviation = 0;
								}
							}
							break;
						}

						//default:
						//	exitLoop = true;
						//	break;
					}
					//if (exitLoop)
					//	break;

					var command = this.GenerateCommand ();
					BaseStuff.CccTest.WriteLineToStandardError (string.Format ("MOVE-R   : SEND :: {0}", command));
					Console.WriteLine (command);
					// commands, um soll zu erreichen

					// *** INPUT #1 ***
					conLine = Console.ReadLine ();
					if (string.IsNullOrEmpty (conLine)) {
						BaseStuff.CccTest.WriteLineToStandardError ("TERMINATE for empty input");
						break;
					}

					BaseStuff.CccTest.WriteLineToStandardError (string.Format ("MOVE-R   : RECV :: {0}", conLine));

				//	System.Windows.Forms.MessageBox.Show (string.Format ("done : {0}", iteration));

				}
			}	
		}

		new public void CalculateResult()
		{
			string conLine ;
			// *** READ CONSOLE INPUTS ***
			BaseStuff.CccTest.WriteLineToStandardError (string.Format ("running : {0}", this));
			Console.WriteLine ("GET_NUMBER");
			var rodNumberTxt = Console.ReadLine ();
			var rodNumber = int.Parse (rodNumberTxt);

			// *** CREATE HELPER OBJECTS ***
			var hotDoc = new HotDoc3 (rodNumber);

			hotDoc.MoveAny (995);
			hotDoc.MoveAny (495);

			
			BaseStuff.CccTest.WriteLineToStandardError (string.Format ("SEND :: {0}", "EXIT"));
			Console.WriteLine ("EXIT");
			conLine = Console.ReadLine ();
			BaseStuff.CccTest.WriteLineToStandardError (string.Format ("RECV :: {0}", conLine));

		}
	}
}
