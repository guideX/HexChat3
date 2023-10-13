using HexChat.Business.Messages.Base;
using HexChat.Models.Interfaces;
namespace HexChat.Models.Message {
    /// <summary>
    /// Pass Message Model
    /// </summary>
    public class PassMessageModel : IRCMessage, IClientMessage {
        /// <summary>
        /// Password
        /// </summary>
        private readonly string password;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="password"></param>
        public PassMessageModel(string password) {
            this.password = password;
        }
        /// <summary>
        /// Tokens
        /// </summary>
        public IEnumerable<string> Tokens => new[] { "PASS", password };
    }
}