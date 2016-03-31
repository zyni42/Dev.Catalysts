using System;
using System.Collections.Generic;

using System.Net;
using System.Net.Sockets;

namespace BaseStuff
{
	public class TcpIpServer : IDisposable
	{
		public class ClientSocketEventArgs : EventArgs
		{
			Socket _clientSocket = null;

			public Socket ClientSocket
			{
				get { return _clientSocket; }
				set { _clientSocket = value; }
			}

			public ClientSocketEventArgs (Socket socket)
			{
				ClientSocket = socket;
			}
		}
		
		Socket _server = null;
		List<Socket> _clients = new List<Socket> ();

		public IPEndPoint ServerEndPoint
		{
			get { return _server.LocalEndPoint as IPEndPoint; }
		}

		public void CreateAndListen (string ip, int port)
		{
			if (_server != null) Close ();
			_server = new Socket (SocketType.Stream, ProtocolType.Tcp);
			_server.Bind (new IPEndPoint (IPAddress.Parse (ip), port));
			_server.Blocking = false;
			_server.Listen (5);
		}

		public void HandleClientConnectionEvent ()
		{
			try
			{
				var clientSocket = _server.Accept ();
				EventHandler<ClientSocketEventArgs> handler = ClientConnected;
				if (handler != null)
				{
					handler (this, new ClientSocketEventArgs (clientSocket));
				}
			}
			catch (SocketException sex)
			{
				if (sex.ErrorCode == 10035)		// WSAEWOULDBLOCK (on non blocking sockets, if no request available)
				{
					System.Threading.Thread.Sleep (100);
				}
				else throw sex;
			}
		}

		protected event EventHandler<ClientSocketEventArgs> ClientConnected;

		public void Close ()
		{
			if (_clients.Count > 0)
			{
				foreach (var client in _clients)
				{
					if (client.Connected) client.Disconnect (false);
					client.Shutdown (SocketShutdown.Both);
					client.Close ();
				}
				_clients.Clear ();
			}

			if (_server != null)
			{
				_server.Close ();
				_server = null;
			}
		}

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects).
					this.Close ();
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.

				disposedValue = true;
			}
		}

		// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
		// ~TcpIpServer() {
		//   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
		//   Dispose(false);
		// }

		// This code added to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			// TODO: uncomment the following line if the finalizer is overridden above.
			// GC.SuppressFinalize(this);
		}
		#endregion
	}
}
