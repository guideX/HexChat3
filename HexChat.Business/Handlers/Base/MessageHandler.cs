using HexChat.Business.Interfaces.Handler;
using HexChat.Business.Interfaces.Messages;
namespace HexChat.Business.Handlers.Base {
    /// <summary>
    /// Message Handler
    /// </summary>
    /// <typeparam name="TServerMessage"></typeparam>
    public abstract class MessageHandler<TServerMessage> : IMessageHandler<TServerMessage> where TServerMessage : IServerMessage {
        /// <summary>
        /// Message
        /// </summary>
        private TServerMessage? _message;
        /// <summary>
        /// Message
        /// </summary>
        public TServerMessage? Message {
            get {
                return _message;
            }
            set {
                _message = value;
            }
        }
        /// <summary>
        /// Handle Async
        /// </summary>
        /// <param name="serverMessage"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public abstract Task HandleAsync(TServerMessage serverMessage, Client client);
    }
}
