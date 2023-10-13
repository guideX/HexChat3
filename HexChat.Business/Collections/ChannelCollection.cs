using HexChat.Business.Business;
using System.Collections.ObjectModel;
namespace HexChat.Business.Collections {
    /// <summary>
    /// Channel Collection
    /// </summary>
    public class ChannelCollection : ObservableCollection<ChannelBusiness> {
        /// <summary>
        /// Get Channel
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ChannelBusiness GetChannel(string name) {
            var channel = Items.FirstOrDefault(c => c.Model.Name == name);
            if (channel is null) {
                channel = new ChannelBusiness(name);
                ClientBusiness.DispatcherInvoker.Invoke(() => Add(channel));
            }
            return channel;
        }
    }
}