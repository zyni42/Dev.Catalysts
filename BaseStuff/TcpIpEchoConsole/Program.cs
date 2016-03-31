using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BaseStuff;

namespace TcpIpEchoConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			TcpIpEchoServer srv = new TcpIpEchoServer ("127.0.0.1", 42424);

			Console.WriteLine ("Running TCP/IP Echoserver (IP \"{0}\", Port {1}) ... <press ESC to abort>",
				srv.ServerEndPoint.Address.ToString (),
				srv.ServerEndPoint.Port
				);
			while (true)
			{
				if (Console.KeyAvailable)
				{
					var cki = Console.ReadKey (true);
					if (cki.Key == ConsoleKey.Escape)
					{
						srv.Close ();
						break;
					}
				}
				srv.RunStep ();
			}
		}
	}
}
