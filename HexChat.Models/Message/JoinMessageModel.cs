using HexChat.Business.Messages.Base;
using HexChat.Models.Interfaces;
namespace HexChat.Models.Message {
    /// <summary>
    /// Join Message Model
    /// </summary>
    public class JoinMessageModel : IRCMessage, IServerMessage, IClientMessage {
        #region "private variables"
        /// <summary>
        /// Channels
        /// </summary>
        private readonly string _channels;
        /// <summary>
        /// Keys
        /// </summary>
        private readonly string _keys;
        /// <summary>
        /// Nick
        /// </summary>
        private string _nick;
        /// <summary>
        /// Channel
        /// </summary>
        private string _channel;
        #endregion
        #region "public properties"
        /// <summary>
        /// Nick
        /// </summary>
        public string Nick { get; set; } = string.Empty;
        /// <summary>
        /// Channel
        /// </summary>
        public string Channel { get; set; } = string.Empty;
        #endregion
        #region "methods"
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="parsedMessage"></param>
        public JoinMessageModel(ParsedIRCMessageModel parsedMessage) {
            _keys = string.Empty;
            _nick = parsedMessage.Prefix.From!;
            _channel = parsedMessage.Parameters[0];
            _channels = string.Empty;
        }
        /// <summary>
        /// Join Message Model
        /// </summary>
        /// <param name="channels"></param>
        /// <param name="keys"></param>
        public JoinMessageModel(string channels, string keys = "") {
            _keys = string.Empty;
            _channels = channels;
            _keys = keys;
            _channel = string.Empty;
            _nick = string.Empty;
        }
        /// <summary>
        /// Join Message Model
        /// </summary>
        /// <param name="channels"></param>
        public JoinMessageModel(params string[] channels) {
            _channels = string.Join(",", channels);
            _keys = string.Empty;
            _nick = string.Empty;
            _channel = string.Empty;
        }
        /// <summary>
        /// Join Message Model
        /// </summary>
        /// <param name="channelsWithKeys"></param>
        public JoinMessageModel(Dictionary<string, string> channelsWithKeys) {
            _channels = string.Join(",", channelsWithKeys.Keys);
            _keys = string.Join(",", channelsWithKeys.Values);
            _nick = string.Empty;
            _channel = string.Empty;
        }
        /// <summary>
        /// Tokens
        /// </summary>
        public IEnumerable<string> Tokens => new[] { "JOIN", _channels, _keys };
        #endregion
    }
}