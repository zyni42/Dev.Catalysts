using System;
using System.Text;

using System.Net;
using System.Net.Sockets;

namespace BaseStuff
{
	public class TcpIpClient : IDisposable
	{
		Socket _client = null;

		public void CreateAndConnect (string ip, int port)
		{
			if (_client != null) Close ();
			_client = new Socket (SocketType.Stream, ProtocolType.Tcp);
			_client.Connect (IPAddress.Parse (ip), port);
		}

		public string ReceiveText ()
		{
			if (_client.Available == 0) return string.Empty;

			byte[] rcvdBytes = new byte[1024];
			_client.ReceiveBufferSize = 1024;
			int numRcvdBytes = 0;
			string rcvdString = "";
			StringBuilder stb = new StringBuilder ();

			while ((numRcvdBytes = _client.Receive (rcvdBytes)) > 0)
			{
				rcvdString = Encoding.ASCII.GetString (rcvdBytes, 0, numRcvdBytes);
				stb.Append (rcvdString);
			}

			return stb.ToString ();
		}

		public void SendText (string textToSend)
		{
			_client.Send (Encoding.ASCII.GetBytes (textToSend));
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
