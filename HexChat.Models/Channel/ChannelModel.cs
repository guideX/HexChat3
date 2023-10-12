using HexChat.Models.Message;
using HexChat.Models.User;
using System.Collections.ObjectModel;
namespace HexChat.Models.Channel {
    /// <summary>
    /// Channel Model
    /// </summary>
    public class ChannelModel {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Topic
        /// </summary>
        public string? Topic { get; set; }
        /// <summary>
        /// Users
        /// </summary>
        public ObservableCollection<ChannelUserModel> Users { get; set; }
        /// <summary>
        /// Messages
        /// </summary>
        public ObservableCollection<ChannelMessageModel> Messages { get; set; }
        /// <summary>
        /// Channel Model
        /// </summary>
        public ChannelModel(string name, string? topic = null) {
            Name = name;
            Topic = topic;
        }
    }
}