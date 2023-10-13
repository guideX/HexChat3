using HexChat.Business.Business;
using HexChat.Business.Commands;
using HexChat.Models;
using HexChat.Models.Message;
namespace HexChat.Business.EventArgs {
    public delegate void CtcpHandler(ClientBusiness client, CtcpEventArgs ctcpEventArgs);
    public class CtcpEventArgs : System.EventArgs {
        public string From { get; }
        public IRCPrefixModel Prefix { get; }
        public string To { get; }
        public string Message { get; }
        public string CtcpCommand { get; }
        public string CtcpMessage { get; } = "";
        internal CtcpEventArgs(PrivMsgMessageModel privMsgMessage) {
            From = privMsgMessage.From!;
            Prefix = privMsgMessage.Prefix!;
            To = privMsgMessage.To!;
            Message = privMsgMessage.Message!;
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