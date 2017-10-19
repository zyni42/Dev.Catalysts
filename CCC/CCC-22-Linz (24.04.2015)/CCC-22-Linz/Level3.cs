using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC_22_Linz
{
	class Level3 : Level2, BaseStuff.ICccLevel
	{
		public class HotDoc3 : Level1.HotDoc
		{
			public HotDoc3 (int numRods) : base (numRods)
			{

			}
			public void __MoveRight (float to)
			{
				string conLine;
				for (int iteration = 0; ; iteration ++)
				{
					Console.WriteLine ("GET_POSITION");
					var posTxt = Console.ReadLine ();
					var pos = float.Parse (posTxt);
					BaseStuff.CccTest.WriteLineToStandardError (string.Format ("POS  :: {0}", pos));
					int stillToGo = (int)((int)(to) - pos);
					if (stillToGo <= 0) {
						BaseStuff.CccTest.WriteLineToStandardError (string.Format ("POS  :: {0} --> DONE", pos));
						break;
					}

					switch (iteration % 4) {
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
							if (stillToGo < 5)
								BaseStuff.CccTest.WriteLineToStandardError (string.Format ("2GO<5:: {0}", stillToGo));

							for (int r = 1; r <= this.Rods.Length; r++) {
								if ((r % 2) == 1) {
									this.Rods[r-1].Deviation = Math.Min (5, stillToGo);
								}
							}
							break;
						}

						case 2: {
							for (int r = 1; r <= this.Rods.Length; r++) {
								this.Rods[r-1].Lifted = true;
							}
							break;
						}

						// lift the lowered rods to 0
						case 3: {
							for (int r = 1; r <= this.Rods.Length; r++) {
								this.Rods[r-1].Deviation = 0;
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

			public void MoveAny (float to)
			{
				string conLine;
				for (int iteration = 0; ; iteration ++)
				{
					Console.WriteLine ("GET_POSITION");
					var posTxt = Console.ReadLine ();
					var pos = float.Parse (posTxt);

					BaseStuff.CccTest.WriteLineToStandardError (string.Format ("POS  :: {0}", pos));

					int stillToGo = (int)((int)(to) - pos);

					if (stillToGo > 0)
						stillToGo = Math.Min (stillToGo, 5);
					else
						stillToGo = Math.Max (stillToGo, -5);

					if (stillToGo == 0)
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
	
			public void MoveRight (float to)
			{
				string conLine;
				for (int iteration = 0; ; iteration ++)
				{
					Console.WriteLine ("GET_POSITION");
					var posTxt = Console.ReadLine ();
					var pos = float.Parse (posTxt);

					BaseStuff.CccTest.WriteLineToStandardError (string.Format ("POS  :: {0}", pos));

					int stillToGo = (int)((int)(to) - pos);
					stillToGo = Math.Min (stillToGo, 5);
					if (stillToGo <= 0)
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
			public void MoveRight9 (float to)
			{
				string conLine;
				for (int iteration = -2; ; iteration ++)
				{
					Console.WriteLine ("GET_POSITION");
					var posTxt = Console.ReadLine ();
					var pos = float.Parse (posTxt);

					BaseStuff.CccTest.WriteLineToStandardError (string.Format ("POS  :: {0}", pos));

					int stillToGo = (int)((int)(to) - pos);
					if (stillToGo <= 9) {
						BaseStuff.CccTest.WriteLineToStandardError (string.Format ("TO GO:: {0} ****************************************************", stillToGo));
					//	Reset ();
						for (int m = 0; m < stillToGo; m++) { 
							pos = MoveRight1 ();
							stillToGo = (int)((int)(to) - pos);
							if (stillToGo <= 0)
								return;
							BaseStuff.CccTest.WriteLineToStandardError (string.Format ("------------------------------------------------------------"));
						}
						return; // MUahahahahhaa
					}
					stillToGo = Math.Min (5, stillToGo);

					if (iteration == -2) {
						for (int r = 1; r <= this.Rods.Length; r++) {
							this.Rods[r-1].Lifted = true;
							this.Rods[r-1].Deviation = 0;
						}
					}
					else if (iteration == -1) {
						for (int r = 1; r <= this.Rods.Length; r++) {
							if ((r % 2) == 0) {
								this.Rods[r-1].Lifted = false;
								this.Rods[r-1].Deviation = -stillToGo;
							}
						}
					}
					else { 
						switch (iteration % 6) {
							// defiere SOLL
							case 0: {
								for (int r = 1; r <= this.Rods.Length; r++) {
									if ((r % 2) == 1) {
										this.Rods[r-1].Lifted = true;
										this.Rods[r-1].Deviation = 0;
									}
									else {
										this.Rods[r-1].Lifted = false;
										this.Rods[r-1].Deviation = -stillToGo;
									}
								}
								break;
							}

							case 1: {
								for (int r = 1; r <= this.Rods.Length; r++) {
									if ((r % 2) == 1) {
										this.Rods[r-1].Lifted = true;
										this.Rods[r-1].Deviation = 0;
									}
									else {
										this.Rods[r-1].Lifted = true;
										this.Rods[r-1].Deviation = -stillToGo;
									}
								}
								break;
							}

							case 2: {
								for (int r = 1; r <= this.Rods.Length; r++) {
									if ((r % 2) == 1) {
										this.Rods[r-1].Lifted = false;
										this.Rods[r-1].Deviation = 0;
									}
									else {
										this.Rods[r-1].Lifted = true;
										this.Rods[r-1].Deviation = -stillToGo;
									}
								}
								break;
							}

							// lift the lowered rods to 0
							case 3: {
								for (int r = 1; r <= this.Rods.Length; r++) {
									if ((r % 2) == 1) {
										this.Rods[r-1].Lifted = false;
										this.Rods[r-1].Deviation = -stillToGo;
									}
									else {
										this.Rods[r-1].Lifted = true;
										this.Rods[r-1].Deviation = 0;
									}
								}
								break;
							}

							case 4: {
								for (int r = 1; r <= this.Rods.Length; r++) {
									if ((r % 2) == 1) {
										this.Rods[r-1].Lifted = true;
										this.Rods[r-1].Deviation = -stillToGo;
									}
									else {
										this.Rods[r-1].Lifted = true;
										this.Rods[r-1].Deviation = 0;
									}
								}
								break;
							}

							case 5: {
								for (int r = 1; r <= this.Rods.Length; r++) {
									if ((r % 2) == 1) {
										this.Rods[r-1].Lifted = true;
										this.Rods[r-1].Deviation = -stillToGo;
									}
									else {
										this.Rods[r-1].Lifted = false;
										this.Rods[r-1].Deviation = 0;
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
	
			public void MoveRight10 (float to)
			{
				string conLine;
				for (int iteration = -2; ; iteration ++)
				{
					Console.WriteLine ("GET_POSITION");
					var posTxt = Console.ReadLine ();
					var pos = float.Parse (posTxt);

					BaseStuff.CccTest.WriteLineToStandardError (string.Format ("POS  :: {0}", pos));

					int stillToGo = (int)((int)(to) - pos);
					if (stillToGo <= 9) {
						BaseStuff.CccTest.WriteLineToStandardError (string.Format ("TO GO:: {0} ****************************************************", stillToGo));
					//	Reset ();
						for (int m = 0; m < stillToGo; m++) { 
							MoveRight1 ();
							BaseStuff.CccTest.WriteLineToStandardError (string.Format ("------------------------------------------------------------"));
						}
						return; // MUahahahahhaa
					}

					if (iteration == -2) {
						for (int r = 1; r <= this.Rods.Length; r++) {
							this.Rods[r-1].Lifted = true;
							this.Rods[r-1].Deviation = 0;
						}
					}
					else if (iteration == -1) {
						for (int r = 1; r <= this.Rods.Length; r++) {
							if ((r % 2) == 0) {
								this.Rods[r-1].Lifted = false;
								this.Rods[r-1].Deviation = -5;
							}
						}
					}
					else { 
						switch (iteration % 6) {
							// defiere SOLL
							case 0: {
								for (int r = 1; r <= this.Rods.Length; r++) {
									if ((r % 2) == 1) {
										this.Rods[r-1].Lifted = true;
										this.Rods[r-1].Deviation = 5;
									}
									else {
										this.Rods[r-1].Lifted = false;
										this.Rods[r-1].Deviation = -5;
									}
								}
								break;
							}

							case 1: {
								for (int r = 1; r <= this.Rods.Length; r++) {
									if ((r % 2) == 1) {
										this.Rods[r-1].Lifted = true;
										this.Rods[r-1].Deviation = 5;
									}
									else {
										this.Rods[r-1].Lifted = true;
										this.Rods[r-1].Deviation = -5;
									}
								}
								break;
							}

							case 2: {
								for (int r = 1; r <= this.Rods.Length; r++) {
									if ((r % 2) == 1) {
										this.Rods[r-1].Lifted = false;
										this.Rods[r-1].Deviation = 5;
									}
									else {
										this.Rods[r-1].Lifted = true;
										this.Rods[r-1].Deviation = -5;
									}
								}
								break;
							}

							// lift the lowered rods to 0
							case 3: {
								for (int r = 1; r <= this.Rods.Length; r++) {
									if ((r % 2) == 1) {
										this.Rods[r-1].Lifted = false;
										this.Rods[r-1].Deviation = -5;
									}
									else {
										this.Rods[r-1].Lifted = true;
										this.Rods[r-1].Deviation = 5;
									}
								}
								break;
							}

							case 4: {
								for (int r = 1; r <= this.Rods.Length; r++) {
									if ((r % 2) == 1) {
										this.Rods[r-1].Lifted = true;
										this.Rods[r-1].Deviation = -5;
									}
									else {
										this.Rods[r-1].Lifted = true;
										this.Rods[r-1].Deviation = 5;
									}
								}
								break;
							}

							case 5: {
								for (int r = 1; r <= this.Rods.Length; r++) {
									if ((r % 2) == 1) {
										this.Rods[r-1].Lifted = true;
										this.Rods[r-1].Deviation = -5;
									}
									else {
										this.Rods[r-1].Lifted = false;
										this.Rods[r-1].Deviation = 5;
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

			public float MoveRight1 ()
			{
				float pos = int.MinValue;
				for (int iteration = 0; ; iteration ++)
				{
					bool exitLoop = false;
					switch (iteration) {
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
									this.Rods[r-1].Deviation = 1;
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
									this.Rods[r-1].Deviation = 0;
								}
							}
							break;
						}

						case 5: {
							for (int r = 1; r <= this.Rods.Length; r++) {
								if ((r % 2) == 1) {
									this.Rods[r-1].Lifted = true;
								}
							}
							break;
						}
						default:
							exitLoop = true;
							break;
					}
					if (!exitLoop) {

						var command = this.GenerateCommand ();
						if (string.IsNullOrWhiteSpace (command)) { 
							BaseStuff.CccTest.WriteLineToStandardError (string.Format ("MOVE-R-1 : SEND :: ** NIX **"));
							continue;
						}
						BaseStuff.CccTest.WriteLineToStandardError (string.Format ("MOVE-R-1 : SEND :: {0}", command));
						Console.WriteLine (command);
						// commands, um soll zu erreichen

						// *** INPUT #1 ***
						var conLine = Console.ReadLine ();
						BaseStuff.CccTest.WriteLineToStandardError (string.Format ("MOVE-R-1 : RECV :: {0}", conLine));
					}

					Console.WriteLine ("GET_POSITION");
					var posTxt = Console.ReadLine ();
					pos = float.Parse (posTxt);

					BaseStuff.CccTest.WriteLineToStandardError (string.Format ("MOVE-R-1 : POS  :: {0}", pos));
					if (exitLoop)
						break;

				}
				return pos;
			}
			//public void Reset ()
			//{
			//	string posTxt;
			//	float pos;
			//	bool allAreLifted = true;
			//	for (int r = 1; r <= this.Rods.Length; r++) {
			//		if (this.Rods[r-1].Lifted == false) {
			//			allAreLifted = false;
			//			break;
			//		}
			//	}
			//	if (allAreLifted)
			//		return;

			//	System.Windows.Forms.MessageBox.Show ("öjl");

			//	for (int iteration = 0; ; iteration ++)
			//	{
			//		bool exitLoop = false;
			//		switch (iteration) {
			//			// defiere SOLL
			//			case 0: {
			//				for (int r = 1; r <= this.Rods.Length; r++) {
			//					if (this.Rods[r-1].Lifted == false) {
			//						this.Rods[r-1].Deviation = 0;
			//					}
			//				}
			//				break;
			//			}

			//			case 1: {
			//				for (int r = 1; r <= this.Rods.Length; r++) {
			//					if (this.Rods[r-1].Lifted == false) {
			//						this.Rods[r-1].Lifted = true;
			//					}
			//				}
			//				break;
			//			}

			//			case 2: {
			//				for (int r = 1; r <= this.Rods.Length; r++) {
			//					if (this.Rods[r-1].Deviation != 0) {
			//						this.Rods[r-1].Lifted = false;
			//					}
			//				}
			//				break;
			//			}

			//			case 3: {
			//				for (int r = 1; r <= this.Rods.Length; r++) {
			//					if (this.Rods[r-1].Lifted == false) {
			//						this.Rods[r-1].Deviation = 0;
			//					}
			//				}
			//				break;
			//			}

			//			case 4: {
			//				for (int r = 1; r <= this.Rods.Length; r++) {
			//					if (this.Rods[r-1].Lifted == false) {
			//						this.Rods[r-1].Lifted = true;
			//					}
			//				}
			//				break;
			//			}
			
			//			default:
			//				exitLoop = true;
			//				break;
			//		}
			//		if (exitLoop)
			//			break;

			//		var command = this.GenerateCommand ();
			//		if (string.IsNullOrWhiteSpace (command)) { 
			//			BaseStuff.CccTest.WriteLineToStandardError (string.Format ("RESET : SEND :: ** NIX **"));
			//			continue;
			//		}
			//		BaseStuff.CccTest.WriteLineToStandardError (string.Format ("RESET : SEND :: {0}", command));
			//		Console.WriteLine (command);
			//		// commands, um soll zu erreichen

			//		// *** INPUT #1 ***
			//		var conLine = Console.ReadLine ();
			//		BaseStuff.CccTest.WriteLineToStandardError (string.Format ("RESET : RECV :: {0}", conLine));

			//		Console.WriteLine ("GET_POSITION");
			//		posTxt = Console.ReadLine ();
			//		pos = float.Parse (posTxt);

			//		BaseStuff.CccTest.WriteLineToStandardError (string.Format ("RESET {0}: POS  :: {1} ###########", iteration, pos));

			//	}			
			//	Console.WriteLine ("GET_POSITION");
			//	posTxt = Console.ReadLine ();
			//	pos = float.Parse (posTxt);

			//	BaseStuff.CccTest.WriteLineToStandardError (string.Format ("RESET    : POS  :: {0}", pos));
			//}
		}
		public Level3() { }

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
