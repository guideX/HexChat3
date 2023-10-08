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
        public string Name { get; }
        /// <summary>
        /// Topic
        /// </summary>
        public string? Topic { get; private set; }
        /// <summary>
        /// Users
        /// </summary>
        public ObservableCollection<ChannelUserModel> Users { get; }
        /// <summary>
        /// Messages
        /// </summary>
        public ObservableCollection<ChannelMessageModel> Messages { get; }
        /// <summary>
        /// User Statuses
        /// </summary>
        public static char[] UserStatuses = new[] { '~', '&', '@', '%', '+' };
        /// <summary>
        /// Channel Model
        /// </summary>
        /// <param name="name"></param>
        public ChannelModel(string name) {
            Name = name;
            Users = new ObservableCollection<ChannelUserModel>();
            Messages = new ObservableCollection<ChannelMessageModel>();
        }
        /// <summary>
        /// Add User
        /// </summary>
        /// <param name="user"></param>
        internal void AddUser(UserModel user) {
            AddUser(user, string.Empty);
        }
        /// <summary>
        /// Add User
        /// </summary>
        /// <param name="user"></param>
        /// <param name="status"></param>
        internal void AddUser(UserModel user, string status) {
            Client.DispatcherInvoker.Invoke(() => Users.Add(new ChannelUser(user, status)));
        }
        /// <summary>
        /// Remove User
        /// </summary>
        /// <param name="nick"></param>
        internal void RemoveUser(string nick) {
            var user = GetUser(nick);
            if (user != null) {
                Client.DispatcherInvoker.Invoke(() => Users.Remove(user));
            }
        }
        /// <summary>
        /// Set Topic
        /// </summary>
        /// <param name="topic"></param>
        internal void SetTopic(string topic) {
            Topic = topic;
        }
        /// <summary>
        /// Get User
        /// </summary>
        /// <param name="nick"></param>
        /// <returns></returns>
        public ChannelUserModel? GetUser(string nick) => Users.FirstOrDefault(u => string.Equals(u.Nick, nick, StringComparison.InvariantCultureIgnoreCase));
    }
}