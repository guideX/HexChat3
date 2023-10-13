using HexChat.Constant;
namespace HexChat.Models.Message {
    /// <summary>
    /// PrivMsg Message
    /// </summary>
    public class PrivMsgMessageModel {
        /// <summary>
        /// From
        /// </summary>
        public string? From { get; }
        /// <summary>
        /// Prefix
        /// </summary>
        public IRCPrefixModel? Prefix { get; }
        /// <summary>
        /// To
        /// </summary>
        public string To { get; }
        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; }
        /// <summary>
        /// Is Channel Message
        /// </summary>
        public bool IsChannelMessage { get; }
        /// <summary>
        /// Is Ctcp
        /// </summary>
        public bool IsCtcp { get; }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="parsedMessage"></param>
        public PrivMsgMessageModel(ParsedIRCMessageModel parsedMessage) {
            if (parsedMessage != null) {
                From = parsedMessage.Prefix.From;
                Prefix = parsedMessage.Prefix;
                To = parsedMessage.Parameters[0];
                Message = parsedMessage.Trailing;
                IsChannelMessage = To[0] == '#';
                IsCtcp = Message.Contains(Constants.CtcpDelimiter);
            } else {
                To = "";
                Message = "";
            }
        }
    }
}