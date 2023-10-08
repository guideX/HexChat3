using HexChat.Models.Message;
using System.Collections.ObjectModel;
using System.Windows.Input;
namespace HexChat.ViewModels {
    /// <summary>
    /// Tab Item View Model
    /// </summary>
    public abstract class TabItemViewModel : BaseViewModel {
        /// <summary>
        /// Message
        /// </summary>
        public ObservableCollection<MessageModel> Messages { get; } = new ObservableCollection<MessageModel>();
        /// <summary>
        /// Mesage
        /// </summary>
        private string message;
        /// <summary>
        /// Message
        /// </summary>
        public string Message { get => message; set => SetProperty(ref message, value); }
        /// <summary>
        /// Send Message Command
        /// </summary>
        public ICommand SendMessageCommand { get; protected set; }
    }
}