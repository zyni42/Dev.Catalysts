using System;
using System.Collections.Generic;
using System.Linq;

using System.Net;
using System.Net.Sockets;
using System.Text;

namespace BaseStuff
{
	public class TcpIpEchoServer : TcpIpServer
	{
		private TcpIpEchoServer () {}
		public TcpIpEchoServer (string ip, int port)
		{
			CreateAndListen(ip, port);
		}

		public override void DoServerWork()
		{
			foreach (var client in _clients)
			{
				byte[] byteBuffer = new byte[1024];
				List<Byte> rcvdBytes = new List<byte>(1024);
				client.ReceiveBufferSize = 1024;
				int numRcvdBytes = 0;

				if (client.Available == 0) continue;

				// Receive data
				rcvdBytes.Clear();
				numRcvdBytes = 0;
				while (true)
				{
					try
					{
						numRcvdBytes = client.Receive(byteBuffer);
						rcvdBytes.AddRange(byteBuffer.Take (numRcvdBytes).ToArray ());
					}					
					catch (SocketException sex)
					{
						if (sex.ErrorCode == 10035)		// WSAEWOULDBLOCK (on non blocking sockets, if no request available)
						{
							System.Threading.Thread.Sleep (100);
							break;
						}
						else throw sex;
					}
				}
				var rcvdBytesArray = rcvdBytes.ToArray();

				Console.WriteLine("Client (IP \"{0}\", Port {1}): \"{2}\"",
					(client.RemoteEndPoint as IPEndPoint).Address.ToString(),
					(client.RemoteEndPoint as IPEndPoint).Port,
					Encoding.ASCII.GetString(rcvdBytesArray, 0, rcvdBytesArray.Length)
					);

				// Send data back
				client.Send(rcvdBytesArray);
			}
		}
	}
}
