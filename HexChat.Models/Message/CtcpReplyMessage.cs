using HexChat.Models.Interfaces;
namespace HexChat.Business.Messages {
    /// <summary>
    /// Ctcp Reply Message
    /// </summary>
#pragma warning disable CRRSP08 // A misspelled word has been found
    public class CtcpReplyMessage : IClientMessage {
#pragma warning restore CRRSP08 // A misspelled word has been found
        /// <summary>
        /// Target
        /// </summary>
        public string Target { get; }
        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="target"></param>
        /// <param name="text"></param>
        public CtcpReplyMessage(string target, string text) {
            Target = target;
            Message = $":{CtcpCommandBusiness.CtcpDelimiter}{text}{CtcpCommandBusiness.CtcpDelimiter}";
        }
        /// <summary>
        /// Tokens
        /// </summary>
        public IEnumerable<string> Tokens => new[] { "NOTICE", Target, Message };
        /// <summary>
        /// To String
        /// </summary>
        /// <returns></returns>
        public override string ToString() => string.Join(" ", Tokens);
    }
}