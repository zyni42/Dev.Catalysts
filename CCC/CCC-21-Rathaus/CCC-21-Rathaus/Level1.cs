using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC_21_Rathaus
{
	class Level1 : BaseStuff.ICccLevel<string>
	{
		public Level1 (string inputFile)
		{
			var inputData = BaseStuff.CccTest.ReadInputFile (inputFile);

			// Parse and prepare input data
		}

		public string CalculateResult()
		{
			throw new NotImplementedException();

			var result = string.Empty;

			// Calculate result
			result += "something" + " ";

			return result.Substring (0,result.Length-1);
		}
	}
}
