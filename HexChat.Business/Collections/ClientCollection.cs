using HexChat.Business.Wrappers;
using HexChat.Models.Message;
namespace HexChat.Business.Collections {
    /// <summary>
    /// Client Collection
    /// </summary>
    public class ClientCollection {
        #region "private variables"
        /// <summary>
        /// Clients
        /// </summary>
        private List<ClientWrapper> _clients;
        /// <summary>
        /// Server
        /// </summary>
        private string _server;
        /// <summary>
        /// Port
        /// </summary>
        private string _port;
        #endregion
        #region "public methods"
        /// <summary>
        /// Constructor
        /// </summary>
        public ClientCollection(string server, string port) {
            _server = server;
            _port = port;
            _clients = new();
        }
        /// <summary>
        /// Add Client
        /// </summary>
        /// <summary>
        /// Send Message
        /// </summary>
        /// <param name="user"></param>
        /// <param name="message"></param>
        public bool SendMessageAsUser(string channel, string user, string message) {
            var clientsFound = _clients.Where(c => c.User == user);
            var clientMessage = new ClientMessageToSendModel(channel, message);
            if (clientsFound.Any()) {
                var client = clientsFound.FirstOrDefault();
                if (client != null) {
                    client.Send(clientMessage);
                    return true;
                }
            } else {
                var client = new ClientWrapper(_server, _port, user, user, "", channel);
                client.Send(clientMessage);
                client.Connect();
                _clients.Add(client);
                return true;
            }
            return false;
        }
        #endregion
    }
}