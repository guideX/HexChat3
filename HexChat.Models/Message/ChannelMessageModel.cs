using HexChat.Models.Channel;
using HexChat.Models.User;
namespace HexChat.Models.Message {
    /// <summary>
    /// Represents a channel message
    /// </summary>
    public class ChannelMessageModel : EventArgs {
        /// <summary>
        /// User
        /// </summary>
        public UserModel User { get; }
        /// <summary>
        /// Channel
        /// </summary>
        public ChannelModel Channel { get; }
        /// <summary>
        /// Text
        /// </summary>
        public string Text { get; }
        /// <summary>
        /// Timestamp
        /// </summary>
        public DateTime Timestamp { get; }

        public ChannelMessageModel(UserModel user, ChannelModel channel, string text) {
            User = user;
            Channel = channel;
            Text = text;
            Timestamp = DateTime.Now;
        }
    }
}