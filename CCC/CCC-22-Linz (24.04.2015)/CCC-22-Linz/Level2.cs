using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC_22_Linz
{
	class Level2 : Level1, BaseStuff.ICccLevel
	{
		public Level2() { }

		new public void CalculateResult()
		{
			string conLine ;
			// *** READ CONSOLE INPUTS ***
			BaseStuff.CccTest.WriteLineToStandardError (string.Format ("running : {0}", this));
			Console.WriteLine ("GET_NUMBER");
			var rodNumberTxt = Console.ReadLine ();
			var rodNumber = int.Parse (rodNumberTxt);

			// *** CREATE HELPER OBJECTS ***
			var hotDoc = new HotDoc (rodNumber);


			for (int iteration = -1; ; iteration ++)
			{
				Console.WriteLine ("GET_POSITION");
				var posTxt = Console.ReadLine ();
				var pos = float.Parse (posTxt);

				if (pos > 1000)
					break; // MUahahahahhaa

				if (iteration == -1) {
					for (int r = 1; r <= hotDoc.Rods.Length; r++) {
						if ((r % 2) == 0) {
							hotDoc.Rods[r-1].Lifted = false;
							hotDoc.Rods[r-1].Deviation = -5;
						}
					}
				}
				else { 
					switch (iteration % 6) {
						// defiere SOLL
						case 0: {
							for (int r = 1; r <= hotDoc.Rods.Length; r++) {
								if ((r % 2) == 1) {
									hotDoc.Rods[r-1].Lifted = true;
									hotDoc.Rods[r-1].Deviation = 5;
								}
								else {
									hotDoc.Rods[r-1].Lifted = false;
									hotDoc.Rods[r-1].Deviation = -5;
								}
							}
							break;
						}

						case 1: {
							for (int r = 1; r <= hotDoc.Rods.Length; r++) {
								if ((r % 2) == 1) {
									hotDoc.Rods[r-1].Lifted = true;
									hotDoc.Rods[r-1].Deviation = 5;
								}
								else {
									hotDoc.Rods[r-1].Lifted = true;
									hotDoc.Rods[r-1].Deviation = -5;
								}
							}
							break;
						}

						case 2: {
							for (int r = 1; r <= hotDoc.Rods.Length; r++) {
								if ((r % 2) == 1) {
									hotDoc.Rods[r-1].Lifted = false;
									hotDoc.Rods[r-1].Deviation = 5;
								}
								else {
									hotDoc.Rods[r-1].Lifted = true;
									hotDoc.Rods[r-1].Deviation = -5;
								}
							}
							break;
						}

						// lift the lowered rods to 0
						case 3: {
							for (int r = 1; r <= hotDoc.Rods.Length; r++) {
								if ((r % 2) == 1) {
									hotDoc.Rods[r-1].Lifted = false;
									hotDoc.Rods[r-1].Deviation = -5;
								}
								else {
									hotDoc.Rods[r-1].Lifted = true;
									hotDoc.Rods[r-1].Deviation = 5;
								}
							}
							break;
						}

						case 4: {
							for (int r = 1; r <= hotDoc.Rods.Length; r++) {
								if ((r % 2) == 1) {
									hotDoc.Rods[r-1].Lifted = true;
									hotDoc.Rods[r-1].Deviation = -5;
								}
								else {
									hotDoc.Rods[r-1].Lifted = true;
									hotDoc.Rods[r-1].Deviation = 5;
								}
							}
							break;
						}

						case 5: {
							for (int r = 1; r <= hotDoc.Rods.Length; r++) {
								if ((r % 2) == 1) {
									hotDoc.Rods[r-1].Lifted = true;
									hotDoc.Rods[r-1].Deviation = -5;
								}
								else {
									hotDoc.Rods[r-1].Lifted = false;
									hotDoc.Rods[r-1].Deviation = 5;
								}
							}
							break;
						}
			
						//default:
						//	exitLoop = true;
						//	break;
					}
				}
				//if (exitLoop)
				//	break;

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
