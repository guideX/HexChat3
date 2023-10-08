using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace HexChat.Models.User {
    /// <summary>
    /// User Model
    /// </summary>
    public class UserModel : INotifyPropertyChanged {
        #region "private variables"
        /// <summary>
        /// Nick
        /// </summary>
        private string? _nick;
        /// <summary>
        /// Real Name
        /// </summary>
        private string? _realName;
        #endregion
        #region "events"
        /// <summary>
        /// Property Changed
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion
        #region "properties"
        /// <summary>
        /// Nick
        /// </summary>
        public string? Nick {
            get { return _nick; }
            set {
                if (_nick != value) {
                    _nick = value;
                    if (OnPropertyChanged != null) OnPropertyChanged();
                }
            }
        }
        /// <summary>
        /// Real Name
        /// </summary>
        public string? RealName {
            get { return _realName; }
            set {
                if (_realName != value) {
                    _realName = value;
                    if (OnPropertyChanged != null) OnPropertyChanged();
                }
            }
        }
        #endregion
        #region "methods"
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nick"></param>
        /// <exception cref="ArgumentException"></exception>
        public UserModel(string? nick) {
            if (string.IsNullOrWhiteSpace(nick)) throw new ArgumentException("'Nick' is empty.");
            _nick = nick;
            _realName = null;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nick"></param>
        /// <param name="realName"></param>
        public UserModel(string nick, string realName) : this(nick) {
            if (string.IsNullOrWhiteSpace(nick)) throw new ArgumentException("'Nick' is empty.");
            if (string.IsNullOrWhiteSpace(realName)) throw new ArgumentException("'Real Name' is empty.");
            _realName = realName;
        }
        /// <summary>
        /// On Property Changed
        /// </summary>
        /// <param name="propertyName"></param>
        private void OnPropertyChanged([CallerMemberName] string propertyName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}