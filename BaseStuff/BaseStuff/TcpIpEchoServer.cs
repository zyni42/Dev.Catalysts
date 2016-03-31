using System;
using System.Collections.Generic;

namespace BaseStuff
{
	public class TcpIpEchoServer : TcpIpServer
	{
		bool _running = false;
		
		private TcpIpEchoServer () {}
		public TcpIpEchoServer (string ip, int port)
		{
			ClientConnected += TcpIpEchoServer_ClientConnected;
			CreateAndListen(ip, port);
		}

		public void Run ()
		{
			_running = true;
			while (_running)
			{
				HandleClientConnectionEvent ();
			}
		}

		public void RunStep ()
		{
			HandleClientConnectionEvent ();
		}

		private void TcpIpEchoServer_ClientConnected(object sender, ClientSocketEventArgs e)
		{
			byte[] byteBuffer = new byte[1024];
			List<Byte> rcvdBytes = new List<byte> (1024);
			e.ClientSocket.ReceiveBufferSize = 1024;
			int numRcvdBytes = 0;

			while (_running)
			{
				if (e.ClientSocket.Available == 0)
				{
					System.Threading.Thread.Sleep (100);
					continue;
				}

				// Receive data
				rcvdBytes.Clear();
				numRcvdBytes = 0;
				while ((numRcvdBytes = e.ClientSocket.Receive (byteBuffer)) > 0)
				{
					rcvdBytes.AddRange (byteBuffer);
				}

				// Send data back
				var rcvdBytesArray = rcvdBytes.ToArray ();
				e.ClientSocket.Send (rcvdBytesArray);
			}
		}

		public new void Close()
		{
			ClientConnected -= TcpIpEchoServer_ClientConnected;
			base.Close();
			_running = false;
		}
	}
}
