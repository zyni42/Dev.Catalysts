using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC_23_Rathaus
{
	class Program
	{
		private static bool _visUseDesktopSize = false;
		static void Main(string[] args)
		{
			if (!ParseCl (args))
				return;
			var vis = new Vis.CCC23RathausView (_visUseDesktopSize);
			Class2 l2 = null;
			//l2 = new Class2 ("..\\..\\Level2\\level2_example.in"); l2.CalculateResult ();
			Car1.ResetCarNames (); l2 = new Class2 ("..\\..\\Level2\\level2_1.in"); if (l2.CalculateResult (vis)) return;
			Car1.ResetCarNames (); l2 = new Class2 ("..\\..\\Level2\\level2_2.in"); if (l2.CalculateResult (vis)) return;
			Car1.ResetCarNames (); l2 = new Class2 ("..\\..\\Level2\\level2_3.in"); if (l2.CalculateResult (vis)) return;
			Car1.ResetCarNames (); l2 = new Class2 ("..\\..\\Level2\\level2_4.in"); if (l2.CalculateResult (vis)) return;
			Car1.ResetCarNames (); l2 = new Class2 ("..\\..\\Level2\\level2_5.in"); if (l2.CalculateResult (vis)) return;
			Car1.ResetCarNames (); l2 = new Class2 ("..\\..\\Level2\\level2_6.in"); if (l2.CalculateResult (vis)) return;
		}

		static bool ParseCl(string[] args)
		{
			string usageError = null;
			for (int i = 0; i < args.Length; i++) {
				var arg = args[i];
				if (arg [0] != '-' && arg [0] != '/') {
					usageError = string.Format ("argument must start with '-' or '/'. Found: [{0}]", arg);
					break;
				}
				string netArg = arg.Substring (1);
				string par0 = i+1 < args.Length ? args[i+1] : null;
				string par1 = i+2 < args.Length ? args[i+2] : null;

				if (string.Compare (netArg, "desktop", true) == 0) {
					_visUseDesktopSize = true;
				}
				else {
					usageError = string.Format ("unknown argument : [{0}]", arg);
					break;
				}
			}

			if (usageError != null) {
				Console.WriteLine ();
				Console.WriteLine ("usage error: {0}", usageError);
				Console.WriteLine ();
				Console.WriteLine ("usage: prog [-desktop]");
				Console.WriteLine ("where        -desktop ... max width of vis == whole desktop (default: main screen)");
				Console.WriteLine ();
				return false;
			}
			return true;
		}
	}
}
