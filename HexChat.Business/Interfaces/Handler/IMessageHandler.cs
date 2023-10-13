using HexChat.Business.Business;
using HexChat.Models.Interfaces;
namespace HexChat.Business.Interfaces.Handler {
    /// <summary>
    /// Message Handler
    /// </summary>
    /// <typeparam name="TServerMessage"></typeparam>
    internal interface IMessageHandler<TServerMessage> where TServerMessage : IServerMessage {
        /// <summary>
        /// Message
        /// </summary>
        TServerMessage Message { get; }
        /// <summary>
        /// Handle Async
        /// </summary>
        /// <param name="serverMessage"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        Task HandleAsync(TServerMessage serverMessage, ClientBusiness client);
    }
}