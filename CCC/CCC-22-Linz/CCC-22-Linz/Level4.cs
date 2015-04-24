using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CCC_22_Linz
{
	class Level4 : Level3, BaseStuff.ICccLevel
	{
		public Level4() { }

		public class Rod4
		{
			public Rod4 (int posX, int posY)
			{
				PosX = posX;
				PosY = posY;

				GroupA = ((posX % 2) == 0) ^ ((posY % 2) == 0);
				GroupB = !GroupA ;
			}
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

			public int PosX;
			public int PosY;
			public bool GroupA;
			public bool GroupB;
		}

		public class HotDoc4
		{
			public HotDoc4 (int numRodsX, int numRodsY)
			{
				_rX = numRodsX;
				_rY = numRodsY;
				Rods = new Rod4[numRodsX,numRodsY];
				for (int rX = 0; rX < numRodsX; rX++) {
					for (int rY = 0; rY < numRodsY; rY++) {
						Rods[rX,rY] = new Rod4 (rX, rY);
					}
				}
			//	MessageBox.Show ("olj");
			}
			public int _rX, _rY;
			public Rod4 [,] Rods;

			public string GenerateCommand ()
			{
				StringBuilder stb = new StringBuilder ();
				for (int rX = 0; rX < _rX; rX++) {
					for (int rY = 0; rY < _rY; rY++) {
						bool mustLift     =  Rods[rX,rY].Lifted;
						bool mustDeviateX = (Rods[rX,rY].DeviationX != Rods[rX,rY].Old_deviationX);
						bool mustDeviateY = (Rods[rX,rY].DeviationY != Rods[rX,rY].Old_deviationY);

						if (mustDeviateX || mustDeviateY || mustLift) {
							var l = string.Format (" {0} {1} {2} {3}", rX+1, rY+1, Rods[rX,rY].DeviationX, Rods[rX,rY].DeviationY);
							stb.Append (l);
						}
						Rods[rX,rY].Store ();
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
					var posTxts = Console.ReadLine ().Split (new string [] {" "}, StringSplitOptions.RemoveEmptyEntries);
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
							for (int rX = 0; rX < _rX; rX++) {
								for (int rY = 0; rY < _rY; rY++) {
									if (this.Rods[rX,rY].GroupA) {
										this.Rods[rX,rY].Lifted = false;
									}
								}
							}
							break;
						}

						case 1: {
							for (int rX = 0; rX < _rX; rX++) {
								for (int rY = 0; rY < _rY; rY++) {
									if (this.Rods[rX,rY].GroupB) {
										this.Rods[rX,rY].DeviationX = stillToGoX;
										this.Rods[rX,rY].DeviationY = stillToGoY;
									}
								}
							}
							break;
						}

						case 2: {
							for (int rX = 0; rX < _rX; rX++) {
								for (int rY = 0; rY < _rY; rY++) {
									if (this.Rods[rX,rY].GroupA) {
										this.Rods[rX,rY].Lifted = true;
									}
								}
							}
							break;
						}

						case 3: {
							for (int rX = 0; rX < _rX; rX++) {
								for (int rY = 0; rY < _rY; rY++) {
									if (this.Rods[rX,rY].GroupB) {
										this.Rods[rX,rY].Lifted = false;
									}
								}
							}
							break;
						}

						case 4: {
							for (int rX = 0; rX < _rX; rX++) {
								for (int rY = 0; rY < _rY; rY++) {
									if (this.Rods[rX,rY].GroupB) {
										this.Rods[rX,rY].Lifted = true;
										this.Rods[rX,rY].DeviationX = 0;
										this.Rods[rX,rY].DeviationY = 0;
									}
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
				//	BaseStuff.CccTest.WriteLineToStandardError (string.Format ("MOVE-R   : SEND :: {0}", command));
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
			var rodNumberTxt = Console.ReadLine ().Split (new string [] {" "}, StringSplitOptions.RemoveEmptyEntries);
			var rodNumberX = int.Parse (rodNumberTxt[0]);
			var rodNumberY = int.Parse (rodNumberTxt[1]);

			// *** CREATE HELPER OBJECTS ***
			var hotDoc = new HotDoc4 (rodNumberX, rodNumberY);

#if true
			hotDoc.MoveAny (400,  40);
			hotDoc.MoveAny (400, 200);
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
	}
}
