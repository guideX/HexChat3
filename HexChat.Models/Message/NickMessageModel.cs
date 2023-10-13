using HexChat.Business.Messages.Base;
using HexChat.Models.Interfaces;
namespace HexChat.Models.Message {
    /// <summary>
    /// Nick Message Model
    /// </summary>
    public class NickMessageModel : IRCMessage, IServerMessage, IClientMessage {
        /// <summary>
        /// Old Nick
        /// </summary>
        public string OldNick { get; }
        /// <summary>
        /// New Nick
        /// </summary>
        public string NewNick { get; }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="parsedMessage"></param>
        public NickMessageModel(ParsedIRCMessageModel parsedMessage) {
            OldNick = parsedMessage.Prefix.From!;
            NewNick = parsedMessage.Parameters[0];
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="newNick"></param>
        public NickMessageModel(string newNick) {
            NewNick = newNick;
            OldNick = "";
        }
        /// <summary>
        /// Tokens
        /// </summary>
        public IEnumerable<string> Tokens => new[] { "NICK", NewNick };
    }
}