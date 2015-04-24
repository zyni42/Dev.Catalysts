using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC_22_Linz
{
	class Level1 : BaseStuff.ICccLevel
	{
		class Rod
		{
			public void Store ()
			{
				Old_deviation = Deviation;
				Old_lifted    = Lifted;
			}
			public bool Old_lifted = true;
			public int  Old_deviation = 0;
			private bool _lifted = true;

			public bool Lifted
			{
				get { return _lifted; }
				set { _lifted = value; }
			}
			private int _deviation = 0;

			public int Deviation
			{
				get { return _deviation; }
				set { _deviation = value; }
			}
		}

		class HotDoc
		{
			public HotDoc (int numRods)
			{
				Rods = new Rod[numRods];
				for (int r = 0; r < numRods; r++) {
					Rods[r] = new Rod ();
				}
			}
			public Rod [] Rods { get; set; }
			public string GenerateCommand ()
			{
				StringBuilder stb = new StringBuilder ();
				for (int r = 0; r < this.Rods.Length; r++) {
					bool mustLift    =  Rods[r].Lifted;
					bool mustDeviate = (Rods[r].Deviation != Rods[r].Old_deviation);

					if (mustDeviate || mustLift) {
						var l = string.Format (" {0} {1}", r+1, Rods[r].Deviation);
						stb.Append (l);
					}
					Rods[r].Store ();
				}
				if (stb.Length > 0)
					stb.Insert (0, "MOVE");

				return stb.ToString ();
			}
		}
		public Level1() { }

		public void CalculateResult()
		{

			string conLine ;
			// *** READ CONSOLE INPUTS ***
			BaseStuff.CccTest.WriteLineToStandardError (string.Format ("running : {0}", this));
			Console.WriteLine ("GET_NUMBER");
			var rodNumberTxt = Console.ReadLine ();
			var rodNumber = int.Parse (rodNumberTxt);

			// *** CREATE HELPER OBJECTS ***
			var hotDoc = new HotDoc (rodNumber);

			for (int iteration = 0; ; iteration ++)
			{
				bool exitLoop = false;
				switch (iteration) {
					// defiere SOLL
					case 0: {
						for (int r = 1; r <= hotDoc.Rods.Length; r++) {
							if ((r % 2) == 0) hotDoc.Rods[r-1].Lifted = false;
						}
						break;
					}

					case 1: {
						for (int r = 1; r <= hotDoc.Rods.Length; r++) {
							if (hotDoc.Rods [r - 1].Lifted) {
								hotDoc.Rods [r - 1].Lifted = false;
							}
							else {
								hotDoc.Rods [r - 1].Deviation = -5;
								hotDoc.Rods [r - 1].Lifted    = true;
							}
						}
						break;
					}

					case 2: {
						for (int r = 1; r <= hotDoc.Rods.Length; r++) {
							if (hotDoc.Rods [r - 1].Lifted) {
								hotDoc.Rods [r - 1].Deviation = 5;
							}
						}
						break;
					}

					// lift the lowered rods to 0
					case 3: {
						for (int r = 1; r <= hotDoc.Rods.Length; r++) {
							if (hotDoc.Rods [r - 1].Lifted == false) {
								hotDoc.Rods [r - 1].Lifted = true;
								hotDoc.Rods [r - 1].Deviation = 0;
							}
						}
						break;
					}

					case 4: {
						for (int r = 1; r <= hotDoc.Rods.Length; r++) {
							if (hotDoc.Rods [r - 1].Lifted    == true &&
								hotDoc.Rods [r - 1].Deviation == 5) {
									hotDoc.Rods [r - 1].Lifted = false;
							}
						}
						break;
					}

					case 5: {
						for (int r = 1; r <= hotDoc.Rods.Length; r++) {
							if (hotDoc.Rods [r - 1].Lifted == false) {
								hotDoc.Rods [r - 1].Deviation = 0;
							}
						}
						break;
					}
					
					//case 6: {
					//	for (int r = 1; r <= hotDoc.Rods.Length; r++) {
					//		hotDoc.Rods [r - 1].Lifted = true;
					//		hotDoc.Rods [r - 1].Deviation = 0;
					//	}
					//	break;
					//}

					default:
						exitLoop = true;
						break;
				}
				if (exitLoop)
					break;

				var command = hotDoc.GenerateCommand ();
				BaseStuff.CccTest.WriteLineToStandardError (string.Format ("SEND :: {0}", command));
				Console.WriteLine (command);
				// commands, um soll zu erreichen

				// *** INPUT #1 ***
				conLine = Console.ReadLine ();
				if (string.IsNullOrEmpty (conLine)) {
					BaseStuff.CccTest.WriteLineToStandardError ("TERMINATE for empty input");
					break;
				}

				BaseStuff.CccTest.WriteLineToStandardError (string.Format ("RECV :: {0}", conLine));

			//	System.Windows.Forms.MessageBox.Show (string.Format ("done : {0}", iteration));

			}
			BaseStuff.CccTest.WriteLineToStandardError (string.Format ("SEND :: {0}", "EXIT"));
			Console.WriteLine ("EXIT");
			conLine = Console.ReadLine ();
			BaseStuff.CccTest.WriteLineToStandardError (string.Format ("RECV :: {0}", conLine));
		}
	}
}
