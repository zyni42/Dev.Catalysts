using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cccFractalsLevel01
{
	class Program
	{
		private enum Shape { Triangle, Squre };
		static void Main (string [] args)
		{
			try {
				var shape = Shape.Squre;
				var factorBase = 0;
				char [] sep = new char[] { '=' };
				if (args.Length != 3) throw new	Exception (string.Format ("need 3 parameters, found {0}", args.Length));

				if (string.Compare (args[0], "tri", true) == 0) {
					shape = Shape.Triangle;
					factorBase = 3;
				}
				else if (string.Compare (args[0], "sq", true) == 0) {
					shape = Shape.Squre;					
					factorBase = 4;
				}
				else
					throw new Exception (string.Format ("parameter #1 must be \"tri\" or \"sq\" but ist \"{0}\"", args[0]));
				var arr1 = args[1].Trim().Split (sep,StringSplitOptions.RemoveEmptyEntries);
				var arr2 = args[2].Trim().Split (sep,StringSplitOptions.RemoveEmptyEntries);
				var length     = int.Parse (arr1[1]);
				var iterations = int.Parse (arr2[1]);

				double dLength  = length;
				var fullLength1 = dLength * factorBase;

				Console.WriteLine ("    iterations  = {0}",  iterations);
				Console.WriteLine ("        length  = {0}",     length);
				Console.WriteLine ("       dLength  = {0}",    dLength);
				Console.WriteLine ("    fullLength1 = {0}", fullLength1);

				switch (shape) {
					case Shape.Triangle:
						for (int i = 0; i < iterations; i++) {
							fullLength1 = (fullLength1 * 4) / 3;
						}
						break;
					case Shape.Squre:
						for (int i = 0; i < iterations; i++) {
							fullLength1 = (fullLength1 * 5) / 3;
						}
						break;
				}

				Console.WriteLine ("--> fullLength1 = {0}", fullLength1);

				var factor = 
					shape == Shape.Triangle
						? Math.Pow (((double)4)/((double)3), iterations)
						: shape == Shape.Squre
							? Math.Pow (((double)5)/((double)3), iterations)
							: (double)0;

				var fullLength2 = dLength * factorBase;
				Console.WriteLine ("         factor = {0}", factor);
				Console.WriteLine ("    fullLength2 = {0}", fullLength2);
				fullLength2 *= factor;
				Console.WriteLine ("--> fullLength2 = {0}", fullLength2);
			}
			catch (Exception ex) {
				Console.WriteLine ("*************************************");
				Console.WriteLine ("** EXCEPTION ** {1}", ex.Message);
				Console.WriteLine ("*************************************");
				Console.WriteLine (ex);
				Console.WriteLine ("*************************************");
			}
		}
	}
}
