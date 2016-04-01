using System;
using System.Text;

using System.Net;
using System.Net.Sockets;

namespace BaseStuff
{
	public class TcpIpClient : IDisposable
	{
		Socket _client = null;
		byte[] _rcvdBytes = new byte[1024];

		public IPEndPoint LocalEndPoint
		{
			get { return _client.LocalEndPoint as IPEndPoint; }
		}

		public bool IsClosed
		{
			get { return (_client == null); }
		}

		public void CreateAndConnect (string ip, int port)
		{
			if (_client != null) Close ();
			_client = new Socket (SocketType.Stream, ProtocolType.Tcp);
			_client.ReceiveBufferSize = 1024;
			_client.Connect (IPAddress.Parse (ip), port);
			_client.Blocking = false;
		}

		public string ReceiveText ()
		{
			if (_client.Available == 0) return string.Empty;

			int numRcvdBytes = 0;
			string rcvdString = "";
			StringBuilder stb = new StringBuilder ();

			while (true)
			{
				try
				{
					numRcvdBytes = _client.Receive(_rcvdBytes);
					rcvdString = Encoding.ASCII.GetString (_rcvdBytes, 0, numRcvdBytes);
					stb.Append (rcvdString);
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

			return stb.ToString ();
		}

		public void SendText (string textToSend)
		{
			try
			{
				_client.Send (Encoding.ASCII.GetBytes (textToSend));
			}
			catch (SocketException sex)
			{
				if (sex.ErrorCode == 10053)			// WSAECONNABORTED Software caused connection abort.
				{
					Console.WriteLine ("WSAECONNABORTED -> closing client...");
					Close ();
				}
			}
		}

		public void Close ()
		{
			if (_client != null)
			{
				if (_client.Connected) _client.Disconnect (false);
				_client.Shutdown (SocketShutdown.Both);
				_client.Close ();
				_client = null;
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
		// ~TcpIpClient() {
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
