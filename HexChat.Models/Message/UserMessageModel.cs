using HexChat.Business.Messages.Base;
using HexChat.Models.Interfaces;
namespace HexChat.Models.Message {
    /// <summary>
    /// User Message Model
    /// </summary>
    public class UserMessage : IRCMessage, IClientMessage {
        /// <summary>
        /// Username
        /// </summary>
        public string UserName { get; }
        /// <summary>
        /// Real Name
        /// </summary>
        public string RealName { get; }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="realName"></param>
        public UserMessage(string userName, string realName) {
            UserName = userName;
            RealName = realName;
        }
        /// <summary>
        /// Tokens
        /// </summary>
        public IEnumerable<string> Tokens => new[] { "USER", UserName, "0", "-", RealName };
    }
}