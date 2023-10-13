using HexChat.Business.Business;
using HexChat.Business.Commands;
using HexChat.Models;
namespace HexChat.Business.EventArgs {
    /// <summary>
    /// Ctcp Handler
    /// </summary>
    /// <param name="client"></param>
    /// <param name="ctcpEventArgs"></param>
    public delegate void CtcpHandler(ClientBusiness client, CtcpEventArgs ctcpEventArgs);
    /// <summary>
    /// Ctcp Event Args
    /// </summary>
    public class CtcpEventArgs : System.EventArgs {
        /// <summary>
        /// From
        /// </summary>
        public string From { get; }
        /// <summary>
        /// Prefix
        /// </summary>
        public IRCPrefixModel Prefix { get; }
        /// <summary>
        /// To
        /// </summary>
        public string To { get; }
        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; }
        /// <summary>
        /// Ctcp Command
        /// </summary>
        public string CtcpCommand { get; }
        /// <summary>
        /// Ctcp Message
        /// </summary>
        public string CtcpMessage { get; } = "";
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="privMsgMessage"></param>
        internal CtcpEventArgs(PrivMsgMessageBusiness privMsgMessage) {
            From = privMsgMessage.Model.From!;
            Prefix = privMsgMessage.Model.Prefix!;
            To = privMsgMessage.Model.To;
            Message = privMsgMessage.Model.Message;
            var ctcpMessage = Message.Replace(CtcpCommands.CtcpDelimiter, string.Empty);
            if (ctcpMessage.Contains(" ")) {
                var startIndex = ctcpMessage.IndexOf(' ');
                CtcpCommand = ctcpMessage.Remove(startIndex);
                CtcpMessage = ctcpMessage.Substring(startIndex + 1);
                return;
            }
            CtcpCommand = ctcpMessage;
        }
    }
}