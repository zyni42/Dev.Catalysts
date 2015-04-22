using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseStuff
{
	public interface ICccLevel
	{
		void CalculateResult ();
	}

	public interface ICccLevel<T> : ICccLevel
	{
		new T CalculateResult ();
	}
}
