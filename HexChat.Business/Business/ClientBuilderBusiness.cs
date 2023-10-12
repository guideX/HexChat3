using HexChat.Business.Connection;
using HexChat.Business.Interfaces.Connection;
using HexChat.Models.User;
namespace HexChat.Business.Business {
    /// <summary>
    /// Client Builder Business
    /// </summary>
    public class ClientBuilderBusiness {
        /// <summary>
        /// User
        /// </summary>
        private UserModel _user;
        /// <summary>
        /// Connection
        /// </summary>
        private IConnection _connection;
        /// <summary>
        /// Password
        /// </summary>
        private string _password;
        /// <summary>
        /// Constructor
        /// </summary>
        public ClientBuilderBusiness() {
            _password = "";
        }
        /// <summary>
        /// Configures the nick and real name you wish to use when joining the server
        /// </summary>
        /// <param name="nick">Nick you wish to use</param>
        /// <param name="realName">Real name you wish to use</param>
        /// <returns>The ClientBuilder</returns>
        public ClientBuilderBusiness WithNick(string nick, string realName = "") {
            _user = new UserModel(nick, realName);
            return this;
        }
        /// <summary>
        /// With Server
        /// </summary>
        /// <param name="host">The host of the server you wish to connect to</param>
        /// <param name="port">The port of the server (Default port is usually 6667)</param>
        /// <param name="password">Password, in case the server requires it (optional)</param>
        /// <returns>The ClientBuilder</returns>
        public ClientBuilderBusiness WithServer(string host, int port, string password = null) {
            _connection = new TcpClientConnection(host, port);
            _password = password;
            return this;
        }
        /// <summary>
        /// Builds the Client
        /// </summary>
        /// <returns>A configured Client</returns>
        /// <exception cref="InvalidOperationException">Nick and connection must be defined using the ClientBuilder.</exception>
        public ClientBusiness Build() {
            _ = _user ?? throw new InvalidOperationException("Nick must be defined. Please use the WithNick method before building the client.");
            _ = _connection ?? throw new InvalidOperationException("Connection must be defined. Please use the WithServer method before building the client.");
            return new ClientBusiness(_user, _password, _connection);
        }
    }
}