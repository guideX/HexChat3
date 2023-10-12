﻿using HexChat.Models.Channel;
using HexChat.Models.Message;
using HexChat.Models.User;
using System.Collections.ObjectModel;
namespace HexChat.Business.Business {
    /// <summary>
    /// Channel Business
    /// </summary>
    public class ChannelBusiness {
        /// <summary>
        /// Model
        /// </summary>
        public ChannelModel Model;
        /// <summary>
        /// User Statuses
        /// </summary>
        public static char[] UserStatuses = new[] { '~', '&', '@', '%', '+' };
        /// <summary>
        /// Channel Model
        /// </summary>
        /// <param name="name"></param>
        public ChannelBusiness(string name) {
            Model = new ChannelModel(name);
            Model.Users = new ObservableCollection<ChannelUserModel>();
            Model.Messages = new ObservableCollection<ChannelMessageModel>();
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
        public void AddUser(UserModel user, string status) {
            ClientBusiness.DispatcherInvoker.Invoke(() => Users.Add(new ChannelUser(user, status)));
        }
        /// <summary>
        /// Remove User
        /// </summary>
        /// <param name="nick"></param>
        public void RemoveUser(string nick) {
            var user = GetUser(nick);
            if (user != null)
                ClientBusiness.DispatcherInvoker.Invoke(() => Users.Remove(user));
        }
        /// <summary>
        /// Set Topic
        /// </summary>
        /// <param name="topic"></param>
        //public void SetTopic(string topic) {
        //Model.Topic = topic;
        //}
        /// <summary>
        /// Get User
        /// </summary>
        /// <param name="nick"></param>
        /// <returns></returns>
        public ChannelUserModel? GetUser(string nick) => Model.Users.FirstOrDefault(u => string.Equals(u.Nick, nick, StringComparison.InvariantCultureIgnoreCase));
    }
}