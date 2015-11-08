using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC_23_Rathaus
{
	class Program
	{
		static void Main(string[] args)
		{
			var vis = new Vis.CCC23RathausView ();
			Class2 l2 = null;
			//l2 = new Class2 ("..\\..\\Level2\\level2_example.in"); l2.CalculateResult ();
			Car1.ResetCarNames (); l2 = new Class2 ("..\\..\\Level2\\level2_1.in"); if (l2.CalculateResult (vis)) return;
			Car1.ResetCarNames (); l2 = new Class2 ("..\\..\\Level2\\level2_2.in"); if (l2.CalculateResult (vis)) return;
			Car1.ResetCarNames (); l2 = new Class2 ("..\\..\\Level2\\level2_3.in"); if (l2.CalculateResult (vis)) return;
			Car1.ResetCarNames (); l2 = new Class2 ("..\\..\\Level2\\level2_4.in"); if (l2.CalculateResult (vis)) return;
			Car1.ResetCarNames (); l2 = new Class2 ("..\\..\\Level2\\level2_5.in"); if (l2.CalculateResult (vis)) return;
			Car1.ResetCarNames (); l2 = new Class2 ("..\\..\\Level2\\level2_6.in"); if (l2.CalculateResult (vis)) return;
		}
	}
}
