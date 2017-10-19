using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC_23_Rathaus
{
	public class Road
	{
		public int		CountSegments { get; set; }
		public Car1 []	Segments;

		public Road (int countSegments)
		{
			CountSegments = countSegments;
			Segments = new Car1 [CountSegments];
		}

		public Car1 this[int idx] {
			get { return Segments[idx]; }
			set { Segments[idx] = value; }
		}
	}
}
