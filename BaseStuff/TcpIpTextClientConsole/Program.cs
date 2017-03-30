using System;
using System.Text;

using BaseStuff;

namespace TcpIpTextClientConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			string srvIp = "127.0.0.1";
			int srvPort = 42424;
			StringBuilder stbInput = new StringBuilder();

			TcpIpClient cli = new TcpIpClient ();
			cli.CreateAndConnect (srvIp, srvPort);

			Console.WriteLine("Running TCP Client on Server (IP \"{0}\", Port {1}) ... <press ESC to abort>",
				srvIp,
				srvPort
				);
			while (true)
			{
				if (Console.KeyAvailable)
				{
					var cki = Console.ReadKey(true);
					if (cki.Key == ConsoleKey.Escape)
					{
						cli.Close();
						break;
					}
					else if (cki.Key == ConsoleKey.Enter)
					{
						Console.WriteLine();
						cli.SendText (stbInput.ToString());
						stbInput.Clear();
					}
					else
					{
						stbInput.Append(cki.KeyChar);
						Console.Write(cki.KeyChar);
					}
				}

				string rcvdText = cli.ReceiveText ();
				if (!string.IsNullOrWhiteSpace(rcvdText))
				{
					Console.WriteLine (string.Format(">> {0}", rcvdText));
				}
			}
		}
	}
}
