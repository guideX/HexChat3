using HexChat.Business.Business;
using HexChat.Business.Handlers.Base;
using HexChat.Models.Message;
namespace HexChat.Business.Handlers {
    /// <summary>
    /// Join Handler
    /// </summary>
    public class JoinHandler : MessageHandler<JoinMessageModel> {
        /// <summary>
        /// Handle Async
        /// </summary>
        /// <param name="serverMessage"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public override Task HandleAsync(JoinMessageModel serverMessage, ClientBusiness client) {
            var channel = client.Channels.GetChannel(serverMessage.Channel);
            if (serverMessage.Nick != client.User.Nick) {
                var user = client.Peers.GetUser(serverMessage.Nick);
                channel.AddUser(user, string.Empty);
            }
            return Task.CompletedTask;
        }
    }
}