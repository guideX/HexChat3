using HexChat.Models.Channel;
using System.Collections.ObjectModel;
namespace HexChat.Business.Collections {
    /// <summary>
    /// Channel Collection
    /// </summary>
    public class ChannelCollection : ObservableCollection<ChannelModel> {
        /// <summary>
        /// Get Channel
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ChannelModel GetChannel(string name) {
            var channel = Items.FirstOrDefault(c => c.Name == name);
            if (channel is null) {
                channel = new ChannelModel(name);
                Client.DispatcherInvoker.Invoke(() => Add(channel));
            }
            return channel;
        }
    }
}