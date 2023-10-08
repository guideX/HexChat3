using HexChat.Models.User;
using System.Collections.ObjectModel;
namespace HexChat.Business.Collections {
    /// <summary>
    /// An observable collection that represents all users the client knows about
    /// </summary>
    public class UserCollection : ObservableCollection<UserModel> {
        /// <summary>
        /// Get User
        /// </summary>
        /// <param name="nick"></param>
        /// <returns></returns>
        public UserModel GetUser(string nick) {
            var user = Items.FirstOrDefault(u => string.Equals(u.Nick, nick, StringComparison.InvariantCultureIgnoreCase));
            if (user is null) {
                user = new UserModel(nick);
                Client.DispatcherInvoker.Invoke(() => Add(user));
            }
            return user;
        }
    }
}