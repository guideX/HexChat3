using HexChat.Models.Interfaces;
using HexChat.Constant;
namespace HexChat.Business.Business {
    /// <summary>
    /// Ctcp Reply Message
    /// </summary>
    public class CtcpReplyMessageBusiness : IClientMessage {
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
        public CtcpReplyMessageBusiness(string target, string text) {
            Target = target;
            Message = $":{Constants.CtcpDelimiter}{text}{Constants.CtcpDelimiter}";
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