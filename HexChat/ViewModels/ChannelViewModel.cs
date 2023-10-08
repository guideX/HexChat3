﻿using MvvmHelpers.Commands;
using HexChat.IrcProtocol;
using HexChat.IrcProtocol.Messages;
using HexChat.Messages;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
namespace HexChat.ViewModels {
    /// <summary>
    /// Channel View Model
    /// </summary>
    public class ChannelViewModel : TabItemViewModel {
        /// <summary>
        /// Channel
        /// </summary>
        public Channel Channel { get; }
        /// <summary>
        /// Sort Users Command
        /// </summary>
        public ICommand SortUsersCommand { get; }
        /// <summary>
        /// Open Query Command
        /// </summary>
        public ICommand OpenQueryCommand { get; }
        /// <summary>
        /// Matrix Client
        /// </summary>
        private MatrixProtocol.Wrapper.MatrixWrapper _matrixClient;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="matrixClient"></param>
        public ChannelViewModel(Channel channel, MatrixProtocol.Wrapper.MatrixWrapper matrixClient) {
            _matrixClient = matrixClient;
            Channel = channel;
            channel.Messages.CollectionChanged += Messages_CollectionChanged;
            SendMessageCommand = new AsyncCommand(SendChannelMessage);
            SortUsersCommand = new Command(SortUsers);
            OpenQueryCommand = new AsyncCommand<ChannelUser>(OpenQuery);
        }
        /// <summary>
        /// Sort Users
        /// </summary>
        /// <param name="items"></param>
        private void SortUsers(object items) {
            var view = CollectionViewSource.GetDefaultView(items) as ListCollectionView;
            view.CustomSort = new ChannelUserComparer();
        }
        /// <summary>
        /// Open Query
        /// </summary>
        /// <param name="channelUser"></param>
        /// <returns></returns>
        private async Task OpenQuery(ChannelUser channelUser) {
            await App.EventAggregator.PublishOnUIThreadAsync(new OpenQueryMessage(channelUser.User));
        }
        /// <summary>
        /// Send Channel Message
        /// </summary>
        /// <returns></returns>
        private async Task SendChannelMessage() {
            if (string.IsNullOrWhiteSpace(Message)) {
                return;
            }
            Messages.Add(Models.Message.Sent(new ChannelMessage(App.Client.User, Channel, Message)));
            await App.Client.SendAsync(new PrivMsgMessage(Channel.Name, Message));
            Message = string.Empty;
        }
        /// <summary>
        /// Messages Collection Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Messages_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            foreach (ChannelMessage message in e.NewItems) {
                App.Dispatcher.Invoke(() => Messages.Add(Models.Message.Received(message)));
                if (!message.User.Nick.Contains("[m]"))
                    _matrixClient.SendMessage(_matrixClient.CurrentChannelID, message.User.Nick + "[l]: " + message.Text);
            }
        }
        /// <summary>
        /// To String
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Channel.Name;
    }
}