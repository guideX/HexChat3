using HexChat.Business.EventArgs;
using HexChat.Business.Interfaces.Connection;
using Microsoft.VisualBasic;
using System.Diagnostics.CodeAnalysis;
using System.Net.WebSockets;
using System.Text;
namespace HexChat.Business.Connection {
    /// <summary>
    /// Web Socket Client Connection
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class WebSocketClientConnection : IConnection {
        /// <summary>
        /// Data Received
        /// </summary>
        public event EventHandler<DataReceivedEventArgs>? DataReceived;
        /// <summary>
        /// Connected
        /// </summary>
        public event EventHandler? Connected;
        /// <summary>
        /// Disconnected
        /// </summary>
        public event EventHandler? Disconnected;
        /// <summary>
        /// Client Web Socket
        /// </summary>
        private readonly ClientWebSocket? clientWebSocket = new();
        /// <summary>
        /// Disposal Token Source
        /// </summary>
        private readonly CancellationTokenSource? disposalTokenSource = new();
        /// <summary>
        /// Address
        /// </summary>
        private readonly string _address;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="address"></param>
        public WebSocketClientConnection(string address) {
            _address = address;
        }
        /// <summary>
        /// Connect Async
        /// </summary>
        /// <returns></returns>
        public async Task ConnectAsync() {
            if(clientWebSocket != null) {
                var token = disposalTokenSource!.Token;
                await clientWebSocket.ConnectAsync(new Uri(_address), token).ConfigureAwait(false);
                while (clientWebSocket.State != WebSocketState.Open)
                    await Task.Delay(100).ConfigureAwait(false);
                Connected?.Invoke(this, System.EventArgs.Empty);
                RunDataReceiver()
                    .SafeFireAndForget(
                        continueOnCapturedContext: false,
                        onException: ex => Disconnected?.Invoke(this, System.EventArgs.Empty));
            }
        }
        /// <summary>
        /// Run Data Reciever
        /// </summary>
        /// <returns></returns>
        private async Task RunDataReceiver() {
            var buffer = new ArraySegment<byte>(new byte[1024]);
            bool isCancellationRequest = disposalTokenSource!.IsCancellationRequested;
            while (isCancellationRequest) {
                if (clientWebSocket != null) {
                    var received = await clientWebSocket.ReceiveAsync(buffer, disposalTokenSource.Token).ConfigureAwait(false);
                    byte[] bufferArray = buffer.Array != null ? buffer.Array : Array.Empty<byte>();
                    var receivedAsText = Encoding.ASCII.GetString(bufferArray, 0, received.Count);
                    DataReceived?.Invoke(this, new DataReceivedEventArgs(receivedAsText));
                }
            }
            Disconnected?.Invoke(this, System.EventArgs.Empty);
        }

        public async Task SendAsync(string data) {
            if (!data.EndsWith(Constants.vbCrLf)) {
                data += Constants.vbCrLf;
            }

            var dataToSend = new ArraySegment<byte>(Encoding.ASCII.GetBytes(data));
            await clientWebSocket.SendAsync(dataToSend, WebSocketMessageType.Text, true, disposalTokenSource.Token)
                    .ConfigureAwait(false);
        }

        public void Dispose() {
            disposalTokenSource.Cancel();
            _ = clientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
        }
    }
}