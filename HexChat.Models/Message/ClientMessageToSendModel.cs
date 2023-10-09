namespace HexChat.Models.Message {
    /// <summary>
    /// Client Message To Send Model
    /// </summary>
    public class ClientMessageToSendModel {
        #region "public variables"
        /// <summary>
        /// Channel
        /// </summary>
        public string Channel { get; set; }
        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Sent
        /// </summary>
        public bool Sent { get; set; }
        #endregion
        #region "public methods"
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="message"></param>
        public ClientMessageToSendModel(string channel, string message) {
            Channel = channel;
            Message = message;
        }
        #endregion
    }
}