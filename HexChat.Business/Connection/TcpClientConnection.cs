using HexChat.Business.EventArgs;
using HexChat.Business.Interfaces.Connection;
using Microsoft.VisualBasic;
using System.Net.Sockets;
namespace HexChat.Business.Connection {
    /// <summary>
    /// Tcp Client Connection
    /// </summary>
    public class TcpClientConnection : IConnection {
        /// <summary>
        /// Tcp Client
        /// </summary>
        private TcpClient? _tcpClient;
        /// <summary>
        /// Stream Reader
        /// </summary>
        private StreamReader? streamReader;
        /// <summary>
        /// Stream Writer
        /// </summary>
        private StreamWriter? streamWriter;
        /// <summary>
        /// Disposed
        /// </summary>
        private bool disposed = false;
        /// <summary>
        /// Indicates that data has been received through the connection
        /// </summary>
        public event EventHandler<DataReceivedEventArgs>? DataReceived;
        /// <summary>
        /// Indicates that the TCP connection is completed
        /// </summary>
        public event EventHandler? Connected;
        /// <summary>
        /// Indicates that the TCP connection was closed
        /// </summary>
        public event EventHandler? Disconnected;
        /// <summary>
        /// Host
        /// </summary>
        private readonly string _host;
        /// <summary>
        /// Port
        /// </summary>
        private readonly int _port;
        /// <summary>
        /// Tcp Client Connection
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public TcpClientConnection(string host, int port = 6667) {
            if (string.IsNullOrWhiteSpace(host)) throw new ArgumentNullException(nameof(host));
            if (port <= 0) throw new ArgumentException($"Port {port} is invalid.", nameof(port));
            _host = host;
            _port = port;
        }
        /// <summary>
        /// Connect Async
        /// </summary>
        /// <returns></returns>
        public async Task ConnectAsync() {
            if(_tcpClient != null) _tcpClient?.Dispose();
            _tcpClient = new TcpClient();
            await _tcpClient.ConnectAsync(_host, _port).ConfigureAwait(false);
            streamReader = new StreamReader(_tcpClient.GetStream());
            streamWriter = new StreamWriter(_tcpClient.GetStream());
            Connected?.Invoke(this, System.EventArgs.Empty);
            RunDataReceiver()
                .SafeFireAndForget(
                    continueOnCapturedContext: false,
                    onException: ex => Disconnected?.Invoke(this, System.EventArgs.Empty));
        }
        /// <summary>
        /// Run Data Receiver
        /// </summary>
        /// <returns></returns>
        private async Task RunDataReceiver() {
            string? line;
            if (streamReader != null) {
                while ((line = await streamReader.ReadLineAsync().ConfigureAwait(false)) != null) if (!string.IsNullOrWhiteSpace(line)) DataReceived?.Invoke(this, new DataReceivedEventArgs(line));
                Disconnected?.Invoke(this, System.EventArgs.Empty);
            }
        }
        /// <summary>
        /// Sends raw data to the IRC server
        /// </summary>
        /// <param name="data">Data to be sent</param>
        /// <returns>The task object representing the asynchronous operation</returns>
        public async Task SendAsync(string data) {
            if (!data.EndsWith(Constants.vbCrLf)) data += Constants.vbCrLf;
            if (streamWriter != null) {
                await streamWriter.WriteAsync(data)
                    .ConfigureAwait(false);
                await streamWriter.FlushAsync()
                    .ConfigureAwait(false);
            }
        }
        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing) {
            if (disposed) return;
            if (disposing) {
                streamReader?.Dispose();
                streamWriter?.Dispose();
                _tcpClient?.Dispose();
            }
            disposed = true;
        }
        ~TcpClientConnection() => Dispose(false);
    }
}