using HexChat.Business.Handlers.Base;
using HexChat.Models.Interfaces;
namespace HexChat.Business.Handlers {
    /// <summary>
    /// Custom Message Handler
    /// </summary>
    /// <typeparam name="TServerMessage"></typeparam>
    public abstract class CustomMessageHandler<TServerMessage> : MessageHandler<TServerMessage>, ICustomHandler
        where TServerMessage : IServerMessage {
        public bool Handled { get; set; }
    }
}